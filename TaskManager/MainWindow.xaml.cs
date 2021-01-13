﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TaskManager.Commands;
using TaskManager.Model;
using TaskManager.ViewModel;

namespace TaskManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new ApplicationViewModel();
            NoteStatus.ItemsSource = Enum.GetValues(typeof(Status)).Cast<Status>();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var db = new TaskManagerContext();
            var logs = db.MetaDatas.Select(s => $"Date: {s.EventDate}   Event: {s.Information}");
            MessageBox.Show(String.Join("\n", logs));
        }
    }
}