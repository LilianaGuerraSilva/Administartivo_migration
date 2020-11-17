using System;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using LibGalac.Aos.UI.Wpf;
using Galac.Adm.Rpt.GestionCompras;
using Galac.Adm.Uil.GestionCompras.Reportes;
namespace Galac.Adm.Uil.GestionCompras {
    /// <summary>
    /// L�gica de interacci�n para frmCompraInformes.xaml
    /// </summary>

    public partial class frmCompraInformes: LibFrmRptBase {
        #region Propiedades
        LibXmlMemInfo AppMemoryInfo { get; set; }
        LibXmlMFC Mfc { get; set; }
        #endregion //Propiedades
        #region Constructores

        public frmCompraInformes(LibXmlMemInfo initAppMemoryInfo, LibXmlMFC initMfc) {
            InitializeComponent();
            AppMemoryInfo = initAppMemoryInfo.GlobalValuesGetInstanceOnlyWith("Compania");
            Mfc = initMfc;
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void OnLoaded() {
            base.OnLoaded();
            var vViewModel = DataContext as clsCompraInformesViewModel;
            if (vViewModel != null) {
                vViewModel.AppMemoryInfo = AppMemoryInfo;
                vViewModel.Mfc = Mfc;
                vViewModel.RequestClose += (sender, args) => {
                    Close();
                };
            }
        }
        #endregion //Metodos Generados


    } //End of class frmCompraInformes.xaml

} //End of namespace Galac..Uil.ComponenteNoEspecificado

