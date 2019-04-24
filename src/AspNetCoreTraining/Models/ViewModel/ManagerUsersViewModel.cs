using Microsoft.AspNetCore.Identity;

namespace AspNetCoreTraining.Models.ViewModel
{
    public class ManagerUsersViewModel
    {
        public IdentityUser[] Administrators { get; set; }
        public IdentityUser[] Everyone { get; set; }
    }
}