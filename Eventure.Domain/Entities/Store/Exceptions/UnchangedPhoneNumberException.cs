using System;

namespace Eventure.Domain.Entities
{
    public class UnchangedPhoneNumberException : Exception
    {
        public UnchangedPhoneNumberException() : base("Phone Number value already exists") { }
    }
}