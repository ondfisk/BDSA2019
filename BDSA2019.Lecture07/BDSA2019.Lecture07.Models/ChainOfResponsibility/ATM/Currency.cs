// Taken from: https://www.journaldev.com/1617/chain-of-responsibility-design-pattern-in-java#chain-of-responsibility-design-pattern-example

namespace BDSA2019.Lecture07.Models.ChainOfResponsibility.ATM
{
    public class Currency
    {
        public int Amount { get; }

        public Currency(int amount)
        {
            Amount = amount;
        }
    }
}