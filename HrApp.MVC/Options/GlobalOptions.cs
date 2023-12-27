namespace HrApp.MVC;

public class GlobalOptions
{
    public static TokenOptions TokenOptions { get; private set; }

    public static void Initialize(IConfiguration configuration)
    {
        TokenOptions = new TokenOptions();
        configuration.GetSection("TokenOptions").Bind(TokenOptions);
    }
}
