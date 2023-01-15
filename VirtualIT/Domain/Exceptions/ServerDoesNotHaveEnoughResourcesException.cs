using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualIT.Domain.Exceptions {
    public class ServerDoesNotHaveEnoughResourcesException : ApplicationException {
        public ServerDoesNotHaveEnoughResourcesException(string serverNaam, string vmNaam) : base($"{serverNaam} heeft niet genoeg resources om {vmNaam} te hosten") {
        }
    }
}
