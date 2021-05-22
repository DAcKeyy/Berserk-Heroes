using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Berserk.Networking.Messages
{
    interface ITokenable
    {
        public string AccessToken { get; set; }
    }
}
