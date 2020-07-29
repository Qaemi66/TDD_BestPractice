using System;
using System.Collections.Generic;
using System.Text;
using TDD_Sample.Models;

namespace TDD_Sample.Services.Contracts
{
    public interface IIdentityVerifier
    {
        void Initialize();

        bool Validate(string applicantName, int applicantAge, string applicantAddress);

        void Validate(string applicantName, int applicantAge, string applicantAddress, out bool isValid);

        void Validate(string applicantName, int applicantAge, string applicantAddress, ref IdentityVerificationStatus status);
    }
}
