using System;

namespace DAL.Exceptions
{
    public sealed class InvalidUserDataException : Exception
    {
        public InvalidUserDataException():base("Invalid user data.")
        { }

        
    }
}
