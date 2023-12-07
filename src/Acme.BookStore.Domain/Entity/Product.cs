using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.BookStore.Entity;

public class Product : FullAuditedEntity<long>
{
    public string Name { get; set; }
}