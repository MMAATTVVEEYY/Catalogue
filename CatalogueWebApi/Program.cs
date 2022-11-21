using CatalogueWebApi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CatalogueAPIDbContext>(option =>
    { option.UseSqlServer(builder.Configuration.GetConnectionString("SQLServer")); }
);

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

app.Run();
// TO DO:
/*
 1. Models - finish +
 2. Controllers - get, put methods for all 3 (maybe 2).
 3. Need to create Tables in DB with Models +

 4. !!!redo brand and category to contain name only!!!, redo Item accordingly+
 5. Add HttpPatch to Item+
 6. Add Pagination to items
 7. Add Sorting to items
 8. Add Filters to Iems
 */


//�������� - ����� ����� ����������
//������� ����� ����� ���������� Category � Item , � ��� �� Category � Brand �� ��������� ������� ������ Category,
//��� ������ ��� �������� ����� ������� ��� ���� Category, Item, � ID, ���, ����� ��� ��������� � ��������� � ���� ������,  � ���, ��� �������� � ��� ���� ��� - ������� ��������� ������
// ������� - �������� ����. �� � ��������� ����� ������� ��� �� ����������� ��������� ����� ����� ��� �� ��������. ��� ������� ���� ����� ��������������

/*����� �������������� ����� ��������� � ������ � Item ������ ����� ��������� ����� ��������� ���� ������� ���� Items
 */