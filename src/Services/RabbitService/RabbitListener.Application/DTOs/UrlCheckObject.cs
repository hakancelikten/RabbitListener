namespace RabbitListener.Application.DTOs
{
    public class UrlCheckObject
    {
        public string Url { get; set; }
        public string StatusCode { get; set; }
        public string ServiceName { get; set; } = "RabbitListener";
    }
}
