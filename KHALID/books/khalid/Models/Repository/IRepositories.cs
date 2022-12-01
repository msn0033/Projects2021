using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace khalid.Models.Repository
{
    public interface IRepositories<T> 
    {
        public IList<T> List();
        public T find(int id);
        public void add(T t);
        public void update(int id, T newobj);
        public void Del(int id);
        public IEnumerable<T> Search(string st);
    }
}
