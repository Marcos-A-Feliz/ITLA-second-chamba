using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySecondChamba.Entities
{
    public class Teacher : Employee
    {
        public string section { get; set; }
        public string subject { get; set; }
        public string Title { get; set; }

    }
}
