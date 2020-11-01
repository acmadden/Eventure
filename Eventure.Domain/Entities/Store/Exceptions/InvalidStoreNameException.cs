using System;

namespace Eventure.Domain.Entities
{
    public class InvalidStoreNameException : Exception
    {
        public InvalidStoreNameException() : base("Invalid store name.") { }
    }
}