using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations;


namespace WebJobAddDBContext
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        [MaxLength(5)]
        public string OrgCode { get; set; }

        public ApplicationUser() : base()
        {

        }

    }
}