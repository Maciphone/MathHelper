namespace MathHelperr.Service.LevelProvider;

public class WebContextProvider :IContextProvider
{
    private readonly IHttpContextAccessor _contextAccessor;

    public WebContextProvider(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public string GetLevel()
    {
        return _contextAccessor.HttpContext.Request.Headers["Level"];
    }
}