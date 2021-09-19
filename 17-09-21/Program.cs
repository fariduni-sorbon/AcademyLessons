using System;
using System.Data.SqlClient;

namespace _17_09_21
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Data source=localhost;
                                        Initial Catalog=AcademySummer;
                                        Integrated Security=True";
            while (true)
            {
                Console.Write(@"
0. Exit
1. Create Account
2. Account list
3. Get Balance
4. Transfer from account to account
   Choice: ");
                int.TryParse(Console.ReadLine(), out var choice);
                switch (choice)
                {
                    case 1:
                        Console.Write("Account Name: "); string acc_name = Console.ReadLine();
                        Console.Write("Account Balans: "); int acc_balance = int.Parse(Console.ReadLine());
                        try
                        {
                            CreateAccount(acc_name, acc_balance, connectionString);
                        }
                        catch (Exception exc)
                        {
                            Console.WriteLine(exc.Message);
                        }
                        break;
                    case 2:
                        try
                        {
                            AccountList(connectionString);
                        }
                        catch (Exception exc)
                        {
                            Console.WriteLine(exc.Message);
                        }
                        break;
                    case 3:
                        Console.Write("Acc name:"); acc_name = Console.ReadLine();
                        try
                        {
                            Console.WriteLine(GetBalance(acc_name, connectionString));
                        }
                        catch (Exception exc)
                        {
                            Console.WriteLine(exc.Message);
                        }
                        break;
                    case 4:
                        try
                        {
                            Console.Write("From Accaunt: "); string from_acc = Console.ReadLine();
                            Console.Write("To Accaunt: "); string to_acc = Console.ReadLine();
                            Console.Write("Amount: "); int amount = int.Parse(Console.ReadLine());
                            Transfer(from_acc, to_acc, amount, connectionString);
                        }
                        catch (Exception exc)
                        {
                            Console.WriteLine(exc.Message);
                        }
                        break;
                    default:
                        Console.WriteLine("Wrong Command!");
                        break;
                }
                Console.WriteLine("Press any key...");
                Console.ReadLine();
                Console.Clear();
            }
            Console.ReadLine();
        }

        static void CreateAccount(string AccName, int Balance, string connString)
        {
            var connection = new SqlConnection(connString);
            Account account = new Account { Created_At = DateTime.Now, Name = AccName, Balance = Balance, Is_Active = 1 };
            if (account.Balance == 0)
            {
                account.Is_Active = 0;
            }
            var query = $@"Insert into Account(Account_Name,Is_Active,Balance,Created_At) values (@accName,@isActive,@accBalance,@createdAt)";
            var command = connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@createdAt", DateTime.Now);
            command.Parameters.AddWithValue("@accName", account.Name);
            command.Parameters.AddWithValue("@isActive", account.Is_Active);
            command.Parameters.AddWithValue("@accBalance", account.Balance);
            connection.Open();
            var result = command.ExecuteNonQuery();
            if (result > 0)
            {
                Console.WriteLine("Account created successfully.");
            }
            connection.Close();
        }
        static void AccountList(string connString)
        {
            var connection = new SqlConnection(connString);
            var query = "Select Id,Account_Name,Balance from Account";
            var command = connection.CreateCommand();
            command.CommandText = query;
            connection.Open();
            var sqlReader = command.ExecuteReader();
            while (sqlReader.Read())
            {
                Console.WriteLine($"ID: {sqlReader.GetValue(0)}\tName: {sqlReader.GetValue(1)}\tBalance: {sqlReader.GetValue(2)}");
            }
            sqlReader.Close();
            connection.Close();
        }
        static int GetBalance(string AccName, string connString)
        {
            var connection = new SqlConnection(connString);
            var query = $"select Balance from Account where Account_Name={AccName}";
            var command = connection.CreateCommand();
            command.CommandText = query;
            connection.Open();

            var balance = command.ExecuteScalar();
            return (int)balance;
        }
        static int GetAccId(string AccName, string connString)
        {
            var connection = new SqlConnection(connString);
            var query = $"select Id from Account where Account_Name={AccName}";
            var command = connection.CreateCommand();
            command.CommandText = query;
            connection.Open();
            var Id = command.ExecuteScalar();
            return (int)Id;
        }
        static void Transfer(string FromAcc, string ToAcc, int Amount, string connString)
        {
            if (string.IsNullOrEmpty(FromAcc) || string.IsNullOrEmpty(ToAcc) || Amount == 0)
            {
                Console.WriteLine("Something went wrong.");
                return;
            }
            var connection = new SqlConnection(connString);
            connection.Open();
            SqlTransaction sqlTransaction = connection.BeginTransaction();
            var command = connection.CreateCommand();
            command.Transaction = sqlTransaction;
            try
            {
                var fromAccBalance = GetBalance(FromAcc, connString);
                if (fromAccBalance <= 0 || (fromAccBalance - Amount) < 0)
                {
                    throw new Exception("From account balance not enough amount");
                }
                var fromAccId = GetAccId(FromAcc, connString);
                if (fromAccId == 0)
                {
                    throw new Exception("Account not found");
                }

                command.CommandText = @"INSERT INTO [dbo].[Transactions]([Amount]
             ,[Created_At] ,[Account_Id]) VALUES(@amount , @createdAt, @accountId)";

                command.Parameters.AddWithValue("@amount", Amount);
                command.Parameters.AddWithValue("@createdAt", DateTime.Now);
                command.Parameters.AddWithValue("@accountId", fromAccId);
                var result1 = command.ExecuteNonQuery();

                var toAccId = GetAccId(ToAcc, connString);
                if (fromAccId == 0)
                {
                    throw new Exception("Account not found");
                }
                command.Parameters.Clear();
                command.CommandText = @"INSERT INTO [dbo].[Transactions]([Amount]  ,[Created_At] ,[Account_Id]) 
                                        VALUES(@amount , @createdAt, @accountId)";
                command.Parameters.AddWithValue("@amount", Amount);
                command.Parameters.AddWithValue("@createdAt", DateTime.Now);
                command.Parameters.AddWithValue("@accountId", toAccId);
                var result2 = command.ExecuteNonQuery();

                command.CommandText = $"Update Account set Balance=Balance-{Amount} where Id={fromAccId}" +
                                      $"Update Account set balance=Balance+{Amount} where Id={toAccId}";
                int result3 = command.ExecuteNonQuery();
                if (result1 == 0 || result2 == 0 || result3 == 0)
                {
                    throw new Exception("Something went wrong");
                }
                sqlTransaction.Commit();

                Console.WriteLine("Transfer completed successfully.");
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
                sqlTransaction.Rollback();
                Console.WriteLine("Transfer not completed.");
            }
            finally
            {
                connection.Close();
            }
        }
    }

    class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Is_Active { get; set; }
        public int Balance { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Updated_At { get; set; }
    }
    class Transaction
    {
        public int Id { get; set; }
        public int Account_Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime Created_At { get; set; }
    }
}
