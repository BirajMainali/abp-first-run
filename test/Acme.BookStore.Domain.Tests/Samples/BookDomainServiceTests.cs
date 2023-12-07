using System.Threading.Tasks;
using Acme.BookStore.Dto;
using Acme.BookStore.Entity;
using Acme.BookStore.Services;
using NSubstitute;
using Shouldly;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;
using Xunit;

namespace Acme.BookStore.Samples;

public class BookDomainServiceTests : BookStoreDomainTestBase
{
    private readonly BookDomainService _bookDomainService;
    private readonly IUnitOfWorkManager _unitOfWorkManager = Substitute.For<IUnitOfWorkManager>();
    private readonly IRepository<Book, long> _bookRepo = Substitute.For<IRepository<Book, long>>();

    public BookDomainServiceTests()
    {
        _bookDomainService = new BookDomainService(uow: _unitOfWorkManager, bookRepo: _bookRepo);
    }

    [Fact]
    public async Task test_record_book_records_book()
    {
        var dto = PrepareBookDto();
        var result = await _bookDomainService.RecordBookAsync(dto);
        result.IsSuccess.ShouldBeTrue();
        result.Value.Name.ShouldMatch(dto.Name);
    }

    [Fact]
    public async Task test_empty_name_returns_false_result()
    {
        var dto = PrepareBookDto(shouldReturnEmptyName: true);
        var result = await _bookDomainService.RecordBookAsync(dto);
        result.IsSuccess.ShouldBeFalse();
    }

    private static BookDto PrepareBookDto(bool shouldReturnEmptyName = false)
    {
        return new BookDto
        {
            Name = shouldReturnEmptyName ? string.Empty : "To Kill a Mockingbird"
        };
    }
}