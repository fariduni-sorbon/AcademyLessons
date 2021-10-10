using System;
using System.Collections.Generic;
using System.Threading;

namespace _08_10_21
{
    class Program
    {
        static object locker = new object();
        static Client SelectedClient = new Client();
        static List<Client> clients = new List<Client>()
            {
                new Client{ Id=1,Balance=100},
                new Client{ Id=2,Balance=120},
                new Client{ Id=3,Balance=300},
                new Client{ Id=4,Balance=450},
                new Client{ Id=5,Balance=543}
            };

        static void Main(string[] args)
        {
            Console.WriteLine("Clients List:");
            foreach (var client in clients)
            {
                Console.WriteLine($"{client.Id}. Balance = {client.Balance}");
            }
            try
            {
                Thread thread1 = new Thread(new ThreadStart(InsertClient));
                Thread thread2 = new Thread(new ParameterizedThreadStart(SelectClient));
                Thread thread3 = new Thread(new ParameterizedThreadStart(UpdateClient));
                Thread thread4 = new Thread(new ParameterizedThreadStart(DeleteClient));
                Console.WriteLine("\nInsert new Client");
                thread1.Start();
                thread2.Start(3);
                thread3.Start(3);
                //---------------------------------------------------------------------------
                Console.WriteLine("\nSelect Client with id=3");
                if (SelectedClient.Balance!=0)
                {
                    Console.WriteLine($"{SelectedClient.Id}. Balance = {SelectedClient.Balance}");

                }
                //---------------------------------------------------------------------------
                Console.WriteLine("\nUpdate Client balance with Id=3");
                foreach (var client in clients)
                {
                    Console.WriteLine($"{client.Id}. Balance = {client.Balance}");
                }
                thread4.Start(6);
                //---------------------------------------------------------------------------
                Console.WriteLine("\nDelete Client with Id=6");

                foreach (var client in clients)
                {
                    Console.WriteLine($"{client.Id}. Balance = {client.Balance}");
                }
            }
            catch (Exception exc )
            {
                Console.WriteLine(exc.Message);
            }
            Console.ReadLine();
        }
        //-----------------------------------------------
        //-----------------------------------------------
        static void InsertClient()
        {
            lock (locker)
            {
                Client client = new Client();
                client.Id = 6;
                client.Balance = 600;
                clients.Add(client);
            }
        }

        static void SelectClient( object id)
        {           
            int x = (int)id;
            Client client = clients.Find((Client c) => { return c.Id == x; });
            SelectedClient = client;  
            Thread.Sleep(0);
        }

        static void UpdateClient(object id)
        {
            lock (locker)
            {
                int x = (int)id;
                Client client = clients.Find((Client c) => { return c.Id == x; });
                client.Balance = 340;
            }
        Thread.Sleep(500);

        }

        static void DeleteClient(object id)
        {
            lock (locker)
            {
                int x = (int)id;
                Client client = clients.Find((Client c) => { return c.Id == x; });
                clients.Remove(client);
            }
            Thread.Sleep(0);
        }
        //-----------------------------------------------
        //-----------------------------------------------
    }

    class Client
    {
        public int Id { get; set; }
        public int Balance { get; set; }       
    }
}
