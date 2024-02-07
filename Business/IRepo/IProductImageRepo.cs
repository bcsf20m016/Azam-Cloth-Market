 using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.IRepo
{
    public interface IProductImageRepo
    {
        public Task<int> CreateProductImage(ProductImageDTO productImage);
        public Task<int> DeleteProductImageByImageID(int imageId);
        public Task<int> DeleteProductImageByProductID(int productId);
        public Task<IEnumerable<ProductImageDTO>> getProductImages(int productId);
        public Task<int> DeleteProductImageByName(string name);
    }

}
