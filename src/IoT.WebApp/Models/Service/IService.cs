using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace IoT.WebApp.Models.Service
{
    interface IService
    {
        ICollection<IEntity> GetAll();
        IEntity Get(int id);
        IEntity Update(int id);
        IEntity Create(IEntity entity);
        IEntity Delete(int id);
    }
}
