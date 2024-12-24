using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DAL;
using BLL.Services.Bases;
using BLL.Models;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{

    

    public class BookService : ServiceBase, IService<Book, BookModel>
    {
        public BookService(Db db) : base(db)
        {

        }

        public IQueryable<BookModel> Query()
        {
            return _db.Books
                .Include(b => b.Author)
                .Include(b => b.BookGenre) //Many to many için eklenildi.
                .ThenInclude(bg => bg.Genre) //Many to many için ... BookModel'de eklediğimiz için.
                .OrderBy(b => b.Name)
                .Select(b => new BookModel()
                {
                    Record = b
                });
        }



        public ServiceBase Create(Book record)
        {
            if (_db.Books.Any(b => b.Name.ToUpper() == record.Name.ToUpper().Trim())) //Aynu isim var mı diye kontrol eder .
                return Error("A book with the same name exists!");

            record.Name = record.Name.Trim();  //Boşlukları twmizler.


            _db.Books.Add(record);
            _db.SaveChanges();
            return Success("Book is created successfully");

        }

        public ServiceBase Update(Book record)
        {
            if (_db.Books.Any(b => b.Id != record.Id && b.Name.ToUpper() == record.Name))
                return Error("Book with the same name exists..."); // Aynı isimde kayıt var mı kontrol ediyoruz.

            var entity = _db.Books.Include(b => b.BookGenre).SingleOrDefault(b => b.Id == record.Id);

            if (entity == null)
                return Error("Book not found!");

            _db.BookGenres.RemoveRange(entity.BookGenre);
            entity.Name = record.Name.Trim();
            entity.NumberOfPages = record.NumberOfPages;
            entity.PublishDate = record.PublishDate;
            entity.Price = record.Price;
            entity.IsTopSeller = record.IsTopSeller;
            entity.AuthorId = record.AuthorId;
            entity.BookGenre = record.BookGenre;

            _db.Books.Update(entity);
            _db.SaveChanges();

            return Success("Book is updated successfully");

        }


        
        public ServiceBase Delete(int id)
        {
            var entity = _db.Books.Include(b => b.BookGenre).SingleOrDefault(b => b.Id == id);

            if (entity == null)
                return Error("Book not Found!");
            _db.BookGenres.RemoveRange(entity.BookGenre);

            _db.Books.Remove(entity);
            _db.SaveChanges();

            return Success("Book deleted successfully.");

        }

    }
}
