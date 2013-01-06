using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JTL.Core
{
    public class AuthorizationFailedException : Exception
    {
        public AuthorizationFailedException(Exception inner)
            : base("Invalid username or password specified.", inner)
        {
        }
    }
}
