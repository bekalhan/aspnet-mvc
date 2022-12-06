using Microsoft.AspNetCore.Identity;

namespace WP.Models
{
    public class User:IdentityUser
    {
        public string UserSurname { get; set; }
    }
}
