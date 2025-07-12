using Microsoft.EntityFrameworkCore;
using Gestao_Escolar.DbContext;
using Gestao_Escolar.Services;
using Gestao_Escolar;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(Program).Assembly);


 https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
    .UseSnakeCaseNamingConvention() 
    );


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


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("*") 
              .AllowAnyHeader() 
              .AllowAnyMethod(); 
    });
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
