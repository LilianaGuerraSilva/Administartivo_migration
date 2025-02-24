using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using Galac.Adm.Ccl.DispositivosExternos;

namespace Galac.Adm.Ccl.Venta {
    [Serializable]
    public class Caja {
        #region Variables
        private int _ConsecutivoCompania;
        private int _Consecutivo;
        private string _NombreCaja;
        private bool _UsaGaveta;
        private ePuerto _Puerto;
        private string _Comando;
        private bool _PermitirAbrirSinSupervisor;
        private bool _UsaAccesoRapido;
        private bool _UsaMaquinaFiscal;
        private eFamiliaImpresoraFiscal _FamiliaImpresoraFiscal;
        private eImpresoraFiscal _ModeloDeMaquinaFiscal;
        private string _SerialDeMaquinaFiscal;
        private eTipoConexion _TipoConexion;
        private ePuerto _PuertoMaquinaFiscal;
        private bool _AbrirGavetaDeDinero;
        private string _UltimoNumeroCompFiscal;
        private string _UltimoNumeroNCFiscal;
        private string _UltimoNumeroNDFiscal;
        private string _IpParaConexion;
        private string _MascaraSubred;
        private string _Gateway;
        private string _NumeroFactura;
        private bool _PermitirDescripcionDelArticuloExtendida;
        private bool _PermitirNombreDelClienteExtendido;
        private bool _UsarModoDotNet;
        private bool _RegistroDeRetornoEnTxt;
        private string _NombreOperador;
        private DateTime _FechaUltimaModificacion;
        private long _fldTimeStamp;        
        XmlDocument _datos;
        #endregion //Variables
        #region Propiedades

        public int ConsecutivoCompania {
            get { return _ConsecutivoCompania; }
            set { _ConsecutivoCompania = value; }
        }

        public int Consecutivo {
            get { return _Consecutivo; }
            set { _Consecutivo = value; }
        }

        public string NombreCaja {
            get { return _NombreCaja; }
            set { _NombreCaja = LibString.Mid(value,0,60); }
        }

        public bool UsaGavetaAsBool {
            get { return _UsaGaveta; }
            set { _UsaGaveta = value; }
        }

        public string UsaGaveta {
            set { _UsaGaveta = LibConvert.SNToBool(value); }
        }


        public ePuerto PuertoAsEnum {
            get { return _Puerto; }
            set { _Puerto = value; }
        }

        public string Puerto {
            set { _Puerto = (ePuerto)LibConvert.DbValueToEnum(value); }
        }

        public string PuertoAsDB {
            get { return LibConvert.EnumToDbValue((int)_Puerto); }
        }

        public string PuertoAsString {
            get { return LibEnumHelper.GetDescription(_Puerto); }
        }

        public string Comando {
            get { return _Comando; }
            set { _Comando = LibString.Mid(value,0,10); }
        }

        public bool PermitirAbrirSinSupervisorAsBool {
            get { return _PermitirAbrirSinSupervisor; }
            set { _PermitirAbrirSinSupervisor = value; }
        }

        public string PermitirAbrirSinSupervisor {
            set { _PermitirAbrirSinSupervisor = LibConvert.SNToBool(value); }
        }

        public bool UsaAccesoRapidoAsBool {
            get { return _UsaAccesoRapido; }
            set { _UsaAccesoRapido = value; }
        }

        public string UsaAccesoRapido {
            set { _UsaAccesoRapido = LibConvert.SNToBool(value); }
        }

        public bool UsaMaquinaFiscalAsBool {
            get { return _UsaMaquinaFiscal; }
            set { _UsaMaquinaFiscal = value; }
        }

        public string UsaMaquinaFiscal {
            set { _UsaMaquinaFiscal = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsaMaquinaFiscal"); }
        }
        
        public eFamiliaImpresoraFiscal FamiliaImpresoraFiscalAsEnum {
            get { return _FamiliaImpresoraFiscal; }
            set { _FamiliaImpresoraFiscal = value; }
        }
        
        public string FamiliaImpresoraFiscal {
            set { _FamiliaImpresoraFiscal = (eFamiliaImpresoraFiscal)LibConvert.DbValueToEnum(value); }
        }

        public string FamiliaImpresoraFiscalAsDB {
            get { return LibConvert.EnumToDbValue((int) _FamiliaImpresoraFiscal); }
        }

        public string FamiliaImpresoraFiscalAsString {
            get { return LibEnumHelper.GetDescription(_FamiliaImpresoraFiscal); }
        }

        public eImpresoraFiscal ModeloDeMaquinaFiscalAsEnum {
            get { return _ModeloDeMaquinaFiscal; }
            set { _ModeloDeMaquinaFiscal = value; }
        }
        
        public string ModeloDeMaquinaFiscal {
            set { _ModeloDeMaquinaFiscal = (eImpresoraFiscal)LibConvert.DbValueToEnum(value); }
        }

        public string ModeloDeMaquinaFiscalAsDB {
            get { return LibConvert.EnumToDbValue((int)_ModeloDeMaquinaFiscal); }
        }

        public string ModeloDeMaquinaFiscalAsString {
            get { return LibEnumHelper.GetDescription(_ModeloDeMaquinaFiscal); }
        }

        public string SerialDeMaquinaFiscal {
            get { return _SerialDeMaquinaFiscal; }
            set { _SerialDeMaquinaFiscal = LibString.Mid(value, 0, 15); }
        }
        public eTipoConexion TipoConexionAsEnum {
            get { return _TipoConexion; }
            set { _TipoConexion = value; }
        }
        public string TipoConexion {
            set { _TipoConexion = (eTipoConexion)LibConvert.DbValueToEnum(value); }
        }

        public string TipoConexionAsDB {
            get { return LibConvert.EnumToDbValue((int) _TipoConexion); }
        }

        public string TipoConexionAsString {
            get { return LibEnumHelper.GetDescription(_TipoConexion); }
        }
        public ePuerto PuertoMaquinaFiscalAsEnum {
            get { return _PuertoMaquinaFiscal; }
            set { _PuertoMaquinaFiscal = value; }
        }

        public string PuertoMaquinaFiscal {
            set { _PuertoMaquinaFiscal = (ePuerto)LibConvert.DbValueToEnum(value); }
        }

        public string PuertoMaquinaFiscalAsDB {
            get { return LibConvert.EnumToDbValue((int)_PuertoMaquinaFiscal); }
        }

        public string PuertoMaquinaFiscalAsString {
            get { return LibEnumHelper.GetDescription(_PuertoMaquinaFiscal); }
        }

        public bool AbrirGavetaDeDineroAsBool {
            get { return _AbrirGavetaDeDinero; }
            set { _AbrirGavetaDeDinero = value; }
        }

        public string AbrirGavetaDeDinero {
            set { _AbrirGavetaDeDinero = LibConvert.SNToBool(value); }
        }

        public string UltimoNumeroCompFiscal {
            get { return _UltimoNumeroCompFiscal; }
            set { _UltimoNumeroCompFiscal = LibString.Mid(value, 0, 12); }
        }

        public string UltimoNumeroNCFiscal {
            get { return _UltimoNumeroNCFiscal; }
            set { _UltimoNumeroNCFiscal = LibString.Mid(value, 0, 12); }
        }

        public string UltimoNumeroNDFiscal {
            get { return _UltimoNumeroNDFiscal; }
            set { _UltimoNumeroNDFiscal = LibString.Mid(value, 0, 12); }
        }

        public string IpParaConexion {
            get { return _IpParaConexion; }
            set { _IpParaConexion = LibString.Mid(value, 0, 15); }
        }

        public string MascaraSubred {
            get { return _MascaraSubred; }
            set { _MascaraSubred = LibString.Mid(value, 0, 15); }
        }

        public string Gateway {
            get { return _Gateway; }
            set { _Gateway = LibString.Mid(value, 0, 15); }
        }

        public bool PermitirDescripcionDelArticuloExtendidaAsBool {
            get { return _PermitirDescripcionDelArticuloExtendida; }
            set { _PermitirDescripcionDelArticuloExtendida = value; }
        }

        public string PermitirDescripcionDelArticuloExtendida {
            set { _PermitirDescripcionDelArticuloExtendida = LibConvert.SNToBool(value); }
        }

        public bool PermitirNombreDelClienteExtendidoAsBool {
            get { return _PermitirNombreDelClienteExtendido; }
            set { _PermitirNombreDelClienteExtendido = value; }
        }

        public string PermitirNombreDelClienteExtendido {
            set { _PermitirNombreDelClienteExtendido = LibConvert.SNToBool(value); }
        }


        public bool UsarModoDotNetAsBool {
            get { return _UsarModoDotNet; }
            set { _UsarModoDotNet = value; }
        }

        public string UsarModoDotNet {
            set { _UsarModoDotNet = LibConvert.SNToBool(value); }
        }
		
        public bool RegistroDeRetornoEnTxtAsBool {
            get { return _RegistroDeRetornoEnTxt; }
            set { _RegistroDeRetornoEnTxt = value; }
        }

        public string RegistroDeRetornoEnTxt {
            set { _RegistroDeRetornoEnTxt = LibConvert.SNToBool(value); }
        }
		
        public string NombreOperador {
            get { return _NombreOperador; }
            set { _NombreOperador = LibString.Mid(value,0,10); }
        }

        public DateTime FechaUltimaModificacion {
            get { return _FechaUltimaModificacion; }
            set { _FechaUltimaModificacion = LibConvert.DateToDbValue(value); }
        }

        public string NumeroFactura {
            get { return _NumeroFactura; }
            set { _NumeroFactura = value; }
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

        public Caja() {
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            ConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");
            Consecutivo = 0;
            NombreCaja = string.Empty;
            UsaGavetaAsBool = false;
            PuertoAsEnum = ePuerto.COM1;
            Comando = string.Empty;
            PermitirAbrirSinSupervisorAsBool = false;
            UsaAccesoRapidoAsBool = false;
            UsaMaquinaFiscalAsBool = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsaMaquinaFiscal");
            FamiliaImpresoraFiscalAsEnum = eFamiliaImpresoraFiscal.EPSONPNP;
            ModeloDeMaquinaFiscalAsEnum = eImpresoraFiscal.EPSON_PF_220;
            SerialDeMaquinaFiscal = string.Empty;
            TipoConexionAsEnum = eTipoConexion.PuertoSerial;
            PuertoMaquinaFiscalAsEnum = ePuerto.COM1;
            AbrirGavetaDeDineroAsBool = false;
            UltimoNumeroCompFiscal = string.Empty;
            UltimoNumeroNCFiscal = string.Empty;
            UltimoNumeroNDFiscal = string.Empty;
            IpParaConexion = string.Empty;
            MascaraSubred = string.Empty;
            Gateway = string.Empty;
            PermitirDescripcionDelArticuloExtendidaAsBool = false;
            PermitirNombreDelClienteExtendidoAsBool = false;
            UsarModoDotNetAsBool = false;
            RegistroDeRetornoEnTxtAsBool = false;
            NombreOperador = string.Empty;
            FechaUltimaModificacion = LibDate.Today();
            fldTimeStamp = 0;
        }

        public Caja Clone() {
            Caja vResult = new Caja();
            vResult.ConsecutivoCompania = _ConsecutivoCompania;
            vResult.Consecutivo = _Consecutivo;
            vResult.NombreCaja = _NombreCaja;
            vResult.UsaGavetaAsBool = _UsaGaveta;
            vResult.PuertoAsEnum = _Puerto;
            vResult.Comando = _Comando;
            vResult.PermitirAbrirSinSupervisorAsBool = _PermitirAbrirSinSupervisor;
            vResult.UsaAccesoRapidoAsBool = _UsaAccesoRapido;
            vResult.UsaMaquinaFiscalAsBool = _UsaMaquinaFiscal;
            vResult.FamiliaImpresoraFiscalAsEnum = _FamiliaImpresoraFiscal;
            vResult.ModeloDeMaquinaFiscalAsEnum = _ModeloDeMaquinaFiscal;
            vResult.SerialDeMaquinaFiscal = _SerialDeMaquinaFiscal;
            vResult.TipoConexionAsEnum = _TipoConexion;
            vResult.PuertoMaquinaFiscalAsEnum = _PuertoMaquinaFiscal;
            vResult.AbrirGavetaDeDineroAsBool = _AbrirGavetaDeDinero;
            vResult.UltimoNumeroCompFiscal = _UltimoNumeroCompFiscal;
            vResult.UltimoNumeroNCFiscal = _UltimoNumeroNCFiscal;
            vResult.IpParaConexion = _IpParaConexion;
            vResult.MascaraSubred = _MascaraSubred;
            vResult.Gateway = _Gateway;
            vResult.NumeroFactura = _NumeroFactura;
            vResult.PermitirDescripcionDelArticuloExtendidaAsBool = _PermitirDescripcionDelArticuloExtendida;
            vResult.PermitirNombreDelClienteExtendidoAsBool = _PermitirNombreDelClienteExtendido;
            vResult.UsarModoDotNetAsBool = _UsarModoDotNet;
            vResult.RegistroDeRetornoEnTxtAsBool = _RegistroDeRetornoEnTxt;
            vResult.NombreOperador = _NombreOperador;
            vResult.FechaUltimaModificacion = _FechaUltimaModificacion;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
            return "Consecutivo Compania = " + _ConsecutivoCompania.ToString() +
               "\nConsecutivo = " + _Consecutivo.ToString() +
                "\nNombre Caja = " + _NombreCaja +
                "\nUsa Gaveta = " + _UsaGaveta +
                "\nPuerto = " + _Puerto.ToString() +
                "\nComando = " + _Comando +
                "\nPermitir Abrir Sin Supervisor = " + _PermitirAbrirSinSupervisor +
                "\nUsa Tecla de Acceso Rápido para Abrir Gaveta = " + _UsaAccesoRapido +
                "\nUsa Maquina Fiscal = " + _UsaMaquinaFiscal +
               "\namilia de máquina Fiscal = " + _FamiliaImpresoraFiscal.ToString() +
               "\nModelo De Maquina Fiscal = " + _ModeloDeMaquinaFiscal.ToString() +
               "\nSerial De Maquina Fiscal = " + _SerialDeMaquinaFiscal +
               "\nTipo Conexion = " + _TipoConexion.ToString() +
               "\nPuerto de la máquina Fiscal = " + _PuertoMaquinaFiscal.ToString() +
                "\nAbrir Gaveta De Dinero = " + _AbrirGavetaDeDinero +
               "\nUltimo Numero Comp Fiscal = " + _UltimoNumeroCompFiscal +
               "\nUltimo Numero NCFiscal = " + _UltimoNumeroNCFiscal +
               "\nUltimo Numero NDFiscal = " + _UltimoNumeroNDFiscal +
               "\nIp Para Conexion = " + _IpParaConexion +
               "\nMascara Subred = " + _MascaraSubred +
               "\nGateway = " + _Gateway +
               "\nPermitir Descripcion Del Articulo Extendida = " + _PermitirDescripcionDelArticuloExtendida +
               "\nPermitir Nombre Del Cliente Extendido = " + _PermitirNombreDelClienteExtendido +
               "\nUsarModoDotNet = " + _UsarModoDotNet +
               "\negistro De Retorno En Txt  = " + _RegistroDeRetornoEnTxt +
               "\nNombre Operador = " + _NombreOperador +
                "\nFecha Ultima Modificacion = " + _FechaUltimaModificacion.ToShortDateString();
        }
        #endregion //Metodos Generados
    } //End of class Caja
} //End of namespace Galac.Adm.Ccl.Venta

