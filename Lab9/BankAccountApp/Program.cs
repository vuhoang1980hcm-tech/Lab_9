using BankAccountApp;

var acc = new BankAccount("Trung Pham", 1000);
acc.Credit(500);
Console.WriteLine("After credit 500: " + acc.Balance);
acc.Debit(200);
Console.WriteLine("After debit 200: " + acc.Balance);
acc.Withdraw(300);
Console.WriteLine("After withdraw 300: " + acc.Balance);