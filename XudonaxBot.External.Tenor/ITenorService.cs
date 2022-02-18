namespace XudonaxBot.External.Tenor
{
    public interface ITenorService
    {
        Task<string?> GetRandomGifFor(string searchText);
    }
}
