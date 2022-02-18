namespace XudonaxBot.External.Tenor.Models
{
    internal class MediaObject
    {
        public string Preview { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public int Size { get; set; } = 0;
        public int[] Dims { get; set; } = Array.Empty<int>();

        public int Width => (Dims.Length == 2) ? Dims[0] : 0;
        public int Height => (Dims.Length == 2) ? Dims[1] : 0;
    }
}
