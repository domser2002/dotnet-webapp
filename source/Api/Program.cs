using Domain.Abstractions;
using Frontend.Validators;
using Frontend.Validators.Abstractions;
using Infrastructure.Repositories;
using Infrastructure.FakeRepositories;
using Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http.Features;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IOfferRepository, OfferRepository>();
builder.Services.AddSingleton<IInquireRepository, InquireRepository>();
builder.Services.AddSingleton<IContactInformationRepository, ContactInformationRepository>();
builder.Services.AddSingleton<IRequestRepository, RequestRepository>();
int minStringLength = 3;
int maxStringLength = 15;
int minDimension = 0;
int maxDimension = 50;
int minWeight = 0;
int maxWeight = 20;
builder.Services.AddSingleton<IRegistrationValidator>(provider =>
{
    return new RegistrationValidator(minStringLength, maxStringLength);
});
builder.Services.AddSingleton<IContactInformationValidator>(provider =>
{
    return new ContactInformationValidator(minStringLength, maxStringLength);
});
builder.Services.AddSingleton<IInquireValidator>(provider =>
{
    return new InquireValidator(minDimension, maxDimension, minWeight, maxWeight, minStringLength, maxStringLength);
});

builder.Services.Configure<IISServerOptions>(options =>
{
    options.AllowSynchronousIO = true;
});

builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = long.MaxValue;
});

// Konfiguracja autentykacji JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "https://dev-16zwhx0tnshxv7h2.us.auth0.com";
        options.Audience = "oq9dIThIfvXR71YXAvPqahwjpARlTGf7"; // Nazwa audytorium z Auth0
        options.TokenValidationParameters = new TokenValidationParameters
        {
            RoleClaimType = "role", // Klucz roli w tokenie
        };
    });

// Konfiguracja autoryzacji
builder.Services.AddAuthorization();

var app = builder.Build();
// Configure the HTTP request pipeline.

// Dodanie autentykacji do potoku middleware
app.UseAuthentication();
app.UseAuthorization();

// Cors
app.UseCors(builder => builder.WithOrigins("http://localhost:3000", "https://courierhubreact.azurewebsites.net").AllowAnyMethod().AllowAnyHeader());

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
