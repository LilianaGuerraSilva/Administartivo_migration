using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm.Helpers;
using Galac.Adm.Ccl.Venta;
using Galac.Adm.Brl.Venta;
using Galac.Adm.Uil.Venta.ViewModel;
using LibGalac.Aos.UI.Mvvm.Validation;
namespace Galac.Adm.Uil.Venta.Reportes {

    public class clsCajaCerradaViewModel: LibInputRptViewModelBase<CajaApertura> {
        #region Variables
        const string NombreCajaPropertyName = "NombreCaja";
        string _NombreCaja;
        private FkCajaViewModel _ConexionNombreCaja = null;
        #endregion //Variables
        #region Propiedades
        public override string DisplayName { get { return "Caja Cerrada"; } }
        public LibXmlMemInfo AppMemoryInfo { get; set; }

        public RelayCommand<string> ChooseNombreCajaCommand {
            get;
            private set;
        }

        [LibRequired(ErrorMessage = "El Nombre de la Caja es requerido.")]
        [LibGridColum("Nombre Caja", eGridColumType.Connection, ConnectionSearchCommandName = "ChooseNombreCajaCommand", IsForSearch = true, IsForList = true, ColumnOrder = 0, Width = 190)]
        public string NombreCaja {
            get {
                return _NombreCaja;
            }
            set {
                if (_NombreCaja != value) {
                    _NombreCaja = value;
                    RaisePropertyChanged(NombreCajaPropertyName);
                    if (LibString.IsNullOrEmpty(NombreCaja, true)) {
                        ConexionNombreCaja = null;
                    }
                }
            }
        }

        public LibXmlMFC Mfc { get; set; }
        public override bool IsSSRS => throw new NotImplementedException();
        public int ConsecutivoCaja { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }
        public FkCajaViewModel ConexionNombreCaja {
            get {
                return _ConexionNombreCaja;
            }
            set {
                if (_ConexionNombreCaja != value) {
                    _ConexionNombreCaja = value;
                    RaisePropertyChanged(NombreCajaPropertyName);
                }
                if (_ConexionNombreCaja == null) {
                    NombreCaja = string.Empty;
                }
            }
        }
        #endregion //Propiedades
        #region Constructores
        public clsCajaCerradaViewModel() {
            FechaDesde = LibConvert.ToDate("01/" + LibDate.Today().Month.ToString() + "/" + LibDate.Today().Year.ToString());
            FechaHasta = LibDate.Today();
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseNombreCajaCommand = new RelayCommand<string>(ExecuteChooseNombreCajaCommand);
        }

        private void ExecuteChooseNombreCajaCommand(string valNombreCaja) {
            try {
                if (valNombreCaja == null) {
                    valNombreCaja = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("NombreCaja", valNombreCaja);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoCompania", LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"));
                ConexionNombreCaja = ChooseRecord<FkCajaViewModel>("Caja", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (ConexionNombreCaja != null) {
                    ConsecutivoCaja = ConexionNombreCaja.Consecutivo;
                    NombreCaja = ConexionNombreCaja.NombreCaja;
                } else {
                    ConsecutivoCaja = 0;
                    NombreCaja = string.Empty;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, DisplayName);
            }
        }

        protected override ILibBusinessSearch GetBusinessComponent() {
            return new clsCajaAperturaNav();
        }
        #endregion //Metodos Generados

    } //End of class clsCajaCerradaViewModel
} //End of namespace Galac.Adm.Uil.Venta