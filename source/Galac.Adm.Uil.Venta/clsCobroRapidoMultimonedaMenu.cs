using Galac.Adm.Brl.Venta;
using Galac.Adm.Uil.Venta.ViewModel;
using Galac.Saw.Ccl.SttDef;
using LibGalac.Aos.Base;
using LibGalac.Aos.UI.Mvvm.Messaging;
using LibGalac.Aos.Uil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Galac.Adm.Uil.Venta
{
    public class clsCobroRapidoMultimonedaMenu : ILibMenu {

        private bool cobro = false;

        public string MostrarPantallaDeCobroRapidoEnMultimoneda(int valConsecutivoCompania, string valNumeroFactura, DateTime valFecha, decimal valTotalFactura, string valTipoDeDocumento, string valCodigoMonedaDelDocumento, string valCodigoMonedaDeCobro) {
            eTipoDocumentoFactura vTipoDeDocumento = (eTipoDocumentoFactura)LibConvert.DbValueToEnum(valTipoDeDocumento);
            CobroRapidoMultimonedaViewModel vViewModel = new CobroRapidoMultimonedaViewModel(valConsecutivoCompania, valNumeroFactura, valFecha, valTotalFactura, vTipoDeDocumento, valCodigoMonedaDelDocumento, valCodigoMonedaDeCobro, true);
            vViewModel.SeCobro += (arg) => cobro = arg;
            LibMessages.EditViewModel.ShowEditor(vViewModel, true);
            string vResult = vViewModel.XmlDatosDelCobro != null ? vViewModel.XmlDatosDelCobro.ToString() : string.Empty;
            return vResult;
        }

        public static bool ChooseFromInterop(ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
            return LibFKRetrievalHelper.ChooseRecord<FkFacturaRapidaViewModel>("Punto de Venta", ref refXmlDocument, valSearchCriteria, valFixedCriteria, new clsFacturaRapidaNav());
        }

        public void Ejecuta(eAccionSR valAction, int handler) {
            new NotImplementedException();
        }
    }
}
