using _.Contracts;
using _.WebAPI.Core.IConfiguration;
using Microsoft.AspNetCore.Mvc;

namespace _.WebAPI.Controllers;

[ApiController]
[Route("api/v1/users")]
public class UsersController : ControllerBase
{
    private readonly ILogger<UsersController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public UsersController(ILogger<UsersController> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    [HttpPost("register")]
    public async Task<IActionResult> CreateUser(RegisterRequest request)
    {
        var errors = new List<string>();
        try
        {
            /* I realise I could have delegated some things to the repository to have cleaner code here
             * I will do that on v2 and in future versions, doing that here will break things
             * TODO: Delegate these procedures to repositories
             * Also, a lot of optimisations need to be made and the logic cleaned up, maybe by failing early to avoid all these if statements
             * 
             * <summary>
             *  This functions attempts to create a user if he/she doesn't exist and the post data is valid
             * </summary>
             * <parameters>
             *  RegisterRequest request: This is a record defined in the _.Contracts folder, I used it to get the required data for registration
             * </parameters>
             */
            if (request.FirstName == string.Empty)
                errors.Add("First name is required");
            if (request.LastName == string.Empty)
                errors.Add("Last name is required");
            if (request.NationalUserId <= 0 || request.NationalUserId.ToString().Length != 20)
                errors.Add("Error: National Id is required");

            if (errors.Any())
                return new JsonResult(errors) {StatusCode = 401};

            var checkIfUserExists = await _unitOfWork.Users.GetUserByNationalId(request.NationalUserId);
            await _unitOfWork.CompleteAsync();

            if (checkIfUserExists != null)
                return new JsonResult("User with that National Id already exists") { StatusCode = 400 };
            

            var user = await _unitOfWork.Users.RegisterUser(request);
            await _unitOfWork.CompleteAsync();

            if (!user) return new JsonResult("User registration failed") { StatusCode = 500 };

            var storedUser = await _unitOfWork.Users.GetUserByNationalId(request.NationalUserId);
            await _unitOfWork.CompleteAsync();

            if (storedUser == null)
                return new JsonResult("Database integrity was violated and user was not created properly")
                    { StatusCode = 400 };

            return Ok(request);
        }
        catch (Exception e)
        {
            var enumerable = errors.Append(e.ToString());
            return new JsonResult(enumerable.ToString()){StatusCode = 500};
        }
    }

    [HttpGet("/user/{id:int}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        /* <summary>
         *  This function retrieves a user using their User Id
         * </summary>
         * 
         * Future me: Note that this only returns the monetary info of the user. Other info needs to be extracted from the other table holding the user data
         *
         * <parameters>
         *  int id: This is an integer value uniquely identifying the user in the User table
         *</parameters>
         */
        var user = await _unitOfWork.Users.GetUserById(id);

        if (user == null)
            return NotFound();

        return Ok(user);
    }
}