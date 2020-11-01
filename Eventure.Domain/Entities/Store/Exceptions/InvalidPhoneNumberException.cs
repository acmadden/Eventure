using System;

namespace Eventure.Domain.Entities
{
    public class InvalidPhoneNumberException : Exception
    {
        public InvalidPhoneNumberException() : base("Invalid store name.") { }
    }
}