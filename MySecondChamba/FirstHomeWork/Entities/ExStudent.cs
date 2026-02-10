using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySecondChamba.Entities
{
    public class ExStudent : CommunityMember
    {
        public string title { get; set; }
        public string CareerStudied { get; set; }
        public int GraduationYear { get; set; }
    }
}
