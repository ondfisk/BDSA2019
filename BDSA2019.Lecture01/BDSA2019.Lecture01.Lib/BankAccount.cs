using System;

namespace BDSA2019.Lecture01.Lib
{
    public class BankAccount
    {
        public int Balance { get; private set; }

        public BankAccount(int balance = 0)
        {
            Balance = balance;
        }

        public void Deposit(int amount)
        {
            checked
            {
                Balance += amount;
            }
        }
    }
}
