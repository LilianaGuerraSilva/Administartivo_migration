using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using LibGalac.Aos.UI.Wpf;
using LibGalac.Aos.Uil;
using Entity = Galac.Saw.Ccl.Cliente;

namespace Galac.Saw.Uil.Cliente {
    /// <summary>
    /// Implementación de la Lista para el objeto
    /// </summary>
    public sealed class clsClienteList: LibUicMF, ILibDbSearchModule {
        #region Variables
        Entity.Cliente _Record;
        ILibBusinessComponentWithSearch<IList<Entity.Cliente>, IList<Entity.Cliente>> _Reglas;
        #endregion //Variables
        #region Propiedades
        public Entity.Cliente Record {
            get { return _Record;}
            set {_Record = value;}
        }
        #endregion //Propiedades
        #region Constructores
        public clsClienteList(LibXmlMemInfo initAppMemoryInfo, LibXmlMFC initMFC):base(initAppMemoryInfo, initMFC) {
            _Record = new Entity.Cliente();
        }
        #endregion //Constructores
        #region Metodos Generados

        #region Inicializacion BRL - a modificar si Remoting
        private void RegistraCliente() {
            if (WorkWithRemoting) {
                _Reglas = (ILibBusinessComponentWithSearch<IList<Entity.Cliente>, IList<Entity.Cliente>>)RegisterType();
            } else {
                _Reglas = new Galac.Saw.Brl.Cliente.clsClienteNav();
            }
        }
        #endregion //Inicializacion BRL - a modificar si Remoting

        public bool CanBeChoosen(IList<Entity.Cliente> valRecord, eAccionSR valAction) {
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
            string vCodigo = insParser.GetString(0, "Codigo","");
            vParams.AddInString("Codigo", vCodigo, 10);
            insParser  = null;
            return vParams.Get();
        }

        #region Miembros de ILibDbSearchModule
        void ILibDbSearch.DoAction(object refCallingForm, eAccionSR valAction, string valExtendedAction, XmlDocument valXmlRow) {
            if (valAction == eAccionSR.Escoger) {
                //do nothing;
            } else {
                RegistraCliente();
                IList<Entity.Cliente> vResulset = _Reglas.GetData(eProcessMessageType.SpName, "ClienteGET", GetPkParams(valXmlRow));
                if (vResulset != null && vResulset.Count > 0) {
                    if (_Reglas.CanBeChoosen(vResulset, valAction)) {
                        clsClienteIpl vRecord = new clsClienteIpl(AppMemoryInfo, Mfc);
                        vRecord.ListCliente = vResulset;
                        frmClienteInput insFrmInput = new frmClienteInput("Cliente", valAction, valExtendedAction);
                        if (insFrmInput.InitLookAndFeelAndSetValues(vRecord.ListCliente, vRecord)) {
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
            vResult = _Reglas.GetDataFromModule("Cliente", ref refResulset, valXmlParamsExpression);
            return vResult;
        }
        LibGalac.Aos.Base.Report.ILibReportInfo ILibDbSearch.GetDataRetrievesInstance() {
            return new Galac.Saw.Brl.Cliente.Reportes.clsClienteRpt();
        }
        bool ILibDbSearchModule.GetDataFromModule(string valModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            RegistraCliente();
            vResult = _Reglas.GetDataFromModule(valModule, ref refXmlDocument, valXmlParamsExpression);
            return vResult;
        }
        #endregion //Miembros de ILibDbSearchModule
        #region Métodos para Escoger

        public static bool ChooseCliente(System.Windows.Window refOwner, ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
            bool vResult = false;
            vResult = LibFkRetrieval.ChooseRecord(refOwner, new Galac.Saw.Uil.Cliente.Controles.GSClienteSch(), ref refXmlDocument, valSearchCriteria, valFixedCriteria, "Cliente", new clsClienteList(null, null));
            return vResult;
        }

        public static bool ChooseCiudad(System.Windows.Window refOwner, ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
            bool vResult = false;
            //vResult = LibFkRetrieval.ChooseRecord(refOwner, new Galac.dbo.Uil.Ciudad.Controles.GSCiudadSch(), ref refXmlDocument, valSearchCriteria, valFixedCriteria, "Ciudad", new clsClienteList(null, null));
            return vResult;
        }

        public static bool ChooseZonaCobranza(System.Windows.Window refOwner, ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
            bool vResult = false;
            //vResult = LibFkRetrieval.ChooseRecord(refOwner, new Galac.Saw.Uil.Tablas.Sch.GSZonaCobranzaSch(), ref refXmlDocument, valSearchCriteria, valFixedCriteria, "Zona Cobranza", new clsClienteList(null, null));
            return vResult;
        }

        public static bool ChooseVendedor(System.Windows.Window refOwner, ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
            bool vResult = false;
            //vResult = LibFkRetrieval.ChooseRecord(refOwner, new Galac.dbo.Uil.Vendedor.Controles.GSVendedorSch(), ref refXmlDocument, valSearchCriteria, valFixedCriteria, "Vendedor", new clsClienteList(null, null));
            return vResult;
        }

        public static bool ChooseCuenta(System.Windows.Window refOwner, ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
            bool vResult = false;
            //vResult = LibFkRetrieval.ChooseRecord(refOwner, new Galac.dbo.Uil.Cuenta.Controles.GSCuentaSch(), ref refXmlDocument, valSearchCriteria, valFixedCriteria, "Cuenta", new clsClienteList(null, null));
            return vResult;
        }

        public static bool ChooseSectorDeNegocio(System.Windows.Window refOwner, ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
            bool vResult = false;
            //vResult = LibFkRetrieval.ChooseRecord(refOwner, new Galac.dbo.Uil.SectorDeNegocio.Controles.GSSectorDeNegocioSch(), ref refXmlDocument, valSearchCriteria, valFixedCriteria, "Sector de Negocio", new clsClienteList(null, null));
            return vResult;
        }
        #endregion //Métodos para Escoger
        #endregion //Metodos Generados


    } //End of class clsClienteList

} //End of namespace Galac.Saw.Uil.Clientes

