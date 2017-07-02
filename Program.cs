using System;
using Proto;

namespace bikeshare
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting actor");
            var props = Actor.FromProducer(() => new MetroBikeShareActor());
            var pid = Actor.Spawn(props);

            Console.ReadLine();
        }
    }
}
