using MediatR;
using System.Text.RegularExpressions;
using URLShortener.Application.Url.Requests;
using URLShortener.Domain.Interfaces;

namespace URLShortener.Application.Url.Handlers
{
    public class AddUrlHandler : IRequestHandler<AddUrlRequest, string>
    {
        private readonly IUrlRepository urlRepository;

        public AddUrlHandler(IUrlRepository urlRepository)
        {
            this.urlRepository = urlRepository;
        }

        public async Task<string> Handle(AddUrlRequest request, CancellationToken cancellationToken)
        {

            string pattern = @"^(https?:\/\/)?([а-я0-9_-]{1,32}|[a-z0-9_-]{1,32})\.([а-я0-9_-]{1,8}|[a-z0-9_-]\S{1,8})$";
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);

            if (!regex.IsMatch(request.Url))
                throw new Exception("Это не ссылка");

            var responce = await urlRepository.GetByLongUrl(request.Url);

            if (responce != null)
                return responce.ShortUrl;

            string shortUrl = Guid.NewGuid().ToString().Substring(0, 6);

            return await urlRepository.AddUrl(request.Url, shortUrl);

        }
    }
}
