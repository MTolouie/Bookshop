namespace Bookshop_BlazorServer.Utilities;

public static class WwwRootUrlProvider
{
    public static string GetAuthorAvatarPath(this IHttpContextAccessor _httpContextAccessor)
    {
        return $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host.Value}/AdminLayout/images/AuthorAvatar/";
    }

    public static string GetBookImagePath(this IHttpContextAccessor _httpContextAccessor)
    {
        return $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host.Value}/AdminLayout/images/BookImage/";
    }

    public static string GetGalleryImagePath(this IHttpContextAccessor _httpContextAccessor)
    {
        return $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host.Value}/AdminLayout/images/BookImage/Gallery/";
    }
    public static string GetSliderImagePath(this IHttpContextAccessor _httpContextAccessor)
    {
        return $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host.Value}/AdminLayout/images/SliderImage/";
    }


}
