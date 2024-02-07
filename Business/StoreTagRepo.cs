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
    public class StoreTagRepo : IStoreTagRepo
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public StoreTagRepo(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<int> AddStoreTag(int storeID, int tagID)
        {
            if(tagID != 0 && storeID != 0)
            {
                StoreTags addTags = new StoreTags();
                addTags.storeId = storeID;
                addTags.tagId = tagID;
                var tag = await _db.tags.FirstOrDefaultAsync(i=>i.Id == tagID);
                addTags.tagName = tag.Name;
                await _db.storeTags.AddAsync(addTags);
            }
            return await _db.SaveChangesAsync();
        }
        public async Task<int> DeleteStoreTag(int tagID, int storeID)
        {
            if (tagID != 0 && storeID != 0)
            {
                var checkTag = _db.storeTags.FirstOrDefault(i=>i.tagId == tagID && i.storeId == storeID);
                _db.storeTags.Remove(checkTag);
            }
            return await _db.SaveChangesAsync();
        }
        public async Task<IEnumerable<StoreTagDTO>> getStoreTags(int storeID)
        {
            return _mapper.Map<IEnumerable<StoreTags>, IEnumerable<StoreTagDTO>>(_db.storeTags.Where(i => i.storeId == storeID).ToList());
        }
        public bool isTagAvailable(int tagID, int storeID)
        {
            var checkTag = _db.storeTags.FirstOrDefault(i => i.tagId == tagID && i.storeId == storeID);
            if (checkTag != null)
            {
                return true;
            }
            return false;
        }
        public async Task<IEnumerable<StoreDTO>> getStoresWithTags(string storeTag)
        {
            //var tagsList = _db.storeTags.Where(i => i.tagName.ToLower().Contains(tag.ToLower())).ToList();
            //List<Store> stores = new List<Store>();
            //foreach(var tags in tagsList)
            //{
            //    stores.Add( await _db.Stores.FirstOrDefaultAsync(i => i.Id == tags.storeId));
            //}
            //return _mapper.Map<IEnumerable<Store>, IEnumerable<StoreDTO>>(stores);
            List<Store> stores = new List<Store>();
            if (storeTag != "")
            {
                var allStoreTags = _db.storeTags.Where(i => i.tagName.ToLower().Contains(storeTag.ToLower())).ToList();
                foreach (var tag in allStoreTags)
                {
                    var id = _db.Stores.FirstOrDefault(i => i.Id == tag.storeId);
                    if (!stores.Contains(id))
                    {
                        stores.Add(await _db.Stores.Include(i => i.StoreImages).FirstOrDefaultAsync(i => i.Id == tag.storeId));
                    }
                }
            }
            return _mapper.Map<IEnumerable<Store>, IEnumerable<StoreDTO>>(stores);
        }
        public async Task<IEnumerable<StoreTagDTO>> getAllStoreTags()
        {
            return _mapper.Map<IEnumerable<StoreTags>, IEnumerable<StoreTagDTO>>(_db.storeTags).ToList();
        }
    }
}
