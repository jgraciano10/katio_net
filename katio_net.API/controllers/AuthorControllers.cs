using System;
using katio.Business.Interfaces;
using katio.Data.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace katio_net.API.Controllers;
[Route("Api/[controller]")]
[ApiController]


public class AuthorController: ControllerBase
{
    private readonly IAuthorService _autorService;
    public AuthorController(IAuthorService authorService){
        _autorService = authorService;
    }
    [HttpGet]
    [Route("GetAllAuthors")]
    public async Task<IActionResult> Index()
    {
        var response = await _autorService.GetAllAuthors();
        return response.TotalElements > 0 ? Ok(response): StatusCode(StatusCodes.Status204NoContent,response);

    }

}