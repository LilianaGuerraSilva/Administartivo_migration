using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using LibGalac.Aos.UI.Wpf;
using LibGalac.Aos.Uil;
using Galac.Adm.Ccl.Banco;

namespace Galac.Adm.Uil.Banco {
    /// <summary>
    /// Implementación de la Lista para el objeto
    /// </summary>
    public sealed class clsSolicitudesDePagoList: LibUicMF, ILibDbSearchModule {
        #region Variables
        SolicitudesDePago _Record;
        ILibBusinessMasterComponentWithSearch<IList<SolicitudesDePago>, IList<SolicitudesDePago>> _Reglas;
        #endregion //Variables
        #region Propiedades
        public SolicitudesDePago Record {
            get { return _Record;}
            set {_Record = value;}
        }
        #endregion //Propiedades
        #region Constructores
        public clsSolicitudesDePagoList(LibXmlMemInfo initAppMemoryInfo, LibXmlMFC initMFC):base(initAppMemoryInfo, initMFC) {
            _Record = new SolicitudesDePago();
        }
        #endregion //Constructores
        #region Metodos Generados

        #region Inicializacion BRL - a modificar si Remoting
        private void RegistraCliente() {
            if (WorkWithRemoting) {
                _Reglas = (ILibBusinessMasterComponentWithSearch<IList<SolicitudesDePago>, IList<SolicitudesDePago>>)RegisterType();
            } else {
                _Reglas = new Galac.Adm.Brl.Banco.clsSolicitudesDePagoNav();
            }
        }
        #endregion //Inicializacion BRL - a modificar si Remoting

        public bool CanBeChoosen(IList<SolicitudesDePago> valRecord, eAccionSR valAction) {
            bool vResult = false;
            //PRIMERO VALIDACIONES DE ESTA CAPA SI LAS HUBIERE, LUEGO IR A NEGOCIO
            RegistraCliente();
            vResult = _Reglas.CanBeChoosen(valRecord, valAction);
            return vResult;
        }

        private StringBuilder GetPkParams(XmlDocument valXmlRowDocument) {
            LibXmlDataParse insParser = new LibXmlDataParse(valXmlRowDocument);
            LibGpParams vParams = new LibGpParams();
            int vConsecutivoCompania = insParser.GetInt(0, "ConsecutivoCompania",0);
            vParams.AddInInteger("ConsecutivoCompania", vConsecutivoCompania);
            int vConsecutivoSolicitud = insParser.GetInt(0, "ConsecutivoSolicitud",0);
            vParams.AddInInteger("ConsecutivoSolicitud", vConsecutivoSolicitud);
            insParser  = null;
            return vParams.Get();
        }

        #region Miembros de ILibDbSearchModule
        void ILibDbSearch.DoAction(object refCallingForm, eAccionSR valAction, string valExtendedAction, XmlDocument valXmlRow) {
            if (valAction == eAccionSR.Escoger) {
                //do nothing;
            } else {
                RegistraCliente();
                IList<SolicitudesDePago> vResulset = _Reglas.GetData(eProcessMessageType.SpName, "SolicitudesDePagoGET", GetPkParams(valXmlRow), true);
                if (vResulset != null && vResulset.Count > 0) {
                    if (_Reglas.CanBeChoosen(vResulset, valAction)) {
                        clsSolicitudesDePagoIpl vRecord = new clsSolicitudesDePagoIpl(AppMemoryInfo, Mfc);
                        vRecord.ListSolicitudesDePago = vResulset;
                        frmSolicitudesDePagoInput insFrmInput = new frmSolicitudesDePagoInput("Solicitudes De Pago", valAction, valExtendedAction);
                        if (insFrmInput.InitLookAndFeelAndSetValues(vRecord.ListSolicitudesDePago, vRecord)) {
                            LibApiAwp.SetOwnerToWindow(insFrmInput, refCallingForm);
                            insFrmInput.Show();
                        } else {
                            insFrmInput.Close();
                        }
                    } else {
                        throw new GalacAlertException("No se puede escoger el registro.");
                    }
                } else {
                    throw new GalacAlertException("El conjunto de registros seleccionados por usted en la búsqueda original ha sido modificado en la base de datos."
                        + "\r\nPor favor, salga de la lista y vuelva a ejecutar la consulta para que se hagan visibles los cambios de los otros usuarios.");
                }
            }
        }

        bool ILibDbSearch.GetData(string valQuery, ref XmlDocument refResulset, StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            RegistraCliente();
            vResult = _Reglas.GetDataFromModule("Solicitudes De Pago", ref refResulset, valXmlParamsExpression);
            return vResult;
        }

        LibGalac.Aos.Base.Report.ILibReportInfo ILibDbSearch.GetDataRetrievesInstance() {
            return new Galac.Adm.Brl.Banco.Reportes.clsSolicitudesDePagoRpt();
        }
        bool ILibDbSearchModule.GetDataFromModule(string valModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            RegistraCliente();
            vResult = _Reglas.GetDataFromModule(valModule, ref refXmlDocument, valXmlParamsExpression);
            return vResult;
        }
        #endregion //Miembros de ILibDbSearchModule
        #region Métodos para Escoger

        //public static bool ChooseCuentaBancaria(System.Windows.Window refOwner, ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
        //    bool vResult = false;
        //    vResult = LibFkRetrieval.ChooseRecord(refOwner, new Galac.Adm.Uil.Banco.Sch.GSCuentaBancariaSch(), ref refXmlDocument, valSearchCriteria, valFixedCriteria, "Cuenta Bancaria", new clsSolicitudesDePagoList(null, null));
        //    return vResult;
        //}


        public static bool ChooseSolicitudesDePago(System.Windows.Window refOwner, ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
            bool vResult = false;
            vResult = LibFkRetrieval.ChooseRecord(refOwner, new Galac.Adm.Uil.Banco.Sch.GSSolicitudesDePagoSch(), ref refXmlDocument, valSearchCriteria, valFixedCriteria, "Solicitudes De Pago", new clsSolicitudesDePagoList(null, null));
            return vResult;
        }
        #endregion //Métodos para Escoger
        #endregion //Metodos Generados


    } //End of class clsSolicitudesDePagoList

} //End of namespace Galac.Adm.Uil.Banco

