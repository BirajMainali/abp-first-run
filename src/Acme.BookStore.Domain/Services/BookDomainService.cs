using System;
using System.Threading.Tasks;
using Acme.BookStore.Dto;
using Acme.BookStore.Entity;
using Acme.BookStore.UseCase;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;

namespace Acme.BookStore.Services;

public class BookDomainService : IScopedDependency
{
    private readonly IUnitOfWorkManager _uow;
    private readonly IRepository<Book, long> _productRepo;

    public BookDomainService(IUnitOfWorkManager uow, IRepository<Book, long> productRepo)
    {
        _uow = uow;
        _productRepo = productRepo;
    }

    public async Task<Result<Book>> RecordBookAsync(BookDto dto)
    {
        using var uow = _uow.Begin();
        if (dto.Name.IsNullOrEmpty())
        {
            return Result<Book>.Failure("Product name is not specified")!;
        }

        var product = new Book
        {
            Name = dto.Name
        };
        await _productRepo.InsertAsync(product);
        await uow.SaveChangesAsync();
        await uow.CompleteAsync();
        return Result<Book>.Success(product)!;
    }
}