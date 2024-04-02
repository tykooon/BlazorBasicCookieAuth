using BlazorWebAppCustomAuth.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlazorWebAppCustomAuth.Authentication;

public static class EndpointHandlers
{
    internal static async Task<IResult> LoginAsync(
        [FromBody] LoginModel loginModel,
        [FromServices] SignInManager<AppUser> signInManager)
    {
        if (loginModel == null)
        {
            return Results.BadRequest();
        }

        var res = await signInManager.PasswordSignInAsync(loginModel.Username, loginModel.Password, true, false);
        if (res.Succeeded)
        {
            return Results.Ok();
        }

        return Results.Unauthorized();
    }

    internal static async Task<IResult> LogoutAsync(
        [FromServices] SignInManager<AppUser> signInManager)
    {
        await signInManager.SignOutAsync();
        return Results.Ok();
    }

    internal static async Task<IResult> RegisterAsync(
        [FromBody] RegisterModel registerModel,
        [FromServices] UserManager<AppUser> userManager)
    {
        if (registerModel == null)
        {
            return Results.BadRequest();
        }

        var user = new AppUser { UserName = registerModel.Username };
        var res = await userManager.CreateAsync(user, registerModel.Password);

        if (res.Succeeded)
        {
            return Results.Ok();
        }

        return Results.BadRequest(string.Join(", ", res.Errors.Select(er => er.Description)));
    }
}
