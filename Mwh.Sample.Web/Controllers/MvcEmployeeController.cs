﻿
namespace Mwh.Sample.Web.Controllers;

/// <summary>
/// MvcEmployeeController
/// </summary>
public class MvcEmployeeController : BaseController
{
    private readonly IEmployeeClient client;

    /// <summary>
    /// Mvc Employee Controller Constructor
    /// </summary>
    /// <param name="employeeClient"></param>
    public MvcEmployeeController(IEmployeeClient employeeClient) : base()
    {
        client = employeeClient;
    }

    /// <summary>
    /// Default Page for MvcEmployeeController
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult> Index()
    {
        var list = await client.GetEmployeesAsync(cts.Token).ConfigureAwait(false);

        return View(list);
    }

    /// <summary>
    /// View Employee Details
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult> Details(int id)
    {
        var emp = await client.FindEmployeeByIdAsync(id, cts.Token).ConfigureAwait(false);
        return View(emp);
    }

    /// <summary>
    /// Load Page to Create A New Employee
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public ActionResult Create() { return View(new EmployeeDto()); }

    /// <summary>
    /// Save New Employee
    /// </summary>
    /// <param name="employee"></param>
    /// <returns></returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(EmployeeDto employee)
    {
        EmployeeResponse reqResponse;
        if (employee != null)
        {
            reqResponse = await client.SaveAsync(employee, cts.Token).ConfigureAwait(false);
        }
        return RedirectToAction("Index");
    }

    /// <summary>
    /// Edit an employee by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult> Edit(int id)
    {
        var emp = await client.FindEmployeeByIdAsync(id, cts.Token).ConfigureAwait(false);
        return View(emp);
    }

    /// <summary>
    /// Save Employee
    /// </summary>
    /// <param name="id"></param>
    /// <param name="employee"></param>
    /// <returns></returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(int id, EmployeeDto employee)
    {
        EmployeeResponse reqResponse;
        if (employee != null)
        {
            if (employee.id == id)
                reqResponse = await client.UpdateAsync(id, employee, cts.Token).ConfigureAwait(false);
        }
        return RedirectToAction("Index");
    }

    /// <summary>
    /// Select an Employee to delete
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult> Delete(int id)
    {
        var emp = await client.FindEmployeeByIdAsync(id, cts.Token).ConfigureAwait(false);
        return View(emp);
    }

    /// <summary>
    /// Delete Employee
    /// </summary>
    /// <param name="id"></param>
    /// <param name="employee"></param>
    /// <returns></returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Delete(int id, EmployeeDto employee)
    {
        if (employee != null)
        {
            if (employee.id == id)
            {
                var result = await client.DeleteAsync(id, cts.Token).ConfigureAwait(false);
            }
        }
        return RedirectToAction("Index");
    }
}

