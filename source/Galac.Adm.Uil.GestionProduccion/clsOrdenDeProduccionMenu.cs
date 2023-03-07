using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.UI.Wpf;
using LibGalac.Aos.Uil;
using Galac.Adm.Ccl.GestionProduccion;
using Galac.Adm.Brl.GestionProduccion;
using Galac.Adm.Uil.GestionProduccion.ViewModel;
using LibGalac.Aos.UI.Mvvm.Messaging;
using System.Xml.Linq;

namespace Galac.Adm.Uil.GestionProduccion {
    public class clsOrdenDeProduccionMenu: ILibMenuMultiFile {
        int _Consecutivo;
        string _ViewModelStr;
        public clsOrdenDeProduccionMenu(int initConsecutivo) {
            _Consecutivo = initConsecutivo;
        }

        public clsOrdenDeProduccionMenu() {
        }

        public string ViewModelStr {
            get { return _ViewModelStr; }
            private set { _ViewModelStr = value; }
        }
        #region Metodos Generados

        void ILibMenuMultiFile.Ejecuta(eAccionSR valAction, int valUseInterop, IDictionary<string, XmlDocument> refGlobalValues) {
            if (valAction == eAccionSR.Contabilizar) {
                ViewModelStr = EjecutaStr(valAction, valUseInterop != 0);
            } else {
                OrdenDeProduccionViewModel vViewModel = new OrdenDeProduccionViewModel(new OrdenDeProduccion() { ConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"), Consecutivo = _Consecutivo }, eAccionSR.Consultar);
                vViewModel.InitializeViewModel(eAccionSR.Consultar);
                LibMessages.EditViewModel.ShowEditor(vViewModel, true);
            }
        }

        public static bool ChooseFromInterop(ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
            return LibFKRetrievalHelper.ChooseRecord<FkOrdenDeProduccionViewModel>("Orden de Producción", ref refXmlDocument, valSearchCriteria, valFixedCriteria, new clsOrdenDeProduccionNav());
        }

        string EjecutaStr(eAccionSR valAction, bool valUseInterop) {
            string vResult = string.Empty;
            OrdenDeProduccionViewModel vViewModel = new OrdenDeProduccionViewModel(new OrdenDeProduccion() { ConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"), Consecutivo = _Consecutivo }, valAction);
            vViewModel.InitializeViewModel(valAction);
            LibMessages.EditViewModel.ShowEditor(vViewModel, valUseInterop);
            if (vViewModel.DialogResult) {
                XElement vXmlResult = new XElement("Datos",
                    new XElement("Documento",
                        new XElement("Consecutivo", vViewModel.Consecutivo)
                        , new XElement("Codigo", vViewModel.Codigo)
                        , new XElement("StatusOp", (int)vViewModel.StatusOp)
                        , new XElement("FechaFinalizacion", LibConvert.ToStr(vViewModel.FechaFinalizacion, "dd/MM/yyyy"))
                        , new XElement("FechaDeAnulacion", LibConvert.ToStr(vViewModel.FechaAnulacion, "dd/MM/yyyy"))
                        ));
                vResult = vXmlResult.ToString();
            }
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsOrdenDeProduccionMenu

} //End of namespace Galac.Adm.Uil.GestionProduccion

