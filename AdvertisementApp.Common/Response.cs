namespace AdvertisementApp.Common
{
    public class Response : IResponse
    {
        public Response(ResponseType responseType)
        {
            ResponseType = responseType;
        }

        public Response(ResponseType responseType, string message) : this(responseType)
        {
            Message = message;
        }

        public string Message { get; set; }
        public ResponseType ResponseType { get; set; }
    }
}