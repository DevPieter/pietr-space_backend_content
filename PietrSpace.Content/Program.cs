using Microsoft.EntityFrameworkCore;
using PietrSpace.Content.Info.Data.Postgres.Data;
using PietrSpace.Content.Info.Data.Postgres.Repositories;
using PietrSpace.Content.Info.Data.Repositories;
using PietrSpace.Content.Info.Logic.Services;
using PietrSpace.Content.Quotes.Data.Postgres.Data;
using PietrSpace.Content.Quotes.Data.Postgres.Repositories;
using PietrSpace.Content.Quotes.Data.Repositories;
using PietrSpace.Content.Quotes.Logic.Services;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<NpgsqlQuoteDataContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("QuoteDataContext"))
);

builder.Services.AddScoped<IQuoteRepository, QuoteRepository>();
builder.Services.AddScoped<QuoteService>();

builder.Services.AddDbContext<NpgsqlInfoDataContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("InfoDataContext"))
);

builder.Services.AddScoped<IInfoRepository, InfoRepository>();
builder.Services.AddScoped<InfoService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
