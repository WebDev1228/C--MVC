﻿
namespace Mwh.Sample.Repository.Repository;

public class EmployeeDB : IEmployeeDB
{
    private EmployeeContext _context;

    public EmployeeDB(EmployeeContext context)
    {
        _context = context;
    }

    private static List<EmployeeDto> Create(List<Employee> list)
    {
        if (list == null) return new List<EmployeeDto>();
        return list.Select(item => Create(item)).OrderBy(x => x.Name).ToList();
    }
    private static List<DepartmentDto> Create(List<Department> list)
    {
        if (list == null) return new List<DepartmentDto>();
        return list.Select(item => Create(item)).OrderBy(x => x.Name).ToList();
    }
    private static DepartmentDto Create(Department? item)
    {
        if (item == null) return new DepartmentDto();

        return new DepartmentDto()
        {
            Id = item.Id,
            Name = item.Name,
            Description = item.Description,
            Employees = item?.Employees?.Select(s => Create(s)).ToArray()
        };
    }

    private static EmployeeDto Create(Employee? entity)
    {
        if (entity is null) return new EmployeeDto();

        return new EmployeeDto()
        {
            Id = entity.Id,
            State = entity?.State ?? "TX",
            Age = entity?.Age ?? 0,
            Country = entity?.Country ?? "USA",
            Department = entity is null ? EmployeeDepartmentEnum.IT : (EmployeeDepartmentEnum)entity.DepartmentId,
            Name = entity?.Name ?? "DEFAULT"
        };
    }

    public async Task<bool> DeleteEmployeeAsync(int ID)
    {
        var delEmployee = await _context.Employees.Where(w => w.Id == ID).FirstOrDefaultAsync();
        if (delEmployee is null)
        {
            return false;
        }
        _context.Employees.Remove(delEmployee);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<EmployeeDto> EmployeeAsync(int id)
    {
        return Create(await _context.Employees.Where(w => w.Id == id).FirstOrDefaultAsync());
    }

    public async Task<List<EmployeeDto>> EmployeeCollectionAsync()
    {
        return Create(await _context.Employees.OrderBy(o => o.Name).ToListAsync());
    }

    public async Task<EmployeeDto> UpdateAsync(EmployeeDto? emp)
    {
        if (emp == null) return new EmployeeDto();

        if (emp.Id == 0)
        {
            var saveUser = new Employee()
            {
                Name = emp.Name,
                State = emp.State,
                Age = emp.Age,
                Country = emp.Country,
                DepartmentId = (int)emp.Department
            };
            await _context.Employees.AddAsync(saveUser);
            await _context.SaveChangesAsync();
            emp.Id = saveUser.Id;
        }
        else
        {
            var saveUser = await _context.Employees.Where(w => w.Id == emp.Id).FirstOrDefaultAsync();
            if (saveUser != null)
            {
                _context.Attach(saveUser);
                saveUser.Name = emp.Name;
                saveUser.State = emp.State;
                saveUser.Age = emp.Age;
                saveUser.Country = emp.Country;
                saveUser.DepartmentId = (int)emp.Department;
                saveUser.LastUpdatedDate = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            else
            {
                saveUser = new Employee()
                {
                    Id = emp.Id,
                    Name = emp.Name,
                    State = emp.State,
                    Age = emp.Age,
                    Country = emp.Country,
                    DepartmentId = (int)emp.Department
                };
                await _context.Employees.AddAsync(saveUser);
                await _context.SaveChangesAsync();
                emp.Id = saveUser.Id;

            }
        }
        return await EmployeeAsync(emp.Id);
    }

    public async Task<DepartmentDto> DepartmentAsync(int id)
    {
        return Create(await _context.Departments.Where(w => w.Id == id).Include(i => i.Employees).FirstOrDefaultAsync());
    }

    public async Task<List<DepartmentDto>> DepartmentCollectionAsync()
    {
        return Create(await _context.Departments.OrderBy(o => o.Name).ToListAsync());
    }

    public async Task<DepartmentDto?> UpdateAsync(DepartmentDto? dept)
    {
        if (dept == null) return null;

        if (dept.Id == 0)
        {
            return new DepartmentDto();
        }
        else
        {
            var saveUser = await _context.Departments.Where(w => w.Id == dept.Id).FirstOrDefaultAsync();
            if (saveUser != null)
            {
                _context.Attach(saveUser);
                saveUser.Name = dept.Name;
                saveUser.Description = dept.Description;
                await _context.SaveChangesAsync();
            }
            else
            {
                var newDept = new Department()
                {
                    Name = dept.Name,
                    Id = dept.Id,
                    Description = dept.Description,
                };
                await _context.Departments.AddAsync(newDept);
                await _context.SaveChangesAsync();
            }
        }
        return await DepartmentAsync(dept.Id);
    }
}
