﻿
namespace Mwh.Sample.Common.Interfaces;
/// <summary>
/// Interface IEmployeeService
/// </summary>
public interface IEmployeeService
{
    /// <summary>
    /// Deletes the asynchronous.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="token">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    /// <returns>Task&lt;EmployeeResponse&gt;.</returns>
    Task<EmployeeResponse> DeleteAsync(int id, CancellationToken token);

    /// <summary>
    /// Finds the by identifier asynchronous.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="token">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    /// <returns>Task&lt;EmployeeModel&gt;.</returns>
    Task<EmployeeModel> FindByIdAsync(int id, CancellationToken token);

    /// <summary>
    /// Lists the asynchronous.
    /// </summary>
    /// <param name="token">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    /// <returns>Task&lt;IEnumerable&lt;EmployeeModel&gt;&gt;.</returns>
    Task<IEnumerable<EmployeeModel>> GetAsync(CancellationToken token);

    /// <summary>
    /// Saves the asynchronous.
    /// </summary>
    /// <param name="employee">The employee.</param>
    /// <param name="token">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    /// <returns>Task&lt;EmployeeResponse&gt;.</returns>
    Task<EmployeeResponse> SaveAsync(EmployeeModel employee, CancellationToken token);

    /// <summary>
    /// Updates the asynchronous.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="employee">The employee.</param>
    /// <param name="token">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    /// <returns>Task&lt;EmployeeResponse&gt;.</returns>
    Task<EmployeeResponse> UpdateAsync(int id, EmployeeModel employee, CancellationToken token);
}
