using URLShortener.Domain.DTO;
using URLShortener.Domain.Models;

namespace URLShortener.Domain.Interfaces
{
    public interface IUrlRepository
    {
        public Task<UrlEntity> GetByLongUrl(string longUrl);
        public Task<UrlEntity> GetByShortUrl(string shortUrl);
        public Task<string> AddUrl(string longUrl , string shortUrl);
        public Task<string> DeleteUrl(string url);
        public Task UpdateUrl(UrlEntity urlEntity);
        public Task<IEnumerable<UrlsDTO>> GetList(int pageNumber, int count = 20);
        public Task<UrlEntity> GetById(int id);
        public Task<int> GetLen();


    }
}
