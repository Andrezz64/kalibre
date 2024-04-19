using kalibre_api;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Injetando os repositorios como dependencia
builder.Services.AddScoped<IDespesaRepository, DespesaRepository>();
builder.Services.AddScoped<IReceitaRepository, ReceitaRepository>();
builder.Services.AddScoped<IDashboardRepository, DashboardRepository>();
builder.Services.AddScoped<IRelatorioRepository, RelatorioRepository>();


builder.Services.AddDbContext<KalibreContext>(options =>{}); // Injetando DbContext como dependência

builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>{
    options.AddPolicy(
        name: "Dev", 
        policy =>{
            policy.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
        });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseCors("Dev");
app.UseAuthorization();

app.MapControllers();

app.Run();
