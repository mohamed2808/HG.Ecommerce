namespace HG.Ecommerce.Application.Abstraction.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message) { }
    }
}
