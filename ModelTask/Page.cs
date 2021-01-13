using Microsoft.Analytics.Interfaces;
using Microsoft.Analytics.Interfaces.Streaming;
using Microsoft.Analytics.Types.Sql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ModelTask
{
    public enum Status
    {
        Undefined = 0,
        Expect = 1,
        Finished = 2
    }
    public class Page
    {
        public int Id { get; protected set; }
        public string Name { get; protected set; }
        public DateTime DateOfStart { get; protected set; }
        public DateTime? DateOfEnd { get; protected set; }
        public string Information { get; protected set; }
        public Status Status { get; protected set; }

        protected Page()
        { }

        public static Result<Page> Create(string name, DateTime dateOfStart, DateTime? dateOfEnd, string information, Status status)
        {
            var errors = new List<string>();

            if (dateOfStart == null) dateOfStart = DateTime.Today;
            if (dateOfEnd != null && dateOfEnd < dateOfStart) errors.Add("End date cannot be less than current");
            if (string.IsNullOrEmpty(name)) errors.Add("Invalid customer name");


            if (errors.Any())
            {
                return Result<Page>.Fail(errors);
            }

            var result = new Page
            {
                Name = name,
                DateOfStart = dateOfStart,
                DateOfEnd = dateOfEnd,
                Information = information,
                Status = status
            };

            return Result<Page>.Success(result);
        }

        public Result<bool> Update(string name, DateTime dateOfStart, DateTime? dateOfEnd, string information, Status status)
        {
            var errors = new List<string>();

            if (dateOfStart == null) dateOfStart = DateTime.Today;
            if (dateOfEnd != null && dateOfEnd < dateOfStart) errors.Add("End date cannot be less than current");
            if (string.IsNullOrEmpty(name)) errors.Add("Invalid customer name");


            if (errors.Any())
            {
                return Result<bool>.Fail(errors);
            }

            Name = name;
            DateOfStart = dateOfStart;
            DateOfEnd = dateOfEnd;
            Information = information;
            Status = status;

            return Result<bool>.Success(true);
        }
    }
}