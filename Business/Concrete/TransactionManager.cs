using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class TransactionManager : ITransactionService
    {
        ITransactionDal _transactionDal;

        public TransactionManager(ITransactionDal transactionDal)
        {
            _transactionDal = transactionDal;
        }

        [SecuredOperation("Admin,User")]
        [CacheRemoveAspect("ITransactionService.Get")]
        public IResult Add(Transaction transaction)
        {
            _transactionDal.Add(transaction);
            return new SuccessResult(Messages.TransactionAdded);
        }

        [SecuredOperation("Admin,User")]
        [CacheRemoveAspect("IUserTransactionGroupService.Get")]
        [CacheRemoveAspect("ITransactionService.Get")]
        public IResult Delete(Transaction transaction)
        {
            _transactionDal.Delete(transaction);
            return new SuccessResult(Messages.Deleted);
        }

        [SecuredOperation("Admin,User")]
        [CacheAspect(duration: 10)]
        public IDataResult<List<Transaction>> GetAllTransactionsByGroupId(int groupId)
        {
            return new SuccessDataResult<List<Transaction>>(_transactionDal.GetAll(t => t.TransactionGroupId == groupId));
        }
    }
}
