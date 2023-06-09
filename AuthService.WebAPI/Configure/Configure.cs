namespace AuthService.WebAPI.Configure;

public static class Configure
{
    public static void UseJwtApplication(this IApplicationBuilder app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
    }
}