using Microsoft.AspNetCore.Mvc;
using SocialNetworkApi.Authorization;
using SocialNetworkApi.Common.DTO.GET.GetUser;
using SocialNetworkApi.Services;

namespace SocialNetworkApi.Controllers;

[Authorize((int) ApplicationUserService.ApplicationRole.User, (int) ApplicationUserService.ApplicationRole.Admin)]
[ApiController]
[Route("[controller]/[action]")]
public class ApplicationUserController : ControllerBase
{
    private readonly IApplicationUserService _applicationUserService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ApplicationUserController(IApplicationUserService applicationUserService,
        IHttpContextAccessor httpContextAccessor)
    {
        _applicationUserService = applicationUserService;
        _httpContextAccessor = httpContextAccessor;
    }

    /// <summary>
    ///     Allows an user to authenticate
    /// </summary>
    /// <param name="request"></param>
    /// <returns>An object with the Authentication token to be used in the functions that require authentication</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /Authenticate
    ///      {
    ///       "email": "something@something.com",
    ///       "password": "aPassword"
    ///      }
    /// </remarks>
    /// <response code="200">Authentication Ok</response>
    /// <response code="500">If there was a problem during the process</response>

    #region AllowAnonymous

    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(typeof(ProducesErrorResponseTypeAttribute), 400)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult Authenticate(Common.DTO.POST.Authentication.AuthenticationRequest request)
    {
        try
        {
            var response = _applicationUserService.AuthenticateAsync(request);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
        }
    }

    /// <summary>
    /// Allows an user to authenticate
    /// </summary>
    /// <param name="user"></param>
    /// <returns>A "Registration successful" message</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /Register
    ///     {
    ///         "firstName": "firstName",
    ///         "lastName": "lastName",
    ///         "email": "something@something.com",
    ///         "password": "aPassword",
    ///         "birthday": "yyyy-MM-dd",
    ///         "gender": "[MASC|FEM]"
    ///     }
    ///
    /// Note: Birthday and Gender ara optional. In case you do not want to complete any of them, delete it/them from the
    /// request, like this:
    ///
    ///     POST /Register
    ///     {
    ///         "firstName": "firstName",
    ///         "lastName": "lastName",
    ///         "email": "something@something.com",
    ///         "password": "aPassword"
    ///     }
    ///
    ///     POST /Register
    ///     {
    ///         "firstName": "firstName",
    ///         "lastName": "lastName",
    ///         "email": "something@something.com",
    ///         "password": "aPassword",
    ///         "birthday": "yyyy-MM-dd"
    ///     }
    ///
    ///     POST /Register
    ///     {
    ///         "firstName": "firstName",
    ///         "lastName": "lastName",
    ///         "email": "something@something.com",
    ///         "password": "aPassword",
    ///         "gender": "[MASC|FEM]"
    ///     }
    /// </remarks>
    /// <response code="200">Registration successful</response>
    /// <response code="500">If there was a problem during the process</response>
    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(typeof(ProducesErrorResponseTypeAttribute), 400)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Register(Common.DTO.POST.Registration.ApplicationUser user)
    {
        try
        {
            await _applicationUserService.RegisterAsync(user);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
        }

        return Ok(new {message = "Registration successful"});
    }

    #endregion

    #region Authorize

    /// <summary>
    /// Get the list of the current users of the application.
    /// Optional filters can be used (If more than one is used, "and" condition is applied).
    /// </summary>
    /// <returns>A list of users of the application</returns>
    /// <param name="gender">The gender name to search for (Optional)</param>
    /// <param name="roleName">The RoleName to search for (Optional)</param>
    /// <response code="200">Ok</response>
    /// <response code="500">If there was a problem during the process</response>
    [HttpGet]
    [ProducesResponseType(typeof(ProducesErrorResponseTypeAttribute), 400)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<Common.DTO.GET.GetUsers.ApplicationUser>>> GetAll(string? gender = null, string? roleName = null)
    {
        try
        {
            return Ok(await _applicationUserService.GetAllAsync(gender, roleName));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
        }
    }

    /// <summary>
    /// Get the profile of the current authenticated user
    /// </summary>
    /// <returns>The User's profile</returns>
    /// <response code="200">Ok</response>
    /// <response code="500">If there was a problem during the process</response>
    [HttpGet]
    [ProducesResponseType(typeof(ProducesErrorResponseTypeAttribute), 400)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Common.DTO.GET.GetUser.ApplicationUser>> Get()
    {
        try
        {
            var contextUserIDs = (UserIDs) _httpContextAccessor!.HttpContext!.Items["UserIDs"]!;
            return await _applicationUserService.GetByIdAsync(contextUserIDs.Id);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
        }
    }

    /// <summary>
    /// Allows an authenticated user to modify his or her profile
    /// </summary>
    /// <param name="user"></param>
    /// <returns>An "Update Ok" message</returns>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /Update
    ///     {
    ///     "firstName": "firstName",
    ///     "lastName": "lastName",
    ///     "password": "aPassword",
    ///     "birthday": "yyyy-MM-dd",
    ///     "gender": "[MASC|FEM]"
    ///     }
    /// 
    /// Note: Fields ara optional. In case you do not want to complete any of them, delete it/them from the
    /// request, like this:
    ///
    ///     POST /Register
    ///     {
    ///         "firstName": "firstName",
    ///         "lastName": "lastName",
    ///     }
    ///
    ///     POST /Update
    ///     {
    ///         "birthday": "yyyy-MM-dd",
    ///         "gender": "[MASC|FEM]"
    ///     }
    /// 
    ///     POST /Update
    ///     {
    ///         "firstName": "firstName",
    ///         "password": "aPassword",
    ///     }
    /// </remarks>
    /// <response code="200">Update successful</response>
    /// <response code="500">If there was a problem during the process</response>
    [HttpPut]
    [ProducesResponseType(typeof(ProducesErrorResponseTypeAttribute), 400)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(Common.DTO.PUT.UpdateApplicationUser.ApplicationUser user)
    {
        try
        {
            var contextUserIDs = (UserIDs) _httpContextAccessor!.HttpContext!.Items["UserIDs"]!;
            await _applicationUserService.UpdateAsync(contextUserIDs.Id, user);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
        }

        return Ok(new {message = "Update Ok"});
    }

    #endregion
}