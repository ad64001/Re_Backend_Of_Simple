using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Re_Backend.Common
{
    public interface IJwtService
    {
        string GenerateToken(string userId);
    }
}
