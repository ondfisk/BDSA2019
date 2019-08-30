using System;
using Xunit;

namespace BDSA2019.Lecture01.Lib.Tests
{
    public class BankAccountTests
    {
        [Fact]
        public void New_account_has_balance_0()
        {
            // Arrange

            // Act
            var account = new BankAccount();

            // Assert
            Assert.Equal(0, account.Balance);
        }

        [Fact]
        public void New_account_given_42_has_balance_42()
        {
            // Arrange

            // Act
            var account = new BankAccount(42);

            // Assert
            Assert.Equal(42, account.Balance);
        }

        [Fact]
        public void Deposit_given_balance_2_when_add_40_has_balance_42()
        {
            // Arrange
            var account = new BankAccount(2);

            // Act
            account.Deposit(40);

            // Assert
            Assert.Equal(42, account.Balance);
        }

        [Fact]
        public void Deposit_given_balance_42_given_int_MaxValue_fails()
        {
            // Arrange
            var account = new BankAccount(42);

            // Act

            // Assert
            Assert.Throws<OverflowException>(() => account.Deposit(int.MaxValue));
        }
    }
}
