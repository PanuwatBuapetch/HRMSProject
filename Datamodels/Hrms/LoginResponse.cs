using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datamodels.Hrms
{
    public class LoginResponse
    {
        public string EmployeeId { get; set; } = "";
        public string FullNameThai { get; set; } = "";
        public string Username { get; set; } = "";
        public string Role { get; set; } = "";
    }
}
