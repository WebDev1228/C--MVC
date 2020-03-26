// <copyright file="JobAssignmentTest.cs">Copyright ©  2019</copyright>
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mwh.SampleCRUD.BL.Models;
using System;

namespace Mwh.SampleCRUD.BL.Models
{
    /// <summary>This class contains parameterized unit tests for JobAssignment</summary>
    [PexClass(typeof(JobAssignmentModel))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [TestClass]
    public partial class JobAssignmentTest
    {
    }
}
