using Microsoft.AspNetCore.Mvc;
using SocialNetworkApi.Authorization;
using SocialNetworkApi.DTO.GET.GetUser;
using SocialNetworkApi.DTO.POST.Authentication;
using SocialNetworkApi.Services;
using ApplicationUser = SocialNetworkApi.DTO.POST.Registration.ApplicationUser;

namespace SocialNetworkApi.Controllers;

[Authorize((int) ApplicationUserService.ApplicationRol.User, (int) ApplicationUserService.ApplicationRol.Admin)]
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
    public ActionResult Authenticate(AuthenticationRequest request)
    {
        try
        {
            var response = _applicationUserService.Authenticate(request);
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
    public IActionResult Register(ApplicationUser user)
    {
        try
        {
            _applicationUserService.Register(user);
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
    /// <param name="rolName">The rol name to search for (Optional)</param>
    /// <response code="200">Ok</response>
    /// <response code="500">If there was a problem during the process</response>
    [HttpGet]
    [ProducesResponseType(typeof(ProducesErrorResponseTypeAttribute), 400)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<List<DTO.GET.GetUsers.ApplicationUser>> GetAll(string? gender = null, string? rolName = null)
    {
        try
        {
            return _applicationUserService.GetAll(gender, rolName).ToList();
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
    public ActionResult<DTO.GET.GetUser.ApplicationUser> Get()
    {
        try
        {
            var contextUserIDs = (UserIDs) _httpContextAccessor!.HttpContext!.Items["UserIDs"]!;
            return _applicationUserService.GetById(contextUserIDs.Id)!;
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
    public IActionResult Update(DTO.PUT.UpdateApplicationUser.ApplicationUser user)
    {
        try
        {
            var contextUserIDs = (UserIDs) _httpContextAccessor!.HttpContext!.Items["UserIDs"]!;
            _applicationUserService.Update(contextUserIDs.Id, user);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
        }

        return Ok(new {message = "Update Ok"});
    }

    #endregion
}