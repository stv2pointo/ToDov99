using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ToDoListV99.Models;

namespace ToDoListV99.Services
{
    public class ItemService : IItemService
    {
        private MyDbContext _context = new MyDbContext();

        public void deleteItem(int id)
        {
            var itemInDb = _context.Items.SingleOrDefault(c => c.ItemId == id);

            _context.Items.Remove(itemInDb);

            _context.SaveChanges();
        }
    }
}