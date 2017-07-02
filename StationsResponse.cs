using System.Collections.Generic;

namespace bikeshare
{
    public class StationsResponse
    {
        public string Type { get; set; }
        public List<Feature> Features { get; set; } = new List<Feature>();
    }
}
