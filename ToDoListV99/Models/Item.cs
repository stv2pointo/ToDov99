﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToDoListV99.Models
{
    public class Item
    {
        public int ItemId { get; set; }
        public string Description { get; set; }
        public bool IsComplete { get; set; }
        public virtual List List { get; set; }

    }
}