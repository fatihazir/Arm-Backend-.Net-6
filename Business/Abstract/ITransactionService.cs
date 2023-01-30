using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ITransactionService
    {
        IDataResult<List<Transaction>> GetAllTransactionsByGroupId(int groupId);
        IResult Add(Transaction transaction);
        IResult Delete(Transaction transaction);
    }
}
