using AutoMapper;
using Business.IRepo;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class ProductRepo : IProductRepo
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public ProductRepo(ApplicationDbContext db, IMapper map)
        {
            _db = db;
            _mapper = map;
        }
        public async Task<ProductDTO> createProduct(ProductDTO product)
        {
            var getValues = await _db.Products.FirstOrDefaultAsync(i => i.Name.ToLower() == product.Name.ToLower() && i.StoreId == product.StoreId);
            if(product.Id == 0 && getValues != null )
            {
                return null;
            }
            if (product.Id != 0)
            {
                var oldCategory = _db.Products.FirstOrDefault(i => i.Id == product.Id);
                //if (getValues != null && oldCategory.Name.ToLower() == product.Name.ToLower())
                //{
                //    oldCategory.Name = product.Name;
                //}
                //else if(getValues != null && oldCategory.Name.ToLower()!= product.Name.ToLower())
                //{
                //    return null;
                //}
                oldCategory.Name = product.Name;
                oldCategory.Price = product.Price;
                oldCategory.Description = product.Description;
                oldCategory.CategoryId = product.CategoryId;
                oldCategory.Image = product.Image;
                oldCategory.Quantity = product.Quantity;
                oldCategory.ClickCount = product.ClickCount;
                if (product.DiscountPercentage >= 0 && product.DiscountPercentage <= 100)
                {
                    oldCategory.DiscountPercentage = product.DiscountPercentage;
                    oldCategory.DiscountPrice = product.DiscountPrice;
                }
                else
                {
                    oldCategory.DiscountPercentage = null;
                    oldCategory.DiscountPrice = null;
                }
                
                oldCategory.Attributes.Clear(); //It'll clear all existing attributes
                foreach (var attrib in product.Attributes)
                {
                    ProductAttributes attribute = new ProductAttributes() { Key = attrib.Key, Value = attrib.Value, Fixed=attrib.Fixed };
                    oldCategory.Attributes.Add(attribute);
                }

                await _db.SaveChangesAsync();
                return _mapper.Map<Product, ProductDTO>(oldCategory);
            }
            Product newProd = _mapper.Map<ProductDTO, Product>(product);
            newProd.ClickCount = 0;
            await _db.Products.AddAsync(newProd);
            await _db.SaveChangesAsync();
            return _mapper.Map<Product, ProductDTO>(newProd);
        }
        public async Task<int> deleteProduct(int id)
        {
            var prod = await _db.Products.FindAsync(id);
            if (prod != null)
            {
                var images = _db.productImages.Where(i => i.ProductId == id).ToList();
                foreach (var image in images)
                {
                    if (File.Exists(image.ProductImageUrl))
                    {
                        File.Delete(image.ProductImageUrl);
                    }
                }

                if(prod.Attributes != null)
                {
                    prod.Attributes.Clear();
                }

                _db.productImages.RemoveRange(images);
                _db.Products.Remove(prod);
                return await _db.SaveChangesAsync();
            }
            else
                return 0;
        }
        public async Task<IEnumerable<ProductDTO>> getAllProducts(int storeId)
        {
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(_db.Products.Include(i => i.ProductImages).Include(i=>i.Attributes).Where(i => i.StoreId == storeId)).ToList();
        }
        public async Task<int> setDiscountOnProduct(ProductDTO p)
        {
            var product = await _db.Products.FindAsync(p.Id);
            product.DiscountPrice = p.Price - ((p.DiscountPercentage / 100) * p.Price);
            return await _db.SaveChangesAsync();

        }
        public async Task<IEnumerable<ProductDTO>> getAllProducts()
        {
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(_db.Products.Include(i => i.ProductImages).Include(i => i.Attributes));
        }
        public async Task<ProductDTO> GetProduct(int id, int storeId)
        {
            var product = _db.Products.Include(i => i.ProductImages).Include(i => i.Attributes).FirstOrDefault(i => i.Id == id && i.StoreId == storeId);
            return _mapper.Map<Product, ProductDTO>(product);
        }
        public async Task<ProductDTO> getProduct(int id)
        {
            try
            {
                ProductDTO product = _mapper.Map<Product, ProductDTO>(await _db.Products.Include(i => i.ProductImages).FirstOrDefaultAsync(i => i.Id == id));
                return product;
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public async Task<IEnumerable<StoreDTO>> getStoresByProductName(string productName)
        {
            List<Store> stores = new List<Store>();
            if (productName != "")
            {
                var products = _db.Products.Where(i => i.Name.ToLower().Contains(productName.ToLower())).ToList();
                foreach (var s in products)
                {
                    var id = _db.Stores.FirstOrDefault(i => i.Id == s.StoreId);
                    if (!stores.Contains(id))
                    {
                        stores.Add(await _db.Stores.Include(i => i.StoreImages).FirstOrDefaultAsync(i => i.Id == s.StoreId));
                    }
                }
            }
            return _mapper.Map<IEnumerable<Store>, IEnumerable<StoreDTO>>(stores);
        }
        public async Task<IEnumerable<StoreDTO>> getStoresByProductPriceRange(int low, int high)
        {
            var products = _db.Products.Where(i => i.Price >= low && i.Price <= high).ToList();
            List<Store> stores = new List<Store>();
            foreach (var s in products)
            {
                var temp = await _db.Stores.Include(i => i.StoreImages).FirstOrDefaultAsync(i => i.Id == s.StoreId);
                if (!stores.Contains(temp))
                {
                    stores.Add(temp);
                }
            }
            return _mapper.Map<IEnumerable<Store>, IEnumerable<StoreDTO>>(stores);

        }
        public async Task<int> updateProduct(ProductDTO product)
        {
            var prod = await _db.Products.FindAsync(product.Id);
            var updatedProd = _mapper.Map<ProductDTO, Product>(product, prod);
            _db.Products.Update(updatedProd);
            return await _db.SaveChangesAsync();
        }
        public async Task<IEnumerable<ProductDTO>> getDiscountedProducts(int storeId)
        {
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(await _db.Products.Include(i => i.ProductImages).Where(i => i.StoreId == storeId && i.DiscountPercentage != null).ToListAsync());
        }
        public async Task<IEnumerable<ProductDTO>> getDiscountedCategoryProducts(int storeId)
        {
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(await _db.Products.Include(i => i.ProductImages).Where(i => i.StoreId == storeId && i.Category.DiscountPercentage != null).ToListAsync());
        }
        public async Task<IEnumerable<ProductDTO>> getCategoryProducts(int categoryId)
        {
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(await _db.Products.Include(i => i.ProductImages).Where(i => i.CategoryId == categoryId).ToListAsync());
        }
        public async Task<int> getProductCount(int storeId)
        {
            return _db.Products.Where(i => i.StoreId == storeId).Count();
        }

        public async Task<int> clickProductCount(int productID)
        {
            var product = _db.Products.FirstOrDefault(i => i.Id == productID);
            if (product.ClickCount == null)
            {
                product.ClickCount = 1;
            }
            else
            {
                product.ClickCount++;
            }
            return await _db.SaveChangesAsync();
        }

    }
}