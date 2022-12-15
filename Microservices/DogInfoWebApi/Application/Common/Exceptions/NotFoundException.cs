namespace Application.Common.Exceptions
{
    public sealed class NotFoundException : Exception
    {
        public NotFoundException():base("Entities not found.")
        { }

        public NotFoundException(int id) : base($"Entity with id: {id} not found.")
        { }

        public NotFoundException(string value) : base($"Entity with value: {value} not found.")
        { }
    }
}
