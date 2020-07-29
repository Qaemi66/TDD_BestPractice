using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using TDD_Sample.Entities;
using TDD_Sample.Models;
using TDD_Sample.Services;
using TDD_Sample.Services.Contracts;

namespace TDD_Sample.UnitTest
{
    [TestClass]
    public class LoanApplicationProcessorTest
    {
        private LoanProduct CreateLoanProduct() { 
            return new LoanProduct { Id = 99, ProductName = "Loan", InterestRate = 5.25m };
        }

        private LoanAmount CreateLoanAmount()
        {
            return new LoanAmount { CurrencyCode = "Rial", Principal = 2_000_000_0 };
        }

        private Applicant CreateApplicant(int salary)
        {
            return new Applicant { Id = 1, Name = "User 1", Age = 25, Address = "This place", Salary = salary };
        }

        [TestMethod]
        public void DeclineLowSalary()
        {
            //Arrange
            var product = CreateLoanProduct();
            var amount = CreateLoanAmount();
            var applicant = CreateApplicant(1_100_000_0);
            var application = new LoanApplication { Id = 42, Product = product, Amount = amount, Applicant = applicant };

            var mockIdentityVerifier = new Mock<IIdentityVerifier>();
            var mockCreditScorer = new Mock<ICreditScorer>();


            var processor = new LoanApplicationProcessor(mockIdentityVerifier.Object, mockCreditScorer.Object);

            //Act
            processor.Process(application);

            //Assert
            Assert.IsFalse(application.IsAccepted);
        }

        delegate void ValidateCallback(
            string applicantName,
            int applicantAge,
            string applicantAddress,
            ref IdentityVerificationStatus status);

        [TestMethod]
        public void Accept()
        {
            //Arrange
            var product = CreateLoanProduct();
            var amount = CreateLoanAmount();
            var applicant =CreateApplicant(1_500_000_0);
            var application = new LoanApplication { Id = 42, Product = product, Amount = amount, Applicant = applicant };

            var mockIdentityVerifier = new Mock<IIdentityVerifier>();

            mockIdentityVerifier.Setup(x => x.Validate(It.IsAny<string>(), applicant.Age, applicant.Address)).
                Returns(true);

            /*for out param*/
            //var isValid = true;
            //mockIdentityVerifier.Setup(x=>x.Validate(It.IsAny<string>(), applicant.Age, applicant.Address, out isValid));

            /*for ref param*/
            //mockIdentityVerifier
            //.Setup(x => x.Validate(applicant.Name,
            //    applicant.Age,
            //    applicant.Address,
            //    ref It.Ref<IdentityVerificationStatus>.IsAny))
            //.Callback(new ValidateCallback(
            //    (string applicantName,
            //     int applicantAge,
            //     string applicantAddress,
            //     ref IdentityVerificationStatus status) =>
            //        status = new IdentityVerificationStatus { Passed = true }));


            var mockCreditScorer = new Mock<ICreditScorer>();

            mockCreditScorer.Setup(c => c.Score).Returns(110_000);

            mockCreditScorer.Setup(x => x.ScoreResult.ScoreValue.Score).Returns(110_000);

            var processor = new LoanApplicationProcessor(mockIdentityVerifier.Object, mockCreditScorer.Object);

            //Act
            processor.Process(application);

            //Assert
            Assert.IsTrue(application.IsAccepted);
        }
               

        [TestMethod]
        public void InitializeIdentityVerifier()
        {
            //Arrange
            var product = CreateLoanProduct();
            var amount = CreateLoanAmount();
            var applicant = CreateApplicant(1_500_000_0);
                
            var application = new LoanApplication { Id = 42, Product = product, Amount = amount, Applicant = applicant };

            var mockIdentityVerifier = new Mock<IIdentityVerifier>(MockBehavior.Strict);
            mockIdentityVerifier.Setup(x => x.Validate(applicant.Name, applicant.Age, applicant.Address))
                .Returns(true);
            mockIdentityVerifier.Setup(x => x.Initialize());

            var mockCreditScorer = new Mock<ICreditScorer>();
            mockCreditScorer.Setup(x => x.ScoreResult.ScoreValue.Score).Returns(110_000);


            var processor = new LoanApplicationProcessor(mockIdentityVerifier.Object, mockCreditScorer.Object);

            //Act
            processor.Process(application);

            //Assert
            /*We must avoid multiple asserts but some of this codes are inevitable*/
            mockIdentityVerifier.Verify(x => x.Initialize());
            mockIdentityVerifier.Verify(x => x.Validate(applicant.Name, applicant.Age, applicant.Address));
            mockCreditScorer.Verify(x => x.CalculateScore(applicant.Name, applicant.Address), Times.Once);
            mockCreditScorer.VerifyGet(x => x.ScoreResult.ScoreValue.Score, Times.Once);

            mockIdentityVerifier.VerifyNoOtherCalls();
        }
        
        /// <summary>
        /// Partial Mocking
        /// </summary>
        [TestMethod]
        public void AcceptUsingPartialMock()
        {
            //Arrange
            var product = CreateLoanProduct();
            var amount = CreateLoanAmount();
            var applicant = CreateApplicant(1_500_000_0);

            var application = new LoanApplication { Id = 42, Product = product, Amount = amount, Applicant = applicant };

            var mockIdentityVerifier = new Mock<IdentityVerifierServiceGateway>();

            mockIdentityVerifier.Setup(x => x.CallService(applicant.Name, applicant.Age, applicant.Address))
                .Returns(true);

            var mockCreditScorer = new Mock<ICreditScorer>();
            mockCreditScorer.Setup(x => x.ScoreResult.ScoreValue.Score).Returns(110_000);

            var sut = new LoanApplicationProcessor(mockIdentityVerifier.Object, mockCreditScorer.Object);

            //Act
            sut.Process(application);

            //Assert
            Assert.IsTrue(application.IsAccepted);
        }
    }
}
