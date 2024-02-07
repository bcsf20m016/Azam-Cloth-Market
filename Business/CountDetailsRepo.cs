using AutoMapper;
using Business.IRepo;
using DataAccess.Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class CountDetailsRepo : ICountDetailsRepo
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public CountDetailsRepo(ApplicationDbContext db, IMapper map)
        {
            _db = db;
            _mapper = map;
        }
        public void addClick(int storeId)
        {
            var getDetails = _db.countDetails.FirstOrDefault(i => i.StoreId == storeId && i.date == DateTime.UtcNow.Date);
            if (getDetails != null)
            {
                getDetails.clicks = getDetails.clicks + 1;

            }
            else
            {
                getDetails.StoreId = storeId;
                getDetails.date = DateTime.UtcNow.Date;
                getDetails.clicks = 1;
                _db.countDetails.Add(getDetails);
                _db.SaveChanges();
            }

        }

        public async Task<IEnumerable<StoreDTO>> getWeeklyTopStores()
        {
            var noOfStores = _db.countDetails.Count();
            List<StoreDTO> stores = _mapper.Map<IEnumerable<Store>, IEnumerable<StoreDTO>>(_db.Stores).ToList();
            for (int i = 0; i < noOfStores; i++)
            {
                int clickCount = 0;
                List<CountDetails> temp = new List<CountDetails>();
                temp = _db.countDetails.Where(j => j.StoreId == stores[i].Id && j.date >= DateTime.Now.AddDays(-7)).ToList();
                //for (int day = 5; day >= 1; day--)
                //{
                //    temp = _db.countDetails.Where(j => j.StoreId == stores[i].Id && j.date >= DateTime.Now.Date.AddDays(-(day + 1)) && j.date <= DateTime.Now.Date.AddDays(-day)).ToList();
                //}
                for (int j = 0; j < temp.Count(); i++)
                {
                    clickCount = clickCount + temp[j].clicks;
                }
                StoreDTO data = new StoreDTO();
                data.Id = stores[i].Id;
                data.ClickCount = clickCount;
                stores.Add(data);
            }
            stores = stores.OrderByDescending(i => i.ClickCount).Take(5).ToList();
            return stores;
        }
    }
}
