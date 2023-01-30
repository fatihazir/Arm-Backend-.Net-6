using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class UserTransactionGroup : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Alias { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
