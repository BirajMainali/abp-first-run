using System;
using System.Threading.Tasks;
using Acme.BookStore.Dto;
using Acme.BookStore.Entity;
using Acme.BookStore.Extensions;
using Acme.BookStore.Services;
using Acme.BookStore.ValueObject;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace Acme.BookStore;

public class ProductService : BookStoreAppService
{
    private readonly IRepository<Product, long> _productRepo;
    private readonly DomainProductService _domainProductService;

    public ProductService(IRepository<Product, long> productRepo, DomainProductService domainProductService)
    {
        _productRepo = productRepo;
        _domainProductService = domainProductService;
    }

    public async Task<HttpStatus> PostAddProduct(ProductDto dto)
    {
        try
        {
            var result = await _domainProductService.AddProductAsync(dto: dto);
            return this.ResolveResponse(result);
        }
        catch (Exception e)
        {
            throw new UserFriendlyException(e.Message);
        }
    }
}