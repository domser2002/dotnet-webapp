using Api.Infrastructure;
using Domain.Abstractions;
using Frontend.Validators;
using Frontend.Validators.Abstractions;
using Infrastructure;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IOfferRepository, OfferRepository>();
builder.Services.AddSingleton<IInquireRepository, InquireRepository>();
builder.Services.AddSingleton<IContactInformationRepository, ContactInformationRepository>();
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
    return new InquireValidator(minDimension, maxDimension, minWeight, maxWeight);
});

var app = builder.Build();
// Configure the HTTP request pipeline.

// Cors
app.UseCors(builder => builder.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader());

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
