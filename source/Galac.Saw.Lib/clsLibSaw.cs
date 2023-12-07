using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using LibGalac.Aos.Base;
using LibGalac.Aos.Brl;
using LibGalac.Aos.Base.Dal;
using LibGalac.Aos.DefGen;
using LibGalac.Aos.Catching;
using System.Text;
using System.Threading;
using Galac.Comun.Ccl.TablasGen;

namespace Galac.Saw.Lib {
    public class clsLibSaw {
        public bool IsValidSerieNumber(string valSerieNumber, bool valIsElectronicInvoice, eValidaPorTipoDeDocumento valValidaPorTipoDeDocumento) {
            bool vResult = false;
            if (LibText.Len(valSerieNumber) <= 4) {
                if (LibConvert.IsNumeric(valSerieNumber) && !valIsElectronicInvoice) {
                    vResult = true;
                } else {
                    if (valIsElectronicInvoice) {
                        vResult = IsValidPrefixForSerie(valSerieNumber, valIsElectronicInvoice, valValidaPorTipoDeDocumento);
                    }
                }
            }
            return vResult;
        }

        private bool IsValidPrefixForSerie(string valSerieNumber, bool valOnlyInvoice, eValidaPorTipoDeDocumento valValidaPorTipoDeDocumento) {
            bool vResult = false;
            string vPatronCaracteresValidosAmbos = "^[B,E,F]{1}[A-Z,a-z,0-9]{1,3}$";
            string vPatronCaracteresValidosBoleta = "^[B,E]{1}[A-Z,a-z,0-9]{1,3}$";
            string vPatronCaracteresValidosGuiaRemision = "^[T]{1}[A-Z,a-z,0-9]{1,3}$";
            string vPatronCaracteresValidosFactura = "^[E,F]{1}[A-Z,a-z,0-9]{1,3}$";
            Regex vRegex = new Regex(vPatronCaracteresValidosFactura);
            switch (valValidaPorTipoDeDocumento) {
                case eValidaPorTipoDeDocumento.Factura:
                    vResult = vRegex.IsMatch(valSerieNumber);
                    break;
                case eValidaPorTipoDeDocumento.Boleta:
                    vRegex = new Regex(vPatronCaracteresValidosBoleta);
                    vResult = vRegex.IsMatch(valSerieNumber);
                    break;
                case eValidaPorTipoDeDocumento.Ambos:
                    vRegex = new Regex(vPatronCaracteresValidosAmbos);
                    vResult = vRegex.IsMatch(valSerieNumber);
                    break;
                case eValidaPorTipoDeDocumento.GuiaDeRemision:
                    vRegex = new Regex(vPatronCaracteresValidosGuiaRemision);
                    vResult = vRegex.IsMatch(valSerieNumber);
                    break;
                default:
                    vRegex = new Regex(vPatronCaracteresValidosAmbos);
                    vResult = vRegex.IsMatch(valSerieNumber);
                    break;
            }
            vRegex = null;
            return vResult;
        }

        public LibResponse IsValidSerieNumber(string valSerieNumber, bool valIsElectronicInvoice, eValidaPorTipoDeDocumento valValidaPorTipoDeDocumento, bool opt) {
            const int SerieNumberMaxLength = 4;
            LibResponse vResult = new LibResponse();
            vResult.Success = true;
            if (LibText.Len(valSerieNumber) >= (SerieNumberMaxLength - 1)) {
                if (valIsElectronicInvoice) {
                    if (LibText.Len(valSerieNumber) != SerieNumberMaxLength) {
                        vResult.Success = false;
                        vResult.AddError("El número de serie con emisión electrónica debe de tener " + SerieNumberMaxLength.ToString() + " caracteres");
                        return vResult;
                    }
                    vResult = IsValidPrefixForSerie(valSerieNumber, valValidaPorTipoDeDocumento);
                } else {
                    if (LibText.Len(valSerieNumber) != (SerieNumberMaxLength - 1)) {
                        vResult.Success = false;
                        vResult.AddError("El número de serie debe de tener " + (SerieNumberMaxLength - 1).ToString() + " caracteres");
                        return vResult;
                    }
                    if (LibConvert.IsNumeric(valSerieNumber)) {
                        vResult.Success = true;
                    } else {
                        vResult.Success = false;
                        vResult.AddError("El número de serie debe de ser solo númerico");
                    }
                }
            } else {
                vResult.Success = false;
                vResult.AddError("El número de serie debe de tener al menos " + (SerieNumberMaxLength - 1).ToString() + " caracteres");
            }
            return vResult;
        }

        private LibResponse IsValidPrefixForSerie(string valSerieNumber, eValidaPorTipoDeDocumento valValidaPorTipoDeDocumento) {
            LibResponse vResult = new LibResponse();
            string vPatronCaracteresValidosAmbos = "^[B,F,E,T]{1}[A-Z,a-z,0-9]{1,3}$";
            string vPatronCaracteresValidosBoleta = "^[B,E]{1}[A-Z,a-z,0-9]{1,3}$";
            string vPatronCaracteresValidosFactura = "^[F,E]{1}[A-Z,a-z,0-9]{1,3}$";
            string vPatronCaracteresValidosGuiaRemision = "^[T]{1}[A-Z,a-z,0-9]{1,3}$";
            Regex vRegex = new Regex(vPatronCaracteresValidosFactura);
            switch (valValidaPorTipoDeDocumento) {
                case eValidaPorTipoDeDocumento.Factura:
                    vResult.Success = vRegex.IsMatch(valSerieNumber);
                    if (!vResult.Success) {
                        vResult.AddError("El formato del número de serie debe de ser F### o E###. Con # como caracteres alfanuméricos");
                    }
                    break;
                case eValidaPorTipoDeDocumento.Boleta:
                    vRegex = new Regex(vPatronCaracteresValidosBoleta);
                    vResult.Success = vRegex.IsMatch(valSerieNumber);
                    if (!vResult.Success) {
                        vResult.AddError("El formato del número de serie debe de ser B### o E###. Con # como caracteres alfanuméricos");
                    }
                    break;
                case eValidaPorTipoDeDocumento.Ambos:
                    vRegex = new Regex(vPatronCaracteresValidosAmbos);
                    vResult.Success = vRegex.IsMatch(valSerieNumber);
                    if (!vResult.Success) {
                        vResult.AddError("El formato del número de serie debe de ser F### o B### o T### o E### según corresponda. Con # como caracteres alfanuméricos");
                    }
                    break;
                case eValidaPorTipoDeDocumento.GuiaDeRemision:
                    vRegex = new Regex(vPatronCaracteresValidosGuiaRemision);
                    vResult.Success = vRegex.IsMatch(valSerieNumber);
                    if (!vResult.Success) {
                        vResult.AddError("El formato del número de serie debe de ser T###. Con # como caracteres alfanuméricos");
                    }
                    break;
                default:
                    vRegex = new Regex(vPatronCaracteresValidosAmbos);
                    vResult.Success = vRegex.IsMatch(valSerieNumber);
                    if (!vResult.Success) {
                        vResult.AddError("El formato del número de serie debe de ser F### o B### o T### o E### según corresponda. Con # como caracteres alfanuméricos");
                    }
                    break;
            }
            vRegex = null;
            return vResult;
        }

        public string Plural(string valMoneda) {
            string vTmp = "";
            if (valMoneda.Trim().Length > 0) {
                valMoneda = valMoneda.Trim();
                valMoneda += " ";
                for (int i = 0; i < valMoneda.Length; i++) {
                    if (valMoneda.Substring(i, 1) == " ") {
                        string vVocales = "AEIOU";
                        if (vVocales.IndexOf(vTmp.Substring(vTmp.Length - 1, 1).ToUpper(), 0) >= 0) {
                            vTmp += "s";
                        } else {
                            vTmp += "es";
                        }
                    }
                    vTmp += valMoneda.Substring(i, 1);
                }
                valMoneda = vTmp.Trim();
            }
            return valMoneda;
        }

        public bool EsValidaLaFechaParaContabilidad(int valConsecutivoCompania, DateTime valFecha, string valErrorMessage) {
            bool vResult = false;
            bool vUsaContabilidad = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Compania", "AccesoCaracteristicaDeContabilidadActiva");
            if (PerteneceAUnMesCerrado(valFecha)) {
                throw new LibGalac.Aos.Catching.GalacAlertException(valErrorMessage + ": " + LibConvert.ToStr(valFecha) + ", pertenece a un mes cerrado.");
            } else if (PerteneceAUnPeriodoCerrado()) {
                throw new LibGalac.Aos.Catching.GalacAlertException(valErrorMessage + ": " + LibConvert.ToStr(valFecha) + " de Cierre, pertenece a un Período Cerrado.");
            } else if (PerteneceAUnPeriodoCerradoAnterior(valConsecutivoCompania, valFecha)) {
                throw new LibGalac.Aos.Catching.GalacAlertException(valErrorMessage + ": " + LibConvert.ToStr(valFecha) + " , pertenece a un Período Cerrado.");
            } else if (PerteneceAUnPeriodoCerrado()) {
                throw new LibGalac.Aos.Catching.GalacAlertException(valErrorMessage + ": " + LibConvert.ToStr(valFecha) + " de Cierre, pertenece a un Período Cerrado.");
            } else {
                vResult = true;
            }
            return vResult;
        }

        private bool PerteneceAUnMesCerrado(DateTime valFecha) {
            bool vResult = false;
            if (!LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Periodo", "UsaCierreDeMes")) {
                return vResult;
            }
            string vSQL = "SELECT Mes1Cerrado, Mes2Cerrado, Mes3Cerrado, Mes4Cerrado, Mes5Cerrado, Mes6Cerrado, Mes7Cerrado, Mes8Cerrado, Mes9Cerrado, Mes10Cerrado, Mes11Cerrado, Mes12Cerrado FROM PERIODO WHERE ConsecutivoPeriodo = @ConsecutivoPeriodo";
            XElement vDataPeriodo = LibBusiness.ExecuteSelect(vSQL, LibGlobalValues.Instance.GetMfcInfo().GetIntAsParam("Periodo"), "", 0);
            vResult = FechaPerteneceAUnMesCerrado(valFecha, vDataPeriodo);
            return vResult;
        }
        private bool FechaPerteneceAUnMesCerrado(DateTime vFecha, XElement vDataPeriodo) {
            bool vResult = false;

            if (LibDate.F1IsGreaterOrEqualThanF2(vFecha, LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDateTime("Periodo", "FechaApertura")) && LibDate.F1IsLessOrEqualThanF2(vFecha, LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDateTime("Periodo", "FechaDeCierre1")) && LibConvert.SNToBool(LibXml.GetPropertyString(vDataPeriodo, "Mes1Cerrado"))) {
                vResult = true;
            } else if (LibDate.F1IsGreaterThanF2(vFecha, LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDateTime("Periodo", "FechaDeCierre1")) && LibDate.F1IsLessOrEqualThanF2(vFecha, LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDateTime("Periodo", "FechaDeCierre2")) && LibConvert.SNToBool(LibXml.GetPropertyString(vDataPeriodo, "Mes2Cerrado"))) {
                vResult = true;
            } else if (LibDate.F1IsGreaterThanF2(vFecha, LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDateTime("Periodo", "FechaDeCierre2")) && LibDate.F1IsLessOrEqualThanF2(vFecha, LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDateTime("Periodo", "FechaDeCierre3")) && LibConvert.SNToBool(LibXml.GetPropertyString(vDataPeriodo, "Mes3Cerrado"))) {
                vResult = true;
            } else if (LibDate.F1IsGreaterThanF2(vFecha, LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDateTime("Periodo", "FechaDeCierre3")) && LibDate.F1IsLessOrEqualThanF2(vFecha, LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDateTime("Periodo", "FechaDeCierre4")) && LibConvert.SNToBool(LibXml.GetPropertyString(vDataPeriodo, "Mes4Cerrado"))) {
                vResult = true;
            } else if (LibDate.F1IsGreaterThanF2(vFecha, LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDateTime("Periodo", "FechaDeCierre4")) && LibDate.F1IsLessOrEqualThanF2(vFecha, LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDateTime("Periodo", "FechaDeCierre5")) && LibConvert.SNToBool(LibXml.GetPropertyString(vDataPeriodo, "Mes5Cerrado"))) {
                vResult = true;
            } else if (LibDate.F1IsGreaterThanF2(vFecha, LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDateTime("Periodo", "FechaDeCierre5")) && LibDate.F1IsLessOrEqualThanF2(vFecha, LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDateTime("Periodo", "FechaDeCierre6")) && LibConvert.SNToBool(LibXml.GetPropertyString(vDataPeriodo, "Mes6Cerrado"))) {
                vResult = true;
            } else if (LibDate.F1IsGreaterThanF2(vFecha, LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDateTime("Periodo", "FechaDeCierre6")) && LibDate.F1IsLessOrEqualThanF2(vFecha, LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDateTime("Periodo", "FechaDeCierre7")) && LibConvert.SNToBool(LibXml.GetPropertyString(vDataPeriodo, "Mes7Cerrado"))) {
                vResult = true;
            } else if (LibDate.F1IsGreaterThanF2(vFecha, LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDateTime("Periodo", "FechaDeCierre7")) && LibDate.F1IsLessOrEqualThanF2(vFecha, LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDateTime("Periodo", "FechaDeCierre8")) && LibConvert.SNToBool(LibXml.GetPropertyString(vDataPeriodo, "Mes8Cerrado"))) {
                vResult = true;
            } else if (LibDate.F1IsGreaterThanF2(vFecha, LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDateTime("Periodo", "FechaDeCierre8")) && LibDate.F1IsLessOrEqualThanF2(vFecha, LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDateTime("Periodo", "FechaDeCierre9")) && LibConvert.SNToBool(LibXml.GetPropertyString(vDataPeriodo, "Mes9Cerrado"))) {
                vResult = true;
            } else if (LibDate.F1IsGreaterThanF2(vFecha, LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDateTime("Periodo", "FechaDeCierre9")) && LibDate.F1IsLessOrEqualThanF2(vFecha, LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDateTime("Periodo", "FechaDeCierre10")) && LibConvert.SNToBool(LibXml.GetPropertyString(vDataPeriodo, "Mes10Cerrado"))) {
                vResult = true;
            } else if (LibDate.F1IsGreaterThanF2(vFecha, LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDateTime("Periodo", "FechaDeCierre10")) && LibDate.F1IsLessOrEqualThanF2(vFecha, LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDateTime("Periodo", "FechaDeCierre11")) && LibConvert.SNToBool(LibXml.GetPropertyString(vDataPeriodo, "Mes11Cerrado"))) {
                vResult = true;
            } else if (LibDate.F1IsGreaterThanF2(vFecha, LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDateTime("Periodo", "FechaDeCierre11")) && LibDate.F1IsLessOrEqualThanF2(vFecha, LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDateTime("Periodo", "FechaCierre")) && LibConvert.SNToBool(LibXml.GetPropertyString(vDataPeriodo, "Mes12Cerrado"))) {
                vResult = true;
            }
            return vResult;
        }

        private bool PerteneceAUnPeriodoCerrado() {
            bool vResult = false;
            if (LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Periodo", "PeriodoCerrado")) {
                vResult = true;
            }
            return vResult;
        }

        private bool PerteneceAUnPeriodoCerradoAnterior(int valConsecutivoCompania, DateTime valFecha) {
            bool vResult = false;
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInDateTime("FechaCierre", valFecha);
            string vSql = "SELECT PeriodoCerrado FROM Periodo WHERE ConsecutivoCompania = @ConsecutivoCompania AND @FechaCierre BETWEEN  FechaAperturaDelPeriodo and FechaCierreDelPeriodo";
            XElement vResultSet = LibBusiness.ExecuteSelect(vSql, vParams.Get(), "", 0);
            if (vResultSet != null) {
                var vEntity = from vRecord in vResultSet.Descendants("GpResult")
                              select vRecord;
                foreach (XElement vItem in vEntity) {
                    if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("PeriodoCerrado"), null))) {
                        vResult = LibConvert.SNToBool(vItem.Element("PeriodoCerrado"));
                        break;
                    }
                }
            }
            return vResult;
        }

        public string CampoMontoPorTasaDeCambioSql(string valFieldCambioABolivares, string valDbFieldNombreMoneda, string valDbFieldNameMonto, bool valUsaCambioOriginal, string valAlias) {
            string vSql = "";
            Galac.Comun.Ccl.TablasGen.IMonedaLocalActual vMonedaLocal = new Galac.Comun.Brl.TablasGen.clsMonedaLocalActual();
            QAdvSql SqlUtil = new QAdvSql("");
            DateTime vFechaVigencia = LibDate.Today();
            decimal vUltimoCambio = 0;
            string vCodiogMoneda;
            bool vExisteUnCambio;
            try {
                Galac.Comun.Ccl.TablasGen.ICambioPdn vCambio;
                vMonedaLocal.CargarTodasEnMemoriaYAsignarValoresDeLaActual(LibDefGen.ProgramInfo.Country, LibDate.Today());
                vCambio = new Galac.Comun.Brl.TablasGen.clsCambioNav();
                vCodiogMoneda = vMonedaLocal.CodigoMoneda(LibDate.Today());
                vExisteUnCambio = vCambio.BuscarUltimoCambioDeMoneda(vCodiogMoneda, out vFechaVigencia, out vUltimoCambio);
                vUltimoCambio = (vExisteUnCambio ? vUltimoCambio : 1);
                if (valUsaCambioOriginal) {
                    valFieldCambioABolivares = (LibString.IsNullOrEmpty(valFieldCambioABolivares) ? SqlUtil.ToSqlValue("1") : valFieldCambioABolivares);
                    vSql = SqlUtil.IIF(valDbFieldNombreMoneda + "=" + SqlUtil.ToSqlValue(vMonedaLocal.NombreMoneda(LibDate.Today())), valDbFieldNameMonto + " * 1 ", valDbFieldNameMonto + " * " + valFieldCambioABolivares, true);
                } else {
                    LibGpParams vParams = new LibGpParams();
                    vParams.AddInBoolean("Activa", true);
                    StringBuilder vSqlEx = new StringBuilder();
                    vSqlEx.AppendLine(";WITH CTE_Cambio(CodigoMoneda,Cambio) AS ");
                    vSqlEx.AppendLine("(SELECT Cambio.CodigoMoneda,Cambio.CambioABolivares ");
                    vSqlEx.AppendLine("FROM Cambio RIGHT JOIN IGV_CambioMaxFecha ON ");
                    vSqlEx.AppendLine("Cambio.FechaDeVigencia = IGV_CambioMaxFecha.FechaDeVigencia AND ");
                    vSqlEx.AppendLine("Cambio.CodigoMoneda = IGV_CambioMaxFecha.CodigoMoneda) ");
                    vSqlEx.AppendLine("SELECT ");
                    vSqlEx.AppendLine("Moneda.Nombre,");
                    vSqlEx.AppendLine("ISNULL(CTE_Cambio.Cambio,1) AS Cambio ");
                    vSqlEx.AppendLine("FROM Moneda ");
                    vSqlEx.AppendLine("LEFT JOIN CTE_Cambio ON ");
                    vSqlEx.AppendLine("Moneda.Codigo = CTE_Cambio.CodigoMoneda ");
                    vSqlEx.AppendLine(" WHERE Moneda.Activa = @Activa ");
                    XElement vGetMonedas = vGetMonedas = LibGalac.Aos.Brl.LibBusiness.ExecuteSelect(vSqlEx.ToString(), vParams.Get(), "", 0);
                    List<XElement> vListMoneda = vGetMonedas.Descendants("GpResult").ToList();
                    List<string> vListComparative = new List<string>();
                    List<string> vListResult = new List<string>();
                    if (vListMoneda.Count > 0) {
                        foreach (XElement vRecord in vListMoneda) {
                            vListComparative.Add(SqlUtil.ToSqlValue(LibXml.GetElementValueOrEmpty(vRecord, "Nombre")));
                            vListResult.Add(valDbFieldNameMonto + " * " + SqlUtil.ToSqlValue(LibImportData.ToDec(LibXml.GetElementValueOrEmpty(vRecord, "Cambio"))));
                        }
                    }
                    vSql = SqlUtil.CaseIf(valDbFieldNombreMoneda, vListComparative.ToArray(), vListResult.ToArray(), "=", valAlias);
                }
            } catch (Exception vEx) {
                throw new GalacException(vEx.Message, eExceptionManagementType.Controlled);
            }
            return vSql;
        }

        public int ObtenerNivelDePrecio(int valConsecutivoCompania, string valCodigoCliente) {
            StringBuilder vSQL = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            eTipoDeNivelDePrecio vTipoDeNivelDePrecio = (eTipoDeNivelDePrecio)LibConvert.DbValueToEnum(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "TipoDeNivelDePrecios"));
            IList<string> vNiveles = new List<string> {
                "Nivel 1",
                "Nivel 2",
                "Nivel 3",
                "Nivel 4"
            };
            int vNivelDePrecio = 1;
            switch (vTipoDeNivelDePrecio) {
                case eTipoDeNivelDePrecio.PorUsuario:
                    bool vResult = false;
                    foreach (string vNivel in vNiveles) {
                        vResult = LibSecurityManager.CurrentUserHasAccessTo("Niveles de Precio", vNivel);
                        if (vResult) {
                            break;
                        } else {
                            vNivelDePrecio++;
                        }
                    }
                    break;
                case eTipoDeNivelDePrecio.PorCliente:
                    vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
                    vParams.AddInString("Codigo", valCodigoCliente, 10);
                    vSQL.AppendLine("SELECT NivelDePrecio FROM dbo.Cliente WHERE Codigo = @Codigo AND ConsecutivoCompania = @ConsecutivoCompania");
                    XElement vXmlNivelDePrecio = LibBusiness.ExecuteSelect(vSQL.ToString(), vParams.Get(), "", 0);
                    vNivelDePrecio = vXmlNivelDePrecio.Descendants("GpResult").Select(s => LibImportData.ToInt(s.Element("NivelDePrecio").Value)).FirstOrDefault();
                    break;
            }
            return vNivelDePrecio;
        }

        public string CampoMontoPorTasaDeCambioSegunCodMonedaSql(string valFieldCambioABolivares, string valDbFieldCodigoMoneda, string valDbFieldNameMonto, bool valUsaCambioOriginal, string valAlias) {
            string vSql = "";
            Galac.Comun.Ccl.TablasGen.IMonedaLocalActual vMonedaLocal = new Galac.Comun.Brl.TablasGen.clsMonedaLocalActual();
            QAdvSql SqlUtil = new QAdvSql("");
            DateTime vFechaVigencia = LibDate.Today();
            decimal vUltimoCambio = 1;
            string vCodigoMoneda;
            bool vExisteUnCambio;
            try {
                Galac.Comun.Ccl.TablasGen.ICambioPdn vCambio = new Galac.Comun.Brl.TablasGen.clsCambioNav();
                vMonedaLocal.CargarTodasEnMemoriaYAsignarValoresDeLaActual(LibDefGen.ProgramInfo.Country, LibDate.Today());
                vCodigoMoneda = vMonedaLocal.CodigoMoneda(LibDate.Today());
                vExisteUnCambio = vCambio.BuscarUltimoCambioDeMoneda(vCodigoMoneda, out vFechaVigencia, out vUltimoCambio);
                vUltimoCambio = (vExisteUnCambio ? vUltimoCambio : 1);
                if (valUsaCambioOriginal) {
                    valFieldCambioABolivares = (LibString.IsNullOrEmpty(valFieldCambioABolivares) ? "1" : valFieldCambioABolivares);
                    vSql = SqlUtil.IIF(valDbFieldCodigoMoneda + "=" + SqlUtil.ToSqlValue(vMonedaLocal.CodigoMoneda(LibDate.Today())), valDbFieldNameMonto + " * 1 ", valDbFieldNameMonto + " * " + valFieldCambioABolivares, true);
                } else {
                    LibGpParams vParams = new LibGpParams();
                    vParams.AddInBoolean("Activa", true);
                    vParams.AddInEnum("TipoDeMoneda", LibConvert.ToInt(eTipoDeMoneda.Fisica));
                    StringBuilder vSqlEx = new StringBuilder();
                    vSqlEx.AppendLine(";WITH CTE_Cambio(CodigoMoneda,Cambio) AS ");
                    vSqlEx.AppendLine("(SELECT Cambio.CodigoMoneda,Cambio.CambioABolivares ");
                    vSqlEx.AppendLine("FROM Cambio RIGHT JOIN IGV_CambioMaxFecha ON ");
                    vSqlEx.AppendLine("Cambio.FechaDeVigencia = IGV_CambioMaxFecha.FechaDeVigencia AND ");
                    vSqlEx.AppendLine("Cambio.CodigoMoneda = IGV_CambioMaxFecha.CodigoMoneda) ");
                    vSqlEx.AppendLine("SELECT ");
                    vSqlEx.AppendLine("Moneda.Codigo,");
                    vSqlEx.AppendLine("ISNULL(CTE_Cambio.Cambio,1) AS Cambio ");
                    vSqlEx.AppendLine("FROM Moneda ");
                    vSqlEx.AppendLine("LEFT JOIN CTE_Cambio ON ");
                    vSqlEx.AppendLine("Moneda.Codigo = CTE_Cambio.CodigoMoneda ");
                    vSqlEx.AppendLine(" WHERE Moneda.Activa = @Activa ");
                    vSqlEx.AppendLine(" AND Moneda.TipoDeMoneda = @TipoDeMoneda");
                    XElement vGetMonedas = vGetMonedas = LibGalac.Aos.Brl.LibBusiness.ExecuteSelect(vSqlEx.ToString(), vParams.Get(), "", 0);
                    List<XElement> vListMoneda = vGetMonedas.Descendants("GpResult").ToList();
                    List<string> vListComparative = new List<string>();
                    List<string> vListResult = new List<string>();
                    if (vListMoneda.Count > 0) {
                        foreach (XElement vRecord in vListMoneda) {
                            vListComparative.Add(SqlUtil.ToSqlValue(LibXml.GetElementValueOrEmpty(vRecord, "Codigo")));
                            vListResult.Add(valDbFieldNameMonto + " * " + SqlUtil.ToSqlValue(LibImportData.ToDec(LibXml.GetElementValueOrEmpty(vRecord, "Cambio"))));
                        }
                    }
                    vSql = SqlUtil.CaseIf(valDbFieldCodigoMoneda, vListComparative.ToArray(), vListResult.ToArray(), "=", valAlias);
                }
            } catch (Exception vEx) {
                throw new GalacException(vEx.Message, eExceptionManagementType.Controlled);
            }
            return vSql;
        }

        public string ConvertHourToLongFormat(string valHours) {
            string vResult = "";
            if (LibString.IsNullOrEmpty(valHours)) {
                valHours = LibDate.CurrentHourAsStr;
            }
            valHours = LibString.Replace(valHours, "\u0020", "");
            valHours = LibString.Replace(valHours, ".", "");
            DateTime vTime = LibConvert.ToDate(valHours);
            vResult = string.Format("{0:HH:mm}", vTime);
            return vResult;
        }

        public string NotaMonedaCambioParaInformes(eMonedaDelInformeMM valMonedaDelInforme, eTasaDeCambioParaImpresion valTasaDeCambio, string valMoneda) {
            StringBuilder vNotaMonedaCambio = new StringBuilder();
            if (valMonedaDelInforme == eMonedaDelInformeMM.EnBolivares) {
                vNotaMonedaCambio.Append("Los montos están expresados en ");
                vNotaMonedaCambio.AppendLine(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania", "NombreMonedaLocal") + ".");
                if (valTasaDeCambio == eTasaDeCambioParaImpresion.DelDia) {
                    vNotaMonedaCambio.Append("Usando la tasa de cambio del día de hoy.");
                } else {
                    vNotaMonedaCambio.Append("Usando la tasa de cambio original.");
                }
            } else if (valMonedaDelInforme == eMonedaDelInformeMM.BolivaresExpresadosEnEnDivisa) {
                vNotaMonedaCambio.Append("Los montos en ");
                vNotaMonedaCambio.Append(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania", "NombreMonedaLocal"));
                vNotaMonedaCambio.AppendLine(" están expresados en " + valMoneda + ".");
                if (valTasaDeCambio == eTasaDeCambioParaImpresion.DelDia) {
                    vNotaMonedaCambio.Append("Usando la tasa de cambio del día de hoy.");
                } else {
                    vNotaMonedaCambio.Append("Usando la tasa de cambio más cercana a la fecha del documento.");
                }
            } else { //if (valMonedaDelInforme == eMonedaDelInformeMM.EnMonedaOriginal) {
                vNotaMonedaCambio.AppendLine("Los montos están expresados en la moneda original de cada documento.");
                vNotaMonedaCambio.Append("En los registros en moneda extranjera, se muestra la tasa de cambio a moneda local que fue registrada.");
            }
            return vNotaMonedaCambio.ToString();
        }
    }
}
