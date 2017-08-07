using System;
using System.Threading.Tasks;
using Proto;

namespace bikeshare
{
    public class StationActor : IActor
    {
        public Feature _station;
        public async Task ReceiveAsync(IContext context)
        {
            switch (context.Message)
            {
                case Update u:
                    if (_station != null) {
                        var diff = u.Station.Properties.BikesAvailable - _station.Properties.BikesAvailable;
                        if (diff != 0)
                        {
                            var verb = diff > 0 ? "gained" : "lost";
                            Console.WriteLine($"{DateTimeOffset.Now}: {_station.Properties.Name} {verb} {Math.Abs(diff)} bikes");
                        }
                    }

                    _station = u.Station;
                    break;
                case Stopping _:
                    Console.WriteLine($"Stopping {_station.Properties.Name}");
                    break;
            }
            await Actor.Done;
        }

        public class Update
        {
            public Feature Station{get;set;}
        }
    }
}
