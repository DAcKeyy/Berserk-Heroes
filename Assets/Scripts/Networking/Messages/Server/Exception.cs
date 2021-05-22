using Berserk.Networking.Messages;

namespace Berserk.Messaging.Messages
{
    public enum ExeptionType
    {
        
    }
    
    public class Exception : IDescriptional
    {
        public Exception ExceptionType;
        public string Description { get; set; }
    }
}