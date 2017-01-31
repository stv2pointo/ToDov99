using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToDoListV99.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public virtual ICollection<List> Lists { get; set; }
    }
}