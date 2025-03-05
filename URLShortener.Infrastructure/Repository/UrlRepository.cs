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
        private readonly ISessionFactory _sessionFactory;

        public UrlRepository(ISession session, ISessionFactory sessionFactory)
        {
            _session = session;
            _sessionFactory = sessionFactory;
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

        public async Task<string> DeleteUrl(string url)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                var urlEntity = session.QueryOver<UrlEntity>()
                                       .Where(u => u.ShortUrl == url)
                                       .SingleOrDefault();

                if (urlEntity != null)
                {
                    using (var transaction = session.BeginTransaction())
                    {
                        session.Delete(urlEntity);
                        transaction.Commit();
                    }
                }
            }
            return url;
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
            using (var session = _sessionFactory.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                session.SaveOrUpdate(urlEntity);
                transaction.Commit();

            }
        }


        public async Task<UrlEntity> GetByShortUrl(string shortUrl)
        {
            return await _session.Query<UrlEntity>()
             .Where(u => u.ShortUrl == shortUrl)
             .FirstOrDefaultAsync();
        }
    }
}
