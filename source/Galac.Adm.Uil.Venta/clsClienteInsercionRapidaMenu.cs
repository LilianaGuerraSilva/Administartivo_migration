using Galac.Adm.Uil.Venta.ViewModel;
using Galac.Saw.Ccl.SttDef;
using Galac.Saw.Ccl.Cliente;
using LibGalac.Aos.Base;
using LibGalac.Aos.UI.Mvvm.Messaging;

namespace Galac.Adm.Uil.Venta {
    public interface ILibClienteInsercionRapidaMenu:ILibMenu {
        string CodigoCliente { get; set; }
        string NombreCliente { get; set; }
        string RifCliente { get; set; }
        bool DialogResult { get; set; }
    }

    public class clsClienteInsercionRapidaMenu:ILibClienteInsercionRapidaMenu {
        #region Variables
        private eTipoDocumentoIdentificacion _TipoDocumentoIdentificacion;
        private string _Nombre;
        private string _Rif;
        #endregion

        #region Propiedades
        public string CodigoCliente { get; set; }
        public string NombreCliente { get; set; }
        public string RifCliente { get; set; }
        public bool DialogResult { get; set; } 
        #endregion

        public clsClienteInsercionRapidaMenu(string valNombre,string valRif,eTipoDocumentoFactura valTipoDocumentoFactura) {
            _TipoDocumentoIdentificacion = (valTipoDocumentoFactura == eTipoDocumentoFactura.Boleta) ?
                                           eTipoDocumentoIdentificacion.DNI : eTipoDocumentoIdentificacion.RUC;
            _Nombre = valNombre;
            _Rif = valRif;            
        }

        void ILibMenu.Ejecuta(eAccionSR valAction,int handler) {
            ClienteInsercionRapidaViewModel clienteInsercionRapidaViewModel = new ClienteInsercionRapidaViewModel();            
            clienteInsercionRapidaViewModel.InitLookAndFeel(_TipoDocumentoIdentificacion,_Rif,_Nombre);
            LibMessages.EditViewModel.ShowEditor(clienteInsercionRapidaViewModel,true);
            DialogResult = clienteInsercionRapidaViewModel.DialogResult;
            if(DialogResult) {
                CodigoCliente = clienteInsercionRapidaViewModel.Codigo;
                NombreCliente = clienteInsercionRapidaViewModel.Nombre;
                RifCliente = clienteInsercionRapidaViewModel.NumeroRIF;
            }
        }
    }
}
