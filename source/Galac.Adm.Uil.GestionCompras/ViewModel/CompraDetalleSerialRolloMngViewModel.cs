using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm.Messaging;
using LibGalac.Aos.UI.Mvvm.Ribbon;
using LibGalac.Aos.UI.Mvvm.Helpers;
using LibGalac.Aos.Brl.Contracts;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using LibGalac.Aos.Catching;
using LibGalac.Aos.ARRpt.Reports;
using Galac.Adm.Brl.GestionCompras;
using Galac.Adm.Brl.GestionCompras.Reportes;
using Galac.Adm.Ccl.GestionCompras;

namespace Galac.Adm.Uil.GestionCompras.ViewModel {

    public class CompraDetalleSerialRolloMngViewModel : LibMngDetailViewModelMfc<CompraDetalleSerialRolloViewModel, CompraDetalleSerialRollo> {
        #region Propiedades

        public override string ModuleName {
            get { return "Compra Detalle Serial Rollo"; }
        }

        public CompraViewModel Master {
            get;
            set;
        }

        public new ObservableCollection<LibGridColumModel> VisibleColumns
        {
            get;
            private set;
        }
        Saw.Ccl.Inventario.eTipoArticuloInv _TipoArticuloInventario;
        public Saw.Ccl.Inventario.eTipoArticuloInv TipoArticuloInventario {
            get { return _TipoArticuloInventario; }
            set {
                _TipoArticuloInventario = value;
                foreach (var item in Items) {
                    item.UsaRollo(value);
                }
            }
        }
        #endregion //Propiedades
        #region Constructores

        public CompraDetalleSerialRolloMngViewModel(CompraViewModel initMaster, Saw.Ccl.Inventario.eTipoArticuloInv initTipoArticuloInv)
            : base(LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            Master = initMaster;
            Title = "Buscar " + ModuleName;
            VisibleColumns = LibGridColumModel.GetGridColumsFromType(typeof(CompraDetalleSerialRolloViewModel));
            VisibleColumns[0].Header = new CompraDetalleSerialRolloViewModel().SinonimoSerial;
            VisibleColumns[1].Header = new CompraDetalleSerialRolloViewModel().SinonimoRollo;
            if (initTipoArticuloInv == Saw.Ccl.Inventario.eTipoArticuloInv.UsaSerial) {
                VisibleColumns.RemoveAt(2);
                VisibleColumns.RemoveAt(1);
            }
            TipoArticuloInventario = initTipoArticuloInv;
        }

        public CompraDetalleSerialRolloMngViewModel(CompraViewModel initMaster, ObservableCollection<CompraDetalleSerialRollo> initDetail, eAccionSR initAction)
            : base(LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            Master = initMaster;
            foreach (var vItem in initDetail) {
                var vViewModel = new CompraDetalleSerialRolloViewModel(Master, vItem, initAction);
                vViewModel.InitializeViewModel(initAction);
                Add(vViewModel);
            }

        }
        #endregion //Constructores
        #region Metodos Generados

        protected override CompraDetalleSerialRolloViewModel CreateNewElement(CompraDetalleSerialRollo valModel) {
            var vNewModel = valModel;
            if (vNewModel == null) {
                vNewModel = new CompraDetalleSerialRollo();
            }
            return new CompraDetalleSerialRolloViewModel(Master, vNewModel, eAccionSR.Insertar, TipoArticuloInventario);
        }

        protected override void RaiseOnCreatedEvent(CompraDetalleSerialRolloViewModel valViewModel) {
            valViewModel.Master = Master;
            base.RaiseOnCreatedEvent(valViewModel);
        }
        #endregion //Metodos Generados


    } //End of class CompraDetalleSerialRolloMngViewModel

} //End of namespace Galac.Adm.Uil.GestionCompras

