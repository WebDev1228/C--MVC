﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mwh.Sample.Common.Models;

namespace Mwh.Sample.Common.Tests.Models
{
    [TestClass]
    public class EmployeeResponseTests
    {
        [TestMethod]
        public void EmployeeResponse_Expected()
        {
            // Arrange
            var employee = new EmployeeModel() 
            { 
                EmployeeID=1,
                Age = 20,
                Name = "Test Employee",
                State = "TX",
                Country = "USA",
                Department = EmployeeDepartment.IT
            };
            var employeeResponse = new EmployeeResponse(employee);

            // Act

            // Assert
            Assert.IsNotNull(employeeResponse);
            Assert.AreEqual(employeeResponse.Success,true);
        }
    }
}
