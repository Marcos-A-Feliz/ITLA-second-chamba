using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySecondChamba.Entities
{
    public class Student : CommunityMember
    {
        public string career { get; set; }
        public int StudentCode { get; set; }
        public int Semester { get; set; }

    }
}
