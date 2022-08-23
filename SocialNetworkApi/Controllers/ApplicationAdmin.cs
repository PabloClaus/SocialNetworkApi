using Microsoft.AspNetCore.Mvc;
using SocialNetworkApi.Authorization;
using SocialNetworkApi.Services;

namespace SocialNetworkApi.Controllers;

[Authorize((int) ApplicationUserService.ApplicationRol.Admin)]
[ApiController]
[Route("[controller]/[action]")]
public class ApplicationAdminController : ControllerBase
{
    private readonly IApplicationUserService _applicationUserService;

    public ApplicationAdminController(IApplicationUserService applicationUserService)
    {
        _applicationUserService = applicationUserService;
    }

    /// <summary>
    /// Allows Admin user to delete an user's profile
    /// </summary>
    /// <param name="id"></param>
    /// <returns>An "DeleteAsync Ok" message</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /DeleteAsync
    ///      {
    ///         "id": 1
    ///      }
    /// </remarks>
    /// <response code="200">Deletes Ok</response>
    /// <response code="500">If there was a problem during the process</response>
    [HttpDelete]
    [ProducesResponseType(typeof(ProducesErrorResponseTypeAttribute), 400)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
           await _applicationUserService.DeleteAsync(id);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
        }
        return Ok("Delete Ok");
    }
}