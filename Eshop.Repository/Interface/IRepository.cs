using Eshop.Domain.Domain_models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eshop.Repository.Interface
{
    public interface IRepository<T> where T:BaseEntity //edno repository za site entiteti nasi koi treba da nasleduvaat od ova baseEntity
{
        IEnumerable<T> GetAll();
        T Get(int id);
        void Insert(T enttity);
        void Update(T entity);
        void Delete(T entity);
}
}
