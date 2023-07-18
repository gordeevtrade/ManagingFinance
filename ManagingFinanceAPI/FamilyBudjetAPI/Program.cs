using Budget.BuisnessLogic.Sevices;
using Budget.BuisnessLogic.Sevices.Interface;
using Budget.DAL.Repositories;
using Budget.DAL.Repositories.Interfaces;
using Domain.ServicesInterface;
using FamilyBudjetAPI;
using FamilyBudjetAPI.Mapping;
using FamilyBudjetAPI.Sevices;
using FamilyBudjetAPI.Sevices.Interface;
using ManagingFinance.BuisnessLogic.Sevices;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

string connection = builder.Configuration.GetConnectionString("DefaultConnection");
string jwtSecretKey = builder.Configuration["MyTokens:JwtSecretKey"];

//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//}).AddJwtBearer(options =>
//{
//    options.Authority = builder.Configuration["GoogleAuth:Authority"];
//    options.Audience = builder.Configuration["GoogleAuth:ClientId"];
//    options.MetadataAddress = builder.Configuration["GoogleAuth:GoogleKeys"];
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidateLifetime = true,
//        ValidateIssuerSigningKey = true
//    };
//});

builder.Services.AddCors(options => options.AddPolicy(name: "ManagingFinance",
    policy =>
    {
        policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
    }));

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "User";
    options.DefaultAuthenticateScheme = "User";
    options.DefaultChallengeScheme = "User";
})
.AddJwtBearer("Google", options =>
{
    options.Authority = builder.Configuration["GoogleAuth:Authority"];
    options.Audience = builder.Configuration["GoogleAuth:ClientId"];
    options.MetadataAddress = builder.Configuration["GoogleAuth:GoogleKeys"];
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    };
})
.AddJwtBearer("User", options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["UserAuth:Issuer"],
        ValidAudience = builder.Configuration["UserAuth:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("123123123_іваіва23423423847а8мг3489347а8г3948е7348к7рівапорапіоапіо34757465765вапвапвамвапвапкапвапвап"))
    };
});

builder.Services.AddDbContext<FinanceContext>(options =>
{
    options.UseSqlServer(connection);
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

builder.Services.AddScoped<IGoogleAuthService, GoogleAuthService>();
builder.Services.AddScoped<IUnitOfWOrk, UnitOfWork>();
builder.Services.AddScoped<ICategoryTypeService, CategoryTypeService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<IFinanceReportService, FinanceReportService>();
builder.Services.AddScoped<IStatisticstService, StatisticsService>();
builder.Services.AddScoped<IStatisticstService, StatisticsService>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddAutoMapper(typeof(AppMappingProfile));

builder.Services.AddControllers();
builder.Services.AddAuthorization();

builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseRouting();
app.UseCors("ManagingFinance");
app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<FinanceContext>();
    dbContext.Database.Migrate();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();