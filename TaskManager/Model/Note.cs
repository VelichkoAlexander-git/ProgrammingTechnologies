using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace TaskManager.Model
{
    public enum Status
    {
        Undefined = 0,
        Expect = 1,
        Finished = 2
    }
    public class Note : INotifyPropertyChanged
    {
        private string name;
        private DateTime dateOfStart;
        private DateTime? dateOfEnd;
        private string information;
        private Status status;

        [Key]
        public int Id { get; set; }
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        public DateTime DateOfStart
        {
            get { return dateOfStart; }
            set
            {
                dateOfStart = value;
                OnPropertyChanged("DateOfStart");
            }
        }
        public DateTime? DateOfEnd
        {
            get { return dateOfEnd; }
            set
            {
                dateOfEnd = value;
                OnPropertyChanged("DateOfEnd");
            }
        }
        public string Information
        {
            get { return information; }
            set
            {
                information = value;
                OnPropertyChanged("Information");
            }
        }
        public Status Status
        {
            get { return status; }
            set
            {
                status = value;
                OnPropertyChanged("Status");
            }
        }

        public Note() 
        { }

        public Result<bool> Update(string name, DateTime dateOfStart, DateTime? dateOfEnd, string information, Status status)
        {
            var errors = new List<string>();

            if (dateOfStart == null) dateOfStart = DateTime.Now;
            if (dateOfEnd != null && dateOfEnd < dateOfStart) errors.Add("End date cannot be less than current");

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

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
