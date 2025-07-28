using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzaHub.Models.ApiModels.Response
{
    public class ValidateUserResponse
    {
        //Use case of default ! -> just to supress the warning
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public int UserId { get; set; }
        public List<string> Roles { get; set; } = default!;
    }
}
