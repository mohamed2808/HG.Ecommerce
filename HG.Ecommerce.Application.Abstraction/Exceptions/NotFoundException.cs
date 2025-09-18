namespace HG.Ecommerce.Application.Abstraction.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }
    }
}
