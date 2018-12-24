using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Repository
{
    public interface IWriteRepository<T>
    {
        T ReadById(string id);
        T Create(T model);
        T Update(T model);
        bool Delete(string id);
    }
}
