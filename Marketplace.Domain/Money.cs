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

        protected Money(decimal amount, string currencyCode, ICurrencyLookup currencyLookup)
        {
            if (string.IsNullOrEmpty(currencyCode))
            {
                throw new ArgumentNullException(
                     nameof(currencyCode),
                     "Currency code must be specified");
            }

            var currency = currencyLookup.FindCurrency(currencyCode);

            if (!currency.InUse)
                throw new ArgumentException(
                $"Currency {currencyCode} is not valid");

            if (decimal.Round(amount, 2) != amount)
                throw new ArgumentOutOfRangeException( nameof(amount), "Amount cannot have more than two decimals");
            Amount = amount;
            Currency = currency;
        }

        private Money(decimal amount, CurrencyDetails currency)
        {
            Amount = amount;
            Currency = currency;
        }

        public decimal Amount { get; set; }
        public CurrencyDetails Currency { get; }
        public Money Add(Money summand)
        {
            if (Currency != summand.Currency)
                throw new CurrencyMismatchException(
                "Cannot sum amounts with different currencies");
            return new Money(Amount + summand.Amount, Currency);
        }

        public Money Subtract(Money subtrahend)
        {
            if (Currency != subtrahend.Currency)
                throw new CurrencyMismatchException(
                "Cannot subtract amounts with different currencies");
            return new Money(Amount - subtrahend.Amount, Currency);
        }

        public static Money operator + (Money summand1, Money summand2) => summand1.Add(summand2);
        public static Money operator - (Money minuend, Money subtrahend) => minuend.Subtract(subtrahend);

    }

    public class CurrencyMismatchException : Exception
    {
        public CurrencyMismatchException(string message) :
        base(message)
        {
        }
    }

}
