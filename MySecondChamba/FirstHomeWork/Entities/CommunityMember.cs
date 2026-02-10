using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySecondChamba.Entities
{
    public class CommunityMember : Community
    {
      public int Id { get; set; }
      public string Name { get; set; }
      public int PhoneNumber { get; set; }
      public string PersonalID { get; set; }
      public Community Comunity { get; set; }

    }
}
