using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using LibGalac.Aos.UI.Wpf;
using LibGalac.Aos.Uil;
using Galac.Adm.Ccl.GestionCompras;

namespace Galac.Adm.Uil.GestionCompras {
    /// <summary>
    /// Implementación de la Lista para el objeto
    /// </summary>
    public sealed class clsProveedorList: LibUicMF, ILibDbSearchModule {
        #region Variables
        Proveedor _Record;
        ILibBusinessComponentWithSearch<IList<Proveedor>, IList<Proveedor>> _Reglas;
        #endregion //Variables
        #region Propiedades
        public Proveedor Record {
            get { return _Record;}
            set {_Record = value;}
        }
        #endregion //Propiedades
        #region Constructores
        public clsProveedorList(LibXmlMemInfo initAppMemoryInfo, LibXmlMFC initMFC):base(initAppMemoryInfo, initMFC) {
            _Record = new Proveedor();
        }
        #endregion //Constructores
        #region Metodos Generados

        #region Inicializacion BRL - a modificar si Remoting
        private void RegistraCliente() {
            if (WorkWithRemoting) {
                _Reglas = (ILibBusinessComponentWithSearch<IList<Proveedor>, IList<Proveedor>>)RegisterType();
            } else {
                _Reglas = new Galac.Adm.Brl.GestionCompras.clsProveedorNav();
            }
        }
        #endregion //Inicializacion BRL - a modificar si Remoting

        public bool CanBeChoosen(IList<Proveedor> valRecord, eAccionSR valAction) {
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
            string vCodigoProveedor = insParser.GetString(0, "CodigoProveedor","");
            vParams.AddInString("CodigoProveedor", vCodigoProveedor, 10);
            insParser  = null;
            return vParams.Get();
        }

        #region Miembros de ILibDbSearchModule
        void ILibDbSearch.DoAction(object refCallingForm, eAccionSR valAction, string valExtendedAction, XmlDocument valXmlRow) {
            if (valAction == eAccionSR.Escoger) {
                //do nothing;
            } else {
                RegistraCliente();
                IList<Proveedor> vResulset = _Reglas.GetData(eProcessMessageType.SpName, "ProveedorGET", GetPkParams(valXmlRow));
                if (vResulset != null && vResulset.Count > 0) {
                    if (_Reglas.CanBeChoosen(vResulset, valAction)) {
                        clsProveedorIpl vRecord = new clsProveedorIpl(AppMemoryInfo, Mfc);
                        vRecord.ListProveedor = vResulset;
                        frmProveedorInput insFrmInput = new frmProveedorInput("Proveedor", valAction, valExtendedAction);
                        if (insFrmInput.InitLookAndFeelAndSetValues(vRecord.ListProveedor, vRecord)) {
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
            vResult = _Reglas.GetDataFromModule("Proveedor", ref refResulset, valXmlParamsExpression);
            return vResult;
        }

        LibGalac.Aos.Base.Report.ILibReportInfo ILibDbSearch.GetDataRetrievesInstance() {
            //return null;
            return new Galac.Adm.Brl.GestionCompras.Reportes.clsProveedorRpt();
        }
        bool ILibDbSearchModule.GetDataFromModule(string valModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            RegistraCliente();
            vResult = _Reglas.GetDataFromModule(valModule, ref refXmlDocument, valXmlParamsExpression);
            return vResult;
        }
        #endregion //Miembros de ILibDbSearchModule
        #region Métodos para Escoger

        public static bool ChooseProveedor(System.Windows.Window refOwner, ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
            bool vResult = false;
            vResult = LibFkRetrieval.ChooseRecord(refOwner, new Galac.Adm.Uil.GestionCompras.Sch.GSProveedorSch(), ref refXmlDocument, valSearchCriteria, valFixedCriteria, "Proveedor", new clsProveedorList(null, null));
            return vResult;
        }

        public static bool ChooseProveedorForPago(System.Windows.Window refOwner, ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
            bool vResult = false;
            vResult = LibFkRetrieval.ChooseRecord(refOwner, new Galac.Adm.Uil.GestionCompras.Sch.GSProveedorSch(), ref refXmlDocument, valSearchCriteria, valFixedCriteria, "ProveedorForPago", new clsProveedorList(null, null));
            return vResult;
        }

        public static bool ChooseTablaRetencion(System.Windows.Window refOwner, ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
            bool vResult = false;
            vResult = LibFkRetrieval.ChooseRecord(refOwner, new Galac.Comun.Uil.TablasLey.Sch.GSTablaRetencionSch(), ref refXmlDocument, valSearchCriteria, valFixedCriteria, "Tabla Retención", new clsProveedorList(null, null));
            return vResult;
        }

        public static bool ChooseTipoProveedor(System.Windows.Window refOwner, ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
            bool vResult = false;
            vResult = LibFkRetrieval.ChooseRecord(refOwner, new Galac.Saw.Uil.Tablas.Sch.GSTipoProveedorSch(), ref refXmlDocument, valSearchCriteria, valFixedCriteria, "Tipo Proveedor", new clsProveedorList(null, null));
            return vResult;
        }

        public static bool ChooseCuenta(System.Windows.Window refOwner, ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
            bool vResult = false;
            vResult = LibFkRetrieval.ChooseRecord(refOwner, new Galac.Contab.Uil.WinCont.Sch.GSCuentaSch(false, false, null), ref refXmlDocument, valSearchCriteria, valFixedCriteria, "Cuenta", new clsProveedorList(null, null));
            return vResult;
        }
        #endregion //Métodos para Escoger
        #endregion //Metodos Generados


    } //End of class clsProveedorList

} //End of namespace Galac.Adm.Uil.GestionCompras

