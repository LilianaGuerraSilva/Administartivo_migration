using LibGalac.Aos.UI.WpfControls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Galac.Adm.Uil.Venta.Views {
    /// <summary>
    /// GSCobroRapidoMultimonedaView.xaml
    /// </summary>
    public partial class GSCobroRapidoMultimonedaView : UserControl {
        
        #region Constructores

        public GSCobroRapidoMultimonedaView() {
            InitializeComponent();
            Loaded += (sender, args) => FocusFirst();
            txtTransferenciaDivisas.LostKeyboardFocus += (sender, args) => FocusFirst();
            txtTotalEfectivoMonedaLocal.LostKeyboardFocus += (sender, args) => FocusSecond();
            var vFields = typeof(GSCobroRapidoMultimonedaView)
                .GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .Where(t=> t.FieldType == typeof(GSNumericBoxWpf));
            foreach (var vField in vFields) {
                GSNumericBoxWpf vNumericBoxWpf = vField.GetValue(this) as GSNumericBoxWpf;
                if (vNumericBoxWpf != null) {
                    vNumericBoxWpf.LostKeyboardFocus += (sender, args) => RecalcularTotales();
                }
            }
        }

        #endregion //Constructores

        private void FocusFirst() {
            txtTotalEfectivoMonedaLocal.Focus();
        }

        private void FocusSecond() {
            txtTotalEfectivoDivisas.Focus();
        }

        private void RecalcularTotales() {
            var vViewModel = DataContext as Galac.Adm.Uil.Venta.ViewModel.CobroRapidoMultimonedaViewModel;
            if(vViewModel != null) {
                vViewModel.CalcularTotales();
            }
        }

    } //End of class GSCobroRapidoResumidoView.xaml

} //End of namespace Galac.Adm.Uil.Venta

