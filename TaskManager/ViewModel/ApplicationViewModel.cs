using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Linq;
using TaskManager.Commands;
using TaskManager.Model;

namespace TaskManager.ViewModel
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        TaskManagerContext db;
        RelayCommand addCommand;
        RelayCommand editCommand;
        RelayCommand deleteCommand;
        IEnumerable<Note> notes;

        private Note selectedNote;

        public IEnumerable<Note> Notes
        {
            get { return notes; }
            set
            {
                notes = value;
                OnPropertyChanged("Notes");
            }
        }
        public Note SelectedNote
        {
            get { return selectedNote; }
            set
            {
                if (value != null)
                selectedNote = new Note()
                {
                    Id = value.Id,
                    Name = value.Name,
                    Information = value.Information,
                    DateOfStart = value.DateOfStart,
                    DateOfEnd = value.DateOfEnd,
                    Status = value.Status
                };
                OnPropertyChanged("SelectedNote");
            }
        }

        public ApplicationViewModel()
        {
            db = new TaskManagerContext();
            db.Notes.Load();
            db.MetaDatas.Load();
            Notes = db.Notes.Local.ToBindingList();
        }

        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand((selectedItem) =>
                  {
                      string str = selectedItem as string;
                      var note = new Note()
                      {
                          Name = str,
                          Information = null,
                          DateOfStart = DateTime.Now,
                          DateOfEnd = null,
                          Status = Status.Undefined
                      };
                      db.Notes.Add(note);
                      db.SaveChanges();
                      int tmp = db.Notes.MaxAsync(t => t.Id).Result;
                      db.MetaDatas.Add(MetaData.Create(DateTime.Now, $"Add Node id = {tmp}").Value);
                      db.SaveChanges();
                      selectedNote = note;
                  }));
            }
        }

        public RelayCommand EditCommand
        {
            get
            {
                return editCommand ??
                    (editCommand = new RelayCommand((selectedItem) =>
                    {
                        if (selectedItem == null) return;
                        Note notes = selectedItem as Note;

                        var note = db.Notes.Find(notes.Id);
                        if (note != null)
                        {
                            var answer = note.Update(notes.Name, notes.DateOfStart, notes.DateOfEnd, notes.Information, notes.Status);
                            if (answer.Succeeded)
                            {
                                db.MetaDatas.Add(MetaData.Create(DateTime.Now, $"Update Node id = {notes.Id}").Value);
                                db.Entry(note).State = EntityState.Modified;
                                db.SaveChanges();
                            }
                        }
                    }));
            }
        }

        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ??
                  (deleteCommand = new RelayCommand((selectedItem) =>
                  {
                      if (selectedItem == null) return;
                      Note fakeNote = selectedItem as Note;
                      Note note = db.Notes.Find(fakeNote.Id);
                      db.Notes.Remove(note);
                      db.MetaDatas.Add(MetaData.Create(DateTime.Now, $"Delete Node id = {fakeNote.Id}").Value);
                      db.SaveChanges();
                  }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
