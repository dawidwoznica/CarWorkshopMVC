using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Application.ApplicationUser
{
    public class CurrentUser(string id, string email, IEnumerable<string> roles)
    {
        public string Id { get; set; } = id;
        public string Email { get; set; } = email;
        public IEnumerable<string> Roles { get; set; } = roles;

        public bool IsInRole(string role) => Roles.Contains(role);
    }
}
