using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace khalid.Models.Repository
{
    public class BookRepositories : IRepositories<Book>
    {
        IList<Book> bookss;
        public BookRepositories()
        {
            bookss = new List<Book>
            {
                new Book{Id=1,Title="java",Description="Exlent",author=new Author{Id=2 },email="aaa@gmail.com",ImageUrl="images.png" },
                new Book{Id=2,Title="python",Description="good",author=new Author() },
                new Book{Id=3,Title="php",Description="veryGood",author=new Author() }
            };
        }
        public void add(Book t)
        {
            t.Id = bookss.Max(x => x.Id) + 1;
            bookss.Add(t);
        }

        public void Del(int id)
        {
            var bo = find(id);
            bookss.Remove(bo);
        }

        public Book find(int id)
        {
            var bo = bookss.SingleOrDefault(a => a.Id == id);
            return bo;
        }

        public IList<Book> List()
        {
            return bookss;
        }

        public IEnumerable<Book> Search(string st)
        {
            throw new NotImplementedException();
        }

        public void update(int id, Book newobj)
        {
            var bo = find(id);
           // bo.Id = newobj.Id;
            bo.Title = newobj.Title;
            bo.Description = newobj.Description;
            bo.author = newobj.author;
            bo.email = newobj.email;
            bo.ImageUrl = newobj.ImageUrl;
        }
    }
}
