using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.IRepo
{
    public interface IStoreTagRepo
    {
        public Task<int> AddStoreTag(int storeID, int tagID);
        public Task<IEnumerable<StoreTagDTO>> getStoreTags(int storeID);
        public bool isTagAvailable(int tagID, int storeID);
        public Task<int> DeleteStoreTag(int tagID, int storeID);
        public Task<IEnumerable<StoreDTO>> getStoresWithTags(string storeTag);
        public Task<IEnumerable<StoreTagDTO>> getAllStoreTags();
    }
}
