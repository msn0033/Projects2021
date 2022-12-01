using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace khalid.Models.Repository
{
    public class AuthorDbRepositories : IRepositories<Author>
    {
        BookDbContext db;
        public AuthorDbRepositories( BookDbContext db)
        {
            this.db = db;
        }

        public void add(Author t)
        {
            db.Authors.Add(t);
            db.SaveChanges();
        }

        public void Del(int id)
        {
            var de = find(id);
            db.Authors.Remove(de);
            db.SaveChanges();
        }

        public Author find(int id)
        {
            return db.Authors.SingleOrDefault(w=>w.Id==id);
        }

        public IList<Author> List()
        {
            return db.Authors.ToList();
        }

        public IEnumerable<Author> Search(string st)
        {
            throw new NotImplementedException();
        }

        public void update(int id, Author newobj)
        {
            db.Authors.Update(newobj);
            db.SaveChanges();
        }
    }
}
