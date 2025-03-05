using MediatR;

namespace URLShortener.Application.Url.Requests
{
    public class DeleteRequest : IRequest<string>
    {
        public string shorUrl { get; set; }
    }
}
