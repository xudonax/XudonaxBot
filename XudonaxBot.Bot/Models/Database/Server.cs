﻿namespace XudonaxBot.Bot.Models.Database
{
    public class Server
    {
        public Guid ServerId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
