using System;
using System.Collections.Generic;
using System.Text;

namespace TDD_Sample.Entities
{
    public class LoanApplication
    {
        public int Id { get; set; }
        public LoanProduct Product { get; set; }
        public LoanAmount Amount { get; set; }
        public Applicant Applicant { get; set; }
        public bool IsAccepted { get; set; }
    }

    public class LoanAmount {
        public string CurrencyCode { get; set; }
        public decimal Principal { get; set; }
    }
}
