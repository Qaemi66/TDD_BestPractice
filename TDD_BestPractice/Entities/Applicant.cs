using System;
using System.Collections.Generic;
using System.Text;

namespace TDD_Sample.Entities
{
    public class Applicant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public decimal Salary { set; get; }
    }
}
