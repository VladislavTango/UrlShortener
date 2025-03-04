using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using URLShortener.Domain.Models;

namespace URLShortener.Infrastructure.AppContext.Mappers
{
    public class UrlMapping : ClassMapping<UrlEntity>
    {
        public UrlMapping()
        {
            Table("Urls");
            Id(x => x.Id, m =>
            {
                m.Generator(Generators.Identity);
            });
            Property(x => x.LongUrl, m =>
            {
                m.Unique(true);
                m.NotNullable(true);
            });
            Property(x => x.ShortUrl, m =>
            {
                m.Unique(true);
                m.NotNullable(true);
            });
            Property(x => x.CreateDate, m =>
            {
                m.NotNullable(true);
            });
            Property(x => x.RedirectCounter, m =>
            {
                m.NotNullable(true);
            });
        }
    }
}
