using AcutePediatricsOrientation.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AcutePediatricsOrientation.ViewModels
{
    public class EditPackageViewModel
    {
        public List<CategoryViewModel> Categories { get; set; }
    }

    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<TopicViewModel> Topics { get; set; }
    }

    public class TopicViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public IEnumerable<DocumentsViewModel> Documents { get; set; }
    }

    public class DocumentsViewModel
    {
        public string Name { get; set; }
    }
}
