using System;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace katio_net.API.Controllers;
[Route("Api/[controller]")]
[ApiController]


public class BooksController
{
    [HttpGet]
    public string Index()
    {
        return "Hola soy frilejon Perez";

    }
}