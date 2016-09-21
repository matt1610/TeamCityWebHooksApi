using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TcWebHooks.Models
{
    public class Request
    {
        public string Project { get; set; }
        public bool BuildSucceeded { get; set; }
    }
}