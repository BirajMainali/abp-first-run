using System.Linq;
using System.Threading.Tasks;
using Acme.BookStore.Dto;
using Acme.BookStore.Entity;
using Acme.BookStore.Extensions;
using Acme.BookStore.Services;
using Acme.BookStore.ValueObject;
using Volo.Abp.Domain.Repositories;

namespace Acme.BookStore;

public class BookService : BookStoreAppService
{
    private readonly IRepository<Book, long> _bookRepo;
    private readonly BookDomainService _bookDomainService;

    public BookService(IRepository<Book, long> bookRepo, BookDomainService bookDomainService)
    {
        _bookRepo = bookRepo;
        _bookDomainService = bookDomainService;
    }

    public async Task<HttpStatus> PostAddBook(BookDto dto)
    {
        var result = await _bookDomainService.RecordBookAsync(dto: dto);
        return this.ResolveResponse(result);
    }

    public async Task<HttpStatus> GetBooks(string query)
    {
        var items = (await _bookRepo.GetQueryableAsync()).Where(x => query.Contains(x.Name)).ToList();
        return this.ResolveResponse(items);
    }
}