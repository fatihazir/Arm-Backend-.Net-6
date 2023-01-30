using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserTransactionGroupController : ControllerBase
    {
        private readonly IUserTransactionGroupService _userTransactionGroupService;

        public UserTransactionGroupController(IUserTransactionGroupService userTransactionGroupService)
        {
            _userTransactionGroupService = userTransactionGroupService;
        }

        [HttpGet("getAllByUserId")]
        public IActionResult GetAll(int userId)
        {
            var result = _userTransactionGroupService.GetAllByUserId(userId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(UserTransactionGroup userTransactionGroup)
        {
            var result = _userTransactionGroupService.AddAndRetriveData(userTransactionGroup);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(UserTransactionGroup userTransactionGroup)
        {
            var result = _userTransactionGroupService.Update(userTransactionGroup);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(UserTransactionGroup userTransactionGroup)
        {
            var result = _userTransactionGroupService.Delete(userTransactionGroup);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

    }
}
