﻿using System;
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
        [Required]
        public string Name { get; set; }
        [Required]
        public int DocumentTypeId { get; set; }
        [Required]
        public string FilePath { get; set; }
        [Required]
        public int TopicId { get; set; }
        public virtual Topic Topic { get; set; }

    }
}
