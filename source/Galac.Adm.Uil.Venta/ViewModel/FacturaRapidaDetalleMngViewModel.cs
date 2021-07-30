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
using Galac.Adm.Brl.Venta;
using Galac.Adm.Ccl.Venta;
using Galac.Saw.Ccl.Inventario;

namespace Galac.Adm.Uil.Venta.ViewModel {

    public class FacturaRapidaDetalleMngViewModel : LibMngDetailViewModelMfc<FacturaRapidaDetalleViewModel, FacturaRapidaDetalle> {
        #region Propiedades

        public override string ModuleName {
            get {
                return "Punto de Venta Detalle";
            }
        }

        public FacturaRapidaViewModel Master {
            get;
            set;
        }

        public new ObservableCollection<LibGridColumModel> VisibleColumns {
            get;
            private set;
        }
        #endregion //Propiedades
        #region Constructores

        public FacturaRapidaDetalleMngViewModel(FacturaRapidaViewModel initMaster)
            : base(LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            Master = initMaster;
            Title = "Buscar " + ModuleName;
        }

        public FacturaRapidaDetalleMngViewModel(FacturaRapidaViewModel initMaster, ObservableCollection<FacturaRapidaDetalle> initDetail, eAccionSR initAction)
            : base(LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            Master = initMaster;
            foreach (var vItem in initDetail) {
                var vViewModel = new FacturaRapidaDetalleViewModel(Master, vItem, initAction);
                vViewModel.InitializeViewModel(initAction);
                Add(vViewModel);
            }
            ColumnaDePrecioAMostrar();
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override FacturaRapidaDetalleViewModel CreateNewElement(FacturaRapidaDetalle valModel) {
            var vNewModel = valModel;
            if (vNewModel == null) {
                vNewModel = new FacturaRapidaDetalle();
            }
            return new FacturaRapidaDetalleViewModel(Master, vNewModel, eAccionSR.Insertar);
        }

        protected override void RaiseOnCreatedEvent(FacturaRapidaDetalleViewModel valViewModel) {
            valViewModel.Master = Master;
            base.RaiseOnCreatedEvent(valViewModel);
        }

        internal void InsertRow(string valArticulo, string valDescripcion, decimal valCantidad, decimal valPrecioSinIVA, decimal valPrecioConIVA, eTipoDeAlicuota valAlicuotaIva, decimal valPorcentajeBaseImponible, eTipoDeArticulo valTipoDeArticulo, eTipoArticuloInv valTipoArticuloInv) {
            try {
                if (Master.Action != eAccionSR.Consultar && Master.Action != eAccionSR.Eliminar) {
                    FacturaRapidaDetalleViewModel vViewModel = CreateNewElement(null);
                    vViewModel.Articulo = valArticulo;
                    vViewModel.Descripcion = valDescripcion;
                    vViewModel.Cantidad = valCantidad;
                    vViewModel.AlicuotaIva = valAlicuotaIva;
                    vViewModel.PorcentajeBaseImponible = valPorcentajeBaseImponible;
                    vViewModel.PrecioSinIVA = valPrecioSinIVA;
                    vViewModel.PrecioConIVA = valPrecioConIVA;
                    vViewModel.AlicuotaIva = valAlicuotaIva;
                    vViewModel.PorcentajeBaseImponible = valPorcentajeBaseImponible;
                    vViewModel.TipoDeArticulo = valTipoDeArticulo;
                    vViewModel.TipoArticuloInv = valTipoArticuloInv;
                    SetPropertiesListener(vViewModel);
                    vViewModel.Action = eAccionSR.Insertar;
                    Add(vViewModel);
                    SelectedItem = vViewModel;
                    if (Items.Count > 1) {
                        SelectedIndex = Items.Count - 1;
                    }
                    RaiseSelectedItemChanged();

                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }

        private int _SelectedIndex;
        public int SelectedIndex {
            get {
                return _SelectedIndex;
            }
            set {
                _SelectedIndex = value;
                RaisePropertyChanged("SelectedIndex");
            }
        }

        private void ColumnaDePrecioAMostrar() {
            VisibleColumns = LibGridColumModel.GetGridColumsFromType(typeof(FacturaRapidaDetalleViewModel));
            if (LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("FacturaRapida", "UsaPrecioSinIva")) {
                VisibleColumns.RemoveAt(4);
            } else {
                VisibleColumns.RemoveAt(3);
            }
        }
        #endregion //Metodos Generados

        internal void ActualizaTotalRenglon() {

            foreach (FacturaRapidaDetalleViewModel vItem in Items) {
                if (LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("FacturaRapida", "UsaPrecioSinIva")) {
                    if (vItem.AlicuotaIva == eTipoDeAlicuota.Exento) {
                        vItem.PrecioConIVA = LibMath.RoundToNDecimals(vItem.CalcularPreciosPorDetail(vItem.PrecioSinIVA, 0, true, 100), Master._CantidadDeDecimales);
                        vItem.PorcentajeAlicuota = 0;
                    } else if (vItem.AlicuotaIva == eTipoDeAlicuota.AlicuotaGeneral) {
                        vItem.PrecioConIVA = LibMath.RoundToNDecimals(vItem.CalcularPreciosPorDetail(vItem.PrecioSinIVA, Master.PorcentajeAlicuota1, true, vItem.PorcentajeBaseImponible), Master._CantidadDeDecimales);
                        vItem.PorcentajeAlicuota = Master.PorcentajeAlicuota1;
                    } else if (vItem.AlicuotaIva == eTipoDeAlicuota.Alicuota2) {
                        vItem.PrecioConIVA = LibMath.RoundToNDecimals(vItem.CalcularPreciosPorDetail(vItem.PrecioSinIVA, Master.PorcentajeAlicuota2, true, vItem.PorcentajeBaseImponible), Master._CantidadDeDecimales);
                        vItem.PorcentajeAlicuota = Master.PorcentajeAlicuota2;
                    } else if (vItem.AlicuotaIva == eTipoDeAlicuota.Alicuota3) {
                        vItem.PrecioConIVA = LibMath.RoundToNDecimals(vItem.CalcularPreciosPorDetail(vItem.PrecioSinIVA, Master.PorcentajeAlicuota3, true, vItem.PorcentajeBaseImponible), Master._CantidadDeDecimales);
                        vItem.PorcentajeAlicuota = Master.PorcentajeAlicuota3;
                    }                    
                    vItem.TotalRenglon = LibMath.RoundToNDecimals(vItem.PrecioSinIVA * vItem.Cantidad, 2);
                } else if (!LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("FacturaRapida", "UsaPrecioSinIva")) {
                    if (LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("FacturaRapida", "AcumularItemsEnRenglonesDeFactura ")) {
                        vItem.TotalRenglon = LibMath.RoundToNDecimals(vItem.PrecioConIVA * vItem.Cantidad, 2);
                    } else {
                        vItem.TotalRenglon = LibMath.RoundToNDecimals(vItem.PrecioConIVA * vItem.Cantidad, 2);
                    }
                    //BuscoPrecioSinIva
                    if (vItem.AlicuotaIva == eTipoDeAlicuota.Exento) {
                        vItem.PrecioSinIVA = LibMath.RoundToNDecimals(vItem.CalcularPreciosPorDetail(vItem.PrecioConIVA, 0, false, 100), Master._CantidadDeDecimales);
                        vItem.PorcentajeAlicuota = 0;
                    } else if (vItem.AlicuotaIva == eTipoDeAlicuota.AlicuotaGeneral) {
                        vItem.PrecioSinIVA = LibMath.RoundToNDecimals(vItem.CalcularPreciosPorDetail(vItem.PrecioConIVA, Master.PorcentajeAlicuota1, false, vItem.PorcentajeBaseImponible), Master._CantidadDeDecimales);
                        vItem.PorcentajeAlicuota = Master.PorcentajeAlicuota1;
                    } else if (vItem.AlicuotaIva == eTipoDeAlicuota.Alicuota2) {
                        vItem.PrecioSinIVA = LibMath.RoundToNDecimals(vItem.CalcularPreciosPorDetail(vItem.PrecioConIVA, Master.PorcentajeAlicuota2, false, vItem.PorcentajeBaseImponible), Master._CantidadDeDecimales);
                        vItem.PorcentajeAlicuota = Master.PorcentajeAlicuota2;
                    } else if (vItem.AlicuotaIva == eTipoDeAlicuota.Alicuota3) {
                        vItem.PrecioSinIVA = LibMath.RoundToNDecimals(vItem.CalcularPreciosPorDetail(vItem.PrecioConIVA, Master.PorcentajeAlicuota3, false, vItem.PorcentajeBaseImponible), Master._CantidadDeDecimales);
                        vItem.PorcentajeAlicuota = Master.PorcentajeAlicuota3;
                    }
                }
            }
        }

        internal decimal MontoTotalExento(int valCantidadDeDecimales) {
            return LibMath.RoundToNDecimals(Items.Where(i => i.AlicuotaIva == eTipoDeAlicuota.Exento).Sum(o => o.PrecioSinIVA * o.Cantidad), valCantidadDeDecimales);
        }

        internal decimal MontoIvaGeneral(int valCantidadDeDecimales) {
            return LibMath.RoundToNDecimals(Items.Where(i => i.AlicuotaIva == eTipoDeAlicuota.AlicuotaGeneral).Sum(o => CalcularMontoIvaPorDetail(o.PrecioSinIVA, o.Cantidad, Master.PorcentajeAlicuota1, o.PorcentajeBaseImponible)), valCantidadDeDecimales);
        }

        internal decimal MontoGravableGeneral(int valCantidadDeDecimales) {
            return LibMath.RoundToNDecimals(Items.Where(i => (i.AlicuotaIva == eTipoDeAlicuota.AlicuotaGeneral)).Sum(o => CalcularMontoBaseImponible(o.PrecioSinIVA, o.Cantidad, o.PorcentajeBaseImponible)), valCantidadDeDecimales);
        }

        internal decimal MontoGravableGeneralConIva(int valCantidadDeDecimales) {
            return LibMath.RoundToNDecimals(Items.Where(i => (i.AlicuotaIva == eTipoDeAlicuota.AlicuotaGeneral)).Sum(o => CalcularMontoBaseImponible(o.PrecioConIVA, o.Cantidad, o.PorcentajeBaseImponible)), valCantidadDeDecimales);
        }

        internal decimal MontoIvaReducida(int valCantidadDeDecimales) {
            return LibMath.RoundToNDecimals(Items.Where(i => (i.AlicuotaIva == eTipoDeAlicuota.Alicuota2)).Sum(o => CalcularMontoIvaPorDetail(o.PrecioSinIVA, o.Cantidad, Master.PorcentajeAlicuota2, o.PorcentajeBaseImponible)), valCantidadDeDecimales);
        }

        internal decimal MontoGravableReducida(int valCantidadDeDecimales) {
            return LibMath.RoundToNDecimals(Items.Where(i => (i.AlicuotaIva == eTipoDeAlicuota.Alicuota2)).Sum(o => CalcularMontoBaseImponible(o.PrecioSinIVA, o.Cantidad, o.PorcentajeBaseImponible)), valCantidadDeDecimales);
        }

        internal decimal MontoGravableReducidaConIva(int valCantidadDeDecimales) {
            return LibMath.RoundToNDecimals(Items.Where(i => (i.AlicuotaIva == eTipoDeAlicuota.Alicuota2)).Sum(o => CalcularMontoBaseImponible(o.PrecioConIVA, o.Cantidad, o.PorcentajeBaseImponible)), valCantidadDeDecimales);
        }

        internal decimal MontoIvaExtendida(int valCantidadDeDecimales) {
            return LibMath.RoundToNDecimals(Items.Where(i => (i.AlicuotaIva == eTipoDeAlicuota.Alicuota3)).Sum(o => CalcularMontoIvaPorDetail(o.PrecioSinIVA, o.Cantidad, Master.PorcentajeAlicuota3, o.PorcentajeBaseImponible)), valCantidadDeDecimales);
        }

        internal decimal MontoGravableExtendida(int valCantidadDeDecimales) {
            return LibMath.RoundToNDecimals(Items.Where(i => (i.AlicuotaIva == eTipoDeAlicuota.Alicuota3)).Sum(o => CalcularMontoBaseImponible(o.PrecioSinIVA, o.Cantidad, o.PorcentajeBaseImponible)), valCantidadDeDecimales);
        }

        internal decimal MontoGravableExtendidaConIva(int valCantidadDeDecimales) {
            return LibMath.RoundToNDecimals(Items.Where(i => (i.AlicuotaIva == eTipoDeAlicuota.Alicuota3)).Sum(o => CalcularMontoBaseImponible(o.PrecioConIVA, o.Cantidad, o.PorcentajeBaseImponible)), valCantidadDeDecimales);
        }

        private decimal CalcularMontoIvaPorDetail(decimal valPrecioSinIva, decimal valCantidad, decimal valPorcentaje, decimal valPorcentajeBaseImponible) {
            decimal vResult = 0;
            if ((valPrecioSinIva > 0) && (valCantidad > 0) && (valPorcentaje > 0) && (valPorcentajeBaseImponible > 0)) {
                vResult = (CalcularMontoBaseImponible(valPrecioSinIva, valCantidad, valPorcentajeBaseImponible) * valPorcentaje) / 100;
            }
            return vResult;
        }

        private decimal CalcularMontoBaseImponible(decimal valPrecioSinIva, decimal valCantidad, decimal valPorcentajeBaseImponible) {
            decimal vResult = 0;
            if ((valPrecioSinIva > 0) && (valCantidad > 0) && (valPorcentajeBaseImponible > 0)) {
                vResult = LibMath.RoundToNDecimals((valPrecioSinIva * valCantidad) * (valPorcentajeBaseImponible / 100), 2);
            }
            return vResult;
        }

        internal decimal CalcularTotalMontoBaseImponible() {
            if (LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("FacturaRapida", "UsaPrecioSinIva") == true) {
                return Items.Where(i => (i.AlicuotaIva != eTipoDeAlicuota.Exento)).Sum(o => o.TotalRenglon);
            } else {
                return LibMath.RoundToNDecimals(Items.Where(i => (i.AlicuotaIva != eTipoDeAlicuota.Exento)).Sum(s => s.PrecioSinIVA * s.Cantidad), 2);
            }
        }

        internal void CalcularPreciosParaEtiquetas(bool valEmpresaUsaPrecioSinIva, bool valEtiquetaIncluyeIva, decimal valPrecioBase, decimal valPorcentajeBaseImponible, decimal valAlicuotaIVA, ref decimal refPrecioSinIva, ref decimal refPrecioConIva) {

            if (valEmpresaUsaPrecioSinIva && valEtiquetaIncluyeIva) {
                refPrecioSinIva= CalcularPrecioSinIvaDeEtiquetas(valPrecioBase, valPorcentajeBaseImponible, (eTipoDeAlicuota)valAlicuotaIVA);
                refPrecioConIva = valPrecioBase;
            } else if (valEmpresaUsaPrecioSinIva && !valEtiquetaIncluyeIva) {
                refPrecioConIva= CalcularPrecioConIvaDeEtiquetas(valPrecioBase, valPorcentajeBaseImponible, (eTipoDeAlicuota)valAlicuotaIVA);
                refPrecioSinIva=valPrecioBase;
            } else if (!valEmpresaUsaPrecioSinIva && valEtiquetaIncluyeIva) {
                refPrecioSinIva= CalcularPrecioSinIvaDeEtiquetas(valPrecioBase, valPorcentajeBaseImponible, (eTipoDeAlicuota)valAlicuotaIVA);
                refPrecioConIva=valPrecioBase;
            } else if (!valEmpresaUsaPrecioSinIva && !valEtiquetaIncluyeIva) {
                refPrecioConIva= CalcularPrecioConIvaDeEtiquetas(valPrecioBase, valPorcentajeBaseImponible, (eTipoDeAlicuota)valAlicuotaIVA);
                refPrecioSinIva=valPrecioBase;
            }
        }

        private decimal CalcularPrecioSinIvaDeEtiquetas(decimal valPrecioConIva, decimal valPorcentajeBaseImponible, eTipoDeAlicuota valAlicuotaIva) {
            decimal vResult = 0;
            FacturaRapidaDetalleViewModel vItem = new FacturaRapidaDetalleViewModel();
            if (valAlicuotaIva == eTipoDeAlicuota.Exento) {
                vResult = LibMath.RoundToNDecimals(vItem.CalcularPreciosPorDetail(valPrecioConIva, 0, false, 100), Master._CantidadDeDecimales);
            } else if (valAlicuotaIva == eTipoDeAlicuota.AlicuotaGeneral) {
                vResult = LibMath.RoundToNDecimals(vItem.CalcularPreciosPorDetail(valPrecioConIva, Master.PorcentajeAlicuota1, false, valPorcentajeBaseImponible), Master._CantidadDeDecimales);
            } else if (valAlicuotaIva == eTipoDeAlicuota.Alicuota2) {
                vResult = LibMath.RoundToNDecimals(vItem.CalcularPreciosPorDetail(valPrecioConIva, Master.PorcentajeAlicuota2, false, valPorcentajeBaseImponible), Master._CantidadDeDecimales);
            } else if (valAlicuotaIva == eTipoDeAlicuota.Alicuota3) {
                vResult = LibMath.RoundToNDecimals(vItem.CalcularPreciosPorDetail(valPrecioConIva, Master.PorcentajeAlicuota3, false, valPorcentajeBaseImponible), Master._CantidadDeDecimales);
            }
            return vResult;
        }

        private decimal CalcularPrecioConIvaDeEtiquetas(decimal valPrecioConIva, decimal valPorcentajeBaseImponible, eTipoDeAlicuota valAlicuotaIva) {
            decimal vResult = 0;
            FacturaRapidaDetalleViewModel vItem = new FacturaRapidaDetalleViewModel();

            if (valAlicuotaIva == eTipoDeAlicuota.Exento) {
                vResult = LibMath.RoundToNDecimals(vItem.CalcularPreciosPorDetail(valPrecioConIva, 0, true, 100), Master._CantidadDeDecimales);
            } else if (valAlicuotaIva == eTipoDeAlicuota.AlicuotaGeneral) {
                vResult = LibMath.RoundToNDecimals(vItem.CalcularPreciosPorDetail(valPrecioConIva, Master.PorcentajeAlicuota1, true, valPorcentajeBaseImponible), Master._CantidadDeDecimales);
            } else if (valAlicuotaIva == eTipoDeAlicuota.Alicuota2) {
                vResult = LibMath.RoundToNDecimals(vItem.CalcularPreciosPorDetail(valPrecioConIva, Master.PorcentajeAlicuota2, true, valPorcentajeBaseImponible), Master._CantidadDeDecimales);
            } else if (valAlicuotaIva == eTipoDeAlicuota.Alicuota3) {
                vResult = LibMath.RoundToNDecimals(vItem.CalcularPreciosPorDetail(valPrecioConIva, Master.PorcentajeAlicuota3, true, valPorcentajeBaseImponible), Master._CantidadDeDecimales);
            }

            return vResult;
        }

        private string CalcularChecksum(string valCodigo) {
            string vResult = "";
            int vLongitudCodigo = 0;

            vLongitudCodigo = LibString.Len(valCodigo);
            valCodigo = LibString.SubString(valCodigo, 0, vLongitudCodigo - 1);
            vLongitudCodigo = LibString.Len(valCodigo);
            int vSuma = 0;
            for (int vPosicion = 0; vPosicion < vLongitudCodigo; vPosicion++) {
                int vValorCaracter = LibConvert.ToInt(LibString.SubString(valCodigo, vLongitudCodigo - 1 - vPosicion, 1));
                if ((vPosicion % 2).Equals(0)) {
                    vSuma += vValorCaracter * 3;
                } else {
                    vSuma += vValorCaracter;
                }
            }

            if ((vSuma % 10).Equals(0)) {
                vResult = "0";
            } else {
                vResult = LibConvert.ToStr(10 - vSuma % 10);
            }
            return vResult;
        }



        internal void ProcesarArticuloPorPesoOPrecio(ref string refCodigo, ref decimal refPrecio, ref decimal refPeso) {
            string vChecksumRecibido = "";
            string vPrefijoCodigoPeso = "";
            string vPrefijoCodigoPrecio = "";
            string vPrefijoPrecioEnCodigoBarra = "";
            string vPrefijoPesoEnCodigoBarra = "";
            bool vUsaPesoEnCodigo = false;
            bool vUsaPrecioEnCodigo = false;
            int vLongitudPrefijo = 0;
            string vCheckSumCalculado = "";
            bool CheckSumCorrecto = false;

            vPrefijoCodigoPeso = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("FacturaRapida", "PrefijoCodigoPeso");
            vPrefijoCodigoPrecio = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("FacturaRapida", "PrefijoCodigoPrecio");
            vUsaPesoEnCodigo = LibConvert.SNToBool(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("FacturaRapida", "UsaPesoEnCodigo"));
            vUsaPrecioEnCodigo = LibConvert.SNToBool(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("FacturaRapida", "UsaPrecioEnCodigo"));

            vLongitudPrefijo = LibString.Len(vPrefijoCodigoPrecio);
            vPrefijoPrecioEnCodigoBarra = LibString.SubString(refCodigo, 0, vLongitudPrefijo);
            vLongitudPrefijo = LibString.Len(vPrefijoCodigoPeso);
            vPrefijoPesoEnCodigoBarra = LibString.SubString(refCodigo, 0, vLongitudPrefijo);
            vChecksumRecibido = LibString.SubString(refCodigo, LibString.Len(refCodigo) - 1, 1);
            vCheckSumCalculado = CalcularChecksum(refCodigo);
            CheckSumCorrecto = LibString.S1IsEqualToS2(vChecksumRecibido, vCheckSumCalculado);

            if (LibString.S1IsEqualToS2(vPrefijoCodigoPrecio, vPrefijoPrecioEnCodigoBarra) && vUsaPrecioEnCodigo) {
                if (CheckSumCorrecto) {
                    ProcesarArticuloPorPrecio(ref refCodigo, ref refPrecio);
                } else {
                    refCodigo = string.Empty;
                    LibMessages.MessageBox.Information(this, "El código en la etiqueta presenta error de lectura, favor verificar e intente de nuevo", " Punto de Venta ");
                }
            } else if (LibString.S1IsEqualToS2(vPrefijoCodigoPeso, vPrefijoPesoEnCodigoBarra) && vUsaPesoEnCodigo) {
                if (CheckSumCorrecto) {
                    ProcesarArticuloPorPeso(ref refCodigo, ref  refPeso);
                } else {
                    refCodigo = string.Empty;
                    LibMessages.MessageBox.Information(this, "El código en la etiqueta presenta error de lectura, favor verificar e intente de nuevo", " Punto de Venta ");
                }
            }
        }

        private void ProcesarArticuloPorPeso(ref string refCodigo, ref decimal refPeso) {

            int vDigitosEnCodigoArticulo = 0;
            int vPosicionDeInicioCodigoArticulo = 0;
            int vDigitosEnPeso = 0;
            string vCodigoBarraResult = "";
            string vPrefijoCodigoPeso = "";
            int vLongitudCodigoArticulo = 0;
            int vDecimalesEnPeso = 0;

            vCodigoBarraResult = refCodigo;
            vDecimalesEnPeso = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("FacturaRapida", "NumDecimalesPeso");
            vPrefijoCodigoPeso = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("FacturaRapida", "PrefijoCodigoPeso");
            vLongitudCodigoArticulo = LibString.Len(vPrefijoCodigoPeso);
            vDigitosEnPeso = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("FacturaRapida", "NumDigitosPeso");
            vDigitosEnCodigoArticulo = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("FacturaRapida", "NumDigitosCodigoArticuloPeso");
            vPosicionDeInicioCodigoArticulo = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("FacturaRapida", "PosicionCodigoArticuloPeso") - 1;
            vDecimalesEnPeso = vDecimalesEnPeso * -1;

            if (LibConvert.Equals(LibString.Len(vPrefijoCodigoPeso), vPosicionDeInicioCodigoArticulo)) {
                refCodigo = LibString.SubString(vCodigoBarraResult, vPosicionDeInicioCodigoArticulo, vDigitosEnCodigoArticulo);
                refPeso = LibConvert.ToDec(LibString.SubString(vCodigoBarraResult, vDigitosEnCodigoArticulo + vLongitudCodigoArticulo, vDigitosEnPeso));
            } else {
                refPeso = LibConvert.ToDec(LibString.SubString(vCodigoBarraResult, vLongitudCodigoArticulo, vDigitosEnPeso));
                refCodigo = LibString.SubString(vCodigoBarraResult, vLongitudCodigoArticulo + vDigitosEnPeso, vDigitosEnCodigoArticulo);
            }
            refPeso = refPeso * LibConvert.ToDec(Math.Pow(10, vDecimalesEnPeso));
            vDecimalesEnPeso = vDecimalesEnPeso * -1;
            refPeso = LibMath.RoundToNDecimals(refPeso,vDecimalesEnPeso);
        }

        private void ProcesarArticuloPorPrecio(ref string refCodigo, ref decimal refPrecio) {

            int vDigitosEnCodigoArticulo = 0;
            int vPosicionDeInicioCodigoArticulo = 0;
            int vDigitosEnPrecio = 0;
            string vCodigoBarraResult = "";
            string vPrefijoCodigoPrecio = "";
            int vLongitudCodigoArticulo = 0;
            int vDecimalesEnPrecio = 0;

            vCodigoBarraResult = refCodigo;
            vDecimalesEnPrecio = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("FacturaRapida", "NumDecimalesPrecio");
            vPrefijoCodigoPrecio = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("FacturaRapida", "PrefijoCodigoPrecio");
            vLongitudCodigoArticulo = LibString.Len(vPrefijoCodigoPrecio);
            vDigitosEnPrecio = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("FacturaRapida", "NumDigitosPrecio");
            vDigitosEnCodigoArticulo = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("FacturaRapida", "NumDigitosCodigoArticuloPrecio");
            vPosicionDeInicioCodigoArticulo = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("FacturaRapida", "PosicionCodigoArticuloPrecio")-1;
            vDecimalesEnPrecio = vDecimalesEnPrecio * -1;

            if (LibConvert.Equals(LibString.Len(vPrefijoCodigoPrecio), vPosicionDeInicioCodigoArticulo)) {
                refCodigo = LibString.SubString(vCodigoBarraResult, vPosicionDeInicioCodigoArticulo, vDigitosEnCodigoArticulo);
                refPrecio = LibConvert.ToDec(LibString.SubString(vCodigoBarraResult, vDigitosEnCodigoArticulo + vLongitudCodigoArticulo, vDigitosEnPrecio));
            } else {
                refPrecio = LibConvert.ToDec(LibString.SubString(vCodigoBarraResult, vLongitudCodigoArticulo, vDigitosEnPrecio));
                refCodigo = LibString.SubString(vCodigoBarraResult, vLongitudCodigoArticulo + vDigitosEnPrecio, vDigitosEnCodigoArticulo);
            }
            refPrecio = refPrecio * LibConvert.ToDec(Math.Pow(10, vDecimalesEnPrecio));
        }

        public void RecalcularRenglonesDeFacturaEnEspera() {
            foreach (FacturaRapidaDetalleViewModel vItem in Items) {
                vItem.PrecioConIVA = LibMath.RoundToNDecimals((vItem.PrecioConIVA / Master._CambioOriginalDeFacturaEnEspera) * Master.CambioMostrarTotalEnDivisas, Master._CantidadDeDecimales);
                vItem.PrecioSinIVA = LibMath.RoundToNDecimals(vItem.PrecioConIVA / FactorBaseAlicuotaIva(vItem.AlicuotaIva), Master._CantidadDeDecimales);
            }
        }

        private decimal FactorBaseAlicuotaIva(eTipoDeAlicuota valTipoDeAlicuota) {
            decimal vResult = 0;
            switch(valTipoDeAlicuota) {
            case eTipoDeAlicuota.Exento:
                vResult = 1m;
                break;
            case eTipoDeAlicuota.AlicuotaGeneral:
                vResult = 1 + (Master.PorcentajeAlicuota1 / 100m);
                break;
            case eTipoDeAlicuota.Alicuota2:
                vResult = 1 + (Master.PorcentajeAlicuota2 / 100m);
                break;
            case eTipoDeAlicuota.Alicuota3:
                vResult = 1 + (Master.PorcentajeAlicuota3 / 100m);
                break;
            default:
                vResult = 1 + (Master.PorcentajeAlicuota1 / 100m);
                break;
            }
            return vResult;
        }

    } //End of class FacturaRapidaDetalleMngViewModel
}//End of namespace Galac.Adm.Uil.Venta

