using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using khalid.Models;
using khalid.Models.Repository;
using khalid.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace khalid.Controllers
{
    public class BookController : Controller
    {
        private readonly IHostingEnvironment _ho;
        private readonly IRepositories<Author> _author;
        private readonly IRepositories<Book> _book;

        public BookController(IRepositories<Book>book, IRepositories<Author>author,IHostingEnvironment _ho) 
        {
            this._author = author;
            this._ho = _ho;
            this._book = book;
        }
        // GET: Book
        public ActionResult Index()
        {
            var obj = _book.List();
            return View(obj);
        }

        // GET: Book/Details/5
        public ActionResult Details(int id)
        {
            var obj = _book.find(id);
            return View(obj);
        }

        // GET: Book/Create
        public ActionResult Create()
        {
            var tm = new BookAuthor
            { Authors = selectitemAuthor() };

            return View(tm);
        }

        // POST: Book/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookAuthor obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string fullpath = Create_path_new_file(obj.File) ?? string.Empty;
                    if(fullpath !=string.Empty)
                    saveFile(fullpath, obj.File);

                    if (obj.AuthorId == -1)
                    {
                        ViewBag.message = "رجاء اختار عنصر من قائمة الموؤلفين !";
                        return View(fill_AuthorsList());
                    }
                    Book bo;
                    if (obj.File != null)
                    {
                        bo = new Book
                        {
                            Id = obj.BookId,
                            Title = obj.title,
                            Description = obj.Description,
                            email = obj.email,
                            author = _author.find(obj.AuthorId),

                            ImageUrl = obj.File.FileName

                        };
                    }
                    else
                    {

                         bo = new Book
                        {
                            Id = obj.BookId,
                            Title = obj.title,
                            Description = obj.Description,
                            email = obj.email,
                            author = _author.find(obj.AuthorId)
                        };
                    }
                    _book.add(bo);
                    return RedirectToAction(nameof(Index));
                }
                catch( Exception ex)
                {
                    return View(ex.Message);
                }
            }//end if

            return View(nameof(Details));
        }

        // GET: Book/Edit/5
        public ActionResult Edit(int id)
        {
            var book = _book.find(id);

            var tm = new BookAuthor
            {
                BookId = book.Id,
                title = book.Title,
                Description = book.Description,
                AuthorId = book.author.Id,
                Authors = _author.List().ToList(),
                ImageUrl=book.ImageUrl
                
            };
            return View(tm);
        }

        // POST: Book/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, BookAuthor obj)
        {
            try
            {
                // TODO: Add update logic here

                string filename = string.Empty;
                if (obj.File != null)
                {

                    //intiailiz file new
                    string fullpath = Create_path_new_file(obj.File);

                    //intiailiz file old
                    string fullOldPath = oldfile(obj) ?? string.Empty;

                    // full=oldfile(obj)?oldfile(obj):null

                    //Delete file old
                    if (fullOldPath !=string.Empty)
                    System.IO.File.Delete(fullOldPath);

                    //Save file new
                    saveFile(fullpath, obj.File);  
                }
                var bok = new Book
                {
                    Id = obj.BookId,
                    Title = obj.title,
                    Description = obj.Description,
                    email=obj.email,
                    author = _author.find(obj.AuthorId),
                    ImageUrl=null
                };
                _book.update(obj.BookId, bok);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                return View(ex.Message);
            }
        }

        // GET: Book/Delete/5
        public ActionResult Delete(int id)
        {
            var bt = _book.find(id);
            return View(bt);
        }

        // POST: Book/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDelete(int id)
        {
            try
            {
                // TODO: Add delete logic here
                _book.Del(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        List<Author> selectitemAuthor()
        {

            var au = _author.List().ToList();
            au.Insert(0, new Author { Id = -1, FullName = "--- select item ---" });
            return au;
        }
        private BookAuthor fill_AuthorsList()
        {
            var bo = new BookAuthor
            {
                Authors = selectitemAuthor()
            };
            return bo;
        }

        //copy image to wwwroot-->uploads

        //Create new path for File / image
        //intiailiz file new
        private string Create_path_new_file(IFormFile fi)
        {
            if (fi != null)
            {
                string upload = Path.Combine(_ho.WebRootPath, "uploads");
                string fullpath = Path.Combine(upload, fi.FileName);
                return fullpath;
            }
            return null;
            
        }
        private string pathroot()
        {
            string upload = Path.Combine(_ho.WebRootPath, "uploads");
            return upload;
        }
        private void saveFile(string fullpath_new,IFormFile fi)
        {
            //Save file new
            if (fi != null)
            {
                FileStream cc = new FileStream(fullpath_new, FileMode.Create);
                fi.CopyTo(cc);
                cc.Close();
            }
        }
        //intiailiz  return path old file full
        private string oldfile(BookAuthor obj)
        {
            if (obj.ImageUrl != null)
            {
                string oldfilename = obj.ImageUrl;
                string oldfullpath = Path.Combine(pathroot(), oldfilename);
                return oldfullpath;
            }
            return null;
        }

        public ActionResult Search(string word)
        {
            var  ser= _book.Search(word);
            return View("Index", ser);
        }
    }
   
}