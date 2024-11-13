using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShopProject.Test.Models
{
    public class Tag
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }

    
    public class PetTag
    {
        public long PetId { get; set; }
        public Pet Pet { get; set; }

        public long TagId { get; set; }
        public Tag Tag { get; set; }
    }
    public class PhotoUrl
    {
        public long Id { get; set; }
        public string Url { get; set; }
        public long PetId { get; set; }
        public Pet Pet { get; set; }
    }
}
