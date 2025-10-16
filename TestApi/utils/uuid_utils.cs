using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApi.utils
{
    internal class uuid_utils
    {
        static public string createUUID()
        {
            var uuid = Guid.NewGuid().ToString();
            return uuid;
        }
    }
}
