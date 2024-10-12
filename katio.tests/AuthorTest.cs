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
public class AuthorTest
{
    private readonly IRepository<int, Author> _authorRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuthorService _authorService;
    private Author newAuthor = new Author
        {
           Name = "Gabriel",
           LastName = "Garcia",
           Country = "Colombia",
           BirthDate = new DateOnly(1998,10,19)
        };

    public AuthorTest ()
    {
        _authorRepository = Substitute.For<IRepository<int, Author>>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _authorService = new AuthorService(_unitOfWork);
        
    }
    [TestMethod]

    public async Task GetAllAuthorsSuccess()
    {
        _authorRepository.GetAllAsync().ReturnsForAnyArgs(Task.FromResult<List<Author>>(new List<Author>(){newAuthor}));
        _unitOfWork.AuthorRepository.Returns(_authorRepository);
        var result = await _authorService.GetAllAuthors();
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result.ResponseElements, typeof(List<Author>));
        Assert.AreEqual(HttpStatusCode.OK, result.statusCode);
        
    }
    
    [TestMethod]
     public async Task GetAllAuthorsFailed()
    {
        _authorRepository.GetAllAsync().ReturnsForAnyArgs(Task.FromResult<List<Author>>(new List<Author>()));
        _unitOfWork.AuthorRepository.Returns(_authorRepository);
        var result = await _authorService.GetAllAuthors();
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result.ResponseElements, typeof(List<Author>));
        Assert.AreEqual(HttpStatusCode.NotFound, result.statusCode);
        
    }
    [TestMethod]
    public async Task CreateAuthorSuccess()
    {

        _authorRepository.AddAsync(newAuthor).ReturnsForAnyArgs(Task.CompletedTask);
        _unitOfWork.AuthorRepository.Returns(_authorRepository);
        var result = await _authorService.CreateAuthor(newAuthor);
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result.ResponseElements, typeof(List<Author>));
        Assert.AreEqual(HttpStatusCode.OK, result.statusCode);
    }

    [TestMethod]
    public async Task CreateAuthorFailed()
    {

        _authorRepository.AddAsync(newAuthor).ThrowsForAnyArgs(new Exception());
        _unitOfWork.AuthorRepository.Returns(_authorRepository);
        var result = await _authorService.CreateAuthor(newAuthor);
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result.ResponseElements, typeof(List<Author>));
        Assert.AreEqual(HttpStatusCode.InternalServerError, result.statusCode);
    }

}
