using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace TaskManager.Model
{
    public class MetaData
    {        
        [Key]
        public int Id { get; set; }
        public DateTime EventDate { get; set; }
        public string Information { get; set; }

        protected MetaData()
        { }

        public static Result<MetaData> Create(DateTime eventDate, string information)
        {
            var errors = new List<string>();

            if (eventDate == null) errors.Add("Invalid date");
            if (string.IsNullOrEmpty(information)) errors.Add("Invalid name");


            if (errors.Any())
            {
                return Result<MetaData>.Fail(errors);
            }

            var result = new MetaData
            {
                EventDate = eventDate,
                Information = information            
            };

            return Result<MetaData>.Success(result);
        }
    }
}
