using Core.Entities.Concrete;
using Core.Entities.DTOs;
using Core.Utilities.Results;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IUserService
    {
        List<OperationClaim> GetClaims(User user);
        User Add(User user);
        IDataResult<User> Update(UserUpdateDto user);
        IResult ChangePassword(UserChangePasswordDto userChangePasswordDto);
        User GetByMail(string email);
        
        
    }
}
