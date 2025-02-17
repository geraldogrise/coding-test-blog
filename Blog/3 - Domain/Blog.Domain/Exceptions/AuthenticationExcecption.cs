using System.Runtime.Serialization;


namespace Blog.Domain.Exceptions
{
    [Serializable]
    public class AuthenticationExcecption : Exception
    {
        public AuthenticationExcecption(string message)
            : base(message)
        {
        }

        public AuthenticationExcecption(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected AuthenticationExcecption(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
