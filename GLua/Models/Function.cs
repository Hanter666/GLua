using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLua.Models
{
    class Function
    {
        public string Name { get; set; }
        public string Descr { get; set; }
        public string Side { get; set; }
        public string Url { get; set; }
        public List<string[]> Args { get; set; } = new List<string[]>();
        public List<string[]> Returns { get; set; } = new List<string[]>();
        public string[] Example { get; set; }
        public List<Function> Childs { get; set; } = new List<Function>();
    }
}
