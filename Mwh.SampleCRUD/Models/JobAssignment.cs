﻿namespace SampleCRUD.Models
{
    using System;
    using System.Collections.Generic;

    public class JobAssignmentList : List<JobAssignment>
    {
    }
    public class JobAssignment
    {
        public int CompLevel { get; set; }
        public DateTime EndDate { get; set; }
        public int OutcomeCode { get; set; }
        public DateTime StartDate { get; set; }
        public string Title { get; set; }

    }
}
