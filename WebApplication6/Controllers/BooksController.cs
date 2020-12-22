using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication6.Models;

namespace WebApplication6.Controllers
{
    public class BooksController : ODataController
    {
        private readonly AppDbContext _context;

        public BooksController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        [EnableQuery]
        public virtual IQueryable<Book> Get()
        {
            return _context.Books;
        }

        [HttpGet("({key})")]
        [EnableQuery]
        public virtual SingleResult<Book> Get([FromODataUri] Guid key)
        {
            return SingleResult.Create(_context.Books.Where(h => h.Id == key));
        }
    }
}
