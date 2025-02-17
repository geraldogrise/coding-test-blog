using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Aggreagates.Auth
{
    public interface IAuthService
    {
        public string GenerateJwtToken(string username);
    }
}
