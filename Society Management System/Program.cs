using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Society_Management_System.Model;
using Society_Management_System.Model.ComplaintsRepo;
using Society_Management_System.Model.FlatsRepo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




//Connection String

var connectionString = builder.Configuration.GetConnectionString("SocietyConnection") ?? throw new InvalidOperationException("Connection string 'SocietyConnection' not found.");
builder.Services.AddDbContext<SocietyContext>(options => options.UseSqlServer(connectionString));


builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
}).AddEntityFrameworkStores<SocietyContext>();


builder.Services.AddScoped<IComplaintsRepository , ComplaintsRepository>();
builder.Services.AddScoped<IFlatsRepository , FlatsRepository>();


//SignalR

builder.Services.AddSignalR();

//JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["JwtConfig:Issuer"],
        ValidAudience = builder.Configuration["JwtConfig:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtConfig:Key"]!)),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true

    };

});
builder.Services.AddAuthentication();

//builder.Services.AddMemoryCache();

// Add CORS policy with chat
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200") // Angular's dev server
             // netilify's dev server
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials();
        });
});


var app = builder.Build();
// Enable CORS
app.UseCors("AllowAll");    

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
