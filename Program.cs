using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using RateLimitNet6Demo;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//加载配置
//builder.Services.AddOptions();
//注册缓存
builder.Services.AddMemoryCache();
//从appsettings.json中加载常规配置，IpRateLimiting与配置文件中节点对应
var retelimitOpt = builder.Configuration.GetSection("IpRateLimiting");
builder.Services.Configure<IpRateLimitOptions>(retelimitOpt);
//从appsettings.json中加载Ip规则
builder.Services.Configure<IpRateLimitPolicies>(builder.Configuration.GetSection("IpRateLimitPolicies"));
//将规则存到内存里
builder.Services.AddInMemoryRateLimiting();
builder.Services.AddSingleton<IRateLimitConfiguration, CustomerRateLimitConfiguration>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//管道处理加入限流
app.UseIpRateLimiting();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
