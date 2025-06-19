using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Society_Management_System.Model;
using Society_Management_System.Model.ComplaintsRepo;
using Society_Management_System.Model.FlatsRepo;
using Society_Management_System.Model.BillsRepo;
using Society_Management_System.Model.NoticesRepo;
using Society_Management_System.Model.BookingsRepo;
using Society_Management_System.Model.VisitorsRepo;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;
using Stripe;
using Society_Management_System.Model.SocietyRepo;
using Society_Management_System.Services.EmailService;
using Hangfire;
using Society_Management_System.Model.JobRepo;

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
builder.Services.AddScoped<IBillsRepository , BillsRepository>();
builder.Services.AddScoped<INoticesReopsitory , NoticesRepository>();
builder.Services.AddScoped<IBookingRepository , BookingRepository>();
builder.Services.AddScoped<IVisitorsRepository , VisitorsRepository>();
builder.Services.AddScoped<ISocietyRepository , SocietyRepository>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IJobRepository, JobRepository>();


//hangfire

builder.Services.AddHangfire((sp, config) =>
{
    var connectionStringHangFire = sp.GetRequiredService<IConfiguration>().GetConnectionString("SocietyConnection");
    config.UseSqlServerStorage(connectionStringHangFire);

});

builder.Services.AddHangfireServer();


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

//payment (Stripe)  
StripeConfiguration.ApiKey = "sk_test_tR3PYbcVNZZ796tH88S4VQ2u";




//builder.Services.AddMemoryCache();

// Add CORS policy with chat
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            //policy.WithOrigins("http://localhost:4200") // Angular's dev server
            policy.WithOrigins("https://wizardamansociety.netlify.app") // netilify's dev server

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
app.UseRouting();
app.UseStaticFiles();
app.UseEndpoints(endpoints => endpoints.MapControllers());
app.UseHangfireDashboard();
app.UseHangfireServer();

app.Run();
