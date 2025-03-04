using MediatR;
using URLShortener.Application.Url.Requests;
using URLShortener.Domain.Interfaces;

namespace URLShortener.Application.Url.Handlers
{
    public class DeleteUrlHandler : IRequestHandler<DeleteRequest, int>
    {
        private readonly IUrlRepository _urlRepository;

        public DeleteUrlHandler(IUrlRepository urlRepository)
        {
            _urlRepository = urlRepository;
        }

        public async Task<int> Handle(DeleteRequest request, CancellationToken cancellationToken)
        {
            if (_urlRepository.GetById(request.Id) != null)
              throw new Exception("Нет такой записи");

            return await _urlRepository.DeleteUrl(request.Id);

        }
    }
}
