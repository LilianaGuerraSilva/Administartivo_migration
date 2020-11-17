using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using LibGalac.Aos.UI.Wpf;
using LibGalac.Aos.Uil;
using Galac.Saw.Ccl.Inventario;

namespace Galac.Saw.Uil.Inventario {
    /// <summary>
    /// Implementación de la Lista para el objeto
    /// </summary>
    public sealed class clsAlmacenList: LibUicMF, ILibDbSearchModule {
        #region Variables
        Almacen _Record;
        ILibBusinessComponentWithSearch<IList<Almacen>, IList<Almacen>> _Reglas;
        #endregion //Variables
        #region Propiedades
        public Almacen Record {
            get { return _Record;}
            set {_Record = value;}
        }
        #endregion //Propiedades
        #region Constructores
        public clsAlmacenList(LibXmlMemInfo initAppMemoryInfo, LibXmlMFC initMFC):base(initAppMemoryInfo, initMFC) {
            _Record = new Almacen();
        }
        #endregion //Constructores
        #region Metodos Generados

        #region Inicializacion BRL - a modificar si Remoting
        private void RegistraCliente() {
            if (WorkWithRemoting) {
                _Reglas = (ILibBusinessComponentWithSearch<IList<Almacen>, IList<Almacen>>)RegisterType();
            } else {
                _Reglas = new Galac.Saw.Brl.Inventario.clsAlmacenNav();
            }
        }
        #endregion //Inicializacion BRL - a modificar si Remoting

        public bool CanBeChoosen(IList<Almacen> valRecord, eAccionSR valAction) {
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
                IList<Almacen> vResulset = _Reglas.GetData(eProcessMessageType.SpName, "AlmacenGET", GetPkParams(valXmlRow));
                if (vResulset != null && vResulset.Count > 0) {
                    if (_Reglas.CanBeChoosen(vResulset, valAction)) {
                        clsAlmacenIpl vRecord = new clsAlmacenIpl(AppMemoryInfo, Mfc);
                        vRecord.ListAlmacen = vResulset;
                       
                        frmAlmacenInput insFrmInput = new frmAlmacenInput("Almacén", valAction, valExtendedAction);
                        if (insFrmInput.InitLookAndFeelAndSetValues(vRecord.ListAlmacen, vRecord)) {
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
            vResult = _Reglas.GetDataFromModule("Almacén", ref refResulset, valXmlParamsExpression);
            return vResult;
        }

        LibGalac.Aos.Base.Report.ILibReportInfo ILibDbSearch.GetDataRetrievesInstance() {
            return new Galac.Saw.Brl.Inventario.Reportes.clsAlmacenRpt();
        }
        bool ILibDbSearchModule.GetDataFromModule(string valModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            RegistraCliente();
            vResult = _Reglas.GetDataFromModule(valModule, ref refXmlDocument, valXmlParamsExpression);
            return vResult;
        }
        #endregion //Miembros de ILibDbSearchModule
        #region Métodos para Escoger

        public static bool ChooseAlmacen(System.Windows.Window refOwner, ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
            bool vResult = false;
            vResult = LibFkRetrieval.ChooseRecord(refOwner, new Galac.Saw.Uil.Inventario.Sch.GSAlmacenSch(), ref refXmlDocument, valSearchCriteria, valFixedCriteria, "Almacén", new clsAlmacenList(null, null));
            return vResult;
        }

        public static bool ChooseCliente(System.Windows.Window refOwner, ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
            bool vResult = false;
            vResult = LibFkRetrieval.ChooseRecord(refOwner, new Galac.Saw.Uil.Cliente.Controles.GSClienteSch(), ref refXmlDocument, valSearchCriteria, valFixedCriteria, "Cliente", new clsAlmacenList(null, null));
            return vResult;
        }

        public static bool ChooseCentroDeCostos(System.Windows.Window refOwner, ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
            bool vResult = false;
            vResult = LibFkRetrieval.ChooseRecord(refOwner, new Galac.Contab.Uil.ContabAdm.Sch.GSCentroDeCostosSch(), ref refXmlDocument, valSearchCriteria, valFixedCriteria, "Centro De Costos", new clsAlmacenList(null, null));
            return vResult;
        }
        #endregion //Métodos para Escoger
        #endregion //Metodos Generados

    } //End of class clsAlmacenList

} //End of namespace Galac.Saw.Uil.Inventario

