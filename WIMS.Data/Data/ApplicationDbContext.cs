using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WIMS.Data;

namespace WIMS.MVC.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<BugItem> BugItems { get; set; }
        public DbSet<FeatureItem> FeatureItems { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Note> Notes { get; set; }


    }



}
