using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace khalid.Models.Repository
{
    public class AuthorRepositories:IRepositories<Author>
    {
        IList<Author> author;

        public AuthorRepositories()
        {
            author = new List<Author>()
            {
                new Author{Id=1,FullName="murad salem"},
                new Author{Id=2,FullName="ali daleh"},
                new Author{Id=8,FullName="hossen nasser"},
            };
        }

        public void add(Author t)
        {
            
           
            t.Id = author.Max(x => x.Id) + 1;
            author.Add(t);
        }

        public void Del(int id)
        {
            var au = find(id);
            author.Remove(au);

        }

        public Author find(int id)
        {
            var au = author.SingleOrDefault(s => s.Id == id);
            return au;
        }

        public IList<Author> List()
        {
            return author;
        }

        public IEnumerable<Author> Search(string st)
        {
            throw new NotImplementedException();
        }

        public void update(int id, Author newobj)
        {
            var old = find(id);
            old.Id = newobj.Id;
            old.FullName = newobj.FullName;
        }
    }
}
