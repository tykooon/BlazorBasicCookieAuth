using BlazorWebAppCustomAuth.Authentication;
using static BlazorWebAppCustomAuth.Authentication.EndpointHandlers;
using BlazorWebAppCustomAuth.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDbContext<UserDbContext>(opt => opt.UseInMemoryDatabase("UserDb"));

builder.Services.AddIdentityCore<AppUser>()
    .AddEntityFrameworkStores<UserDbContext>()
    .AddSignInManager<SignInManager<AppUser>>()
    .AddUserManager<UserManager<AppUser>>();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddAuthentication(opt => opt.DefaultScheme = IdentityConstants.ApplicationScheme)
    .AddCookie(IdentityConstants.ApplicationScheme);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapPost("/login", LoginAsync);
app.MapPost("/register", RegisterAsync);
app.MapPost("/logout", LogoutAsync);

app.Run();