using System;
using System.Data.SqlClient;
namespace _13_09_21
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Data Source=localhost;
                                        Initial Catalog=UserDB;
                                        Integrated Security=True";

            Person person = new Person();

            try
            {
                Console.Write("Person first name: ");
                string firstname = Console.ReadLine();
                Console.Write("Person last name: ");
                string lastname = Console.ReadLine();
                Console.Write("Person birthday: ");
                DateTime birthday = DateTime.Parse(Console.ReadLine());

                person.FirstName = firstname;
                person.LastName = lastname;
                person.BirthDate = birthday;
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                try
                {
                    InsertPerson(connection, person);
                }
                catch (Exception exc)
                {
                    Console.WriteLine(exc.Message);
                }
                SelectPerson(connection);
                //SelectPersonById(connection,3);
                //UpdatePerson(connection, 1, person);
                //DeletePerson(connection, 1);
            }
            Console.ReadLine();
        }


        static void InsertPerson(SqlConnection connection,Person person)
        {
            var sqlQuery = $"Insert into Person (FirstName, LastName, BirthDate) " +
                $"values" +
                $"('{person.FirstName}', '{person.LastName}', {person.BirthDate}) ";
            var sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = sqlQuery;
            var result = sqlCommand.ExecuteNonQuery();
            if (result>0)
            {
                Console.WriteLine("The Person is added.");
            }
        }
        static void SelectPerson(SqlConnection connection)
        {
            var sqlQuery = @"
            SELECT 
             Id,  
             LastName, 
            FirstName,
            BirthDate
            FROM Person";
            var sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = sqlQuery;
            var sqlReader = sqlCommand.ExecuteReader();
            while (sqlReader.Read())
            {
                Console.WriteLine($"ID: {sqlReader.GetValue(0)}\t" +
                                  $"LastName: {sqlReader.GetValue(2)}\t" +
                                  $"FirstName: { sqlReader.GetValue(1)}\t" +
                                  $"BirthDate: { sqlReader.GetValue(3)}");

            }
            sqlReader.Close();
        }
        static void SelectPersonById(SqlConnection connection,int personId)
        {
            var sqlQuery = $@"
            SELECT 
             Id,  
             LastName, 
            FirstName,
            BirthDate
            FROM Person where Id ={personId} ";
            var sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = sqlQuery;
            var sqlReader = sqlCommand.ExecuteReader();
            while (sqlReader.Read())
            {
                Console.WriteLine($"ID: {sqlReader.GetValue(0)}\t" +
                                 $"LastName: {sqlReader.GetValue(2)}\t" +
                                 $"FirstName: { sqlReader.GetValue(1)}\t" +
                                 $"BirthDate: { sqlReader.GetValue(3)}");

            }
            sqlReader.Close();
        }
        static void UpdatePerson(SqlConnection connection , int personId, Person person)
        {
            var sqlQuery = $"Update Person set" +
                $"LastName='{person.LastName}'" +
                $"FirstName='{person.FirstName}'" +
                $"BirthDate={person.BirthDate}";

            var sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = sqlQuery;
            var result = sqlCommand.ExecuteNonQuery();
            if (result>0)
            {
                Console.WriteLine("Person updated.");
            }
            else
            {
                Console.WriteLine($"Person with this Id: {personId} not found!");
            }
        }
        static void DeletePerson(SqlConnection connection, int personId)
        {
            var sqlQuery = $"Delete Person where Id ={personId}";
            var sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = sqlQuery;
            var result = sqlCommand.ExecuteNonQuery();
            if (result > 0)
            {
                Console.WriteLine("Person deleted.");
            }
            else
            {
                Console.WriteLine($"Person with this Id: {personId} not found!");
            }
        }
    }

    class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
