using URLShortener.Domain.DTO;

namespace URLShortener.Application.Url.Reponse
{
    public class GetUrlsResponse
    {
        public IEnumerable<UrlsDTO> Urls { get; set; }
        public int PageCount { get; set; }
    }
}
