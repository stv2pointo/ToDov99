using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ToDoListV99.Models;

namespace ToDoListV99.Controllers.api
{
    public class ItemsController : ApiController
    {
        private MyDbContext _context;

        public ItemsController()
        {
            _context = new MyDbContext();

        }

        public IHttpActionResult GetItems(string query = null)
        {
            return Ok(_context.Items.ToList());
        }

        public IHttpActionResult DeleteItem(int id)
        {
            var itemInDb = _context.Items.SingleOrDefault(c => c.ItemId == id);

            if (itemInDb == null)
                return NotFound();

            _context.Items.Remove(itemInDb);
            _context.SaveChanges();

            return Ok();
        }
    }
}
