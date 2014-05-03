using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minerva.Repositories
{
    public interface IFtpConnection
    {
        bool Connect(string user, string pass, string url);
    }
}
