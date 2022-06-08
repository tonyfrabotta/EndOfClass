using DemoApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();  // OAS 3.1 

// Configure your Services

builder.Services.AddTransient<ILookupCurrentStatus, StatusLookup>(); // Lazy setup.
builder.Services.AddTransient<ILookupDevelopers, DeveloperLookup>();

// Configure Adapters

var developerApiAddress = builder.Configuration.GetValue<string>("developerApi");

builder.Services.AddHostedService<BackgroundTask>(); // Channels

builder.Services.AddHttpClient<DeveloperApiAdapter>(httpClient =>
{
    // where you can do the configuration for the thing.
    httpClient.BaseAddress = new Uri(developerApiAddress);
})
    .AddPolicyHandler(ApiPolicies.GetClusterRetryPolicy())
.AddPolicyHandler(ApiPolicies.GetClusterCircuitBreakerPolicy());


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run(); // Period of time. 
