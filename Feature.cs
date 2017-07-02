namespace bikeshare
{
    public class Feature
    {
        public string Type { get; set; }
        public Geometry Geometry { get; set; }
        public StationProperties Properties {get;set;} = new StationProperties();
    }
}
