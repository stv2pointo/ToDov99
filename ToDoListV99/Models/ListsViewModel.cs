using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToDoListV99.Models
{
    public class ListsViewModel
    {
        public int ListId { get; set; }
        public string ListName { get; set; }
        public List<CheckBoxViewModel> Categories { get; set; }//is the name CAtegories a problem?

    }
}