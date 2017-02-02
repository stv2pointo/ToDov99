using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToDoListV99.Models
{
    public class ListToCategory
    {
        public int ListToCategoryId { get; set; }
        public int ListId { get; set; }
        public int CategoryId { get; set; }
        public virtual List List { get; set; }
        public virtual Category Category { get; set; }
       
    }
}