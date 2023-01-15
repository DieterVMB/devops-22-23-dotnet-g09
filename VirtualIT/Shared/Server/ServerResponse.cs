using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualIT.Shared.Server
{
    public class ServerResponse
    {
        public class Index
        {
            public IEnumerable<ServerDto.Index>? Servers { get; set; }
        }

        public class Create
        {
            public int ServerId { get; set; }
            public string Naam { get; set; }
            public int Storage { get; set; }
        }
    }
}
