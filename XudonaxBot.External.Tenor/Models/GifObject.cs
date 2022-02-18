namespace XudonaxBot.External.Tenor.Models
{
    internal class GifObject
    {
        public float Created { get; set; } = 0;
        public bool HasAudio { get; set; } = false;
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string ItemUrl { get; set; } = string.Empty;
        public bool HasCaption { get; set; } = false;
        public string Url { get; set; } = string.Empty;
        public List<string> Tags { get; set; } = new List<string>();
        public List<Dictionary<string, MediaObject>> Media { get; set; } = new List<Dictionary<string, MediaObject>>();
    }
}
