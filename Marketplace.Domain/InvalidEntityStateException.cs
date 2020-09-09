using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Domain
{
    class InvalidEntityStateException : Exception
    {
        public InvalidEntityStateException(object entity, string message)
            : base($"Entity {entity.GetType().Name} state change rejected, {message}")
        {
        }
    }
}
