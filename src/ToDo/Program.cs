
using Microsoft.EntityFrameworkCore;
using ToDo.Context;
using ToDo.Contracts;
using ToDo.GenericRepository;
using ToDo.Repository;
using ToDo.Services;

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

            builder.Services.AddDbContext<Context.Context>(opt => opt.UseInMemoryDatabase("ToDoList"));

            builder.Services.AddScoped<ICategoria, CategoriaService>();
            builder.Services.AddScoped<ITarefa, TarefaService>();
            builder.Services.AddScoped<IUsuario, UsuarioService>();


            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddScoped<UsuarioRepository>();



            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
