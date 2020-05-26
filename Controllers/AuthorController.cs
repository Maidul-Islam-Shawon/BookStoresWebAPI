using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoresWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BookStoresWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly BookStores_DEVContext _context;
        public AuthorController(BookStores_DEVContext context)
        {
            _context = context;
        }
        

         [HttpGet]
        public IEnumerable<Author> Get()
        {
            //using (var context = new BookStores_DEVContext())
            //{
                //get all author data
                 return _context.Authors.ToList();


            //get by id
            //int id = 5;
            //return context.Author.Where(auth=>auth.AuthorId==id).ToList();


            //save a new author
            //Author author = new Author();
            //author.FirstName = "Test";
            //author.LastName = "Delete";

            //context.Author.Add(author);
            //context.SaveChanges();
            //return context.Author.ToList();


            //Update a authore
            //Author author = _context.Authors.Where(auth => auth.FirstName == "Harry").FirstOrDefault();
            //author.Address = "Morden";
            //author.City = "London";
            //author.State = "England";
            //author.Phone = "01905769690";
            //author.EmailAddress = "harry@hogwards.ac.uk";
            //author.Zip = "SM4 5SH";

            //_context.Authors.Update(author);

            //return _context.Authors.Where(auth => auth.FirstName == "Harry").ToList();


            //Delete a Author from List
            //Author author = context.Author.Where(auth => auth.FirstName == "Test").FirstOrDefault();
            //context.Author.Remove(author);
            //context.SaveChanges();
            //return context.Author.ToList();

            // }

        }
    }
}
