

using Microsoft.AspNetCore.Identity;

namespace WebApplication3
{
    public class LoginDetail
    {

        public int UserId { get; set; }

        public  required string UserName { get; set; }

        public required string password { get; set; }

    }


    public  class Loginuser
    {
        public int UserId { get; set; }

        public required string UserName { get; set; }

        public required string password { get; set; }
        public string?  role { get; set; }



    }
    public class ApplicationUser : IdentityUser
    {
    }
}
