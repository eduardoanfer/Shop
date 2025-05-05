using System.Text;
using Microsoft.EntityFrameworkCore;
using Shop;
using Shop.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;


var builder = WebApplication.CreateBuilder(args);
var key = Encoding.ASCII.GetBytes(Setings.Secret);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddResponseCompression(options =>
{
    options.Providers.Add<GzipCompressionProvider>(); // zipar as infos e mandar para tela
    options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/json" });
    // quero comprimir tudo que for aplication.json
});
builder.Services.AddResponseCaching();
builder.Services.AddControllers(); // Adiciona suporte para controllers
builder.Services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("Database")); //qual banco
builder.Services.AddAuthentication(x => 
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };

});
// builder.Services.AddDbContext<DataContext>(
//     opt=> 
//         opt.UseSqlServer(builder
//             .Configuration
//             .GetConnectionString("connectionstring")));

    
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapControllers(); // Mapeia os endpoints dos controllers
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseAuthentication();
    app.UseAuthorization();
}

app.UseHttpsRedirection();





app.Run();

