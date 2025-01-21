using Microsoft.EntityFrameworkCore;
using ToDo.Context;
using ToDo.Contracts;
using ToDo.GenericRepository;
using ToDo.Repository;
using ToDo.Services;
using DotNetEnv;


namespace ToDo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Registrar serviços
            builder.Services.AddScoped<ICategoria, CategoriaService>();
            builder.Services.AddScoped<ITarefa, TarefaService>();
            builder.Services.AddScoped<IUsuario, UsuarioService>();
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddScoped<UsuarioRepository>();
            builder.Services.AddScoped<TarefaRepository>();
            builder.Services.AddScoped<CategoriaRepository>();

            Env.Load("./dockerconfig.env");
            // Configuração de banco de dados
            var server = Environment.GetEnvironmentVariable("DB_SERVER");
            var database = Environment.GetEnvironmentVariable("DB_NAME");
            var user = Environment.GetEnvironmentVariable("DB_USER");
            var password = Environment.GetEnvironmentVariable("DB_PASSWORD");

            var connectionString = $"Server={server};Database={database};User Id={user};Password={password};TrustServerCertificate=True;";


            builder.Services.AddDbContext<Context.AppDbContext>(options =>
                options.UseSqlServer(connectionString));

            // Configuração de CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .WithExposedHeaders("Access-Control-Allow-Origin", "Access-Control-Allow-Headers", "Access-Control-Allow-Methods");
                });
            });

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();

            // Descomente caso queira usar HTTPS no Docker, mas isso pode exigir mais configurações (certificados)
            //app.UseHttpsRedirection();

            // Ativar autorização
            app.UseAuthorization();

            // Mapear os controllers
            app.MapControllers();
            app.UseCors("AllowAllOrigins");
            app.Urls.Add("http://0.0.0.0:8080");

            System.Globalization.CultureInfo.DefaultThreadCurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
            System.Globalization.CultureInfo.DefaultThreadCurrentUICulture = System.Globalization.CultureInfo.InvariantCulture;


            // Definir a URL para o Kestrel ouvir em todas as interfaces e na porta 8080

            app.Run();
        }
    }
}
