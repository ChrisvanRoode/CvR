using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;

var options = new WebApplicationOptions
{
    Args = args,
    WebRootPath = "wwwroot"
};

var builder = WebApplication.CreateBuilder(options);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:5001")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDbSettings"));

builder.Services.AddSingleton<IMongoClient, MongoClient>(sp =>
    new MongoClient(builder.Configuration.GetValue<string>("MongoDbSettings:ConnectionString")));

builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer(); // Needed for .NET 6 and later
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ChrisAPI", Version = "v1" });
});

var app = builder.Build();

var projectRootPath = app.Environment.ContentRootPath;
var fileProvider = new PhysicalFileProvider(projectRootPath);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Chris API V1");
    });
}

app.UseCors();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = fileProvider,
    RequestPath = ""
});
app.UseRouting();
app.UseEndpoints(endpoints =>
    endpoints.MapControllers()
);

app.MapGet("/", async context =>
{
    var filePath = Path.Combine(app.Environment.ContentRootPath, "views/index.html");
    if (!File.Exists(filePath))
    {
        context.Response.StatusCode = 404;
        await context.Response.WriteAsync("File not found");
        return;
    }

    context.Response.ContentType = "text/html";
    await context.Response.WriteAsync(File.ReadAllText(filePath));
});

app.MapGet("/admin", async context =>
{
    var filePath = Path.Combine(app.Environment.ContentRootPath, "views/admin.html");
    if (!File.Exists(filePath))
    {
        context.Response.StatusCode = 404;
        await context.Response.WriteAsync("File not found");
        return;
    }

    context.Response.ContentType = "text/html";
    await context.Response.WriteAsync(File.ReadAllText(filePath));
});

app.Run();
