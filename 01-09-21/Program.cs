using System;

namespace _01_09_21
{
    class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player();
            player.Play();
            player.Pause();
            player.Stop();

            Console.WriteLine("\n-------------------------------\n");

            IRecordable recordable;
            recordable = player;
            recordable.Record();
            recordable.Pause();
            recordable.Stop();

            Console.ReadLine();
        }
    }

    class Player : IPlayeble, IRecordable
    {
        public void Pause()
        {
            Console.WriteLine("Playing pause");
        }

        public void Play()
        {
            Console.WriteLine("Start playing");
        }

        public void Stop()
        {
            Console.WriteLine("Stop playing");
        }

        void IRecordable.Pause()
        {
            Console.WriteLine("Pause recording");
        }

        void IRecordable.Record()
        {
            Console.WriteLine("Start recording");
        }

        void IRecordable.Stop()
        {
            Console.WriteLine("Stop recording");
        }
    }

    public interface IPlayeble
    {
        void Play();
        void Pause();
        void Stop();
    }

    public interface IRecordable
    {
        void Record();
        void Pause();
        void Stop();
    }
}

