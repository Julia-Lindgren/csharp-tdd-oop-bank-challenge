﻿using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Boolean.CSharp.Main
{
    public class CurrentAccount : IAccount
    {
        public AccountType Type { get; } = AccountType.Current;
        public decimal Balance { get; set; } = 0m;
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();
  
        public bool Deposit(decimal amount)
        {
            if (amount < 0) 
                return false; 

            Balance += amount;
            Transaction transaction = new Transaction(amount, Balance, TransactionType.Deposit);
            Transactions.Add(transaction);

            return true;
        }

        public bool Withdraw(decimal amount)
        {
            if ((Balance - amount) < 0) //Not enough money in account
                return false;

            Balance -= amount;
            Transaction transaction = new Transaction(amount, Balance, TransactionType.Withdraw);
            Transactions.Add(transaction);

            return true;
        }

        public void GenerateStatement()
        {
            Console.WriteLine("{0,-12} || {1, -8} || {2, -8} || {3, -8}",
                    "date",
                    "credit",
                    "debit",
                    "balance"
                );

            foreach (var transaction in Transactions)
            {
                decimal? credit = null;
                decimal? debit = null;

                switch (transaction.Type)
                {
                    case(TransactionType.Deposit):
                        credit = transaction.Amount;
                        break;
                    case(TransactionType.Withdraw):
                        debit = transaction.Amount;
                        break;
                }

                Console.WriteLine("{0,-12} || {1, -8} || {2, -8} || {3, -8}",
                    transaction.FormattedDate,
                    $"{credit}",
                    $"{debit}",
                    Math.Round(transaction.Balance,2)
                );
            }
        }
    }
}
