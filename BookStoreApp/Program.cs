using Business.Interface;
using Business.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Repository.Context;
using Repository.Interface;
using Repository.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container..
builder.Services.AddSingleton<DapperContext>();
builder.Services.AddScoped<IUserRL, UserRL>();
builder.Services.AddScoped<IOrderRL, OrderRL>();
builder.Services.AddScoped<IAddressRL, AddressRL>();
builder.Services.AddScoped<IWishListRL, WishListRL>();
builder.Services.AddScoped<ICartRL, CartRL>();
builder.Services.AddScoped<IBookRL, BookRL>();

builder.Services.AddScoped<IUserBL, UserBL>();
builder.Services.AddScoped<IOrderBL, OrderBL>();
builder.Services.AddScoped<IAddressBL, AddressBL>();
builder.Services.AddScoped<ICartBL, CartBL>();
builder.Services.AddScoped<IWhishListBL, WishListBL>();
builder.Services.AddScoped<IBookBL, BookBL>();
builder.Services.AddControllers();

// Configure JWT authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = true;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["JwtSettings:Authorization"])),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = false
    };
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Bookstore Swagger UI",
        Description = "Swagger UI for Implementation on Bookstore API",
    });

    //Swagger Authorization Implemantation 
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                     {
                         new OpenApiSecurityScheme
                         {
                             Reference = new OpenApiReference
                             {
                                 Type=ReferenceType.SecurityScheme,
                                 Id="Bearer"
                             }
                         },
                          new string[]{}
                     }
                 });

});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
app.UseCors(
  options => options.WithOrigins("*").AllowAnyMethod().AllowAnyHeader()
      );
app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI(c => {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Showing BookStore API V1");
});
app.Run();
