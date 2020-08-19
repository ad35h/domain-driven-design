using Marketplace.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Domain
{
    public class Money : Value<Money>
    {
        private const string DefaultCurrency = "EUR";
        public static Money FromDecimal(decimal amount, string currency = DefaultCurrency) => new Money(amount);
        public static Money FromString(string amount, string currency = DefaultCurrency) => new Money(decimal.Parse(amount));

        protected Money(decimal amount, string currencyCode = "EUR")
        {
            if (decimal.Round(amount, 2) != amount)
                throw new ArgumentOutOfRangeException( nameof(amount), "Amount cannot have more than two decimals");
            Amount = amount;
            CurrencyCode = currencyCode;
        }

        public decimal Amount { get; set; }
        public string CurrencyCode { get; }
        public Money Add(Money summand) => new Money(Amount + summand.Amount);
        public Money Subtract(Money subtrahend) => new Money(Amount - subtrahend.Amount);
        public static Money operator + (Money summand1, Money summand2) => summand1.Add(summand2);
        public static Money operator - (Money minuend, Money subtrahend) => minuend.Subtract(subtrahend);

    }
}
