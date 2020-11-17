using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using LibGalac.Aos.UI.Wpf;
using LibGalac.Aos.Uil;
using Galac.Adm.Ccl.CajaChica;

namespace Galac.Adm.Uil.CajaChica {
    /// <summary>
    /// Implementación de la Lista para el objeto
    /// </summary>
    public sealed class clsAnticipoList: LibUicMF, ILibDbSearchModule {
        #region Variables
        Anticipo _Record;
        ILibBusinessComponentWithSearch<IList<Anticipo>, IList<Anticipo>> _Reglas;
        #endregion //Variables
        #region Propiedades
        public Anticipo Record {
            get { return _Record;}
            set {_Record = value;}
        }
        #endregion //Propiedades
        #region Constructores
        public clsAnticipoList(LibXmlMemInfo initAppMemoryInfo, LibXmlMFC initMFC):base(initAppMemoryInfo, initMFC) {
            _Record = new Anticipo();
        }
        #endregion //Constructores
        #region Metodos Generados

        #region Inicializacion BRL - a modificar si Remoting
        private void RegistraCliente() {
            if (WorkWithRemoting) {
                _Reglas = (ILibBusinessComponentWithSearch<IList<Anticipo>, IList<Anticipo>>)RegisterType();
            } else {
                _Reglas = new Galac.Adm.Brl.CajaChica.clsAnticipoNav();
            }
        }
        #endregion //Inicializacion BRL - a modificar si Remoting

        public bool CanBeChoosen(IList<Anticipo> valRecord, eAccionSR valAction) {
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
            int vConsecutivoAnticipo = insParser.GetInt(0, "ConsecutivoAnticipo",0);
            vParams.AddInInteger("ConsecutivoAnticipo", vConsecutivoAnticipo);
            insParser  = null;
            return vParams.Get();
        }

        #region Miembros de ILibDbSearchModule
        void ILibDbSearch.DoAction(object refCallingForm, eAccionSR valAction, string valExtendedAction, XmlDocument valXmlRow) {
            if (valAction == eAccionSR.Escoger) {
                //do nothing;
            } else {
                //RegistraCliente();
                //IList<Anticipo> vResulset = _Reglas.GetData(eProcessMessageType.SpName, "AnticipoGET", GetPkParams(valXmlRow));
                //if (vResulset != null && vResulset.Count > 0) {
                //    if (_Reglas.CanBeChoosen(vResulset, valAction)) {
                //        clsAnticipoIpl vRecord = new clsAnticipoIpl(AppMemoryInfo, Mfc);
                //        vRecord.ListAnticipo = vResulset;
                //        frmAnticipoInput insFrmInput = new frmAnticipoInput("Anticipo", valAction, valExtendedAction);
                //        if (insFrmInput.InitLookAndFeelAndSetValues(vRecord.ListAnticipo, vRecord)) {
                //            LibApiAwp.SetOwnerToWindow(insFrmInput, refCallingForm);
                //            insFrmInput.Show();
                //        } else {
                //            insFrmInput.Close();
                //        }
                //    } else {
                //        throw new GalacAlertException("No se puede escoger el registro.");
                //    }
                //} else {
                //    throw new GalacAlertException("El conjunto de registros seleccionados por usted en la búsqueda original ha sido modificado en la base de datos."
                //        + "\r\nPor favor, salga de la lista y vuelva a ejecutar la consulta para que se hagan visibles los cambios de los otros usuarios.");
                //}
            }
        }

        bool ILibDbSearch.GetData(string valQuery, ref XmlDocument refResulset, StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            RegistraCliente();
            vResult = _Reglas.GetDataFromModule("Anticipo", ref refResulset, valXmlParamsExpression);
            return vResult;
        }

        LibGalac.Aos.Base.Report.ILibReportInfo ILibDbSearch.GetDataRetrievesInstance() {
            //return new Galac.Adm.Brl.CajaChica.Reportes.clsAnticipoRpt();
            return null;
        }
        bool ILibDbSearchModule.GetDataFromModule(string valModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            RegistraCliente();
            vResult = _Reglas.GetDataFromModule(valModule, ref refXmlDocument, valXmlParamsExpression);
            return vResult;
        }
        #endregion //Miembros de ILibDbSearchModule
        #region Métodos para Escoger

        public static bool ChooseAnticipo(System.Windows.Window refOwner, ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
            bool vResult = false;
            vResult = LibFkRetrieval.ChooseRecord(refOwner, new Galac.Adm.Uil.CajaChica.Sch.GSAnticipoSch(), ref refXmlDocument, valSearchCriteria, valFixedCriteria, "Anticipo", new clsAnticipoList(null, null));
            return vResult;
        }

        public static bool ChooseBeneficiario(System.Windows.Window refOwner, ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
            bool vResult = false;
            vResult = LibFkRetrieval.ChooseRecord(refOwner, new Galac.Saw.Uil.Banco.Sch.GSBeneficiarioSch(), ref refXmlDocument, valSearchCriteria, valFixedCriteria, "Beneficiario", new clsAnticipoList(null, null));
            return vResult;
        }
        public static bool ChooseCliente(System.Windows.Window refOwner, ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
            bool vResult = false;
           // vResult = LibFkRetrieval.ChooseRecord(refOwner, new Galac.Saw.Uil.Cliente.Sch.GSClienteSch(), ref refXmlDocument, valSearchCriteria, valFixedCriteria, "Cliente", new clsAnticipoList(null, null));
            return vResult;
        }

        public static bool ChooseProveedor(System.Windows.Window refOwner, ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
            bool vResult = false;
           // vResult = LibFkRetrieval.ChooseRecord(refOwner, new Galac.dbo.Uil.Proveedor.Sch.GSProveedorSch(), ref refXmlDocument, valSearchCriteria, valFixedCriteria, "Proveedor", new clsAnticipoList(null, null));
            return vResult;
        }

        public static bool ChooseMoneda(System.Windows.Window refOwner, ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
            bool vResult = false;
            //vResult = LibFkRetrieval.ChooseRecord(refOwner, new Galac.dbo.Uil.Moneda.Sch.GSMonedaSch(), ref refXmlDocument, valSearchCriteria, valFixedCriteria, "Moneda", new clsAnticipoList(null, null));
            return vResult;
        }

        public static bool ChooseCuentaBancaria(System.Windows.Window refOwner, ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
            bool vResult = false;
            //vResult = LibFkRetrieval.ChooseRecord(refOwner, new Galac.dbo.Uil.CuentaBancaria.Sch.GSCuentaBancariaSch(), ref refXmlDocument, valSearchCriteria, valFixedCriteria, "Cuenta Bancaria", new clsAnticipoList(null, null));
            return vResult;
        }

        public static bool ChooseConceptoBancario(System.Windows.Window refOwner, ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
            bool vResult = false;
            //vResult = LibFkRetrieval.ChooseRecord(refOwner, new Galac.dbo.Uil.ConceptoBancario.Sch.GSConceptoBancarioSch(), ref refXmlDocument, valSearchCriteria, valFixedCriteria, "Concepto Bancario", new clsAnticipoList(null, null));
            return vResult;
        }

        public static bool ChooseCotizacion(System.Windows.Window refOwner, ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
            bool vResult = false;
           // vResult = LibFkRetrieval.ChooseRecord(refOwner, new Galac.dbo.Uil.Cotizacion.Sch.GSCotizacionSch(), ref refXmlDocument, valSearchCriteria, valFixedCriteria, "Cotizacion", new clsAnticipoList(null, null));
            return vResult;
        }

        public static bool ChooseRendicion(System.Windows.Window refOwner, ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
            bool vResult = false;
           // vResult = LibFkRetrieval.ChooseRecord(refOwner, new Galac.Adm.Uil.CajaChica.Sch.GSRendicionSch(), ref refXmlDocument, valSearchCriteria, valFixedCriteria, "Rendicion", new clsAnticipoList(null, null));
            return vResult;
        }
        #endregion //Métodos para Escoger
        #endregion //Metodos Generados


    } //End of class clsAnticipoList

} //End of namespace Galac.Adm.Uil.CajaChica

