using System;

namespace DAL.Exceptions
{
    public sealed class NotFoundException:Exception
    {
        public NotFoundException():base("Not found.")
        { }

        public NotFoundException(int id) : base($"Entity with id:{id} not found.")
        { }

        public NotFoundException(string value):base($"Entity with value: {value} not found.")
        { }
    }
}
