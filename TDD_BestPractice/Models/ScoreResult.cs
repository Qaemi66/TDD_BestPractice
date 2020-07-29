using System;
using System.Collections.Generic;
using System.Text;

namespace TDD_Sample.Models
{
    public class ScoreResult
    {
        public virtual  ScoreValue ScoreValue { get; }
    }

    public class ScoreValue
    {
        public virtual int Score { get; }
    }
}
