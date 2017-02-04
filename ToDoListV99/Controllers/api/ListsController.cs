using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ToDoListV99.Models;

namespace ToDoListV99.Controllers.api
{
    public class ListsController : ApiController
    {

        private MyDbContext _context;

        public ListsController()
        {
            _context = new MyDbContext();

        }

        public IHttpActionResult GetLists(string query = null)
        {
            return Ok(_context.Lists.ToList());
        }
    }
}
