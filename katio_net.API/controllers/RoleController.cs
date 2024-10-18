using System;
using katio.Business.Interfaces;
using katio.Data.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace katio_net.API.Controllers;
[Route("Api/[controller]")]
[ApiController]


public class RoleController: ControllerBase
{
    private readonly IRoleService _roleService;
    public RoleController(IRoleService roleService){
        _roleService = roleService;
    }

    [HttpGet]
    [Route("GetAllRoles")]
    public async Task<IActionResult> Index()
    {
        var response = await _roleService.GetAllRoles();
        return response.TotalElements > 0 ? Ok(response): StatusCode(StatusCodes.Status204NoContent,response);

    }

    [HttpPost]
    [Route("CreateRole")]
    public async Task<IActionResult> Create(Role role)
    {
        var response = await _roleService.CreateRol(role);
        return response.statusCode == System.Net.HttpStatusCode.OK? Ok(response): StatusCode((int)response.statusCode, response);
    }

    [HttpDelete]
    [Route("DeleteRole")]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _roleService.DeleteRolesById(id);
        return response.statusCode == System.Net.HttpStatusCode.OK? Ok(response): StatusCode((int)response.statusCode, response);
    }


}    