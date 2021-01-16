using System;
using Moq;
using NUnit.Framework;
using TaskManager.Model;
using TaskManager.ViewModel;

namespace TaskManagerNUnitTests
{
    public class ApplicationViewModelTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void VmShouldInitializeCommandsAndViewModel()
        {
            var viewModel = new ApplicationViewModel();
            Assert.IsNotNull(viewModel.AddCommand, "AddNoteCommand is null");
            Assert.IsNotNull(viewModel.EditCommand, "EditNoteCommand is null");
            Assert.IsNotNull(viewModel.DeleteCommand, "DeleteNoteCommand is null");
            Assert.IsNotNull(viewModel.LogCommand, "LogCommand is null");
        }

        [Test]
        public void AddNoteCmdCanBeExecutedAlways()
        {
            var viewModel = new ApplicationViewModel();
            Assert.IsTrue(viewModel.AddCommand.CanExecute(null));

            var name = "TestName";

            Assert.IsTrue(viewModel.AddCommand.CanExecute(name));
        }

        [Test]
        public void DeleteCmdCanBeExecutedSelectedItemNotNull()
        {
            var viewModel = new ApplicationViewModel();

            var note = new Note
            {
                Id = 1,
                Name = "TestName",
                Information = "TestInformation",
                DateOfStart = DateTime.Now,
                DateOfEnd = null,
                Status = Status.Undefined
            };
            viewModel.Notes.Add(note);
            //viewModel.SelectedNote = note;

            Assert.IsTrue(viewModel.DeleteCommand.CanExecute(note));
        }

        [Test]
        public void DeleteCmdCannotBeExecutedSelectedItemNull()
        {
            var viewModel = new ApplicationViewModel();

            Assert.IsNull(viewModel.SelectedNote);
            Assert.IsFalse(viewModel.DeleteCommand.CanExecute(viewModel.SelectedNote));
        }

        [Test]
        public void EditCmdCannotBeExecutedSelectedItemNull()
        {
            var viewModel = new ApplicationViewModel();

            var note = new Note
            {
                Id = 1,
                Name = "TestName",
                Information = "TestInformation",
                DateOfStart = DateTime.Now,
                DateOfEnd = null,
                Status = Status.Undefined
            };
            viewModel.Notes.Add(note);

            Assert.IsNull(viewModel.SelectedNote);
            Assert.IsFalse(viewModel.EditCommand.CanExecute(viewModel.SelectedNote));
        }

        [Test]
        public void EditCmdCanBeExecutedSelectedItemNotNull()
        {
            var viewModel = new ApplicationViewModel();

            var note = new Note
            {
                Id = 1,
                Name = "TestName",
                Information = "TestInformation",
                DateOfStart = DateTime.Now,
                DateOfEnd = null,
                Status = Status.Undefined
            };
            viewModel.Notes.Add(note);
            viewModel.SelectedNote = note;

            Assert.IsTrue(viewModel.EditCommand.CanExecute(viewModel.SelectedNote));
        }
    }
}