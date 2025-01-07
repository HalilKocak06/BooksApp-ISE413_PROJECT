using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class GenreService : ServiceBase , IService<Genre, GenreModel>
    {
        public GenreService(Db db) : base(db)
        {
        }

        public ServiceBase Create(Genre genre)
        {
            if (_db.Genres.Any(g => g.Name.ToUpper() == genre.Name.ToUpper().Trim()))
                return Error("Store with the same name exists!");
            genre.Name = genre.Name.Trim();
            _db.Add(genre);
            _db.SaveChanges();
            return Success("Store created successfully.");
        }

        

        public IQueryable<GenreModel> Query()
        {
            return _db.Genres.OrderBy(g => g.Name).Select(g => new GenreModel { Record = g });  //burada mutlaka Record = g yapmamız lazım aksi taktirde GenreModel boş olur.
        }

        public ServiceBase Update(Genre genre)
        {
            if (_db.Genres.Any(s => s.Id != genre.Id && s.Name.ToUpper() == genre.Name.ToUpper().Trim()))
                return Error("Store with the same name exists!");
            Genre entity = _db.Genres.SingleOrDefault(s => s.Id == genre.Id);
            entity.Name = genre.Name.Trim();
            _db.Update(entity);
            _db.SaveChanges();
            return Success("Store updated successfully.");
        }


        public ServiceBase Delete(int id)
        {
            Genre entity = _db.Genres.Include(p => p.BookGenres).SingleOrDefault(p => p.Id == id);
            _db.BookGenres.RemoveRange(entity.BookGenres);
            _db.Remove(entity);
            _db.SaveChanges();
            return Success("Store deleted successfully.");
        }

    }

}
