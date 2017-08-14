﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLua
{
    class Function
    {
        public string Name { get; set; }
        public string Descr { get; set; }
        public string Side { get; set; }
        public string Url { get; set; }
        public string[] Args { get; set; }
        public string[] Returns { get; set; }
        public string[] Example { get; set; }
        public List<Function> childs { get; set; }
        public Function()
        {
            childs = new List<Function>();
        }

    }
}