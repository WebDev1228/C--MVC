﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mwh.Sample.Repository.Repository;
using System;
using System.Threading.Tasks;

namespace Mwh.Sample.Repository.Tests.Repository
{
    [TestClass]
    public class EmployeeMockTests
    {
        [TestMethod]
        public async Task DepartmentAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var employeeMock = new EmployeeMock();
            int id = 0;

            // Act
            var result = await employeeMock.DepartmentAsync(id);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task EmployeeAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var employeeMock = new EmployeeMock();
            int id = 0;

            // Act
            var result = await employeeMock.EmployeeAsync(
                id);

            // Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public async Task Delete_StateUnderTest_Id1()
        {
            // Arrange
            var employeeMock = new EmployeeMock();
            int ID = 1;

            // Act
            var result = await employeeMock.DeleteEmployeeAsync(ID);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task Delete_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var employeeMock = new EmployeeMock();
            int ID = 0;

            // Act
            var result = await employeeMock.DeleteEmployeeAsync(ID);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task DeleteAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var employeeMock = new EmployeeMock();
            int ID = 0;

            // Act
            var result = await employeeMock.DeleteEmployeeAsync(
                ID);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DepartmentCollection_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var employeeMock = new EmployeeMock();

            // Act
            var result = employeeMock.DepartmentCollection();

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task DepartmentCollectionAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var employeeMock = new EmployeeMock();

            // Act
            var result = await employeeMock.DepartmentCollectionAsync();

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void EmployeeCollection_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var employeeMock = new EmployeeMock();

            // Act
            var result = employeeMock.EmployeeCollection();

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task EmployeeCollectionAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var employeeMock = new EmployeeMock();

            // Act
            var result = await employeeMock.EmployeeCollectionAsync();

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task UpdateAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var employeeMock = new EmployeeMock();
            EmployeeDto emp = null;

            // Act
            var result = await employeeMock.UpdateAsync(
                emp);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task UpdateAsync_StateUnderTest_ExpectedBehavior1()
        {
            // Arrange
            var employeeMock = new EmployeeMock();
            DepartmentDto emp = null;

            // Act
            var result = await employeeMock.UpdateAsync(emp);

            // Assert
            Assert.IsNull(result);
        }


        [TestMethod]
        public async Task Delete_StateUnderTest_ExpectedBehaviorNotFound()
        {
            // Arrange
            var employeeMock = new EmployeeMock();
            int ID = 0;

            // Act
            var result = await employeeMock.DeleteEmployeeAsync(ID);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task Employee_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var employeeDB = new EmployeeMock();
            int id = 0;

            // Act
            var result = await employeeDB.EmployeeAsync(id);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task EmployeeCollection_ExpectedBehavior()
        {
            // Arrange
            var employeeDB = new EmployeeMock();

            // Act
            var result = await employeeDB.EmployeeCollectionAsync();

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task Update_StateUnderTest_ExpectedBehaviorNewEmployee()
        {
            // Arrange
            var employeeDB = new EmployeeMock();
            var newEmp = new EmployeeDto()
            {
                Age = 33,
                Name = "Test User",
                State = "Texas",
                Country = "USA",
                Department = EmployeeDepartmentEnum.IT
            };

            // Act

            // Get Current count of employees
            var initResult = await employeeDB.EmployeeCollectionAsync();

            // Add New Employee with Update
            var addResult = await employeeDB.UpdateAsync(newEmp);

            // Get updated count of employees
            var updatedResult = await employeeDB.EmployeeCollectionAsync();
            /// Update the Employee
            addResult.Name = "Test User 2";
            addResult.Age = 44;
            addResult.State = "FL";
            addResult.Department = EmployeeDepartmentEnum.Accounting;
            var result = await employeeDB.UpdateAsync(addResult);
            // Get result after update
            var finalResult = await employeeDB.EmployeeAsync(result.id);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(addResult.id, result.id);
            Assert.AreEqual(finalResult.Age, 44);
            Assert.AreEqual(finalResult.State, "FL");

        }
    }
}
