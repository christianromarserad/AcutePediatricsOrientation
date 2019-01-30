using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AcutePediatricsOrientation.Models
{
    public class Documents
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string FilePath { get; set; }
        public string TopicId { get; set; }
        public virtual Topic Topic { get; set; }

    }
}
