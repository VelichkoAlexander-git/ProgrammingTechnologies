using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Data;
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
        RelayCommand logCommand;

        private ObservableCollection<Note> notes;
        public ObservableCollection<Note> Notes
        {
            get { return notes; }
            set
            {
                notes = value;
                OnPropertyChanged("Notes");
            }
        }

        private Note selectedNote;
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
            Notes = db.Notes.Local.ToObservableCollection();

            CvsNote = new CollectionViewSource();
            CvsNote.Source = Notes;
            CvsNote.Filter += ApplyFilter;
        }

        #region Filter
        private string filter;
        public string Filter
        {
            get { return this.filter; }
            set
            {
                this.filter = value;
                OnFilterChanged();
            }
        }
        private void OnFilterChanged()
        {
            CvsNote.View.Refresh();
        }

        internal CollectionViewSource CvsNote { get; set; }
        public ICollectionView AllNote
        {
            get { return CvsNote.View; }
        }

        void ApplyFilter(object sender, FilterEventArgs e)
        {
            Note svm = (Note)e.Item;

            if (string.IsNullOrWhiteSpace(this.Filter) || this.Filter.Length == 0)
            {
                e.Accepted = true;
            }
            else
            {
                e.Accepted = svm.Name.Contains(Filter);
            }
        }
        #endregion

        #region Command
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
                        if (selectedItem == null || SelectedNote == null) return;
                        Note notes = selectedItem as Note;
                        if (notes.Id != SelectedNote.Id) return;

                        var answer = notes.Update(SelectedNote.Name, SelectedNote.DateOfStart, SelectedNote.DateOfEnd, SelectedNote.Information, SelectedNote.Status);
                        if (answer.Succeeded)
                        {
                            db.MetaDatas.Add(MetaData.Create(DateTime.Now, $"Update Node id = {notes.Id}").Value);
                            db.Entry(notes).State = EntityState.Modified;
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

        public RelayCommand LogCommand
        {
            get
            {
                return logCommand ??
                  (logCommand = new RelayCommand((o) =>
                  {
                      LogWindow window = new LogWindow(db.MetaDatas.ToList());
                      window.ShowDialog();
                  }));
            }
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
