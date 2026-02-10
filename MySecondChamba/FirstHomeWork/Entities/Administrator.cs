using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySecondChamba.Entities
{
    public class Administrator : Employee
    {
        public string department { get; set; }
        public string adminMode { get; set; }
        public string employeesupervised { get; set; }
    }
}
