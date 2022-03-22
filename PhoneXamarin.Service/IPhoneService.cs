using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PhoneXamarin.Service
{
    public interface IPhoneService
    {
        Task<IEnumerable<Phone>> GetAll();
        Task<Phone> GetById(int id);
    }
}
