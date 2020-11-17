using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using Galac.Comun.Ccl.SttDef;
using Galac.Adm.Ccl.DispositivosExternos;
using Galac.Adm.Ccl.Venta;
using Galac.Adm.Brl.Venta;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;

namespace Galac.Adm.Brl.DispositivosExternos.MaquinaFiscal {
   public class clsMaquinaFiscal {
      #region variables
      private string _SerialMaquinaFiscal = "";
      private string _NumeroComprobanteFiscal = "";
      private string _PuertoMaquinaFiscal = "";
      private bool _SeImprimioDocumento = false;
      private XElement xmlData;
      //private IMaquinaFiscalPdn insMaquinaFiscal;
      #endregion variables

      #region propiedades

      public string SerialMaquinaFiscal {
         get {
            return _SerialMaquinaFiscal;
         }
         set {
            _SerialMaquinaFiscal = value;
         }
      }

      public string PuertoMaquinaFiscal {
         get {
            return _PuertoMaquinaFiscal;
         }
         set {
            _PuertoMaquinaFiscal = value;
         }
      }

      public string NumeroComprobanteFiscal {
         get {
            return _NumeroComprobanteFiscal;
         }
         set {
            _NumeroComprobanteFiscal = value;
         }
      }

      public bool SeImprimioDocumento {
         get {
            return _SeImprimioDocumento;
         }
         set {
            _SeImprimioDocumento = value;
         }
      }

      #endregion propiedades

      #region Constructor
      public clsMaquinaFiscal() {


      }

      #endregion Contructor
      #region Metodos Generados

      public IMaquinaFiscalPdn InitializeMaquinaFiscal() {
         IMaquinaFiscalPdn vMaquinaFiscal = null;
         ImpresoraFiscalCreator vCreatorMaquinaFiscal = new ImpresoraFiscalCreator();
         XElement vParams = GetConfigMaquinaFiscalParameters();
         string vPuerto = LibXml.GetElementValueOrEmpty(vParams, "PUERTOCOM");
         string vModelo = LibXml.GetElementValueOrEmpty(vParams, "MODELO");
         if (vParams != null && !LibString.IsNullOrEmpty(vPuerto) && !LibString.IsNullOrEmpty(vModelo)) {
            vMaquinaFiscal = vCreatorMaquinaFiscal.Crear(vParams.ToString());
         }
         return vMaquinaFiscal;
      }

      public void LeerDatosDeMaquinaFiscal(IMaquinaFiscalPdn valMaquinaFiscal) {
         SerialMaquinaFiscal = valMaquinaFiscal.ObtenerSerial();
         NumeroComprobanteFiscal = valMaquinaFiscal.ObtenerUltimoNumeroFactura();
         NumeroComprobanteFiscal = LibConvert.ToStr(LibConvert.ToInt(NumeroComprobanteFiscal) + 1);
         NumeroComprobanteFiscal = LibText.FillWithCharToLeft(NumeroComprobanteFiscal, "0", 8);
      }

      private XElement GetConfigMaquinaFiscalParameters() {
         XElement vParams = null;
         string CajaLocal = "";
         int ConsecutivoCompania = 0;
         CajaLocal = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("FacturaRapida", "ConsecutivoCaja");
         ConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");
         if (!CajaLocal.Equals("") && (ConsecutivoCompania > 0)) {
            ICajaPdn vCajaPdn = new Brl.Venta.clsCajaNav() as ICajaPdn;
            XElement xmlCajaDat = vCajaPdn.FindByConsecutivoCaja(ConsecutivoCompania, CajaLocal);
            string Modelo = LibXml.GetPropertyString(xmlCajaDat, "ModeloDeMaquinaFiscal");
            string PuertoMaquinaFiscal = LibXml.GetPropertyString(xmlCajaDat, "PuertoMaquinaFiscal");
            string vCaja = LibXml.GetPropertyString(xmlCajaDat, "ConsecutivoCaja");
            SerialMaquinaFiscal = LibXml.GetPropertyString(xmlCajaDat, "SerialDeMaquinaFiscal");
            vParams = new XElement("GpResult",
               new XElement("PUERTOCOM", PuertoMaquinaFiscal),
               new XElement("MODELO", Modelo));
         }
         return vParams;
      }

      private bool FechaYHoraValidaEnMaquinaFiscal(DateTime valFechaHora, ref string valMensaje) {
         bool vResult = false;
         DateTime vFechaActual = LibDate.Today().Date;
         int vHoraActual = LibConvert.ToInt(LibText.SubString(LibDate.CurrentHourAsStr, 0, 2));
         int vMinActual = LibConvert.ToInt(LibText.SubString(LibDate.CurrentHourAsStr, 3, 2));
         int vHoraMFiscal = LibConvert.ToInt(LibText.SubString(LibConvert.ToShortTimeStr(valFechaHora), 0, 2));
         int vMinMFiscal = LibConvert.ToInt(LibText.SubString(LibConvert.ToShortTimeStr(valFechaHora), 3, 2));
         int vMinutosDiferencia = LibConvert.ToInt(LibMath.Abs(vMinActual - vMinMFiscal));

         if (valFechaHora.Date == vFechaActual) {
            vResult = true;
         } else {
            valMensaje = "La fecha del computador no corresponde con la fecha de la impresora fiscal\r\nSincronizar hora de los dispositivos\r\nFecha de la Maquina Fiscal:" + LibConvert.ToStr(valFechaHora.Date);
            vResult &= true;
         }
         /*
         if (vHoraActual == vHoraMFiscal && vMinutosDiferencia < 45)
         {
            vResult &= true;
         }
         else
         {
            valMensaje = "La hora de la impresora fiscal no esta sincronizada con el PC, consultar a su proveedor de impresora fiscal\r\nhora de la Maquina Fiscal:" + LibConvert.ToStr(valFechaHora.TimeOfDay);
            vResult &= false;
         }
         */
         return vResult;
      }

      public bool DetectarImpresoraFiscal(IMaquinaFiscalPdn valMaquinaFiscal) {
         bool vResult = false;
         try {
            if (valMaquinaFiscal == null) {
               throw new GalacException("No hay una caja fiscal configurada para este usuario, Debe configurar y asignar una caja fiscal para continuar", eExceptionManagementType.Uncontrolled);
            }
            if (valMaquinaFiscal.AbrirConexion()) {
               vResult = ComprobarEstadosDeImpresora(valMaquinaFiscal);
            } else {
               throw new GalacAlertException("No se pudo conectar a la Impresora Fiscal, Revisar Conexiones");
            }
            return vResult;
         } catch (LibGalac.Aos.Catching.GalacException vEx) {
            throw vEx;
         }
      }

      private bool ComprobarEstadosDeImpresora(IMaquinaFiscalPdn valMaquinaFiscal) {
         bool vResult = false;
         eStatusImpresorasFiscales StatusPapel;
         string vSerialMaquinaFiscalInDB = "";
         string vSerialMaquinaFiscalInConnection = "";
         DateTime vFechaYHoraInConnection;
         string vMensaje = "";

         try {

            if (valMaquinaFiscal.ComprobarConexion()) {
               StatusPapel = valMaquinaFiscal.EstadoDelPapel(false);
               vSerialMaquinaFiscalInDB = SerialMaquinaFiscal;
               vSerialMaquinaFiscalInConnection = valMaquinaFiscal.ObtenerSerial();
               vFechaYHoraInConnection = LibConvert.ToDate(valMaquinaFiscal.ObtenerFechaYHora());
               if (StatusPapel.Equals(eStatusImpresorasFiscales.ePocoPapel)) {
                  vResult = true;
                  throw new GalacAlertException("La impresora tiene poco papel");
               } else if (StatusPapel.Equals(eStatusImpresorasFiscales.eSinPapel)) {
                  throw new GalacAlertException("Papel agotado, favor reemplazar");
               }
               if (!vSerialMaquinaFiscalInDB.Equals(vSerialMaquinaFiscalInConnection)) {
                  throw new GalacAlertException("El serial de la maquina fiscal asignada a esta caja no corresponde, Revisar el dispositivo fiscal para continuar");
               }
               if (!(FechaYHoraValidaEnMaquinaFiscal(vFechaYHoraInConnection, ref vMensaje))) {
                  vResult = true;
                  throw new GalacAlertException(vMensaje);
               }
               valMaquinaFiscal.CerrarConexion();
               valMaquinaFiscal = null;
               vResult = true;
            } else {
               throw new GalacAlertException("No se pudo conectar a la Impresora Fiscal, Revisar Conexiones");
            }
            return vResult;
         } catch (GalacException vEx) {
            throw vEx;
         }
      }

      public bool ImprimirDocumentoFiscal(XElement valData, IMaquinaFiscalPdn valMaquinaFiscal) {
         bool vResult = false;
         try {
            eTipoDocumentoFactura TipoDocumento = (eTipoDocumentoFactura)LibConvert.DbValueToEnum(LibText.CleanSpacesToBothSides(LibXml.GetPropertyString(valData, "GpResult", "TipoDeDocumento")));
            if (TipoDocumento.Equals(eTipoDocumentoFactura.ComprobanteFiscal)) {
               vResult = valMaquinaFiscal.ImprimirFacturaFiscal(valData);
            } else if (TipoDocumento.Equals(eTipoDocumentoFactura.NotaDeCredito)) {
               vResult = valMaquinaFiscal.ImprimirNotaCredito(valData);
            }
            return vResult;
         } catch (GalacAlertException vEx) {
            throw vEx;
         }
      }
      #endregion Metodos Generados
   }
}
