using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BilgeAdamGuney.Models
{
    public class MemberResetPassword
    {
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}