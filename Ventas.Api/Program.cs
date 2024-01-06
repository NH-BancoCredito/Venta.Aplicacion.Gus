//se agregs referencia

using Ventas.Application;
using Venta.Infrastructure;
//se agrega referencia

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


 

//se agregaby ñlp yñu   
//Capa de aplicacion-´
builder.Services.AddAplication(); 

 

var connectionString = builder.Configuration.GetConnectionString("dbVenta-cnx");

builder.Services.AddInfraestructure(connectionString);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();


//Adicionar Middleware customizado para tratar las excepciones
//app.

app.MapControllers();

app.Run();
