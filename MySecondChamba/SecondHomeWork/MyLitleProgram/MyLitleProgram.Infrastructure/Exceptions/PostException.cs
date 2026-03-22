namespace MyLitleProgram.Infrastructure.Exceptions
{
    public class PostException : Exception
    {
        public PostException(string message) : base(message) { }

        public PostException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
