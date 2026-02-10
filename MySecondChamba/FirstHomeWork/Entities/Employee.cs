using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySecondChamba.Entities
{
    public class Employee : CommunityMember
    {
        public string profession { get; set; }
        public decimal salary { get; set; }
        public string CompanyName { get; set; }

    }
}
