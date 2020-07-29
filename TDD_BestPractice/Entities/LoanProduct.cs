using System;
using System.Collections.Generic;
using System.Text;

namespace TDD_Sample.Entities
{
public    class LoanProduct
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal InterestRate { get; set; }
    }
}
