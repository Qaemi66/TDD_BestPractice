using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace TDD_Sample.UnitTest
{
    [TestClass]
    public  class BankAccountTests
    {
        [TestMethod]
        public void Debit_WithValidAmount_UpdateBalance() {

            //Arrange
            double beginningBalance = 11.99;
            double debitAmount = 4.55;
            double excepted = 7.44;

            var account = new BankAccount("Mr. Bryan Walton", beginningBalance);

            //Act
            account.Debit(debitAmount);

            //Assert
            double actual = account.Balance;

            Assert.AreEqual(excepted, actual, 0.001, "Account not debited correctly");
        }

        [TestMethod]
        public void Debit_WithAmountIsLessThanZero_ShouldThrowArgoumentOutOfRange()
        {
            //Arrange
            double beginningBalance = 11.99;
            double debitAmount = -100.00;
            BankAccount bankAccount = new BankAccount("Mr. Bryan Walton", beginningBalance);

            //Act and Assert
            Assert.ThrowsException<System.ArgumentOutOfRangeException>(() => bankAccount.Debit(debitAmount));
        }

        [TestMethod]
        public void Debit_WhenAmountIsMoreThanBalance_ShouldThrowArgumentOutOfRange() {
            double beginningBalance = 11.99;
            double debitAmount = -20.00;
            BankAccount bankAccount = new BankAccount("Mr. Bryan Walton", beginningBalance);

            //Act and Assert
            Assert.ThrowsException<System.ArgumentOutOfRangeException>(() => bankAccount.Debit(debitAmount));
        }
    }
}
