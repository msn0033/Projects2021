using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace khalid.Models.Repository
{
    public class BookDbRepositories : IRepositories<Book>
    {
        BookDbContext db;
        public BookDbRepositories( BookDbContext db)
        {
            this.db = db;
        }
        public void add(Book t)
        {
            db.Books.Add(t);
            db.SaveChanges();
        }

        public void Del(int id)
        {
            var bo = find(id);
            db.Books.Remove(bo);
            db.SaveChanges();
        }

        public Book find(int id)
        {
            return db.Books.Include(n=>n.author).SingleOrDefault(a => a.Id == id);
        }

        public IList<Book> List()
        {
            return db.Books.Include(a=>a.author).ToList();
        }

        public IEnumerable<Book> Search(string st)
        {
            var resutl = db.Books.Include(a => a.author).Where(b => b.Title.Contains(st));
            
            return resutl.ToList();
        }

        public void update(int id , Book newobj)
        {
            db.Books.Update(newobj);
            db.SaveChanges();
        }
    }
}
