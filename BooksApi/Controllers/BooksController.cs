using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksApi.Models;
using BooksApi.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BooksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    
    public class BooksController : ControllerBase
    {
        private readonly BookService bookService;

        public BooksController(BookService bookService)
        {
            this.bookService = bookService;
        }

        // GET: api/<BooksController>
        [HttpGet]
        public ActionResult<List<Book>> Get() =>
            bookService.Get();

        // GET api/<BooksController>/5
        [HttpGet("{id:length(24)}", Name = "GetBook")]
        public ActionResult<Book> Get(string id)
        {
            var searchedBook = bookService.Get(id);
            
            if(searchedBook == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(searchedBook);
            }
        }

        // POST api/<BooksController>
        [HttpPost]
        public ActionResult<Book> Create([FromBody] Book book)
        {
            bookService.Create(book);
            return CreatedAtRoute("GetBook", new { id = book.Id.ToString() }, book);
        }


        // PUT api/<BooksController>/5
        [HttpPut("{id:length(24)}")]
        public ActionResult Update(string id, [FromBody] Book bookIn)
        {
            var bookToBeUpdated = bookService.Get(id);
            if(bookToBeUpdated == null)
            {
                return NotFound();
            }
            else
            {
                bookToBeUpdated.Category = bookIn.Category;
                bookService.Update(id, bookToBeUpdated);
                return NoContent();
            }
            
        }

        // DELETE api/<BooksController>/5
        [HttpDelete("{id:length(24)}")]
        public ActionResult Delete(string id)
        {
            var bookToBeDeleted = bookService.Get(id);
            if(bookToBeDeleted == null)
            {
                return NotFound();
            }
            else
            {
                bookService.Remove(bookToBeDeleted.Id);
                return NoContent();
            }
        }
    }
}
