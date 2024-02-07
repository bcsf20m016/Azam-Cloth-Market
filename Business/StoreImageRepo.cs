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
    public class StoreImageRepo : IStoreImageRepo
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public StoreImageRepo(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<int> CreateStoreImage(StoreImageDTO storeImage)
        {
            var image = _mapper.Map<StoreImageDTO, StoreImage>(storeImage);
            await _db.storeImages.AddAsync(image);
            return await _db.SaveChangesAsync();
        }

        public async Task<int> DeleteStoreImageByImageID(int imageId)
        {
            var image = await _db.storeImages.FindAsync(imageId);
            if (image != null)
            {
                _db.storeImages.Remove(image);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<int> DeleteStoreImageByName(string name)
        {
            var image = await _db.storeImages.FirstOrDefaultAsync(i => i.StoreImageUrl.ToLower() == name.ToLower());
            if (image != null)
            {
                _db.storeImages.Remove(image);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<int> DeleteStoreImageByStoreID(int StoreId)
        {
            var images = _db.storeImages.Where(i => i.StoreId == StoreId).ToList();
            if (images != null)
            {
                _db.storeImages.RemoveRange(images);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<IEnumerable<StoreImageDTO>> getStoreImages(int StoreId)
        {
            return _mapper.Map<IEnumerable<StoreImage>, IEnumerable<StoreImageDTO>>(_db.storeImages.Where(i => i.StoreId == StoreId).ToList());
        }
    }
}