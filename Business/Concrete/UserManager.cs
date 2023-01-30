using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Entities.DTOs;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public List<OperationClaim> GetClaims(User user)
        {
            return _userDal.GetClaims(user);
        }

        public User Add(User user)
        {
            var result =_userDal.AddAndRetriveData(user);
            return result;
        }

        public User GetByMail(string email)
        {
            return _userDal.Get(u => u.Email == email);
        }

        [ValidationAspect(typeof(UpdateProfileValidation))]
        public IDataResult<User> Update(UserUpdateDto userUpdateDto)
        {
            User userToUpdate = _userDal.Get(u => u.Id == userUpdateDto.Id);
            userToUpdate.FirstName = userUpdateDto.FirstName;
            userToUpdate.LastName = userUpdateDto.LastName;
            _userDal.Update(userToUpdate);
            return new SuccessDataResult<User>(userToUpdate, Messages.Updated);
        }

        [SecuredOperation("Admin,User")]
        [ValidationAspect(typeof(ChangePasswordValidation))]
        public IResult ChangePassword(UserChangePasswordDto userChangePasswordDto)
        {
            var userToChangePassword = _userDal.Get(u => u.Email == userChangePasswordDto.Email);
            if (userToChangePassword == null)
            {
                return new ErrorResult(Messages.UserNotFound);
            }
            if (!HashingHelper.VerifyPasswordHash(userChangePasswordDto.OldPassword, userToChangePassword.PasswordHash, userToChangePassword.PasswordSalt))
            {
                return new ErrorResult(Messages.PasswordError);
            }

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(userChangePasswordDto.NewPassword, out passwordHash, out passwordSalt);

            userToChangePassword.PasswordHash = passwordHash;
            userToChangePassword.PasswordSalt = passwordSalt;

            _userDal.Update(userToChangePassword);

            return new SuccessResult(Messages.PasswordChanged);
        }
    }
}
