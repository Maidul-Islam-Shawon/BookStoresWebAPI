using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStoresWebAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace BookStoresWebAPI.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private readonly BookStores_DEVContext _context;

        public PublishersController(BookStores_DEVContext context)
        {
            _context = context;
        }

        // GET: api/Publishers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Publisher>>> GetPublishers()
        {
            return await _context.Publishers.ToListAsync();
        }

        // GET: api/Publishers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Publisher>> GetPublisher(int id)
        {
            var publisher = await _context.Publishers.FindAsync(id);

            if (publisher == null)
            {
                return NotFound();
            }

            return publisher;
        }


        // GET: api/Publishers/5 - with details of books and sales.
        [HttpGet("PublisherDetails/{id}")]
        public async Task<ActionResult<Book>> GetPublisherDetails(int id)
        {
            //Eager loading
            //var publisher = _context.Publishers.Include(pub => pub.Books)
            //                                        .ThenInclude(books=>books.BookAuthors)
            //                                   .Include(pub => pub.Books)
            //                                        .ThenInclude(books => books.Sales)
            //                                   .Include(pub=>pub.Users)
            //                                   .Where(pub => pub.PubId == id)
            //                                   .FirstOrDefault();


            //Explicit loading
            // var publisher = await _context.Publishers.SingleAsync(pub => pub.PubId == id);

            //_context.Entry(publisher)
            //    .Collection(pub => pub.Users)
            //    .Query()
            //    //.Where(user=>user.FirstName.Contains("mary"))
            //    .Include(user => user.RefreshTokens)
            //    .Load();

            //_context.Entry(publisher)
            //    .Collection(pub => pub.Books)
            //    .Query()
            //    //.Where(book=>book.Price <= 15)
            //    // .Include(book=>book.BookAuthors)
            //    .Include(book => book.Sales)
            //    .Load();


            var book = await _context.Books.SingleAsync(book => book.BookId == id);
            _context.Entry(book)
                .Reference(bo => bo.Pub)
               .Query().Include(bo=>bo.Users)
                .Load();

            //var user = await _context.Users.SingleAsync(user => user.UserId == 1);
            //_context.Entry(user)
            //      .Reference(usr => usr.Role)
            //      .Load();






            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        // GET: api/Publishers/5 - Post Details with linked tables.
        [HttpGet("PostPublisherDetails")]
        public async Task<ActionResult<Publisher>> PostPublisherDetails()
        {
            var publisher = new Publisher();
            publisher.PublisherName = "JMBP Ltd";
            publisher.City = "Oval";
            publisher.State = "EN";
            publisher.Country = "England";

            Book book1 = new Book();
            book1.Title = "Goblet of the fire";
            book1.PublishedDate = DateTime.Now;
           
            Book book2 = new Book();
            book2.Title = "Twilight";
            book2.PublishedDate = DateTime.Now;

            Sale sale1 = new Sale();
            sale1.Quantity = 2;
            sale1.StoreId = "8042";
            sale1.OrderNum = "xyz";
            sale1.PayTerms = "Net 20";
            sale1.OrderDate = DateTime.Now;

            Sale sale2 = new Sale();
            sale2.Quantity = 5;
            sale2.StoreId = "7131";
            sale2.OrderNum = "abc";
            sale2.PayTerms = "Net 50";
            sale2.OrderDate = DateTime.Now;

            book1.Sales.Add(sale1);
            book2.Sales.Add(sale2);


            publisher.Books.Add(book1);
            publisher.Books.Add(book2);

            _context.Publishers.Add(publisher);

            _context.SaveChanges();


            var publishers = await _context.Publishers.Include(pub => pub.Books)
                                                    .ThenInclude(books => books.BookAuthors)
                                               .Include(pub => pub.Books)
                                                    .ThenInclude(books => books.Sales)
                                               .Include(pub => pub.Users)
                                               .Where(pub => pub.PubId == publisher.PubId)
                                               .FirstOrDefaultAsync();


            if (publisher == null)
            {
                return NotFound();
            }

            return publisher;
        }




        // PUT: api/Publishers/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPublisher(int id, Publisher publisher)
        {
            if (id != publisher.PubId)
            {
                return BadRequest();
            }

            _context.Entry(publisher).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PublisherExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Publishers
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Publisher>> PostPublisher(Publisher publisher)
        {
            _context.Publishers.Add(publisher);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPublisher", new { id = publisher.PubId }, publisher);
        }

        // DELETE: api/Publishers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Publisher>> DeletePublisher(int id)
        {
            var publisher = await _context.Publishers.FindAsync(id);
            if (publisher == null)
            {
                return NotFound();
            }

            _context.Publishers.Remove(publisher);
            await _context.SaveChangesAsync();

            return publisher;
        }

        private bool PublisherExists(int id)
        {
            return _context.Publishers.Any(e => e.PubId == id);
        }
    }
}
