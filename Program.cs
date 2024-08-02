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
//��������
//builder.Services.AddOptions();
//ע�Ỻ��
builder.Services.AddMemoryCache();
//��appsettings.json�м��س������ã�IpRateLimiting�������ļ��нڵ��Ӧ
var retelimitOpt = builder.Configuration.GetSection("IpRateLimiting");
builder.Services.Configure<IpRateLimitOptions>(retelimitOpt);
//��appsettings.json�м���Ip����
builder.Services.Configure<IpRateLimitPolicies>(builder.Configuration.GetSection("IpRateLimitPolicies"));
//������浽�ڴ���
builder.Services.AddInMemoryRateLimiting();
builder.Services.AddSingleton<IRateLimitConfiguration, CustomerRateLimitConfiguration>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//�ܵ������������
app.UseIpRateLimiting();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
