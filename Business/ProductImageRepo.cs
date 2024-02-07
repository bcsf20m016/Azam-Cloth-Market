using AutoMapper;
using Business.IRepo;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class ProductImageRepo: IProductImageRepo
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public ProductImageRepo(ApplicationDbContext db, IMapper map)
        {
            _db = db;
            _mapper = map;
        }

        public async Task<int> CreateProductImage(ProductImageDTO productImage)
        {
            var image = _mapper.Map<ProductImageDTO, ProductImage>(productImage);
            await _db.productImages.AddAsync(image);
            return await _db.SaveChangesAsync();
        }

        public async Task<int> DeleteProductImageByImageID(int imageId)
        {
            var image = await _db.productImages.FindAsync(imageId);
            if (image != null)
            {
                _db.productImages.Remove(image);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<int> DeleteProductImageByName(string url)
        {
            var images = await _db.productImages.FirstOrDefaultAsync(i => i.ProductImageUrl.ToLower() == url.ToLower());
            if (images != null)
            {
                _db.productImages.Remove(images);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<int> DeleteProductImageByProductID(int productId)
        {
            var images = _db.productImages.Where(i => i.ProductId == productId).ToList();
            if (images != null)
            {
                _db.productImages.RemoveRange(images);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<IEnumerable<ProductImageDTO>> getProductImages(int productId)
        {
            return _mapper.Map<IEnumerable<ProductImage>,IEnumerable<ProductImageDTO>>( _db.productImages.Where(i => i.ProductId == productId).ToList());
        }
    }
}
