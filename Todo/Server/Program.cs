using Todo.Server.Extensions;
using Todo.Server.Middlewares;
using Todo.Services.Mappers;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .SetupSqlServer(builder.Configuration)
    .SetupAuthentication(builder.Configuration)
    .SetupInjections(builder.Configuration)
    .SetupActionFilters()
    .SetupSwagger()
    .AddAutoMapper(typeof(DocumentMapper))
    .AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    await app.SeedDatabase();
    app.UseWebAssemblyDebugging();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();

// TODO: deploy