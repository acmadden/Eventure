using System;

namespace Eventure.Domain.Entities
{
    public class PhoneNumberUnchangedException : Exception
    {
        public PhoneNumberUnchangedException() : base("Phone number was unchanged") { }
    }
}