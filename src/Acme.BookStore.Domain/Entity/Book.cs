using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.BookStore.Entity;

public class Book : FullAuditedEntity<long>
{
    public string Name { get; set; }
}