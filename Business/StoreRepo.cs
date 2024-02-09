using AutoMapper;
using Business.IRepo;
using DataAccess.Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Business
{
    public class StoreRepo : IStoreRepo
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;

        public StoreRepo(ApplicationDbContext db, IMapper map, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _mapper = map;
            _userManager = userManager;
        }
        public async Task<StoreDTO> createStore(StoreDTO store)
        {
            if (store.Id != 0)
            {
                Store oldStore = await _db.Stores.FindAsync(store.Id);
                oldStore.Name = store.Name;
                oldStore.Address = store.Address;

                oldStore.phoneNo = store.phoneNo;
                if (oldStore.Type != store.Type)
                {
                    var storeTags = _db.storeTags.Where(i => i.storeId == store.Id).ToList();
                    if (storeTags.Any())
                    {
                        foreach (var tag in storeTags)
                        {
                            _db.storeTags.Remove(tag);
                        }
                    }
                    oldStore.Type = store.Type;
                }
                oldStore.Country = store.Country;
                oldStore.Block = store.Block;
                oldStore.Image = store.Image;
                oldStore.Description = store.Description;
                if (oldStore.ClickCount != null)
                {
                    oldStore.ClickCount = store.ClickCount;
                }
                else
                {
                    oldStore.ClickCount = 0;
                }

                oldStore.Attributes.Clear();
                foreach (var attrib in store.Attributes)
                {
                    StoreAttributes attribute = new StoreAttributes() { Key = attrib.Key, Value = attrib.Value };
                    oldStore.Attributes.Add(attribute);
                }

                await _db.SaveChangesAsync();
                return _mapper.Map<Store, StoreDTO>(oldStore);
            }
            var storeAdminId = _db.Users.FirstOrDefault(i => i.UserName == store.AdminName).Id;
            Store newStore = _mapper.Map<StoreDTO, Store>(store);
            newStore.UserId = storeAdminId;
            newStore.ClickCount = 0;
            newStore.timeNow = DateTime.UtcNow.Date;

            await _db.Stores.AddAsync(newStore);
            await _db.SaveChangesAsync();
            return _mapper.Map<Store, StoreDTO>(newStore);
        }
        public async Task<int> deleteStore(int id)
        {
            var store = await _db.Stores.FindAsync(id);
            var userId = store.UserId;
            if (store != null)
            {
                var images = _db.storeImages.Where(i => i.StoreId == id).ToList();
                foreach (var image in images)
                {
                    if (File.Exists(image.StoreImageUrl))
                    {
                        File.Delete(image.StoreImageUrl);
                    }
                }
                _db.storeImages.RemoveRange(images);
                _db.Stores.Remove(store);
                var  user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    await _userManager.DeleteAsync(user);
                }
                return await _db.SaveChangesAsync();
            }
            return 0;
        }
        public async Task<IEnumerable<StoreDTO>> getAllStores()
        {

            return _mapper.Map<IEnumerable<Store>, IEnumerable<StoreDTO>>(_db.Stores.Include(i => i.StoreImages).Include(i=>i.Attributes));

        }
        public StoreDTO GetStoreByAdminName(string adminName)
        {
            var AdminID = _db.Users.FirstOrDefault(i => i.UserName == adminName).Id;
            Store find = _db.Stores.Include(i => i.StoreImages).Include(i => i.Attributes).FirstOrDefault(i => i.UserId == AdminID);
            if (find == null)
            {
                return null;
            }
            return _mapper.Map<Store, StoreDTO>(find);
        }
        public StoreDTO GetStoreByName(string name)
        {

            Store find = _db.Stores.Include(i => i.StoreImages).FirstOrDefault(i => i.Name == name);
            if (find == null)

            {
                return null;
            }
            return _mapper.Map<Store, StoreDTO>(find);
        }
        public async Task<IEnumerable<StoreDTO>> getStoresByAllFilters(string data)
        {
            var products = _db.Products.Where(i => i.Name.ToLower().Contains(data.ToLower())).ToList();
            var storeCategories = _db.Categories.Where(i => i.Name.ToLower().Contains(data.ToLower())).ToList();
            var stores = _db.Stores.Include(i => i.StoreImages).Where(i => i.Name.ToLower().Contains(data.ToLower())).ToList();

            List<Store> storesList = new List<Store>();
            foreach (var s in products)
            {
                storesList.Add(await _db.Stores.FindAsync(s.StoreId));
            }
            foreach (var s in storeCategories)
            {
                storesList.Add(await _db.Stores.FindAsync(s.StoreId));
            }
            foreach (var s in stores)
            {
                storesList.Add(s);
            }
            return _mapper.Map<IEnumerable<Store>, IEnumerable<StoreDTO>>(storesList);
        }

        public async Task<IEnumerable<StoreDTO>> getStoresByCountry(string country)
        {
            var stores = _db.Stores.Include(i => i.StoreImages).Where(i => i.Country.ToLower().Contains(country.ToLower())).ToList();
            return _mapper.Map<IEnumerable<Store>, IEnumerable<StoreDTO>>(stores);
        }
        public async Task<int> updateStore(StoreDTO store)
        {
            Store oldStore = await _db.Stores.FindAsync(store.Id);
            Store newStore = _mapper.Map<StoreDTO, Store>(store, oldStore);
            _db.Stores.Update(newStore);
            return await _db.SaveChangesAsync();
        }
        public async Task<int> getStoreCount()
        {
            return await _db.Stores.CountAsync();
        }
        public async Task<int> clickStoreCount(int storeID)
        {
            //var store = _db.Stores.FirstOrDefault(i => i.Id == storeID);
            var StoreCount = _db.countDetails.FirstOrDefault(i => i.StoreId == storeID && i.date.Date == DateTime.Today.Date);


            if (StoreCount != null)
            {
                if (StoreCount.clicks == 0)
                {
                    StoreCount.clicks = 1;
                }
                else
                {
                    StoreCount.clicks++;
                }
            }
            else
            {
                CountDetails data = new CountDetails();
                data.clicks = 1;
                data.StoreId = storeID;
                data.date = DateTime.Now.Date;
                _db.countDetails.Add(data);
            }
            return await _db.SaveChangesAsync();
        }









        public async Task<StoreDTO> getStoreById(int storeId)
        {
            // Replace this with your actual DbContext and entity type
            var storeEntity = await _db.Stores.FindAsync(storeId);

            if (storeEntity == null)
            {
                // Handle the case where the store with the given ID is not found
                return null;
            }

            // Use AutoMapper to map the StoreEntity to StoreDTO
            var storeDto = _mapper.Map<StoreDTO>(storeEntity);

            return storeDto;
        }


        public async Task<String> getEmailByUserId(string userId)
        {
            var result = _db.Users.FirstOrDefault(i => i.Id == userId);
            var email = result.Email;
            return email;
        }

        public async Task<List<IdentityUser>> getAllUsers()
        {
            var result = _db.Users.ToList();
            return result;
        }

        //public async Task<bool> deleteUserById(string Id)
        //{
        //    var user = await _userManager.FindByIdAsync(Id);
        //    await _userManager.DeleteAsync(user);
        //    return true;
        //}


    }
}

