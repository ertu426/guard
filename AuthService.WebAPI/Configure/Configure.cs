namespace AuthService.WebAPI.Configure;

public static class Configure
{
    public static void UseApplication(this IApplicationBuilder app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
    }
}