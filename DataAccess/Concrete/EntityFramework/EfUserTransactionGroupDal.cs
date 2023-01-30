using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserTransactionGroupDal : EfEntityRepositoryBase<UserTransactionGroup, ArmProjectContext>, IUserTransactionGroupDal
    {
        public List<UserTransactionGroupDto> UserTransactionGroupDto(int userId)
        {
            using (ArmProjectContext context = new ArmProjectContext())
            {
                var result = from tGroup in context.UserTransactionGroups
                             where tGroup.UserId == userId
                             select new UserTransactionGroupDto
                             {
                                 Id = tGroup.Id,
                                 Alias = tGroup.Alias,
                                 CreatedAt = tGroup.CreatedAt,
                                 UserId = tGroup.UserId,
                                 ResultCount = context.Transactions.Count(t => t.TransactionGroupId == tGroup.Id)
                             };
                return result.ToList();
            }
        }
    }
}
