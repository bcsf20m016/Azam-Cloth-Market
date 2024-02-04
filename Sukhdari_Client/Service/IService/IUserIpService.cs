using DataAccess.Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sukhdari_Client.Service.IService
{
    public interface IUserIpService
    {
        Task<UserIpDTO> GetUserIPAsync();
        void StoreIp(UserIpDTO userIpDTO);

    }
}
