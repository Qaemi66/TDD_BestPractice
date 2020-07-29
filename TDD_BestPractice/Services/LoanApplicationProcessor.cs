using System;
using TDD_Sample.Entities;
using TDD_Sample.Services.Contracts;

namespace TDD_Sample.Services
{
    public class LoanApplicationProcessor
    {
        private const decimal MinimumSalary = 1_500_000_0;
        private const int MinimumAge = 18;
        private const int MinimumCreditScore = 100_000;

        private readonly IIdentityVerifier _identityVerifier;
        private readonly ICreditScorer _creditScorer;

        public LoanApplicationProcessor(
            IIdentityVerifier identityVerifier,
            ICreditScorer creditScorer)
        {
            _identityVerifier = identityVerifier ?? throw new ArgumentNullException(nameof(identityVerifier));
            _creditScorer = creditScorer ?? throw new ArgumentNullException(nameof(creditScorer));
        }

        public bool Process(LoanApplication application)
        {
            application.IsAccepted = false;

            if (application.Applicant.Salary < MinimumSalary)
            {
                return application.IsAccepted;
            }

            if (application.Applicant.Age < MinimumAge)
            {
                return application.IsAccepted;
            }

            _identityVerifier.Initialize();

            var isValidIdentity = _identityVerifier.Validate(application.Applicant.Name, application.Applicant.Age, application.Applicant.Address);

            if (!isValidIdentity)
            {
                return application.IsAccepted;
            }

            try
            {
                _creditScorer.CalculateScore(application.Applicant.Name, application.Applicant.Address);
                /*if (_creditScorer.Score < MinimumCreditScore)*/
                if (_creditScorer.ScoreResult.ScoreValue.Score < MinimumCreditScore)
                {
                    return application.IsAccepted;
                }
            }
            catch (Exception)
            {
                return application.IsAccepted;
            }

            
            application.IsAccepted = true;
            return application.IsAccepted;
        }
    }
}
