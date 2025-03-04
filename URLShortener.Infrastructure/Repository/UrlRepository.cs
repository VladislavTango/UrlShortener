using NHibernate;
using NHibernate.Linq;
using URLShortener.Domain.DTO;
using URLShortener.Domain.Interfaces;
using URLShortener.Domain.Models;

namespace URLShortener.Infrastructure.Repository
{
    public class UrlRepository : IUrlRepository
    {
        private readonly ISession _session;

        public UrlRepository(ISession session)
        {
            _session = session;
        }

        public async Task<string> AddUrl(string longUrl, string shortUrl)
        {
            using var transaction = _session.BeginTransaction();
            var Entity = new UrlEntity
            {
                LongUrl = longUrl,
                ShortUrl = shortUrl,
                CreateDate = DateTime.Now,
                RedirectCounter = 0,
            };

            await _session.SaveAsync(Entity);
            await transaction.CommitAsync();

            return shortUrl;
        }

        public async Task<int> DeleteUrl(int id)
        {
            using var transaction = _session.BeginTransaction();

            var entity = GetById(id);

            await _session.DeleteAsync(entity); 
            await transaction.CommitAsync(); 

            return id;
        }

        public async Task<UrlEntity> GetByLongUrl(string longUrl)
        {
            return await _session.Query<UrlEntity>()
                     .Where(u => u.LongUrl == longUrl)
                     .FirstOrDefaultAsync();
        }
        
        public async Task<UrlEntity> GetById(int id)
        {
            return await _session.Query<UrlEntity>()
                     .Where(u => u.Id == id)
                     .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<UrlsDTO>> GetList(int pageNumber, int count = 20)
        {
            var query = _session.Query<UrlEntity>();

            var result = await query
                .Skip((pageNumber - 1) * count) 
                .Take(count) 
                .Select(x => new UrlsDTO
                {
                    LongUrl = x.LongUrl,
                    ShortUrl = x.ShortUrl,
                    CreateDate = x.CreateDate,
                    RedirectCounter = x.RedirectCounter
                })
                .ToListAsync();

            return result;
        }
        
        public async Task<int> GetLen() 
        {
            return await _session.Query<UrlEntity>().CountAsync();
        }

        public async Task UpdateUrl(UrlEntity urlEntity)
        {
            using var transaction = _session.BeginTransaction();
            var entity = await GetById(urlEntity.Id);
            entity = urlEntity; 

            await _session.UpdateAsync(entity); 
            await transaction.CommitAsync(); 
        }

        public async Task<UrlEntity> GetByShortUrl(string shortUrl)
        {
            return await _session.Query<UrlEntity>()
             .Where(u => u.ShortUrl == shortUrl)
             .FirstOrDefaultAsync();
        }
    }
}
