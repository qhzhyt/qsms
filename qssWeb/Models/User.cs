using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using System.Web;
namespace qssWeb.Models
{
    public class User
    {
        public int userId { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
    }
    
}