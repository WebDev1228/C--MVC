﻿
namespace Mwh.Sample.Common.Repositories;

/// <summary>
/// Employee Service
/// </summary>
public class EmployeeServiceRepository : IEmployeeService
{
    /// <summary>
    /// The employee repository
    /// </summary>
    private readonly IEmployeeRepository _employeeRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="EmployeeServiceRepository"/> class.
    /// </summary>
    /// <param name="employeeRepository">The employee repository.</param>
    public EmployeeServiceRepository(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    /// <summary>
    /// Lists the asynchronous.
    /// </summary>
    /// <param name="token">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    /// <returns>Task&lt;IEnumerable&lt;EmployeeModel&gt;&gt;.</returns>
    public Task<IEnumerable<EmployeeModel>> GetAsync(CancellationToken token)
    {
        return _employeeRepository.ListAsync(token);
    }

    /// <summary>
    /// Finds the by identifier asynchronous.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="token">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    /// <returns>Task&lt;EmployeeModel&gt;.</returns>
    public Task<EmployeeModel> FindByIdAsync(int id, CancellationToken token)
    {
        return _employeeRepository.FindByIdAsync(id, token);
    }

    /// <summary>
    /// save as an asynchronous operation.
    /// </summary>
    /// <param name="employee">The employee.</param>
    /// <param name="token">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    /// <returns>EmployeeResponse.</returns>
    public async Task<EmployeeResponse> SaveAsync(EmployeeModel? employee, CancellationToken token)
    {
        if (employee == null) return new EmployeeResponse("Employee is null");
        try
        {
            var response = await _employeeRepository.AddAsync(employee, token).ConfigureAwait(true);

            if (response == null) return new EmployeeResponse("Repository Response was null");

            return new EmployeeResponse(response);
        }
        catch (Exception ex)
        {
            // Do some logging stuff
            return new EmployeeResponse($"An error occurred when saving the employee: {ex.Message}");
        }
    }

    /// <summary>
    /// update as an asynchronous operation.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="employee">The employee.</param>
    /// <param name="token">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    /// <returns>EmployeeResponse.</returns>
    public async Task<EmployeeResponse> UpdateAsync(int id, EmployeeModel? employee, CancellationToken token)
    {
        if (employee == null)
            return new EmployeeResponse("Employee is null.");

        if (employee.id != id)
            return new EmployeeResponse($"Mismatch in id({id}) && id({employee.id}).");

        var existingEmployee = await _employeeRepository.FindByIdAsync(id, token).ConfigureAwait(true);
        if (existingEmployee == null)
            return new EmployeeResponse("Employee not found.");

        try
        {
            var response = await _employeeRepository.UpdateAsync(employee, token).ConfigureAwait(true);

            if (response == null) return new EmployeeResponse("Repository Response was null");

            return new EmployeeResponse(response);
        }
        catch (Exception ex)
        {
            // Do some logging stuff
            return new EmployeeResponse($"An error occurred when updating the employee: {ex.Message}");
        }
    }

    /// <summary>
    /// delete as an asynchronous operation.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="token">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    /// <returns>EmployeeResponse.</returns>
    public async Task<EmployeeResponse> DeleteAsync(int id, CancellationToken token)
    {
        var existingEmployee = await _employeeRepository.FindByIdAsync(id, token).ConfigureAwait(true);

        if (existingEmployee == null)
            return new EmployeeResponse("Employee not found.");

        if (!existingEmployee.IsValid)
            return new EmployeeResponse("Employee not found.");

        try
        {
            var response = await _employeeRepository.RemoveAsync(existingEmployee, token).ConfigureAwait(true);

            if (response)
                existingEmployee.id = 0;

            return new EmployeeResponse(existingEmployee);
        }
        catch (Exception ex)
        {
            // Do some logging stuff
            return new EmployeeResponse($"An error occurred when deleting the employee: {ex.Message}");
        }
    }
}
