using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcutePediatricsOrientation.ViewModels
{
    public class StaffListViewModel
    {
        public List<StaffViewModel> Users { get; set; }
    }

    public class StaffViewModel
    {
        public string UserName { get; set; }
        public int UserId { get; set; }
        public double Progress { get; set; }
    }
}
