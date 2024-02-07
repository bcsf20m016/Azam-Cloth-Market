using Business.IRepo;
using DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using AutoMapper;
using Models;

namespace Business
{
    public class UserIPRepo : IUserIPRepo
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public UserIPRepo(ApplicationDbContext db, IMapper map)
        {
            _db = db;
            _mapper = map;
        }



        public async Task<int> createIp(UserIpDTO userIpDTO)
        {
            var ip = _mapper.Map<UserIpDTO, IPAddress>(userIpDTO);
            var result = await _db.AddAsync(ip);
            return await _db.SaveChangesAsync();
        }

        public List<int> getPastWeekUniqueUsersCount(int storeId)
        {
            List<int> pastWeekUniqueUsersCount = new List<int>();
            for (int day = 5; day >= -1; day--)
            {

                int count = _db.ipAddresses.Where(j => j.StoreId == storeId && j.timeNow >= DateTime.Now.Date.AddDays(-(day + 1)) && j.timeNow <= DateTime.Now.Date.AddDays(-day)).Select(x => new { x.IP, x.timeNow }).Select(y => y.IP).Distinct().Count();
                pastWeekUniqueUsersCount.Add(count);
            }
            return pastWeekUniqueUsersCount;
        }
    }
}
