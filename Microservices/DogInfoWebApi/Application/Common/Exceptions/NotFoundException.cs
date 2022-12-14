namespace Application.Common.Exceptions
{
    public sealed class NotFoundException : Exception
    {
        public NotFoundException()
        { }

        public NotFoundException(int id) : base($"Entity with id: {id} not found.")
        { }
    }
}
