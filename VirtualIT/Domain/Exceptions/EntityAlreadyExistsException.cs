using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualIT.Domain.Exceptions {
    public class EntityAlreadyExistsException :ApplicationException{
        public EntityAlreadyExistsException(string entityName, string parameterName, string? parameterValue) : base($"'{entityName}' with '{parameterName}':'{parameterValue}' already exists.") {
        }
    }
}
