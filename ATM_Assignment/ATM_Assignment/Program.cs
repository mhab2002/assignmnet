using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.ComponentModel.Design;
using System.Collections.Generic;
namespace ATM_Assignment
{
[Serializable]
class BankAccount
{
    string firstName, lastName, email, cardNumber, pinCode;
    int balance;

    public BankAccount()
    {
        firstName = "";
        lastName = "";
        email = "";
        cardNumber = "";
        pinCode = "";
        balance = 0;
    }
    public BankAccount(string firstName, string lastName, string email, string cardNumber, string pinCode, int balance)
    {
        this.firstName = firstName;
        this.lastName = lastName;
        this.email = email;
        this.cardNumber = cardNumber;
        this.pinCode = pinCode;
        this.balance = balance;
    }

    public void withdraw(int amount)
    {
        if(amount > balance)
        {
            Console.WriteLine("Not enough balance.");
        }
        else if(amount < 0)
        {
            Console.WriteLine("Wrong value.");
        }
        else
        {
            balance -= amount;
            Console.WriteLine("Withdraw successful.");
        }
    }
    public void deposit(int amount)
    {
        if(amount < 0)
        {
            Console.WriteLine("Wrong value.");
        }
        else
        {
            balance += amount;
            Console.WriteLine("Deposit successful.");
        }
    }
    public int Balance
    {
        get
        {
            return balance;
        }
        set
        {
            balance = value;
        }
    }
    public string CardNumber
    {
        get
        {
            return cardNumber;
        }
    }

    public string Pin
    {
        get
        {
            return pinCode;
        }
    }
}

class Program
{   
    static void Main(string[] args)
    {
        BinaryFormatter bf = new BinaryFormatter();
        while (true)
        {
            Console.WriteLine("1-Add user\n2-Check balance\n3-Withdraw\n4-Deposit\n5-Exit");
            string choice = Console.ReadLine();
            if(choice == "1")
            {
                FileStream fs = new FileStream("Account.txt", FileMode.Append, FileAccess.Write);
                Console.Write("Enter first name: ");
                string firstName = Console.ReadLine();
                Console.Write("Enter last name: ");
                string lastName = Console.ReadLine();
                Console.Write("Enter email: ");
                string email = Console.ReadLine();
                Console.Write("Enter card number: ");
                string cardNumber = Console.ReadLine();
                Console.Write("Enter pin number: ");
                string pinCode = Console.ReadLine();
                Console.Write("Enter balance: ");
                int balance = Convert.ToInt32(Console.ReadLine());
                BankAccount x = new BankAccount(firstName, lastName, email, cardNumber, pinCode, balance);
                bf.Serialize(fs, x);
                fs.Close();
            }
            else if(choice == "2")
            {
                Console.Write("Enter card number: ");
                string cardNo = Console.ReadLine();
                Console.Write("Enter pin number: ");
                string pinNo = Console.ReadLine();
                FileStream fs = new FileStream("Account.txt", FileMode.Open, FileAccess.Read);
                while(fs.Position < fs.Length)
                {
                    BankAccount tmp = (BankAccount)bf.Deserialize(fs);
                    string r = Convert.ToString(tmp.Balance);
                    if(tmp.CardNumber == cardNo && tmp.Pin == pinNo)
                    {
                        Console.Write("Your balance is: ");
                        Console.WriteLine(r);
                    }
                }
                fs.Close();
            }
            else if(choice == "3")
            {

                Console.Write("Enter card number: ");
                string cardNo = Console.ReadLine();
                Console.Write("Enter pin number: ");
                string pinNo = Console.ReadLine();
                List<BankAccount> l = new List<BankAccount>();
                FileStream fs = new FileStream("Account.txt", FileMode.Open, FileAccess.Read);
                while (fs.Position < fs.Length)
                {
                    BankAccount tmp = (BankAccount)bf.Deserialize(fs);
                    if (tmp.CardNumber == cardNo && tmp.Pin == pinNo)
                    {
                        Console.Write("Enter amount: ");
                        int amount = Convert.ToInt32(Console.ReadLine());
                        tmp.withdraw(amount);
                    }
                    l.Add(tmp);
                }
                fs.Close();
                fs = new FileStream("Account.txt", FileMode.Create);
                fs.Close();
                fs = new FileStream("Account.txt", FileMode.Append, FileAccess.Write);
                for(int i = 0; i < l.Count; i++)
                {
                    bf.Serialize(fs, l[i]);
                }
                fs.Close();
            }
            else if(choice == "4")
            {
                Console.Write("Enter card number: ");
                string cardNo = Console.ReadLine();
                Console.Write("Enter pin number: ");
                string pinNo = Console.ReadLine();
                List<BankAccount> l = new List<BankAccount>();
                FileStream fs = new FileStream("Account.txt", FileMode.Open, FileAccess.Read);
                while (fs.Position < fs.Length)
                {
                    BankAccount tmp = (BankAccount)bf.Deserialize(fs);
                    if (tmp.CardNumber == cardNo && tmp.Pin == pinNo)
                    {
                        Console.Write("Enter amount: ");
                        int amount = Convert.ToInt32(Console.ReadLine());
                        tmp.deposit(amount);
                    }
                    l.Add(tmp);
                }
                fs.Close();
                fs = new FileStream("Account.txt", FileMode.Create);
                fs.Close();
                fs = new FileStream("Account.txt", FileMode.Append, FileAccess.Write);
                for (int i = 0; i < l.Count; i++)
                {
                    bf.Serialize(fs, l[i]);
                }
                fs.Close();
            }
            else if(choice == "5")
            {
                break;
            }
        }
    }
}

}
