using LibraryMSv3.Models.DatabaseModels;
using LibraryMSv3.Models.DTO;
using LibraryMSv3.Repositories.Interfaces;
using LibraryMSv3.Services;
using LibraryMSv3.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Image = LibraryMSv3.Models.DatabaseModels.Image;

namespace LibraryMSv3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserInfoService _userInfoService;
        private readonly IImageRepository _imageRepository;

        public UserController(IUserService userService, IUserInfoService userInfoService, IImageRepository imageRepository)
        {
            _userService = userService;
            _userInfoService = userInfoService;
            _imageRepository = imageRepository;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "user,admin")]
        [HttpPost("/user/details")]
        public async Task<ActionResult<string>> PostUserDetails(AddInfoDto addInfoDto)
        {
            var userName = User.Identity?.Name;
            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Guid userId = new Guid(userIdStr);
            var userDetails = await _userService.CreateUserDetails(userId, addInfoDto);
            if (userDetails != null)
            {
                return Ok($"User {userName} details was added");
            }
            return BadRequest("Can not user overwrite user details, please use update");
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "user,admin")]
        [HttpPost("/user/address")]
        public async Task<ActionResult<string>> AddUserAddress(AddAddressDto addAddressDto)
        {
            var userName = User.Identity?.Name;
            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Guid userId = new Guid(userIdStr);
            var userAdressAdded = await _userService.CreateUserAddress(userId, addAddressDto);
            if (userAdressAdded != null)
            {
                return Ok($"User {userName} Address was created");
            }
            return BadRequest($"User {userName} Address is already created, use update methods");
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "user,admin")]
        [HttpPost("/upload/user/photo")]
        public async Task<ActionResult<string>> UploadUserPhoto([FromForm] ImageUploadRequest imageUploadRequest)
        {
            var userName = User.Identity?.Name;
            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Guid userId = new Guid(userIdStr);
            var user = await _userService.GetUserByID(userId);
            if (user != null)
            {
                using var memoryStream = new MemoryStream();
                {
                    await imageUploadRequest.Image.CopyToAsync(memoryStream);
                    var imageBytes = memoryStream.ToArray();
                   // var reshapedImage = await _imageReshapeService.ResizeImage(imageBytes);
                    var userPhotoUploaded = await _userService.UploadUserPhoto(userId, imageBytes);
                    if (userPhotoUploaded != null)
                    {
                        return Ok($"User {userName} photo was uploaded");
                    }
                    return BadRequest($"User {userName} photo can not be overrided, please delete photo first");
                }
            }
            return NotFound("Such user does not exists");
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "user,admin")]
        [HttpPut("/user/firstName/")]
        public async Task<ActionResult<string>> UpdateUserFirstName([Required][StringLength(30)] string newFirstName)
        {
            if (ModelState.IsValid)
            {
                var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                Guid userId = new Guid(userIdStr);
                var userInfoDto = await _userService.UpdateUserFirstName(userId, newFirstName);
                if (userInfoDto != null)
                {
                    return Ok($"User '{userInfoDto.FirstName}' first name was changed to '{newFirstName}'");
                }
                return NotFound("Such user does not exists");
            }
            return BadRequest("Please check input parameters");
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "user,admin")]
        [HttpPut("/user/LastName/")]
        public async Task<ActionResult<string>> UpdateUserLastName([Required][StringLength(30)] string newLastName)
        {
            if (ModelState.IsValid)
            {
                var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                Guid userId = new Guid(userIdStr);
                var userInfoDto = await _userService.UpdateUserLastName(userId, newLastName);
                if (userInfoDto != null)
                {
                    return Ok($"User '{userInfoDto.FirstName}' last name was changed to '{newLastName}'");
                }
                return NotFound("Such user does not exists");
            }
            return BadRequest("Please check input parameters");
        }
        
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "user,admin")]
        [HttpPut("/user/personal/code")]
        public async Task<ActionResult<string>> UpdatePersonalCode([Required] long newPersonalCode)
        {
            if (ModelState.IsValid)
            {
                var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                Guid userId = new Guid(userIdStr);
                var userInfoDto = await _userService.UpdatePersonalCode(userId, newPersonalCode);
                if (userInfoDto != null)
                {
                    return Ok($"User '{userInfoDto.FirstName}' personal code was changed to '{newPersonalCode}'");
                }
                return NotFound("Such user does not exists");
            }
            return BadRequest("Please check input parameters");
        }
        
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "user,admin")]
        [HttpPut("/user/phonenumber")]
        public async Task<ActionResult<string>> UpdatePhoneNumber([Required][StringLength(12)] string newPhoneNumber)
        {
            if (ModelState.IsValid)
            {
                var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                Guid userId = new Guid(userIdStr);
                var userInfoDto = await _userService.UpdatePhoneNumber(userId, newPhoneNumber);
                if (userInfoDto != null)
                {
                    return Ok($"User '{userInfoDto.FirstName}' personal phone number was changed to '{newPhoneNumber}'");
                }
                return NotFound("Such user does not exists");
            }
            return BadRequest("Please check input parameters");
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "user,admin")]
        [HttpPut("/user/email")]
        public async Task<ActionResult<string>> UpdateEmail([Required][StringLength(50)] string newEmail)
        {
            if (ModelState.IsValid)
            {
                var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                Guid userId = new Guid(userIdStr);
                var userInfoDto = await _userService.UpdateEmail(userId, newEmail);
                if (userInfoDto != null)
                {
                    return Ok($"User '{userInfoDto.FirstName}' email was changed to '{newEmail}'");
                }
                return NotFound("Such user does not exists");
            }
            return BadRequest("Please check input parameters");
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "user,admin")]
        [HttpPut("/user/city")]
        public async Task<ActionResult<string>> UpdateCity([Required][StringLength(50)] string newCity)
        {
            if (ModelState.IsValid)
            {
                var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                Guid userId = new Guid(userIdStr);
                var adresas = await _userService.UpdateCity(userId, newCity);
                var useris = await _userService.GetUserByID(userId);
                if (adresas != null)
                {
                    return Ok($"User '{useris.FirstName}' city name was changed to '{newCity}'");
                }
                return NotFound("Such user does not exists");
            }
            return BadRequest("Please check input parameters");
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "user,admin")]
        [HttpPut("/user/street")]
        public async Task<ActionResult<string>> UpdateStreet([Required][StringLength(50)] string newStreet)
        {
            if (ModelState.IsValid)
            {
                var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                Guid userId = new Guid(userIdStr);
                var adresas = await _userService.UpdateStreet(userId, newStreet);
                if (adresas != null)
                {
                    return Ok($"User ' ' street name was changed to '{newStreet}'");
                }
                return NotFound("Such user does not exists");
            }
            return BadRequest("Please check input parameters");
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "user,admin")]
        [HttpPut("/user/house_number")]
        public async Task<ActionResult<string>> UpdateHouseNb([Required][StringLength(5)] string newHouseNb)
        {
            if (ModelState.IsValid)
            {
                var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                Guid userId = new Guid(userIdStr);
                var adresas = await _userService.UpdateHouseNb(userId, newHouseNb);
                //    UserDto? useris = await _userService.GetUserByID(userId);
                if (adresas != null)
                {
                    return Ok($"User ' ' house number was changed to '{newHouseNb}'");
                }
                return NotFound("Such user does not exists");
            }
            return BadRequest("Please check input parameters");
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "user,admin")]
        [HttpPut("/user/flat_number")]
        public async Task<ActionResult<string>> UpdateFlatNb([Required][StringLength(5)] string newFlatNb)
        {
            if (ModelState.IsValid)
            {
                var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                Guid userId = new Guid(userIdStr);
                var adresas = await _userService.UpdateFlatNb(userId, newFlatNb);
                if (adresas != null)
                {
                    return Ok($"User ' ' flat number was changed to '{newFlatNb}'");
                }
                return NotFound("Such user does not exists");
            }
            return BadRequest("Please check input parameters");
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "user,admin")]
        [HttpGet("/user/photo")]
        public async Task<ActionResult> GetImage()
        {
            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; 
            Guid userId = new Guid(userIdStr);
            var imageIsRepo = await _imageRepository.GetImage(userId);//gaunam paveiksleli is repozitorijos
            if (imageIsRepo == null)
            {
                return NotFound("Such user image does not exists"); 
            }
            return File(imageIsRepo.ImageBytes, "image/jpeg"); 
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "user,admin")]
        [HttpDelete("/user/image")]
        public async Task<ActionResult<string>> DeleteImage()
        {
            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdStr != null)
            {
                Guid userId = new Guid(userIdStr);
                var user = await _userService.GetUserByID(userId);
                if (user != null)
                {
                    Image? image = await _imageRepository.GetImage(userId);
                    if (image != null)
                    {
                        await _userService.DeleteImageById(userId);
                        return Ok($"User '{user.FirstName}' image was deleted");
                    }
                    return NotFound($"User {user.Id} does not have photo");
                }
            }
            return NotFound("Such user does not exists");
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "user,admin")]
        [HttpGet("/user/all_info")]
        public IActionResult GetUserInformation([Required][StringLength(50)] string idekite_user_id)
        {
            Guid userId = new Guid(idekite_user_id);
               UserInfo userInformation = _userInfoService._GetAllInfoByID(userId);
               Address userAddress = _userInfoService.GetAddressInfo(userId);
                if (userInformation == null)
                {
                    return NotFound("No data received");
                }                    
                if (userAddress == null)
                {
                    return NotFound("No data received");
                }
                var result = new //Use anonyme object to send both objects as one
                {
                    UserInfo = userInformation,
                    OtherObject = userAddress
                };
                return Ok(result);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [HttpDelete("/user")]
        public async Task<ActionResult<string>> DeleteUser([Required][StringLength(100)] string idekite_user_id_kuri_norite_istrinti)
        {
            Guid userId = new Guid(idekite_user_id_kuri_norite_istrinti);
            var user = await _userService.GetUserByID(userId); 
            if (user == null)
            {
                return NotFound("No such user");
            }
            await _userService.DeleteUser(user.Id);
            return Ok($"User {user.FirstName} was deleted from system");
           
        }
    }
}