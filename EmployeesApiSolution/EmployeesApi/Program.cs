using System.Reflection;
using EmployeesApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRouting(options =>
{
    options.ConstraintMap.Add("bsonid", typeof(BsonIdConstaint));
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v1",
        Title = "Our Employee Api",
        Description = "This was for class. Don't expect a paycheck",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Danielle",
            Email = "daniell@aol.com"
        }
    });

    var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileName));
});

// Configure Options
// the IOptions<T> service is now going to be injectable anywhere we want.
builder.Services.Configure<MongoConnectionOptions>(builder.Configuration.GetSection(MongoConnectionOptions.SectionName));

builder.Services.AddScoped<ILookupEmployees, EmployeeLookup>(); // TODO talk about the russian doll thing.
builder.Services.AddScoped<IManageEmployees, EmployeeLookup>();

builder.Services.AddSingleton<EmployeesMongoDbAdapter>(); // MongoDb.Driver says make this a singleton.


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
