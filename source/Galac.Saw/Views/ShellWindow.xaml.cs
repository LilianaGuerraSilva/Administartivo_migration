using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel.Composition;
using Galac.Saw.ViewModel;
using Microsoft.Windows.Controls.Ribbon;

namespace Galac.Saw.Views {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    [Export]
    public partial class ShellWindow : Window {
        [Import]
        public ShellViewModel ViewModel {
            get { return (ShellViewModel)DataContext; }
            set { DataContext = value; }
        }

        public ShellWindow() {
            InitializeComponent();
        }

        private void Window_Activated(object sender, EventArgs e) {
            ViewModel.OnActivatedCommand.Execute(null);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            ViewModel.OnLoadedCommand.Execute(null);
        }

        private void Window_Closed(object sender, EventArgs e) {
            ViewModel.OnClosedCommand.Execute(null);
        }
    }
}
