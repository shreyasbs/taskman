using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMan.ViewModels
{
    public class TokenResponseViewModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
