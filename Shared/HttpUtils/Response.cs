namespace Shared.HttpRequests
{
    public class Response
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public object Result { get; set; }
    }
}
