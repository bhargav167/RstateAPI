using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;

namespace RstateAPI {
    public class LoginModel {
        public string UserName { get; set; }
        public string password { get; set; }
        public string ReturnUrl { get; set; }
        public IList<AuthenticationScheme> Externallogin { get; set; }
    }
}