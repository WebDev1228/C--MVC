﻿
namespace Mwh.Sample.Repository;

public class EmployeeDB : IEmployeeDB
{
    private EmployeeContext _context;

    public EmployeeDB(EmployeeContext context)
    {
        _context = context;
    }

    private List<EmployeeModel> Create(List<Employee> list)
    {
        if (list == null) return new List<EmployeeModel>();
        return list.Select(item => Create(item)).OrderBy(x => x.Name).ToList();
    }

    private EmployeeModel Create(Employee s)
    {
        if (s == null) return new EmployeeModel();

        return new EmployeeModel()
        {
            id = s.Id,
            State = s.State,
            Age = s.Age,
            Country = s.Country,
            Department = (EmployeeDepartment)s.DepartmentId,
            Name = s.Name
        };
    }

    public bool Delete(int ID)
    {
        var delEmployee = _context.Employees.Where(w => w.Id == ID).FirstOrDefault();
        if (delEmployee != null)
        {
            _context.Employees.Remove(delEmployee);
            _context.SaveChanges();
            return true;
        }
        return false;
    }

    public EmployeeModel Employee(int id)
    {
        return Create(_context.Employees.Where(w => w.Id == id).FirstOrDefault());
    }

    public List<EmployeeModel> EmployeeCollection()
    {
        return Create(_context.Employees.OrderBy(o => o.Name).ToList());
    }

    public EmployeeModel Update(EmployeeModel emp)
    {
        if (emp == null) return new EmployeeModel();

        if (emp.id == 0)
        {
            var saveUser = new Employee()
            {
                Name = emp.Name,
                State = emp.State,
                Age = emp.Age,
                Country = emp.Country,
                DepartmentId = (int)emp.Department
            };
            _context.Employees.Add(saveUser);
            _context.SaveChanges();
            emp.id = saveUser.Id;
        }
        else
        {
            var saveUser = _context.Employees.Where(w => w.Id == emp.id).FirstOrDefault();
            if (saveUser != null)
            {
                _context.Attach(saveUser);
                saveUser.Name = emp.Name;
                saveUser.State = emp.State;
                saveUser.Age = emp.Age;
                saveUser.Country = emp.Country;
                saveUser.DepartmentId = (int)emp.Department;
                _context.SaveChanges();
            }
            else
            {
                saveUser = new Employee()
                {
                    Id = emp.id,
                    Name = emp.Name,
                    State = emp.State,
                    Age = emp.Age,
                    Country = emp.Country,
                    DepartmentId = (int)emp.Department
                };
                _context.Employees.Add(saveUser);
                _context.SaveChanges();
                emp.id = saveUser.Id;

            }
        }
        return Employee(emp.id);
    }
}
