﻿
namespace Mwh.Sample.Core.Data.Services;

/// <summary>
/// 
/// </summary>
public class EmployeeService : IDisposable, IEmployeeService
{
    private EmployeeContext _context;

    /// <summary>
    /// 
    /// </summary>
    public EmployeeService()
    {
        _context = new EmployeeContext();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    public EmployeeService(EmployeeContext context)
    {
        _context = context;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="options"></param>
    public EmployeeService(DbContextOptions options)
    {
        _context = new EmployeeContext(options);
    }

    private EmployeeModel Create(Employee? item)
    {
        if (item == null) return new EmployeeModel();

        return new EmployeeModel()
        {
            Name = item.Name,
            State = item.State,
            Country = item.Country,
            Age = item.Age,
            Department = (EmployeeDepartment)item.DepartmentId,
            id = item.Id
        };
    }

    private static Employee Create(EmployeeModel item)
    {
        return new Employee()
        {
            Name = item.Name,
            State = item.State,
            Country = item.Country,
            Age = item.Age,
            DepartmentId = (int)item.Department,
            Id = item.id
        };
    }

    private EmployeeModel[] Get(List<Employee> list)
    {
        return list.Select(s => Create(s)).ToArray();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="namelist"></param>
    /// <returns></returns>
    public int AddMultipleEmployees(string[] namelist)
    {
        var list = new List<Employee>();

        if (namelist == null) return -1;

        foreach (var name in namelist)
        {
            list.Add(new Employee() { Name = name, Age = 33, Country = "USA", DepartmentId = 1, State = "TX" });
        }
        _context.Employees.AddRange(list);
        var dbResult = _context.SaveChanges();
        return dbResult;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<EmployeeResponse> DeleteAsync(int id, CancellationToken token)
    {
        var response = new EmployeeResponse("Init");

        if (id > 0)
        {
            var dbEmp = await _context.Employees
                .Where(w => w.Id == id)
                .FirstOrDefaultAsync(cancellationToken: token)
                .ConfigureAwait(true);

            if (dbEmp == null)
            {
                response = new EmployeeResponse("Employee Not Found");
            }
            else
            {
                _context.Employees.Remove(dbEmp);
                await _context.SaveChangesAsync(token)
                    .ConfigureAwait(true);

                return new EmployeeResponse(new EmployeeModel());
            }
        }
        else
        {
            return new EmployeeResponse("Invalid Employee Id for delete");
        }
        return response;
    }

    /// <summary>
    /// 
    /// </summary>
    public void Dispose()
    {
        ((IDisposable)_context).Dispose();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<EmployeeModel> FindByIdAsync(int id, CancellationToken token)
    { return Create(await _context.Employees.FindAsync(new object[] { id }, cancellationToken: token).ConfigureAwait(false)); }

    public EmployeeModel[] Get()
    {
        return Get(_context.Employees.ToList());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<IEnumerable<EmployeeModel>> GetAsync(CancellationToken token)
    { return Get(await _context.Employees.ToListAsync(cancellationToken: token).ConfigureAwait(false)); }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public EmployeeResponse Save(EmployeeModel item)
    {
        if (item == null)
            return new EmployeeResponse("Employee can not be null");

        Employee? dbEmp;
        if (item.id > 0)
        {
            dbEmp = _context.Employees.Where(w => w.Id == item.id).FirstOrDefault();
            if (dbEmp == null)
            {
                return new EmployeeResponse("Employee Not Found");
            }
            else
            {
                dbEmp.Age = item.Age;
                dbEmp.Country = item.Country;
                dbEmp.DepartmentId = (int)item.Department;
                dbEmp.Name = item.Name;
                dbEmp.State = item.State;
                _context.SaveChanges();
            }
        }
        else
        {
            dbEmp = Create(item);
            _context.Employees.Add(dbEmp);
            _context.SaveChanges();
        }
        return new EmployeeResponse(Create(dbEmp));
    }

    /// <summary>
    /// Save Employee
    /// </summary>
    /// <param name="item"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<EmployeeResponse> SaveAsync(EmployeeModel item, CancellationToken cancellationToken)
    {
        if (item == null)
            return new EmployeeResponse("Employee can not be null");

        var dbEmp = new Employee();
        try
        {
            if (item.id > 0)
            {
                dbEmp = await _context.Employees
                    .Where(w => w.Id == item.id)
                    .FirstOrDefaultAsync(cancellationToken)
                    .ConfigureAwait(true);

                if (dbEmp == null)
                {
                    return new EmployeeResponse("Employee Not Found");
                }
                else
                {
                    dbEmp.Age = item.Age;
                    dbEmp.Country = item.Country;
                    dbEmp.DepartmentId = (int)item.Department;
                    dbEmp.Name = item.Name;
                    dbEmp.State = item.State;
                    await _context.SaveChangesAsync(cancellationToken)
                        .ConfigureAwait(true);

                    var list = await _context.Employees
                        .ToListAsync(cancellationToken)
                        .ConfigureAwait(true);
                }
            }
            else
            {
                dbEmp = Create(item);
                await _context.Employees.AddAsync(dbEmp);
                await _context.SaveChangesAsync(cancellationToken)
                    .ConfigureAwait(true);
            }
        }
        catch (Exception ex)
        {
            return new EmployeeResponse($"Exception:{ex.Message}");
        }

        var newEmp = await _context.Employees
            .Where(w => w.Id == dbEmp.Id)
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(true);

        return new EmployeeResponse(Create(dbEmp));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="employee"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<EmployeeResponse> UpdateAsync(int id, EmployeeModel employee, CancellationToken token)
    {
        if (employee == null)
            return new EmployeeResponse($"Can not update null employee");

        if (employee.id != id)
            return new EmployeeResponse($"Mismatch in id({id}) && id({employee.id}).");

        if (employee.id == 0)
            return new EmployeeResponse($"Can not update employee with id({id})");

        return await SaveAsync(employee, token).ConfigureAwait(false);
    }
}
