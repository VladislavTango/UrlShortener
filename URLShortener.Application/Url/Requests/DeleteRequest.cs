using MediatR;

namespace URLShortener.Application.Url.Requests
{
    public class DeleteRequest : IRequest<int>
    {
        public int Id { get; set; }
    }
}
