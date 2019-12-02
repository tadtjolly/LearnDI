using System;
using System.Collections.Generic;
using System.Text;

namespace LearnDI.EntityDbContext
{
    public class DiEntity
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class LoginEntity
    {
        public int id { get; set; }
        public string user { get; set; }
        public string pass { get; set; }
    }
}
