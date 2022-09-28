using Hangfire;
using Hangfire.MemoryStorage;
using HelloHangfire;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IShowDate, ShowDate>();
builder.Services.AddHangfire(conf =>
{
	conf.UseIgnoredAssemblyVersionTypeResolver().UseMemoryStorage();
}
);
builder.Services.AddHangfireServer(conf =>
{
	conf.SchedulePollingInterval = TimeSpan.FromSeconds(5);
});
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

app.UseHangfireServer();

RecurringJob.AddOrUpdate<IShowDate>(x => x.Print(), "*/10 * * * * *");

app.Run();