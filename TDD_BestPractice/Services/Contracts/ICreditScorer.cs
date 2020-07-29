using System;
using System.Collections.Generic;
using System.Text;
using TDD_Sample.Models;

namespace TDD_Sample.Services.Contracts
{
    public interface ICreditScorer
    {
        int Score { get; }

        void CalculateScore(string applicantName, string applicantAddress);

        ScoreResult ScoreResult { get; }
        int Count { get; set; }
    }
}
