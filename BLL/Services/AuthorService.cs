﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public interface IAuthorService
    {
        public IQueryable<AuthorModel> Query();

        public ServiceBase Create(Author record);

        public ServiceBase Update(Author record);

        public ServiceBase Delete(int id);
    }
    public class AuthorService : ServiceBase, IAuthorService
    {
        public AuthorService(Db db) : base(db)
        {

        }

        public IQueryable<AuthorModel> Query()
        {
            return _db.Authors
                .OrderBy(a => a.Name)
                .Select(a => new AuthorModel()
                {
                    Record = a
                });
        }

        public ServiceBase Create(Author record)
        {
            if (_db.Authors.Any(a => a.Name == record.Name.ToUpper().Trim()))
                return Error("An Author with same name exists!");

            record.Name = record.Name.Trim();  //Boşlukları twmizler.

            _db.Authors.Add(record);
            _db.SaveChanges();
            return Success("Author is created successfully");

        }

        public ServiceBase Update(Author record)
        {
            var entity = _db.Authors.FirstOrDefault(a => a.Id == record.Id);

            entity.Name = record.Name.Trim();
            entity.Surname = record.Surname.Trim();

            _db.Authors.Update(entity);
            _db.SaveChanges();

            return Success("Author updated successfully");
        }


        public ServiceBase Delete(int id)
        {
            //var entity = _db.Authors.FirstOrDefault(a => a.Id == id);
            //if (entity == null)
            //    return Error("Author Not found");
            //if (entity.Books != null && entity.Books.Count > 0)
            //    return Error("There is a book exists!!")

            var entity = _db.Authors.Include(b => b.Books).SingleOrDefault(a => a.Id == id);
            if (entity.Books.Any()) // Count > 0
                return Error("Author has relational pets!");

            _db.Authors.Remove(entity);
            _db.SaveChanges();
            return Success("Author deleted successfully");
        }
    }
}
