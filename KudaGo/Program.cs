
var builder = WebApplication.CreateBuilder(args);

// // Add services to the container.
// builder.Services.AddHttpClient<KudaGo.KudaGoService>(c => c.BaseAddress = new System.Uri("https://kudago.com/public-api/"));
// var configurationSection = builder.Configuration.GetSection(nameof(KudaGoSettings));
// builder.Services.Configure<KudaGoSettings>(configurationSection);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
