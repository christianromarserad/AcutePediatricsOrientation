using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcutePediatricsOrientation.Models
{
    public class Topic
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string CategoryId { get; set; }
        public virtual ICollection<Documents> Documents { get; set; }

        public virtual Category Category { get; set; }
    }
}
