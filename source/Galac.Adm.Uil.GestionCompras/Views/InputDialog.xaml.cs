using LibGalac.Aos.Base;
using LibGalac.Aos.UI.Mvvm.Messaging;
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
using System.Windows.Shapes;

namespace Galac.Adm.Uil.GestionCompras.Views {
    /// <summary>
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class InputDialog : Window {
        
        public InputDialog(string initQuestion, string initDefaultAnswer = "", bool initSearch = false) {
            InitializeComponent();
            lblQuestion.Content = initQuestion;
            txtAnswer.Text = initDefaultAnswer;
            btnSearch.Visibility = !initSearch ? Visibility.Hidden : Visibility.Visible;
        }

        private void btnDialogOk_Click(object sender, RoutedEventArgs e) {
            this.DialogResult = true;
        }

        private void Window_ContentRendered(object sender, EventArgs e) {
            txtAnswer.SelectAll();
            txtAnswer.Focus();
        }

        public string Answer {
            get { return txtAnswer.Text; }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e) {
            txtAnswer.Text = LibMessages.OpenFile.Send(this, "Seleccione archivo de respaldo.", "Archivo .zip (*.sql)|*.sql|Todos los archivos (*.*)|*.*", LibApp.AppPath());
        }

    }
}
