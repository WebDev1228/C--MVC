// ***********************************************************************
// Assembly         : Mwh.Sample.Common.Tests
// Author           : mark
// Created          : 04-04-2020
//
// Last Modified By : mark
// Last Modified On : 04-04-2020
// ***********************************************************************
// <copyright file="EmployeeModelTests.cs" company="Mwh.Sample.Common.Tests">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Mwh.Sample.Common.Models
{


    /// <summary>
    /// Defines test class EmployeeModelTests.
    /// </summary>
    [TestClass]
    public class EmployeeModelTests
    {
        /// <summary>
        /// Defines the test method EmployeeModel_Validate.
        /// </summary>
        [TestMethod]
        public void EmployeeModel_Validate()
        {
            // Arrange
            var employeeModel = new EmployeeModel()
            {
                Age = 20,
                State = "State",
                Country = "Country",
                Department = EmployeeDepartment.Marketing,
                EmployeeID = 0,
                Name = "Name"
            };

            // Act

            // Assert
            Assert.IsNotNull(employeeModel);
            Assert.AreEqual(employeeModel.Name, "Name");
            Assert.AreEqual(employeeModel.State, "State");
            Assert.AreEqual(employeeModel.Country, "Country");
            Assert.AreEqual(employeeModel.Department, EmployeeDepartment.Marketing);
            Assert.AreEqual(employeeModel.Age, 20);
        }
    }
}
