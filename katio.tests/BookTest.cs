using System.Net;
using katio.Business.Interfaces;
using katio.Business.Services;
using katio.Data.Models;
using Katio.Data;
using katio_net.Data;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace katio.tests;
[TestClass]
public class BookTest
{
    private readonly IRepository<int, Book> _bookRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBookService _bookService;

    private Book newBook = new Book
    {
        Title = "Cien Años",
        ISBN10 = "458423622",
        ISBN13 = "4541162262",
        Published = new DateTime(),
        Edition = "Exclusivo",
        DeweyIndex = "616",
        GenresId = 1,
        AuthorId = 1
    };

    public BookTest ()
    {
        _bookRepository = Substitute.For<IRepository<int, Book>>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _bookService = new BookService(_unitOfWork);
        
    }

    [TestMethod]

    public async Task GetAllBooksSuccess()
    {
        _bookRepository.GetAllAsync().ReturnsForAnyArgs(Task.FromResult<List<Book>>(new List<Book>(){newBook}));
        _unitOfWork.BookRepository.Returns(_bookRepository);
        var result = await _bookService.GetAllBooks();
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result.ResponseElements, typeof(List<Book>));
        Assert.AreEqual(HttpStatusCode.OK, result.statusCode);
        
    }

    [TestMethod]

    public async Task GetAllBooksFailed()
    {
        _bookRepository.GetAllAsync().ReturnsForAnyArgs(Task.FromResult<List<Book>>(new List<Book>()));
        _unitOfWork.BookRepository.Returns(_bookRepository);
        var result = await _bookService.GetAllBooks();
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result.ResponseElements, typeof(List<Book>));
        Assert.AreEqual(HttpStatusCode.NotFound, result.statusCode);
        
    }


    [TestMethod]

    public async Task CreateBookSuccess()
    {
        _bookRepository.AddAsync(newBook).ReturnsForAnyArgs(Task.CompletedTask);
        _unitOfWork.BookRepository.Returns(_bookRepository);
        var result = await _bookService.CreateBook(newBook);
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result.ResponseElements, typeof(List<Book>));
        Assert.AreEqual(HttpStatusCode.OK, result.statusCode);
        
    }

    [TestMethod]

    public async Task CreateBookFailed()
    {
        _bookRepository.AddAsync(newBook).ThrowsAsyncForAnyArgs(new Exception());
        _unitOfWork.BookRepository.Returns(_bookRepository);
        var result = await _bookService.CreateBook(newBook);
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result.ResponseElements, typeof(List<Book>));
        Assert.AreEqual(HttpStatusCode.InternalServerError, result.statusCode);
        
    } 

    [TestMethod]

    public async Task GetByNameSuccess()
    {
        _bookRepository.GetAllAsync(x => x.Title.Contains("Cien Años", StringComparison.InvariantCultureIgnoreCase)).ReturnsForAnyArgs(Task.FromResult<List<Book>>(new List<Book>(){newBook}));
        _unitOfWork.BookRepository.Returns(_bookRepository);
        var result = await _bookService.GetByName("Cien Años");
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result.ResponseElements, typeof(List<Book>));
        Assert.AreEqual(HttpStatusCode.OK, result.statusCode);
        Assert.IsTrue(result.TotalElements>0);
        
    }    

    [TestMethod]

    public async Task GetByNameFailed()

    {
        _bookRepository.GetAllAsync(x => x.Title.Contains("Cien Años", StringComparison.InvariantCultureIgnoreCase)).ReturnsForAnyArgs(Task.FromResult<List<Book>>(new List<Book>()));
        _unitOfWork.BookRepository.Returns(_bookRepository);
        var result = await _bookService.GetByName("Cien Años");
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result.ResponseElements, typeof(List<Book>));
        Assert.AreEqual(HttpStatusCode.NotFound, result.statusCode);
        Assert.IsTrue(result.TotalElements==0);
        
    }    

    [TestMethod]

    public async Task UpdateSuccess()
    {
        _bookRepository.Update(newBook).ReturnsForAnyArgs(Task.CompletedTask);
        _unitOfWork.BookRepository.Returns(_bookRepository);
        var result = await _bookService.Update(newBook);
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result.ResponseElements, typeof(List<Book>));
        Assert.AreEqual(HttpStatusCode.OK, result.statusCode);
        Assert.IsTrue(result.TotalElements>0);
        
    }

    [TestMethod]

    public async Task UpdateFailed()
    {
        _bookRepository.Update(newBook).ThrowsAsyncForAnyArgs(new Exception());
        _unitOfWork.BookRepository.Returns(_bookRepository);
        var result = await _bookService.Update(newBook);
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result.ResponseElements, typeof(List<Book>));
        Assert.AreEqual(HttpStatusCode.NotFound, result.statusCode);
        Assert.IsTrue(result.TotalElements==0);
        
    } 
    
    [TestMethod]

    public async Task GetByAuthorIdSuccess()
    {
        _bookRepository.GetAllAsync(x=>x.AuthorId ==1).ReturnsForAnyArgs(Task.FromResult<List<Book>>(new List<Book>(){newBook}));
        _unitOfWork.BookRepository.Returns(_bookRepository);
        var result = await _bookService.GetByAuthorId(1);
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result.ResponseElements, typeof(List<Book>));
        Assert.AreEqual(HttpStatusCode.OK, result.statusCode);
        Assert.IsTrue(result.TotalElements>0);
    }

    [TestMethod]
    public async Task GetByAuthorIdFailed()
    {
        _bookRepository.GetAllAsync(x=>x.AuthorId ==1).ReturnsForAnyArgs(Task.FromResult<List<Book>>(new List<Book>()));
        _unitOfWork.BookRepository.Returns(_bookRepository);
        var result = await _bookService.GetByAuthorId(1);
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result.ResponseElements, typeof(List<Book>));
        Assert.AreEqual(HttpStatusCode.NotFound, result.statusCode);
        Assert.IsTrue(result.TotalElements==0);
    }

    [TestMethod]
    public async Task GetByAuthorNameSuccess()
    {
        _bookRepository.GetAllAsync(x => x.Author.Name.Contains("Gabriel", StringComparison.InvariantCultureIgnoreCase)).ReturnsForAnyArgs(Task.FromResult<List<Book>>(new List<Book>(){newBook}));
        _unitOfWork.BookRepository.Returns(_bookRepository);
        var result = await _bookService.GetByAuthorName("Gabriel");
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result.ResponseElements, typeof(List<Book>));
        Assert.AreEqual(HttpStatusCode.OK, result.statusCode);
        Assert.IsTrue(result.TotalElements>0);
    }

    [TestMethod]
    public async Task GetByAuthorNameFailed()
    {
        _bookRepository.GetAllAsync(x => x.Author.Name.Contains("Gabriel", StringComparison.InvariantCultureIgnoreCase)).ReturnsForAnyArgs(Task.FromResult<List<Book>>(new List<Book>()));
        _unitOfWork.BookRepository.Returns(_bookRepository);
        var result = await _bookService.GetByAuthorName("Gabriel");
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result.ResponseElements, typeof(List<Book>));
        Assert.AreEqual(HttpStatusCode.NotFound, result.statusCode);
        Assert.IsTrue(result.TotalElements==0);
    }
}    