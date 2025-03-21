using Delta.UI;
using static Delta.Html.Tags;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var deltaValueA = new DeltaValue<int>("a", 2);

app.Map("/", (HttpContext context) => body()[
        div()["Hello World!"],
        new Delta.UI.Delta(c => div()[
            "Plus: ", deltaValueA.Value + 2
        ])
    ]
    .ToHtml(context));

app.Run();