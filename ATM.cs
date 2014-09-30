using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATM
{
    // Parent / Base Account class
    class Account
    {
        // attributes
        private decimal _Balance;
        // Property => to protected, only the child class could access this property
        protected decimal Balance
        {
            get { return _Balance; }
            // add set method to allow the child class update the private _Ballance in account
            set { _Balance = value; }
        }
        // custom constructor to set the new account with the parameter balance amount
        protected Account(decimal balance)
        {
            _Balance = balance;
        }


        // ToString() method will be used by the child class
        public override string ToString()
        {
            return string.Format("Account Balance: {0:c}", _Balance);
        }
    }
    // savings account class inherited from the parent/base account class
    class SavingsAccount : Account
    {
        // attributes
        private const decimal DEFAULT_BALANCE = 0.00M;
        // properties
        // does not have as the data balance is stored in the parent/base class

        // parameterless constructor to create the new savingsaccount with 0 in Balance
        public SavingsAccount()
            : this(DEFAULT_BALANCE)
        {
        }

        // custom constructor to create the new savingsaccount with the balance parameter ammount
        // that will be passing to create the constructor for the base Account balance
        public SavingsAccount(decimal balance)
            : base(balance)
        {
        }
        // service method to update/increase the base Account Balance by the amount parameter
        public void Deposit(decimal amount)
        {
            base.Balance += amount; // replace _Balance (private to the account) with base.Balance property
        }

        // service method to decrease the base Account Balance by the amount parameter
        // return false when amount higher than the base Balance => to reject the transaction
        public bool Withdraw(decimal amount)
        {
            bool result = true;
            // using the base.Balance property rather than the private _Balance from account
            if (base.Balance >= amount)
                base.Balance -= amount;
            else
                result = false;
            return result;
        }

        // no ToString() method as it using the parent/ base Account ToString()
    }

    // CreditAccount is the child class which inherited the Balance in the Account class
    class CreditAccount : Account
    {

        // Attribute
        private decimal _Limit;
        public decimal Limit
        {
            get { return _Limit; }
            set { _Limit = value; }
        }
        // new Balance property to override/hide the parent base Account Balance property
        public new decimal Balance
        {
            // credit card the balance is the amount you owed not debit
            get { return -base.Balance; } // return the negative value of base.balance property
        }
        // Custom Constructor with the balance to set the parent/base Account class
        // the limit to be set for the private attribute in this class
        public CreditAccount(decimal balance, decimal limit)
            : base(balance)
        {
            _Limit = limit;
        }

        // service method to update/increase the base.Balance property
        public void Payment(decimal amount)
        {
            base.Balance += amount; // replace _Balance by base balance property
        }

        // service method to update/decrease the base.Balance property
        // return false to reject the purchase amount over the available credit limit
        public bool Purchase(decimal amount)
        {
            bool result = true;
            if (-base.Balance + amount <= _Limit)
                base.Balance -= amount;
            else
                result = false;
            return result;
        }

        //implement ToString() to override the parent/base Account ToString()
        // need to display different output for the balance status
        public override string ToString()
        {
            string result;
            if (base.Balance < 0)
                result = string.Format("Balance: {0:c} owed", -base.Balance);
            else if (base.Balance == 0)
                result = base.ToString(); // forawrd/call the parent/base ToString() to show the current balance
            else
                result = base.ToString() + " in credit";

            result += ", limit " + _Limit.ToString("c");
            return result;
        }
    }
    // part 2
    class TermDeposit : Account
    {
        // attribute
        private ushort _Term;
        // prop
        public ushort Term
        {
            get { return _Term; }
        }

        // cust constructor with the amount to set the parent/base Balance amount
        // number of days to set the private _Term attribute
        public TermDeposit(decimal amount, ushort days)
            : base(amount)
        {
            _Term = days;
        }

        // calculate insterest method has a single parameter for the interest rate and return
        public decimal CalculateInterest(decimal rate)
        {
            return rate / 100 / 365 * base.Balance * _Term;
        }
        // method to override the 
        public override string ToString()
        {
            return string.Format("Term Deposit with {0} in {1} days\n\t- Interest Earning 6.5% is {2:c}", base.ToString(), _Term, CalculateInterest(6.5M));
        }
    }

    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Create Term Deposit account with 1000$ in Balance for 90 days");
            TermDeposit term = new TermDeposit(1000m, 90);
            Console.WriteLine(term); // using Termdesposit ToString

            Console.WriteLine("Testing with double interest 13% bonus {0:c}", term.CalculateInterest(13m));

            /*
            Console.WriteLine("Create credit Account with $0 in Balance 500 limit");
            CreditAccount credit = new CreditAccount(0m, 500m);
            Console.WriteLine(credit); // using Account.ToString()

            if (credit.Purchase(200m) == false)
                Console.WriteLine("\n Can't purchase $200 at credit {0}", credit);
            else
                Console.WriteLine("\nAfter withdraw $200.. the saving has {0}", credit);

            credit.Payment(150m);
            Console.WriteLine("After payment 150.. the credit has {0}", credit);

            if (credit.Purchase(997m) == false)
                Console.WriteLine("\n Can't purchase $200 at credit {0}.. reject transaction", credit);
            else
                Console.WriteLine("\nAfter withdraw $200.. the saving has {0}", credit);

            if (credit.Purchase(449m) == false)
                Console.WriteLine("\n Can't purchase $200 at credit {0}", credit);
            else
                Console.WriteLine("\nAfter withdraw $200.. the saving has {0}", credit);
        

            Console.WriteLine("Create Savings Account with $100 in Balance");
            SavingsAccount saving = new SavingsAccount(100M);
            Console.WriteLine(saving); // using Account.ToString()

            if (saving.Withdraw(200m) == false)
                Console.WriteLine("\n Can't withdraw $200 at Saving {0}", saving);
            else
                Console.WriteLine("\nAfter withdraw $200.. the saving has {0}", saving);

            saving.Deposit(150m);
            Console.WriteLine("After deposit 150.. the saving has {0}", saving);

            if (saving.Withdraw(200m) == false)
                Console.WriteLine("\n Can't withdraw $200 at Saving {0}", saving);
            else
                Console.WriteLine("\nAfter withdraw $200.. the saving has {0}", saving);
            */

        }
    }
}

