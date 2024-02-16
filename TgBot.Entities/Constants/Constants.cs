namespace TgBot.Entities.Constants;

public static class Constants
{
    public static class Emoji
    {
        public const string Eyes = "👀";
        public const string Heart = "❤️";
        public const string ThumbDown = "👎";
    }

    public static class CommandNames
    {
        public const string Start = "/start";
        public const string Search = "Поиск мероприятий";
        public const string Like = Emoji.Heart;
        public const string Dislike = Emoji.ThumbDown;
    }

}