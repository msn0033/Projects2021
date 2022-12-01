using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TourTest.DataGeneral.InterFace
{
     public interface IGenericRepository<TEntity> where TEntity:class
    {
        public Task<IEnumerable<TEntity>> GetAllAsync();
        public void insert(TEntity obj);
    }
}
