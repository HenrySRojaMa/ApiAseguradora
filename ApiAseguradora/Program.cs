using Business.Aseguradoras;
using Business.Clientes;
using Business.Contratos;
using Data;
using Data.Aseguradoras;
using Data.Clientes;
using Data.Contratos;
using Models.Profiles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<SegurosContext>();
builder.Services.AddCors(cores => cores.AddPolicy("AllowWebApp", bull => bull.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().WithExposedHeaders("Content-Disposition")));
builder.Services.AddAutoMapper(typeof(ClientProfile), typeof(InsurerProfile), typeof(ContractProfile));

builder.Services.AddScoped<ITransactionMappings, TransactionMappings>();

builder.Services.AddScoped<IClienteMappings, ClienteMappings>();
builder.Services.AddScoped<IClienteBusiness, ClienteBusiness>();

builder.Services.AddScoped<IAseguradoraMappings, AseguradoraMappings>();
builder.Services.AddScoped<IAseguradoraBusiness, AseguradoraBusiness>();

builder.Services.AddScoped<IContratoMappings, ContratoMappings>();
builder.Services.AddScoped<IContratoBusiness, ContratoBusiness>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors("AllowWebApp");
app.MapControllers();

app.Run();
