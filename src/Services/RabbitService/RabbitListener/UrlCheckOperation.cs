namespace RabbitListener
{
    public class UrlCheckOperation
    {
        private static readonly HttpClient client = new HttpClient();

        private List<string> address;

        private List<LogObject> logObject = new List<LogObject>();

        public UrlCheckOperation(List<string> address)
        {
            this.address = address;
        }

        public List<LogObject> GetStatusCode()
        {
            foreach (var item in address)
            {
                var res = client.Send(new HttpRequestMessage(HttpMethod.Head, item.ToString()));
                logObject.Add(new LogObject() { Address = item.ToString(), StatusCode = res.StatusCode.ToString() });
            }
            return logObject;
        }

    }
}