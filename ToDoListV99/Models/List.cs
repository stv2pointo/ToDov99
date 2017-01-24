using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToDoListV99.Models
{
    public class List
    {
        public int ListId { get; set; }
        public string ListName { get; set; }
        public virtual ICollection<Item> Items { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
    }
}