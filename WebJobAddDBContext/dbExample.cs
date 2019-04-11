using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebJobAddDBContext
{
    class dbExample : DbContext
    {
        /* public dbExample(DbContextOptions options) : base(options)
         {

         }*/

        public dbExample(DbContextOptions options) : base(options)
        {
        }



        //Testing db
        public DbSet<Response> Response { get; set; }

        public DbSet<Information> Information { get; set; }

        
    }
}
