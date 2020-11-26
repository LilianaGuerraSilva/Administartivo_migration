using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using LibGalac.Aos.UI.Wpf;
using LibGalac.Aos.Uil;
using Galac.Saw.Ccl.SttDef;
using Galac.Saw.Uil.Banco.ViewModel;
using Galac.Saw.Brl.Banco;
using Galac.Saw.Uil.Inventario.ViewModel;
using Galac.Saw.Brl.Inventario;
using Galac.Saw.Uil.Banco.ViewModel;
using Galac.Saw.Brl.Banco;

namespace Galac.Saw.Uil.SttDef {
    /// <summary>
    /// Implementación de la Lista para el objeto
    /// </summary>
    public sealed class clsSettValueByCompanyList : LibUicMF, ILibDbSearchModule {
        #region Variables
        SettValueByCompany _Record;
        ILibBusinessComponentWithSearch<IList<SettValueByCompany>, IList<SettValueByCompany>> _Reglas;
        #endregion //Variables
        #region Propiedades
        public SettValueByCompany Record {
            get { return _Record; }
            set { _Record = value; }
        }
        #endregion //Propiedades
        #region Constructores
        public clsSettValueByCompanyList(LibXmlMemInfo initAppMemoryInfo, LibXmlMFC initMFC)
            : base(initAppMemoryInfo, initMFC) {
            _Record = new SettValueByCompany();
        }
        #endregion //Constructores
        #region Metodos Generados

        #region Inicializacion BRL - a modificar si Remoting
        private void RegistraCliente() {
            if (WorkWithRemoting) {
                _Reglas = (ILibBusinessComponentWithSearch<IList<SettValueByCompany>, IList<SettValueByCompany>>)RegisterType();
            } else {
                _Reglas = new Galac.Saw.Brl.SttDef.clsSettValueByCompanyNav(AppMemoryInfo, Mfc);
            }
        }
        #endregion //Inicializacion BRL - a modificar si Remoting

        public bool CanBeChoosen(IList<SettValueByCompany> valRecord, eAccionSR valAction) {
            bool vResult = false;
            //PRIMERO VALIDACIONES DE ESTA CAPA SI LAS HUBIERE, LUEGO IR A NEGOCIO
            RegistraCliente();
            vResult = _Reglas.CanBeChoosen(valRecord, valAction);
            return vResult;
        }

        private StringBuilder GetPkParams(XmlDocument valXmlRowDocument) {
            LibXmlDataParse insParser = new LibXmlDataParse(valXmlRowDocument);
            LibGpParams vParams = new LibGpParams();
            int vConsecutivoCompania = insParser.GetInt(0, "ConsecutivoCompania", 0);
            vParams.AddInInteger("ConsecutivoCompania", vConsecutivoCompania);
            string vNameSettDefinition = insParser.GetString(0, "NameSettDefinition", "");
            vParams.AddInString("NameSettDefinition", vNameSettDefinition, 50);
            insParser = null;
            return vParams.Get();
        }

        #region Miembros de ILibDbSearchModule
        void ILibDbSearch.DoAction(object refCallingForm, eAccionSR valAction, string valExtendedAction, XmlDocument valXmlRow) {
            if (valAction == eAccionSR.Escoger) {
                //do nothing;
            } else {
                RegistraCliente();
                IList<SettValueByCompany> vResulset = _Reglas.GetData(eProcessMessageType.SpName, "SettValueByCompanyGET", GetPkParams(valXmlRow));
                if (vResulset != null && vResulset.Count > 0) {
                    if (_Reglas.CanBeChoosen(vResulset, valAction)) {
                        clsSettValueByCompanyIpl vRecord = new clsSettValueByCompanyIpl(AppMemoryInfo, Mfc);
                        vRecord.ListSettValueByCompany = vResulset;
                        frmSettValueByCompanyInput insFrmInput = new frmSettValueByCompanyInput("Sett Value By Company", valAction, valExtendedAction);
                        if (insFrmInput.InitLookAndFeelAndSetValues(vRecord.ListSettValueByCompany, vRecord)) {
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
            vResult = _Reglas.GetDataFromModule("Sett Value By Company", ref refResulset, valXmlParamsExpression);
            return vResult;
        }

        LibGalac.Aos.Base.Report.ILibReportInfo ILibDbSearch.GetDataRetrievesInstance() {
            throw new NotImplementedException();
        }
        bool ILibDbSearchModule.GetDataFromModule(string valModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            RegistraCliente();
            vResult = _Reglas.GetDataFromModule(valModule, ref refXmlDocument, valXmlParamsExpression);
            return vResult;
        }
        #endregion //Miembros de ILibDbSearchModule
        #region Métodos para Escoger

        public static bool ChooseSettValueByCompany(System.Windows.Window refOwner, ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
            bool vResult = false;
            vResult = LibFkRetrieval.ChooseRecord(refOwner, new Galac.Saw.Uil.SttDef.Sch.GSSettValueByCompanySch(), ref refXmlDocument, valSearchCriteria, valFixedCriteria, "Sett Value By Company", new clsSettValueByCompanyList(null, null));
            return vResult;
        }

        public static bool ChooseSettDefinition(System.Windows.Window refOwner, ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
            bool vResult = false;
            vResult = LibFkRetrieval.ChooseRecord(refOwner, new Galac.Saw.Uil.SttDef.Sch.GSSettDefinitionSch(), ref refXmlDocument, valSearchCriteria, valFixedCriteria, "Sett Definition", new clsSettValueByCompanyList(null, null));
            return vResult;
        }

        public static bool ChooseAnticipo(System.Windows.Window refOwner, ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
            bool vResult = false;
            vResult = LibFkRetrieval.ChooseRecord(refOwner, new Galac.Dbo.Uil.Anticipo.Sch.GSAnticipoSch(), ref refXmlDocument, valSearchCriteria, valFixedCriteria, "Anticipo", new clsSettValueByCompanyList(null, null));
            return vResult;
        }

        public static bool ChooseBeneficiario(System.Windows.Window refOwner, ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
            bool vResult = false;
            vResult = LibFKRetrievalHelper.ChooseRecord<FkBeneficiarioViewModel>("Beneficiario", ref refXmlDocument, valSearchCriteria, valFixedCriteria, new clsBeneficiarioNav());
            return vResult;
        }

        public static bool ChooseCliente(System.Windows.Window refOwner, ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
            bool vResult = false;
            vResult = LibFkRetrieval.ChooseRecord(refOwner, new Galac.Saw.Uil.Cliente.Sch.GSClienteSch(), ref refXmlDocument, valSearchCriteria, valFixedCriteria, "Cliente", new clsSettValueByCompanyList(null, null));
            return vResult;
        }

        public static bool ChooseProveedor(System.Windows.Window refOwner, ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
            bool vResult = false;
            vResult = LibFkRetrieval.ChooseRecord(refOwner, new Galac.Dbo.Uil.Proveedor.Sch.GSProveedorSch(), ref refXmlDocument, valSearchCriteria, valFixedCriteria, "Proveedor", new clsSettValueByCompanyList(null, null));
            return vResult;
        }

        public static bool ChooseMoneda(System.Windows.Window refOwner, ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
            bool vResult = false;
            vResult = LibFkRetrieval.ChooseRecord(refOwner, new Galac.Dbo.Uil.Moneda.Sch.GSMonedaSch(), ref refXmlDocument, valSearchCriteria, valFixedCriteria, "Moneda", new clsSettValueByCompanyList(null, null));
            return vResult;
        }

        public static bool ChooseCuentaBancaria(System.Windows.Window refOwner, ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
            return LibFKRetrievalHelper.ChooseRecord<FkCuentaBancariaViewModel>("Cuenta Bancaria", ref refXmlDocument, valSearchCriteria, valFixedCriteria, new clsCuentaBancariaNav());
        }

        public static bool ChooseConceptoBancario(System.Windows.Window refOwner, ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
            bool vResult = false;
            vResult = LibFkRetrieval.ChooseRecord(refOwner, new Galac.Adm.Uil.Banco.Sch.GSConceptoBancarioSch(), ref refXmlDocument, valSearchCriteria, valFixedCriteria, "Concepto Bancario", new clsSettValueByCompanyList(null, null));
            return vResult;
        }

        public static bool ChooseCotizacion(System.Windows.Window refOwner, ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
            bool vResult = false;
            vResult = LibFkRetrieval.ChooseRecord(refOwner, new Galac.Dbo.Uil.Cotizacion.Sch.GSCotizacionSch(), ref refXmlDocument, valSearchCriteria, valFixedCriteria, "Cotizacion", new clsSettValueByCompanyList(null, null));
            return vResult;
        }

        public static bool ChooseRendicion(System.Windows.Window refOwner, ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
            bool vResult = false;
            vResult = LibFkRetrieval.ChooseRecord(refOwner, new Galac.Dbo.Uil.Rendicion.Sch.GSRendicionSch(), ref refXmlDocument, valSearchCriteria, valFixedCriteria, "Rendicion", new clsSettValueByCompanyList(null, null));
            return vResult;
        }

        public static bool ChooseInventarioStt(System.Windows.Window refOwner, ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
            bool vResult = false;
            vResult = LibFkRetrieval.ChooseRecord(refOwner, new Galac.Saw.Uil.SttDef.Sch.GSInventarioSttSch(), ref refXmlDocument, valSearchCriteria, valFixedCriteria, "5.1.- Inventario", new clsSettValueByCompanyList(null, null));
            return vResult;
        }

        public static bool ChooseAlmacen(System.Windows.Window refOwner, ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
            return LibFKRetrievalHelper.ChooseRecord<FkAlmacenViewModel>("Almacén", ref refXmlDocument, valSearchCriteria, valFixedCriteria, new clsAlmacenNav());
        }

        public static bool ChooseRetencionISLRStt(System.Windows.Window refOwner, ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
            bool vResult = false;
            vResult = LibFkRetrieval.ChooseRecord(refOwner, new Galac.Saw.Uil.SttDef.Sch.GSRetencionISLRSttSch(), ref refXmlDocument, valSearchCriteria, valFixedCriteria, "6.4.- Retención ISLR", new clsSettValueByCompanyList(null, null));
            return vResult;
        }

        public static bool ChooseCiudad(System.Windows.Window refOwner, ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
            bool vResult = false;
            vResult = LibFkRetrieval.ChooseRecord(refOwner, new Galac.Dbo.Uil.Ciudad.Sch.GSCiudadSch(), ref refXmlDocument, valSearchCriteria, valFixedCriteria, "Ciudad", new clsSettValueByCompanyList(null, null));
            return vResult;
        }

        public static bool ChooseVendedorStt(System.Windows.Window refOwner, ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
            bool vResult = false;
            vResult = LibFkRetrieval.ChooseRecord(refOwner, new Galac.Saw.Uil.SttDef.Sch.GSVendedorSttSch(), ref refXmlDocument, valSearchCriteria, valFixedCriteria, "3.3.- Vendedor", new clsSettValueByCompanyList(null, null));
            return vResult;
        }

        public static bool ChooseVendedor(System.Windows.Window refOwner, ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
            bool vResult = false;
            vResult = LibFkRetrieval.ChooseRecord(refOwner, new Galac.Dbo.Uil.Vendedor.Sch.GSVendedorSch(), ref refXmlDocument, valSearchCriteria, valFixedCriteria, "Vendedor", new clsSettValueByCompanyList(null, null));
            return vResult;
        }
        #endregion //Métodos para Escoger
        #endregion //Metodos Generados
    } //End of class clsSettValueByCompanyList

} //End of namespace Galac.Saw.Uil.SttDef

