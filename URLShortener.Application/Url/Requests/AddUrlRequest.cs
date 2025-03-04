using MediatR;

namespace URLShortener.Application.Url.Requests
{
    public class AddUrlRequest : IRequest<string>
    {
        public string Url { get; set; }
    }
}
