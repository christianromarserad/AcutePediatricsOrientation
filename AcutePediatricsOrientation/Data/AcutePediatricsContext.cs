using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AcutePediatricsOrientation.Models
{
    public class AcutePediatricsContext : DbContext
    {
        public AcutePediatricsContext (DbContextOptions<AcutePediatricsContext> options)
            : base(options)
        {
        }

        public DbSet<AcutePediatricsOrientation.Models.Account> Account { get; set; }
    }
}
