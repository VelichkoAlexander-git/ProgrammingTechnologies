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
                selectedNote = value;
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
                      var note = Note.Create(str, DateTime.Today, null, null, Status.Undefined);
                      if (note.Succeeded)
                      {
                          db.Notes.Add(note.Value);
                          db.SaveChanges();
                          int tmp = db.Notes.MaxAsync(t => t.Id).Result;
                          db.MetaDatas.Add(MetaData.Create(DateTime.Now, $"Add Node id = {tmp}").Value);
                          db.SaveChanges();
                          selectedNote = note.Value;
                      }
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
                      Note newNote = selectedItem as Note;

                      Note note = db.Notes.Find(newNote.Id);
                      if (note != null)
                      {
                          note.Update(newNote.Name, newNote.DateOfStart, newNote.DateOfEnd, newNote.Information, newNote.Status);
                          db.Entry(note).State = EntityState.Modified;
                          db.MetaDatas.Add(MetaData.Create(DateTime.Now, $"Update Node id = {note.Id}").Value);
                          db.SaveChanges();
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
                      Note note = selectedItem as Note;
                      db.Notes.Remove(note);
                      db.MetaDatas.Add(MetaData.Create(DateTime.Now, $"Delete Node id = {note.Id}").Value);
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
