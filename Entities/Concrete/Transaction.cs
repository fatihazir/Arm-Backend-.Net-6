using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Transaction : IEntity   
    {
        public int Id { get; set; }
        public int TransactionGroupId { get; set; }
        public string Associations { get; set; }
        public double Support { get; set; }
        public double Confidence { get; set; }
        public double Lift { get; set; }
        public bool IsPositive { get; set; }
    }
}
