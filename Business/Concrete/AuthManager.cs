using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Entities.DTOs;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using Entities.DTOs;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private IUserService _userService;
        private ITokenHelper _tokenHelper;
        private IUserOperationClaimService _userOperationClaimService;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper, IUserOperationClaimService userOperationClaimService)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
            _userOperationClaimService = userOperationClaimService;
        }

        [ValidationAspect(typeof(RegisterValidation))]
        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password) 
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var user = new User
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };
            User addedUser = _userService.Add(user);
            _userOperationClaimService.Add(new UserOperationClaim() { OperationClaimId = 2, UserId = addedUser.Id});
            return new SuccessDataResult<User>(user, Messages.UserRegistered);
        }

        [ValidationAspect(typeof(LoginValidation))]
        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userService.GetByMail(userForLoginDto.Email);
            if (userToCheck == null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.PasswordError);
            }

            return new SuccessDataResult<User>(userToCheck, Messages.SuccessfulLogin);
        }

        public IResult UserExists(string email)
        {
            if (_userService.GetByMail(email) != null)
            {
                return new ErrorResult(Messages.UserAlreadyExists);
            }
            return new SuccessResult();  
        }

        public IDataResult<UserLoginCustomDto> CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims);
            return new SuccessDataResult<UserLoginCustomDto>(new UserLoginCustomDto()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Expiration = accessToken.Expiration,
                Token = accessToken.Token,
                PhotoUrl= user.PhotoUrl
                
            }, Messages.AccessTokenCreated);
        }
    }
}
