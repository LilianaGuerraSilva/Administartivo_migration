using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using LibGalac.Aos.UI.Wpf;
using LibGalac.Aos.Uil;
using Entity = Galac.Saw.Ccl.Vehiculo;

namespace Galac.Saw.Uil.Vehiculo {
    /// <summary>
    /// Implementación de la Lista para el objeto
    /// </summary>
    public sealed class clsVehiculoList: LibUicMF, ILibDbSearchModule {
        #region Variables
        Entity.Vehiculo _Record;
        ILibBusinessComponentWithSearch<IList<Entity.Vehiculo>, IList<Entity.Vehiculo>> _Reglas;
        #endregion //Variables
        #region Propiedades
        public Entity.Vehiculo Record {
            get { return _Record;}
            set {_Record = value;}
        }
        #endregion //Propiedades
        #region Constructores
        public clsVehiculoList(LibXmlMemInfo initAppMemoryInfo, LibXmlMFC initMFC):base(initAppMemoryInfo, initMFC) {
            _Record = new Entity.Vehiculo();
        }
        #endregion //Constructores
        #region Metodos Generados

        #region Inicializacion BRL - a modificar si Remoting
        private void RegistraCliente() {
            if (WorkWithRemoting) {
                _Reglas = (ILibBusinessComponentWithSearch<IList<Entity.Vehiculo>, IList<Entity.Vehiculo>>)RegisterType();
            } else {
                _Reglas = new Galac.Saw.Brl.Vehiculo.clsVehiculoNav();
            }
        }
        #endregion //Inicializacion BRL - a modificar si Remoting

        public bool CanBeChoosen(IList<Entity.Vehiculo> valRecord, eAccionSR valAction) {
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
            int vConsecutivo = insParser.GetInt(0, "Consecutivo",0);
            vParams.AddInInteger("Consecutivo", vConsecutivo);
            insParser  = null;
            return vParams.Get();
        }

        #region Miembros de ILibDbSearchModule
        void ILibDbSearch.DoAction(object refCallingForm, eAccionSR valAction, string valExtendedAction, XmlDocument valXmlRow) {
            if (valAction == eAccionSR.Escoger) {
                //do nothing;
            } else {
                RegistraCliente();
                IList<Entity.Vehiculo> vResulset = _Reglas.GetData(eProcessMessageType.SpName, "VehiculoGET", GetPkParams(valXmlRow));
                if (vResulset != null && vResulset.Count > 0) {
                    if (_Reglas.CanBeChoosen(vResulset, valAction)) {
                        clsVehiculoIpl vRecord = new clsVehiculoIpl(AppMemoryInfo, Mfc);
                        vRecord.ListVehiculo = vResulset;
                        frmVehiculoInput insFrmInput = new frmVehiculoInput("Vehiculo", valAction, valExtendedAction);
                        if (insFrmInput.InitLookAndFeelAndSetValues(vRecord.ListVehiculo, vRecord)) {
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
            vResult = _Reglas.GetDataFromModule("Vehiculo", ref refResulset, valXmlParamsExpression);
            return vResult;
        }
        bool ILibDbSearchModule.GetDataFromModule(string valModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            RegistraCliente();
            vResult = _Reglas.GetDataFromModule(valModule, ref refXmlDocument, valXmlParamsExpression);
            return vResult;
        }
        #endregion //Miembros de ILibDbSearchModule
        #region Métodos para Escoger

        LibGalac.Aos.Base.Report.ILibReportInfo ILibDbSearch.GetDataRetrievesInstance() {
            return new Galac.Saw.Brl.Vehiculo.Reportes.clsVehiculoRpt();
        }
        
        public static bool ChooseVehiculo(System.Windows.Window refOwner, ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
            bool vResult = false;
            vResult = LibFkRetrieval.ChooseRecord(refOwner, new Galac.Saw.Uil.Vehiculo.Sch.GSVehiculoSch(), ref refXmlDocument, valSearchCriteria, valFixedCriteria, "Vehiculo", new clsVehiculoList(null, null));
            return vResult;
        }

        public static bool ChooseModelo(System.Windows.Window refOwner, ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
            bool vResult = false;
            vResult = LibFkRetrieval.ChooseRecord(refOwner, new Galac.Saw.Uil.Vehiculo.Sch.GSModeloSch(), ref refXmlDocument, valSearchCriteria, valFixedCriteria, "Modelo", new clsVehiculoList(null, null));
            return vResult;
        }

        public static bool ChooseColor(System.Windows.Window refOwner, ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
            bool vResult = false;
            vResult = LibFkRetrieval.ChooseRecord(refOwner, new Galac.Saw.Uil.Inventario.Sch.GSColorSch(), ref refXmlDocument, valSearchCriteria, valFixedCriteria, "Color", new clsVehiculoList(null, null));
            return vResult;
        }

        public static bool ChooseCliente(System.Windows.Window refOwner, ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
            bool vResult = false;
            vResult = LibFkRetrieval.ChooseRecord(refOwner, new Galac.Saw.Uil.Cliente.Controles.GSClienteSch(), ref refXmlDocument, valSearchCriteria, valFixedCriteria, "Cliente", new clsVehiculoList(null, null));
            return vResult;
        }
        #endregion //Métodos para Escoger
        #endregion //Metodos Generados


    } //End of class clsVehiculoList

} //End of namespace Galac.Saw.Uil.Vehiculo

