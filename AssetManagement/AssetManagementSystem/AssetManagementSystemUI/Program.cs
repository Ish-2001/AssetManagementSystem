using AssetManagementSystemUI.Services;
using AssetManagementSystemUI.Services.Admin;
using AssetManagementSystemUI.Services.Asset;
using AssetManagementSystemUI.Services.SoftwareLicenseService;
using AssetManagementSystemUI.Services.User;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddScoped<BookService>();
builder.Services.AddScoped<HardwareService>();
builder.Services.AddScoped<SoftwareLicenseService>();
builder.Services.AddScoped<AssetService>();
builder.Services.AddScoped<AdminService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSession();

var app = builder.Build();
//builder.Services.AddScoped<IBookService, BookService>();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Login}/{id?}");

app.Run();
