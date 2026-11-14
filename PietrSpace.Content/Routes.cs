namespace PietrSpace.Content;

public static class Routes
{
    private const string PublicPrefix = "pub";
    private const string InternalPrefix = "internal";

    public static class Controller
    {
        public static class Content
        {
            private const string Base = "content";

            public static class Version2
            {
                private const string Base = $"{Controller.Content.Base}/v2";
                public const string Public = $"{PublicPrefix}/{Base}";
                public const string Internal = $"{InternalPrefix}/{Base}";
            }
        }
    }
}