using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ADFS_Demo2.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ADFS_Demo2.Data
{
    public class ADFS_Demo2Context : IdentityDbContext<ADFS_Demo2User>
    {
        public ADFS_Demo2Context(DbContextOptions<ADFS_Demo2Context> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
