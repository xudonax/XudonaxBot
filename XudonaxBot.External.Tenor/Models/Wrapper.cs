namespace XudonaxBot.External.Tenor.Models
{
    internal class Wrapper
    {
        public string Next { get; set; } = string.Empty;
        public List<GifObject> Results { get; set; } = new List<GifObject>();
    }
}
