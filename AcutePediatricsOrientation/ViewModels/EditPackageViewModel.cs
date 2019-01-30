using AcutePediatricsOrientation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcutePediatricsOrientation.ViewModels
{
    public class EditPackageViewModel
    {
        public List<EditPackageViewModelCategory> Categories { get; set; }
    }

    public class EditPackageViewModelCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<EditPackageViewModelTopic> Topics { get; set; }
    }

    public class EditPackageViewModelTopic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<EditPackageViewModelDocuments> Documents { get; set; }
    }

    public class EditPackageViewModelDocuments
    {
        public string Name { get; set; }
    }
}
