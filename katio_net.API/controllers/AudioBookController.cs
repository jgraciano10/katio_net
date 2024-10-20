using System;
using katio.Business.Interfaces;
using katio.Data.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace katio_net.API.Controllers;
[Route("Api/[controller]")]
[ApiController]


public class AudioBookController: ControllerBase
{
    private readonly IAudioBookService _audioBookService;
    public AudioBookController(IAudioBookService audioBookService){
       _audioBookService = audioBookService;
    }
    [HttpGet]
    [Route("GetAllAudioBooks")]
    public async Task<IActionResult> Index()
    {
        var response = await _audioBookService.GetAllAudioBooks();
        return response.TotalElements > 0 ? Ok(response): StatusCode(StatusCodes.Status204NoContent,response);

    }

    [HttpGet]
    [Route("GetAudioBookById")]
    public async Task<IActionResult> GetAudioBookById(int id)
    {
        var response = await _audioBookService.GetById(id);
        return response.TotalElements > 0 ? Ok(response): StatusCode(StatusCodes.Status204NoContent,response);
    }

    [HttpGet]
    [Route("GetAudioBookByName")]
    public async Task<IActionResult> GetAudioBookByName(string name)
    {
        var response = await _audioBookService.GetByName(name);
        return response.TotalElements > 0 ? Ok(response): StatusCode(StatusCodes.Status204NoContent,response);
    }

    [HttpPost]
    [Route("CreateAudioBook")]
    public async Task<IActionResult> CreateAudioBook(AudioBooks audioBook)
    {
        var response = await _audioBookService.CreateAudioBook(audioBook);
        return response.TotalElements > 0 ? Ok(response): StatusCode(StatusCodes.Status204NoContent,response);
    }

    [HttpPost]
    [Route("UpdateAudioBook")]
    public async Task<IActionResult> UpdateAudioBook(AudioBooks audioBook)
    {
        var response = await _audioBookService.Update(audioBook);
        return response.TotalElements > 0 ? Ok(response): StatusCode(StatusCodes.Status204NoContent,response);
    }

    [HttpDelete]
    [Route("DeleteAudioBook")]
    public async Task<IActionResult> DeleteAudioBook(AudioBooks audioBook)
    {
        var response = await _audioBookService.DeleteAudioBook(audioBook);
        return response.TotalElements > 0 ? Ok(response): StatusCode(StatusCodes.Status204NoContent,response);
    }
}