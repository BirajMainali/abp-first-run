using System;
using System.Threading.Tasks;
using Acme.BookStore.Dto;
using Acme.BookStore.Entity;
using Acme.BookStore.UseCase;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;

namespace Acme.BookStore.Services;

public abstract class DomainProductService : IScopedDependency
{
    private readonly IUnitOfWorkManager _uow;
    private readonly IRepository<Product, long> _productRepo;

    protected DomainProductService(IUnitOfWorkManager uow, IRepository<Product, long> productRepo)
    {
        _uow = uow;
        _productRepo = productRepo;
    }

    public async Task<Result<Product>> AddProductAsync(ProductDto dto)
    {
        using var uow = _uow.Begin();
        if (dto.Name.IsNullOrEmpty())
        {
            return Result<Product>.Failure("Product name is not specified")!;
        }

        var product = new Product
        {
            Name = dto.Name
        };
        await _productRepo.InsertAsync(product);
        await uow.SaveChangesAsync();
        await uow.CompleteAsync();
        return Result<Product>.Success(product)!;
    }
}