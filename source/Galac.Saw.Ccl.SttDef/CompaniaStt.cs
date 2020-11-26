using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using Galac.Saw.Ccl.SttDef;

namespace Galac.Saw.Ccl.SttDef {
    [Serializable]
    public class CompaniaStt : ISettDefinition {
        private string _GroupName = null;
        private string _Module = null;

        public string GroupName {
            get { return _GroupName; }
            set { _GroupName = value; }
        }

        public string Module {
            get { return _Module; }
            set { _Module = value; }
        }
        #region Variables
        private DateTime _FechaDeInicioContabilizacion;
        private bool _EsAsociadoEnCtaDeParticipacion;
        private bool _VerificarDocumentoSinContabilizar;
        private eTipoDeAgrupacionParaLibrosDeVenta _TipoDeAgrupacionParaLibrosDeVenta;
        private bool _IntegracionRIS;
        private DateTime _FechaMinimaIngresarDatos;
        private bool _AutorellenaResumenDiario;
        private eTipoNegocio _TipoNegocio;
        private long _fldTimeStamp;
        private bool _UsarVentasConIvaDiferido;
        private bool _ImprimirVentasDiferidas;
        private eAplicacionAlicuota _AplicacionAlicuotaEspecial;
        private bool _AplicarIVAEspecial;
        private bool _FacturarPorDefectoIvaEspecial;
        private DateTime _FechaInicioAlicuotaIva10Porciento;
        private DateTime _FechaFinAlicuotaIva10Porciento;
        private bool _ImprimirMensajeAplicacionDecreto;
        private eBaseCalculoParaAlicuotaEspecial _BaseDeCalculoParaAlicuotaEspecial;

        XmlDocument _datos;
        #endregion //Variables
        #region Propiedades


        public DateTime FechaDeInicioContabilizacion {
            get { return _FechaDeInicioContabilizacion; }
            set { _FechaDeInicioContabilizacion = LibConvert.DateToDbValue(value); }
        }

        public bool EsAsociadoEnCtaDeParticipacionAsBool {
            get { return _EsAsociadoEnCtaDeParticipacion; }
            set { _EsAsociadoEnCtaDeParticipacion = value; }
        }

        public string EsAsociadoEnCtaDeParticipacion {
            set { _EsAsociadoEnCtaDeParticipacion = LibConvert.SNToBool(value); }
        }


        public bool VerificarDocumentoSinContabilizarAsBool {
            get { return _VerificarDocumentoSinContabilizar; }
            set { _VerificarDocumentoSinContabilizar = value; }
        }

        public string VerificarDocumentoSinContabilizar {
            set { _VerificarDocumentoSinContabilizar = LibConvert.SNToBool(value); }
        }


        public eTipoDeAgrupacionParaLibrosDeVenta TipoDeAgrupacionParaLibrosDeVentaAsEnum {
            get { return _TipoDeAgrupacionParaLibrosDeVenta; }
            set { _TipoDeAgrupacionParaLibrosDeVenta = value; }
        }

        public string TipoDeAgrupacionParaLibrosDeVenta {
            set { _TipoDeAgrupacionParaLibrosDeVenta = (eTipoDeAgrupacionParaLibrosDeVenta)LibConvert.DbValueToEnum(value); }
        }

        public string TipoDeAgrupacionParaLibrosDeVentaAsDB {
            get { return LibConvert.EnumToDbValue((int) _TipoDeAgrupacionParaLibrosDeVenta); }
        }

        public string TipoDeAgrupacionParaLibrosDeVentaAsString {
            get { return LibEnumHelper.GetDescription(_TipoDeAgrupacionParaLibrosDeVenta); }
        }


        public eAplicacionAlicuota AplicacionAlicuotaEspecialAsEnum {
            get { return _AplicacionAlicuotaEspecial; }
            set { _AplicacionAlicuotaEspecial = value; }
        }

        public string AplicacionAlicuotaEspecial {
            set { _AplicacionAlicuotaEspecial = (eAplicacionAlicuota)LibConvert.DbValueToEnum(value); }
        }

        public string AplicacionAlicuotaEspecialAsDB {
            get { return LibConvert.EnumToDbValue((int) _AplicacionAlicuotaEspecial); }
        }

        public string AplicacionAlicuotaEspecialAsString {
            get { return LibEnumHelper.GetDescription(_AplicacionAlicuotaEspecial); }
        }


        public eBaseCalculoParaAlicuotaEspecial BaseDeCalculoParaAlicuotaEspecialAsEnum {
            get { return _BaseDeCalculoParaAlicuotaEspecial; }
            set { _BaseDeCalculoParaAlicuotaEspecial = value; }
        }

        public string BaseDeCalculoParaAlicuotaEspecial {
            set { _BaseDeCalculoParaAlicuotaEspecial = (eBaseCalculoParaAlicuotaEspecial)LibConvert.DbValueToEnum(value); }
        }

        public string BaseDeCalculoParaAlicuotaEspecialAsDB {
            get { return LibConvert.EnumToDbValue((int)_BaseDeCalculoParaAlicuotaEspecial); }
        }

        public string BaseDeCalculoParaAlicuotaEspecialAsString {
            get { return LibEnumHelper.GetDescription(_BaseDeCalculoParaAlicuotaEspecial); }
        }

        public bool IntegracionRISAsBool {
            get { return _IntegracionRIS; }
            set { _IntegracionRIS = value; }
        }

        public string IntegracionRIS {
            set { _IntegracionRIS = LibConvert.SNToBool(value); }
        }
        public bool UsarVentasConIvaDiferidoAsBool {
            get { return _UsarVentasConIvaDiferido; }
            set { _UsarVentasConIvaDiferido = value; }
        }

        public string UsarVentasConIvaDiferido {
            set { _UsarVentasConIvaDiferido = LibConvert.SNToBool(value); }
        }
		
        public bool ImprimirVentasDiferidasAsBool {
            get { return _ImprimirVentasDiferidas; }
            set { _ImprimirVentasDiferidas = value; }
        }

        public string ImprimirVentasDiferidas {
            set { _ImprimirVentasDiferidas = LibConvert.SNToBool(value); }
        }

        public bool AplicarIVAEspecialAsBool {
            get { return _AplicarIVAEspecial; }
            set { _AplicarIVAEspecial = value; }
        }

        public string AplicarIVAEspecial {
            set { _AplicarIVAEspecial = LibConvert.SNToBool(value); }
        }

        public bool ImprimirMensajeAplicacionDecretoAsBool {
            get { return _ImprimirMensajeAplicacionDecreto; }
            set { _ImprimirMensajeAplicacionDecreto = value; }
        }

        public string ImprimirMensajeAplicacionDecreto {
            set { _ImprimirMensajeAplicacionDecreto = LibConvert.SNToBool(value); }
        }

        public DateTime FechaInicioAlicuotaIva10Porciento {
            get { return _FechaInicioAlicuotaIva10Porciento; }
            set { _FechaInicioAlicuotaIva10Porciento = LibConvert.DateToDbValue(value); }
        }

        public DateTime FechaFinAlicuotaIva10Porciento {
            get { return _FechaFinAlicuotaIva10Porciento; }
            set { _FechaFinAlicuotaIva10Porciento = LibConvert.DateToDbValue(value); }
		}
			
        public bool FacturarPorDefectoIvaEspecialAsBool {
            get { return _FacturarPorDefectoIvaEspecial; }
            set { _FacturarPorDefectoIvaEspecial = value; }
        }

        public string FacturarPorDefectoIvaEspecial {
            set { _FacturarPorDefectoIvaEspecial = LibConvert.SNToBool(value); }
        }

        public DateTime FechaMinimaIngresarDatos {
            get { return _FechaMinimaIngresarDatos; }
            set { _FechaMinimaIngresarDatos = LibConvert.DateToDbValue(value); }
        }

        public bool AutorellenaResumenDiarioAsBool {
            get { return _AutorellenaResumenDiario; }
            set { _AutorellenaResumenDiario = value; }
        }

        public string AutorellenaResumenDiario {
            set { _AutorellenaResumenDiario = LibConvert.SNToBool(value); }
        }

        public eTipoNegocio TipoNegocioAsEnum {
            get { return _TipoNegocio; }
            set { _TipoNegocio = value; }
        }

        public string TipoNegocio {
            set { _TipoNegocio = (eTipoNegocio)LibConvert.DbValueToEnum(value); }
        }

        public string TipoNegocioAsDB {
            get { return LibConvert.EnumToDbValue((int) _TipoNegocio); }
        }

        public string TipoNegocioAsString {
            get { return LibEnumHelper.GetDescription(_TipoNegocio); }
        }

        public long fldTimeStamp {
            get { return _fldTimeStamp; }
            set { _fldTimeStamp = value; }
        }

        public XmlDocument Datos {
            get { return _datos; }
            set { _datos = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public CompaniaStt() {
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            FechaDeInicioContabilizacion = LibDate.Today();
            EsAsociadoEnCtaDeParticipacionAsBool = false;
            VerificarDocumentoSinContabilizarAsBool = false;
            TipoDeAgrupacionParaLibrosDeVentaAsEnum = eTipoDeAgrupacionParaLibrosDeVenta.PORRESUMENDEVENTAS;
            IntegracionRISAsBool = false;
            FechaMinimaIngresarDatos = LibDate.Today();
            AutorellenaResumenDiarioAsBool = false;
            TipoNegocioAsEnum = eTipoNegocio.eTN_General;
            UsarVentasConIvaDiferidoAsBool = false;
            ImprimirVentasDiferidasAsBool = false;
            AplicarIVAEspecialAsBool = false;
            FacturarPorDefectoIvaEspecialAsBool = false;
            AplicacionAlicuotaEspecialAsEnum = eAplicacionAlicuota.No_Aplica;
            FechaInicioAlicuotaIva10Porciento = LibDate.Today();
            FechaFinAlicuotaIva10Porciento = LibDate.Today();
            ImprimirMensajeAplicacionDecretoAsBool = false;
            BaseDeCalculoParaAlicuotaEspecialAsEnum = eBaseCalculoParaAlicuotaEspecial.Solo_Base_Imponible_Alicuota_General;
            fldTimeStamp = 0;
        }

        public CompaniaStt Clone() {
            CompaniaStt vResult = new CompaniaStt();
            vResult.FechaDeInicioContabilizacion = _FechaDeInicioContabilizacion;
            vResult.EsAsociadoEnCtaDeParticipacionAsBool = _EsAsociadoEnCtaDeParticipacion;
            vResult.VerificarDocumentoSinContabilizarAsBool = _VerificarDocumentoSinContabilizar;
            vResult.TipoDeAgrupacionParaLibrosDeVentaAsEnum = _TipoDeAgrupacionParaLibrosDeVenta;
            vResult.IntegracionRISAsBool = _IntegracionRIS;
            vResult.FechaMinimaIngresarDatos = _FechaMinimaIngresarDatos;
            vResult.AutorellenaResumenDiarioAsBool = _AutorellenaResumenDiario;
            vResult.TipoNegocioAsEnum = _TipoNegocio;
            vResult.fldTimeStamp = _fldTimeStamp;
            vResult.UsarVentasConIvaDiferidoAsBool = _UsarVentasConIvaDiferido;
            vResult.ImprimirVentasDiferidasAsBool = _ImprimirVentasDiferidas;
            vResult.AplicacionAlicuotaEspecialAsEnum = _AplicacionAlicuotaEspecial;
            vResult.AplicarIVAEspecialAsBool = _AplicarIVAEspecial;
            vResult.FacturarPorDefectoIvaEspecialAsBool = _FacturarPorDefectoIvaEspecial;
            vResult.FechaInicioAlicuotaIva10Porciento = _FechaInicioAlicuotaIva10Porciento;
            vResult.FechaFinAlicuotaIva10Porciento = _FechaFinAlicuotaIva10Porciento;
            vResult.ImprimirMensajeAplicacionDecretoAsBool = _ImprimirMensajeAplicacionDecreto;
            vResult.BaseDeCalculoParaAlicuotaEspecialAsEnum = _BaseDeCalculoParaAlicuotaEspecial;
            return vResult;
        }

        public override string ToString() {
            return "Fecha De Inicio Contabilizacion = " + _FechaDeInicioContabilizacion.ToShortDateString() +
                "\nEs Asociado En Cta De Participacion = " + _EsAsociadoEnCtaDeParticipacion +
                "\nVerificar Documento Sin Contabilizar = " + _VerificarDocumentoSinContabilizar +
                "\nTipo De Agrupacion Para Libros De Venta = " + _TipoDeAgrupacionParaLibrosDeVenta.ToString() +
                "\nIntegración con Sistema Medico (RIS) = " + _IntegracionRIS +
                "\nFecha Minima Ingresar Datos = " + _FechaMinimaIngresarDatos.ToShortDateString() +
                "\n Autorellena Resumen Diario = " + _AutorellenaResumenDiario +
                "\nTipo Negocio  = " + _TipoNegocio.ToString() +
                "\nUsar Ventas con Débito Fiscal Diferido = " + _UsarVentasConIvaDiferido +
                "\nImprimir Ventas Diferidas  = " + _ImprimirVentasDiferidas +
                "\nAplicacion de Alicuota = " + _AplicacionAlicuotaEspecial.ToString() +
                "\nAplicar IVA Especial = " + _AplicarIVAEspecial +
                "\nFacturar por Defecto IVA Especial  = " + _FacturarPorDefectoIvaEspecial +
                "\nFecha de inicio del rango valor del decreto de IVA del 10% = " + _FechaInicioAlicuotaIva10Porciento.ToShortDateString() +
                "\nFecha de finalizacion del rango valor del decreto de IVA del 10% = " + _FechaFinAlicuotaIva10Porciento.ToShortDateString() +
                "\nImprimir Mensaje de Aplicación del Decreto 3085 = " + _ImprimirMensajeAplicacionDecreto +
                "\nBase de Cálculo para Alícuota Especial = " + _BaseDeCalculoParaAlicuotaEspecial.ToString();
            
        }
        #endregion //Metodos Generados


    } //End of class CompaniaStt

} //End of namespace Galac.Saw.Ccl.SttDef

