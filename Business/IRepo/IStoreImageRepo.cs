using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.IRepo
{
    public interface IStoreImageRepo
    {
        public Task<int> CreateStoreImage(StoreImageDTO storeImage);
        public Task<int> DeleteStoreImageByImageID(int imageId);
        public Task<int> DeleteStoreImageByStoreID(int StoreId);
        public Task<IEnumerable<StoreImageDTO>> getStoreImages(int StoreId);
        public Task<int> DeleteStoreImageByName(string name);
    }
}
