using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserTransactionGroupService
    {
        IDataResult<UserTransactionGroup> AddAndRetriveData(UserTransactionGroup userTransactionGroup);
        IDataResult<List<UserTransactionGroupDto>> GetAllByUserId(int userId);
        IResult Update(UserTransactionGroup userTransactionGroup);
        IResult Delete(UserTransactionGroup userTransactionGroup);
    }
}
