using System;
using System.Threading.Tasks;
using Proto;
using RestEase;
using System.Collections.Generic;
using Proto.Schedulers.SimpleScheduler;
using System.Linq;

namespace bikeshare
{
    public class MetroBikeShareActor : IActor
    {
        private ISimpleScheduler scheduler = new SimpleScheduler();
        private readonly Behavior _behavior;
        private readonly IMetroBikeShareApi _metroBikeShareApi;
        private Dictionary<string, PID> _stationActors;
        public MetroBikeShareActor()
        {
            _metroBikeShareApi = RestClient.For<IMetroBikeShareApi>("https://bikeshare.metro.net");
            _stationActors = new Dictionary<string, PID>();
            _behavior = new Behavior();
            _behavior.Become(Refreshing);
        }

        public async Task ReceiveAsync(IContext context)
        {
            await _behavior.ReceiveAsync(context);
        }

        private async Task Refreshing(IContext context)
        {
            var response = await _metroBikeShareApi.GetStationsAsync();

            response.Features
                .ForEach(station => {
                    var name = station.Properties.Name;
                    if (!_stationActors.TryGetValue(name, out PID pid))
                    {
                        pid = context.Spawn(Actor.FromProducer(() => new StationActor()));
                        _stationActors.Add(name, pid);
                    }

                    pid.Tell(new StationActor.Update
                    {
                        Station = station,
                    });
                });
            
            var keepers = new HashSet<string>(response.Features.Select(s => s.Properties.Name));
             _stationActors
                .Where(x => !keepers.Contains(x.Key))
                .Select(x => x.Value)
                .ToList()
                .ForEach(pid => pid.Stop());
            
            _behavior.Become(Running);
            context.Self.Tell(new Run());
            await Actor.Done;
        }

        private async Task Running(IContext context)
        {
            switch (context.Message)
            {
                case Run _:
                    scheduler.ScheduleTellOnce(TimeSpan.FromSeconds(5), context.Self, new Refresh());
                    break;
                case Refresh _:
                    _behavior.Become(Refreshing);
                    context.Self.Tell(new object());
                    break;
            }
            await Actor.Done;
        }

        public class Run { }
        public class Refresh { }
    }
}
