using System;
using katio.Business.Interfaces;
using katio.Data.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace katio_net.API.Controllers;
[Route("Api/[controller]")]
[ApiController]


public class NarratorController: ControllerBase
{
    private readonly INarratorService _narratorService;
    public NarratorController(INarratorService narratorService){
        _narratorService = narratorService;
    }
    [HttpGet]
    [Route("GetAllNarrators")]
    public async Task<IActionResult> Index()
    {
        var response = await _narratorService.GetAllNarrators();
        return response.TotalElements > 0 ? Ok(response): StatusCode(StatusCodes.Status204NoContent,response);

    }

    [HttpPost]
    [Route("CreateNarrator")]
    public async Task<IActionResult> Create(Narrator narrator)
    {
        var response = await _narratorService.CreateNarrator(narrator);
        return response.statusCode == System.Net.HttpStatusCode.OK? Ok(response): StatusCode((int)response.statusCode, response);
    }

     [HttpDelete]
    [Route("DeleteNarrator")]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _narratorService.DeleteNarratorById(id);
        return response.statusCode == System.Net.HttpStatusCode.OK? Ok(response): StatusCode((int)response.statusCode, response);
    }

}