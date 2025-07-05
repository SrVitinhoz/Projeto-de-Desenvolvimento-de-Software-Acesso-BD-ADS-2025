using Microsoft.EntityFrameworkCore;
using Gestao_Escolar.DbContext;
using Gestao_Escolar.Services;  
using Gestao_Escolar;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(Program).Assembly);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Confi conexão com o banco de dados
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

//criar um perfil de mapeamento específico
/*builder.Services.AddAutoMapper(typeof(MappingProfile));*/

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
    .UseSnakeCaseNamingConvention() // Adiciona a convenção de nomenclatura snake_case
    );

// Registrar serviços
builder.Services.AddScoped<ITurmaService, TurmaService>();
builder.Services.AddScoped<IMateriaService, MateriaService>();
builder.Services.AddScoped<IFuncionarioService, FuncionarioService>();
builder.Services.AddScoped<IAlunoService, AlunoService>();
builder.Services.AddScoped<IEventoService, EventoService>();
builder.Services.AddScoped<IChamadaService, ChamadaService>();
builder.Services.AddScoped<IChamadaAlunoService, ChamadaAlunoService>();
builder.Services.AddScoped<IHistoricoSonhosService, HistoricoSonhosService>();
builder.Services.AddScoped<ITransferenciaTurmaService, TransferenciaTurmaService>();
builder.Services.AddScoped<IParticipacaoEventoService, ParticipacaoEventoService>();

var app = builder.Build();



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
