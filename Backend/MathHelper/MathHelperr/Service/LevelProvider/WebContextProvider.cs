namespace MathHelperr.Service.LevelProvider;

public class WebContextProvider :IContextProvider
{
    private readonly IHttpContextAccessor _contextAccessor;

    //provides "Level" from incomming http request, implementation for web application, preparation for possibel desktop app
    public WebContextProvider(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public string GetLevel()
    {
        return _contextAccessor.HttpContext.Request.Headers["Level"];
    }
}