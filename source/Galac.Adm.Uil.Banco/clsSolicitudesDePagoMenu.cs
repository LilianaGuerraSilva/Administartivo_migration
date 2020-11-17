using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.UI.Wpf;
using LibGalac.Aos.Uil;
using Galac.Adm.Ccl.Banco;
using Galac.Adm.Brl.Banco;
using Galac.Adm.Uil.Banco.ViewModel;

namespace Galac.Adm.Uil.Banco {
    public class clsSolicitudesDePagoMenu: ILibMenuMultiFile {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsSolicitudesDePagoMenu() {
        }
        #endregion //Constructores
        #region Metodos Generados

        #region Miembros de ILibMenuMultiFile
        void ILibMenuMultiFile.Ejecuta(eAccionSR valAction, int valUseInterop, IDictionary<string, XmlDocument> refGlobalValues) {
            //if (valAction == eAccionSR.Insertar) {
            //    clsSolicitudesDePagoIpl insSolicitudesDePago = new clsSolicitudesDePagoIpl((LibXmlMemInfo)refGlobalValues[LibGlobalValues.NameAppXmlInfo], (LibXmlMFC)refGlobalValues[LibGlobalValues.NameMFCInfo]);
            //    frmSolicitudesDePagoInput insFrmInput = new frmSolicitudesDePagoInput("Solicitudes De Pago", eAccionSR.Insertar, "");
            //    insFrmInput.InitLookAndFeelAndSetValues(insSolicitudesDePago.ListSolicitudesDePago, insSolicitudesDePago);
            //    if (valUseInterop ==0) {
            //        insFrmInput.Owner = System.Windows.Application.Current.MainWindow;
            //        insFrmInput.Show();
            //    } else {
            //        insFrmInput.ShowDialog();
            //    }
            //} else {
            //    List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
            //    vFixedValues.Add(new LibSearchDefaultValues("Saw.Gv_SolicitudesDePago_B1.ConsecutivoCompania", ((LibXmlMFC)refGlobalValues[LibGlobalValues.NameMFCInfo]).GetInt("Compania").ToString(), false, typeof(int)));
            //    LibFrmSearch insFrmSearch = new LibFrmSearch("Solicitudes De Pago", new Galac.Adm.Uil.Banco.Sch.GSSolicitudesDePagoSch());
            //    insFrmSearch.DbQuery = "Saw.Gp_SolicitudesDePagoSCH";
            //    insFrmSearch.LeaveOpenOnSelect = true;
            //    insFrmSearch.Entity = new clsSolicitudesDePagoList((LibXmlMemInfo)refGlobalValues[LibGlobalValues.NameAppXmlInfo], (LibXmlMFC)refGlobalValues[LibGlobalValues.NameMFCInfo]);
            //    insFrmSearch.CurrentAction = valAction;
            //    insFrmSearch.FixedCriteria = vFixedValues;
            //    if (valUseInterop ==0) {
            //        insFrmSearch.Owner = System.Windows.Application.Current.MainWindow;
            //        insFrmSearch.Show();
            //    } else {
            //        insFrmSearch.ShowDialog();
            //    }
            //}
        }
        #endregion //Miembros de ILibMenu

        public static bool ChooseFromInterop(ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
            return LibFKRetrievalHelper.ChooseRecord<FkSolicitudesDePagoViewModel>("Solicitudes De Pago", ref refXmlDocument, valSearchCriteria, valFixedCriteria, new clsSolicitudesDePagoNav());
        }
        #endregion //Metodos Generados


    } //End of class clsSolicitudesDePagoMenu

} //End of namespace Galac.Adm.Uil.Banco

