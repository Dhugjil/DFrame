using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DFrame.Linq;

namespace Models
{
    public class testlinq:DFrame.Linq.DLinq
    {
        public int age { get; set; }

        public string name { get; set; }

        public string tell { get; set; }

        public int order { get; set; }
    }
}
