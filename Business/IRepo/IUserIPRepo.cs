using DataAccess.Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.IRepo
{
    public interface IUserIPRepo
    {
        public Task<int> createIp(UserIpDTO userIpDTO);
        public List<int> getPastWeekUniqueUsersCount(int storeId);

    }
}
