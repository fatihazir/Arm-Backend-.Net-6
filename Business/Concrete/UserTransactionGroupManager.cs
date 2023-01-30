using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserTransactionGroupManager : IUserTransactionGroupService
    {
        IUserTransactionGroupDal _userTransactionDal;
        ITransactionDal _transactionDal;

        public UserTransactionGroupManager(IUserTransactionGroupDal userTransactionDal, ITransactionDal transactionDal)
        {
            _userTransactionDal = userTransactionDal;
            _transactionDal = transactionDal;
        }

        [SecuredOperation("Admin,User")]
        [CacheRemoveAspect("IUserTransactionGroupService.Get")]
        public IDataResult<UserTransactionGroup> AddAndRetriveData(UserTransactionGroup userTransactionGroup)
        {
            userTransactionGroup.CreatedAt = DateTime.Now;
            var result = _userTransactionDal.AddAndRetriveData(userTransactionGroup);
            return new SuccessDataResult<UserTransactionGroup>(result, Messages.UserTransactionGroupCreated);
        }

        [SecuredOperation("Admin,User")]
        [CacheRemoveAspect("IUserTransactionGroupService.Get")]
        public IResult Delete(UserTransactionGroup userTransactionGroup)
        {
            IResult result = BusinessRules.Run(DeleteEveryTransactionWithRelevantGroup(userTransactionGroup.Id));
            if (result != null)
            {
                return result;
            }
            _userTransactionDal.Delete(userTransactionGroup);
            return new SuccessResult(Messages.Deleted);
        }

        [SecuredOperation("Admin,User")]
        [CacheAspect(duration:10)]
        public IDataResult<List<UserTransactionGroupDto>> GetAllByUserId(int userId)
        {
            return new SuccessDataResult<List<UserTransactionGroupDto>>(_userTransactionDal.UserTransactionGroupDto(userId));
        }

        [SecuredOperation("Admin,User")]
        [CacheRemoveAspect("IUserTransactionGroupService.Get")]
        public IResult Update(UserTransactionGroup userTransactionGroup)
        {
            _userTransactionDal.Update(userTransactionGroup);
            return new SuccessResult(Messages.Updated);
        }

        private IResult DeleteEveryTransactionWithRelevantGroup(int transactionGroupId)
        {
            List<Transaction> transactions = _transactionDal.GetAll(t => t.TransactionGroupId == transactionGroupId);
            foreach (var transactionToDelete in transactions)
            {
                _transactionDal.Delete(transactionToDelete);
            }

            return new SuccessResult();
        }
    }
}
