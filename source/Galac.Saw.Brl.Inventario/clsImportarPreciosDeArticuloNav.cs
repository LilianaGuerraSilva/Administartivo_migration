using Galac.Comun.Brl.TablasGen;
using Galac.Comun.Ccl.TablasGen;
using Galac.Saw.Ccl.Inventario;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Dal;
using LibGalac.Aos.Brl;
using LibGalac.Aos.DefGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml.Linq;

namespace Galac.Saw.Brl.Inventario {
    public class clsImportarPreciosDeArticuloNav : IImportarPreciosDeArticuloPdn {

        #region Variables y Constantes

        private List<PreciosDeArticulo> Precios = null;
        private ePrecioAjustar TipoDePrecio = ePrecioAjustar.PrecioSinIVA;
        private decimal Porcentage;
        List<string> ListaDeArticulosQueNoExisten = null;
        List<string> ListaDeLineasConError = null;
        bool ExistenLineasConFormatoIncorrecto = false;
        bool ExisteAlMenosUnArticuloValido = false;

        #endregion

        #region Propiedades

        public Action<decimal> OnProgressPercentChanged { get; set; }

        #endregion

        #region Constructores

        public clsImportarPreciosDeArticuloNav() {
            Precios = new List<PreciosDeArticulo>();
        }

        public clsImportarPreciosDeArticuloNav(ePrecioAjustar valTipoDePrecioDelArchivo) {
            Precios = new List<PreciosDeArticulo>();
            TipoDePrecio = valTipoDePrecioDelArchivo;
        }

        #endregion

        #region Miembros de IImportarPreciosDeArticuloPdn

        public string ImportFile(string valPathFile, bool valDesincorporarArticulos) {
            Porcentage = 0;
            OnProgressPercentChanged.Invoke(Porcentage);
            List<string> vLines = new List<string>();
            ListaDeLineasConError = new List<string>();
            using(StreamReader vStreamReader = new StreamReader(valPathFile, System.Text.Encoding.UTF8)) {
                vLines = vStreamReader.Lines().ToList();
            }
            OnProgressPercentChanged.Invoke(Porcentage += 20);
            decimal vProgressPercentPerLine = 60 / (vLines.Count + 1);
            int vCont = 1;
            foreach(var vLine in vLines) {
                string[] vElement = LibString.Split(vLine, ";", false);
                if(vElement.Length == 10) {
                    if(TipoDePrecio == ePrecioAjustar.PrecioSinIVA) {
                        AsignarValoresDePreciosSinIva(vElement);
                    } else if(TipoDePrecio == ePrecioAjustar.PrecioConIVA) {
                        AsignarValoresDePrecioConIva(vElement);
                    }
                } else {
                    ExistenLineasConFormatoIncorrecto = true;
                    ListaDeLineasConError.Add(vCont.ToString());
                }
                vCont++;
                OnProgressPercentChanged.Invoke(Porcentage += vProgressPercentPerLine);
            }
            ActualizarPreciosEnmonedaLocal(CrearXmlDePreciosEnMonedaLocal());
            OnProgressPercentChanged.Invoke(Porcentage += 10);
            ActualizarPreciosEnMonedaExtranjera(CrearXmlDePreciosEnMonedaExtranjera());
            OnProgressPercentChanged.Invoke(Porcentage += 10);
            if(valDesincorporarArticulos && ExisteAlMenosUnArticuloValido) {
                OnProgressPercentChanged.Invoke(Porcentage += 5);
                DesincorporarArticulosEIncorporarLosDelArchivo(CrearXmlDeCodigosDeArticulos());
                OnProgressPercentChanged.Invoke(Porcentage += 5);
            }
            OnProgressPercentChanged.Invoke(100);
            return CrearRepuestaDelProceso();
        }

        bool IImportarPreciosDeArticuloPdn.ValidarExtensionDeArchivo(string valPathFile) {
            bool vResult = false;
            string vExtension = Path.GetExtension(valPathFile);
            if(vExtension.Equals(".txt") || vExtension.Equals(".csv")) {
                vResult = true;
            }
            return vResult;
        }

        #endregion

        #region Metodos

        private string CrearRepuestaDelProceso() {
            StringBuilder vRespuesta = new StringBuilder();
            if(ExisteAlMenosUnArticuloValido && (ListaDeArticulosQueNoExisten == null && !ExistenLineasConFormatoIncorrecto)) {
                vRespuesta.AppendLine("Proceso terminado exitosamente. Artículos actualizados.");
            } else {
                int vCont = 1;
                if(!ExisteAlMenosUnArticuloValido) {
                    vRespuesta.AppendLine("No hay Artículos validos en el Archivo");
                    vRespuesta.AppendLine();
                } else {
                    vRespuesta.AppendLine("Proceso terminado con errores: ");
                    vRespuesta.AppendLine();
                }
                if(ExistenLineasConFormatoIncorrecto) {
                    vRespuesta.AppendLine($"     {vCont}. Existen líneas que no tienen un formato valido.");
                    string vMensajeDeLineasConError = "Líneas: ";
                    foreach(var vLine in ListaDeLineasConError) {
                        vMensajeDeLineasConError = $"{vMensajeDeLineasConError}{vLine}";
                        var x = ListaDeLineasConError[ListaDeLineasConError.Count() - 1].ToString();
                        if(vLine != ListaDeLineasConError[ListaDeLineasConError.Count() - 1].ToString()) {
                            vMensajeDeLineasConError += ", ";
                        }
                    }
                    vRespuesta.AppendLine(vMensajeDeLineasConError);
                    vRespuesta.AppendLine();
                    vCont++;
                }
                if(ListaDeArticulosQueNoExisten != null) {
                    vRespuesta.AppendLine($"     {vCont}. Los siguientes artículos no existen:");
                    foreach(var vArticulo in ListaDeArticulosQueNoExisten) {
                        vRespuesta.AppendLine($"            - {vArticulo}");
                    }
                }
            }
            return vRespuesta.ToString();
        }

        private bool ValidarQueArticuloExisteYBuscarTipoDeAlicuota(string valCodigoArticulo, ref string refTipoDeAlicuota) {
            bool vResult = false;
            StringBuilder vSql = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"));
            vParams.AddInString("Codigo", valCodigoArticulo, 30);
            vSql.AppendLine("SELECT AlicuotaIva FROM dbo.ArticuloInventario");
            vSql.AppendLine("WHERE Codigo = @Codigo AND ConsecutivoCompania = @ConsecutivoCompania");
            XElement vXmlArticulo = LibBusiness.ExecuteSelect(vSql.ToString(), vParams.Get(), "", 0);
            if(vXmlArticulo != null) {
                vResult = true;
                refTipoDeAlicuota = vXmlArticulo.Descendants("GpResult").Select(s => (string)s.Element("AlicuotaIva")).FirstOrDefault();
            } else {
                vResult = false;
            }
            return vResult;
        }

        private void AsignarValoresDePreciosSinIva(string[] valElement) {
            PreciosDeArticulo vPrecios = new PreciosDeArticulo();
            string vTipoDeAlicuota = string.Empty;
            vPrecios.Codigo = valElement[0];
            vPrecios.Descripcion = valElement[1].Trim();
            vPrecios.Nivel1 = LibImportData.ToDec(valElement[2].Trim(), 4);
            vPrecios.Nivel2 = LibImportData.ToDec(valElement[3].Trim(), 4);
            vPrecios.Nivel3 = LibImportData.ToDec(valElement[4].Trim(), 4);
            vPrecios.Nivel4 = LibImportData.ToDec(valElement[5].Trim(), 4);
            vPrecios.Nivel1Extranjera = LibImportData.ToDec(valElement[6].Trim(), 4);
            vPrecios.Nivel2Extranjera = LibImportData.ToDec(valElement[7].Trim(), 4);
            vPrecios.Nivel3Extranjera = LibImportData.ToDec(valElement[8].Trim(), 4);
            vPrecios.Nivel4Extranjera = LibImportData.ToDec(valElement[9].Trim(), 4);
            if(!LibString.IsNullOrEmpty(vPrecios.Codigo)) {
                if(ValidarQueArticuloExisteYBuscarTipoDeAlicuota(vPrecios.Codigo, ref vTipoDeAlicuota)) {
                    CalcularPrecioConIvaOSinIvaSegunTipoDePreciosEnArchivo(ref vPrecios, vTipoDeAlicuota);
                    Precios.Add(vPrecios);
                    ExisteAlMenosUnArticuloValido = true;
                } else {
                    ListaDeArticulosQueNoExisten = ListaDeArticulosQueNoExisten == null ? new List<string>() : ListaDeArticulosQueNoExisten;
                    ListaDeArticulosQueNoExisten.Add(vPrecios.Codigo + " " + vPrecios.Descripcion);
                }
            }
        }

        private void AsignarValoresDePrecioConIva(string[] valElement) {
            PreciosDeArticulo vPrecios = new PreciosDeArticulo();
            string vTipoDeAlicuota = string.Empty;
            vPrecios.Codigo = valElement[0];
            vPrecios.Descripcion = valElement[1].Trim();
            vPrecios.Nivel1ConIva = LibImportData.ToDec(valElement[2].Trim(), 4);
            vPrecios.Nivel2ConIva = LibImportData.ToDec(valElement[3].Trim(), 4);
            vPrecios.Nivel3ConIva = LibImportData.ToDec(valElement[4].Trim(), 4);
            vPrecios.Nivel4ConIva = LibImportData.ToDec(valElement[5].Trim(), 4);
            vPrecios.Nivel1ExtranjeraConIva = LibImportData.ToDec(valElement[6].Trim(), 4);
            vPrecios.Nivel2ExtranjeraConIva = LibImportData.ToDec(valElement[7].Trim(), 4);
            vPrecios.Nivel3ExtranjeraConIva = LibImportData.ToDec(valElement[8].Trim(), 4);
            vPrecios.Nivel4ExtranjeraConIva = LibImportData.ToDec(valElement[9].Trim(), 4);
            if(!LibString.IsNullOrEmpty(vPrecios.Codigo)) {
                if(ValidarQueArticuloExisteYBuscarTipoDeAlicuota(vPrecios.Codigo, ref vTipoDeAlicuota)) {
                    CalcularPrecioConIvaOSinIvaSegunTipoDePreciosEnArchivo(ref vPrecios, vTipoDeAlicuota);
                    Precios.Add(vPrecios);
                    ExisteAlMenosUnArticuloValido = true;
                } else {
                    ListaDeArticulosQueNoExisten = ListaDeArticulosQueNoExisten == null ? new List<string>() : ListaDeArticulosQueNoExisten;
                    ListaDeArticulosQueNoExisten.Add(vPrecios.Codigo + " " + vPrecios.Descripcion);
                }
            }
        }

        private void CalcularPrecioConIvaOSinIvaSegunTipoDePreciosEnArchivo(ref PreciosDeArticulo refPrecios, string valTipoDeAlicuota) {
            decimal vPorcentajeAlicuota = 0;
            eTipoDeAlicuota vTipoDeAlicuota = (eTipoDeAlicuota)LibConvert.DbValueToEnum(valTipoDeAlicuota);
            int vCantidadDeDecimalesInt = LibImportData.ToInt(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "CantidadDeDecimales"));
            switch(vTipoDeAlicuota) {
                case eTipoDeAlicuota.Exento:
                    vPorcentajeAlicuota = 0;
                    break;
                case eTipoDeAlicuota.AlicuotaGeneral:
                    vPorcentajeAlicuota = LibImportData.ToDec(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "PorcentajeAlicuota1")) / 100;
                    break;
                case eTipoDeAlicuota.Alicuota2:
                    vPorcentajeAlicuota = LibImportData.ToDec(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "PorcentajeAlicuota2")) / 100;
                    break;
                case eTipoDeAlicuota.Alicuota3:
                    vPorcentajeAlicuota = LibImportData.ToDec(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "PorcentajeAlicuota3")) / 100;
                    break;
            }
            if(TipoDePrecio == ePrecioAjustar.PrecioSinIVA) {
                refPrecios.Nivel1ConIva = LibMath.RoundToNDecimals(refPrecios.Nivel1 + (refPrecios.Nivel1 * vPorcentajeAlicuota), vCantidadDeDecimalesInt);
                refPrecios.Nivel2ConIva = LibMath.RoundToNDecimals(refPrecios.Nivel2 + (refPrecios.Nivel2 * vPorcentajeAlicuota), vCantidadDeDecimalesInt);
                refPrecios.Nivel3ConIva = LibMath.RoundToNDecimals(refPrecios.Nivel3 + (refPrecios.Nivel3 * vPorcentajeAlicuota), vCantidadDeDecimalesInt);
                refPrecios.Nivel4ConIva = LibMath.RoundToNDecimals(refPrecios.Nivel4 + (refPrecios.Nivel4 * vPorcentajeAlicuota), vCantidadDeDecimalesInt);
                refPrecios.Nivel1ExtranjeraConIva = LibMath.RoundToNDecimals(refPrecios.Nivel1Extranjera + (refPrecios.Nivel1Extranjera * vPorcentajeAlicuota), vCantidadDeDecimalesInt);
                refPrecios.Nivel2ExtranjeraConIva = LibMath.RoundToNDecimals(refPrecios.Nivel2Extranjera + (refPrecios.Nivel2Extranjera * vPorcentajeAlicuota), vCantidadDeDecimalesInt);
                refPrecios.Nivel3ExtranjeraConIva = LibMath.RoundToNDecimals(refPrecios.Nivel3Extranjera + (refPrecios.Nivel3Extranjera * vPorcentajeAlicuota), vCantidadDeDecimalesInt);
                refPrecios.Nivel4ExtranjeraConIva = LibMath.RoundToNDecimals(refPrecios.Nivel4Extranjera + (refPrecios.Nivel4Extranjera * vPorcentajeAlicuota), vCantidadDeDecimalesInt);
            } else if(TipoDePrecio == ePrecioAjustar.PrecioConIVA) {
                refPrecios.Nivel1 = LibMath.RoundToNDecimals(refPrecios.Nivel1ConIva / (1 + vPorcentajeAlicuota), vCantidadDeDecimalesInt);
                refPrecios.Nivel2 = LibMath.RoundToNDecimals(refPrecios.Nivel2ConIva / (1 + vPorcentajeAlicuota), vCantidadDeDecimalesInt);
                refPrecios.Nivel3 = LibMath.RoundToNDecimals(refPrecios.Nivel3ConIva / (1 + vPorcentajeAlicuota), vCantidadDeDecimalesInt);
                refPrecios.Nivel4 = LibMath.RoundToNDecimals(refPrecios.Nivel4ConIva / (1 + vPorcentajeAlicuota), vCantidadDeDecimalesInt);
                refPrecios.Nivel1Extranjera = LibMath.RoundToNDecimals(refPrecios.Nivel1ExtranjeraConIva / (1 + vPorcentajeAlicuota), vCantidadDeDecimalesInt);
                refPrecios.Nivel2Extranjera = LibMath.RoundToNDecimals(refPrecios.Nivel2ExtranjeraConIva / (1 + vPorcentajeAlicuota), vCantidadDeDecimalesInt);
                refPrecios.Nivel3Extranjera = LibMath.RoundToNDecimals(refPrecios.Nivel3ExtranjeraConIva / (1 + vPorcentajeAlicuota), vCantidadDeDecimalesInt);
                refPrecios.Nivel4Extranjera = LibMath.RoundToNDecimals(refPrecios.Nivel4ExtranjeraConIva / (1 + vPorcentajeAlicuota), vCantidadDeDecimalesInt);
            }
        }

        private XElement CrearXmlDePreciosEnMonedaLocal() {
            XElement vResult = new XElement("GpData");
            foreach(PreciosDeArticulo vPrecio in Precios) {
                vResult.Add(new XElement("GpResult",
                    new XElement("Codigo", vPrecio.Codigo),
                    new XElement("Descripcion", vPrecio.Descripcion),
                    new XElement("N1", vPrecio.Nivel1),
                    new XElement("N1ConIva", vPrecio.Nivel1ConIva),
                    new XElement("N2", vPrecio.Nivel2),
                    new XElement("N2ConIva", vPrecio.Nivel2ConIva),
                    new XElement("N3", vPrecio.Nivel3),
                    new XElement("N3ConIva", vPrecio.Nivel3ConIva),
                    new XElement("N4", vPrecio.Nivel4),
                    new XElement("N4ConIva", vPrecio.Nivel4ConIva)));
            }
            return vResult;
        }

        private XElement CrearXmlDePreciosEnMonedaExtranjera() {
            XElement vResult = new XElement("GpData");
            foreach(PreciosDeArticulo vPrecio in Precios) {
                vResult.Add(new XElement("GpResult",
                    new XElement("Codigo", vPrecio.Codigo),
                    new XElement("N1E", vPrecio.Nivel1Extranjera),
                    new XElement("N1EConIva", vPrecio.Nivel1ExtranjeraConIva),
                    new XElement("N2E", vPrecio.Nivel2Extranjera),
                    new XElement("N2EConIva", vPrecio.Nivel2ExtranjeraConIva),
                    new XElement("N3E", vPrecio.Nivel3Extranjera),
                    new XElement("N3EConIva", vPrecio.Nivel3ExtranjeraConIva),
                    new XElement("N4E", vPrecio.Nivel4Extranjera),
                    new XElement("N4EConIva", vPrecio.Nivel4ExtranjeraConIva)));
            }
            return vResult;
        }

        private XElement CrearXmlDeCodigosDeArticulos() {
            XElement vResult = new XElement("GpData");
            foreach(PreciosDeArticulo vPrecio in Precios) {
                vResult.Add(new XElement("GpResult",
                    new XElement("Codigo", vPrecio.Codigo)));
            }
            return vResult;
        }

        private void ActualizarPreciosEnmonedaLocal(XElement valXmlPreciosEnMonedaLocal) {
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"));
            vParams.AddInString("NombreOperador", ((CustomIdentity)Thread.CurrentPrincipal.Identity).Login, 10);
            vParams.AddInDateTime("FechaUltimaModificacion", LibDate.Today());
            vParams.AddInXml("XmlDatos", valXmlPreciosEnMonedaLocal);
            string vSql = SqlParaActualizarPreciosDeArticuloEnMonedaLocal();
            int x = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");
            LibBusiness.ExecuteUpdateOrDelete(vSql, vParams.Get(), "", -1);
        }

        private void ActualizarPreciosEnMonedaExtranjera(XElement valXmlPreciosEnMonedaExtranjera) {
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"));
            vParams.AddInString("NombreOperador", ((CustomIdentity)Thread.CurrentPrincipal.Identity).Login, 10);
            vParams.AddInDateTime("FechaUltimaModificacion", LibDate.Today());
            vParams.AddInXml("XmlDatos", valXmlPreciosEnMonedaExtranjera);
            string vSql = SqlParaActualizarPreciosDeArticuloEnMonedaExtranjera();
            LibBusiness.ExecuteUpdateOrDelete(vSql, vParams.Get(), "", -1);
        }

        private void DesincorporarArticulosEIncorporarLosDelArchivo(XElement valXmlCodigosDeArticulos) {
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"));
            vParams.AddInXml("XmlCodigos", valXmlCodigosDeArticulos);
            string vSql = SqlParaDesincorporarArticulosEIncorporarLosDelArchivo();
            LibBusiness.ExecuteUpdateOrDelete(vSql, vParams.Get(), "", -1);
        }

        #endregion

        #region Queries

        private string SqlParaActualizarPreciosDeArticuloEnMonedaLocal() {
            StringBuilder vSql = new StringBuilder();
            QAdvSql InsSql = new QAdvSql(string.Empty);
            vSql.AppendLine("DECLARE @hdoc " + InsSql.NumericTypeForDb(10, 0) + "");
            vSql.AppendLine("EXEC sp_xml_preparedocument @hdoc OUTPUT, @XmlDatos");
            vSql.AppendLine(";WITH CTE_Datos AS(");
            vSql.AppendLine("SELECT Codigo,");
            vSql.AppendLine("	    Descripcion,");
            vSql.AppendLine("	    N1,");
            vSql.AppendLine("       N1ConIva,");
            vSql.AppendLine("       N2,");
            vSql.AppendLine("	    N2ConIva,");
            vSql.AppendLine("	    N3,");
            vSql.AppendLine("	    N3ConIva,");
            vSql.AppendLine("	    N4,");
            vSql.AppendLine("	    N4ConIva");
            vSql.AppendLine("FROM OPENXML(@hdoc, 'GpData/GpResult', 2)");
            vSql.AppendLine("WITH (");
            vSql.AppendLine("	Codigo " + InsSql.VarCharTypeForDb(30) + ",");
            vSql.AppendLine("	Descripcion " + InsSql.VarCharTypeForDb(7000) + ",");
            vSql.AppendLine("	N1 " + InsSql.DecimalTypeForDb(25, 4) + ",");
            vSql.AppendLine("	N1ConIva " + InsSql.DecimalTypeForDb(25, 4) + ",");
            vSql.AppendLine("	N2 " + InsSql.DecimalTypeForDb(25, 4) + ",");
            vSql.AppendLine("	N2ConIva " + InsSql.DecimalTypeForDb(25, 4) + ",");
            vSql.AppendLine("	N3 " + InsSql.DecimalTypeForDb(25, 4) + ",");
            vSql.AppendLine("	N3ConIva " + InsSql.DecimalTypeForDb(25, 4) + ",");
            vSql.AppendLine("	N4 " + InsSql.DecimalTypeForDb(25, 4) + ",");
            vSql.AppendLine("	N4ConIva " + InsSql.DecimalTypeForDb(25, 4) + ")");
            vSql.AppendLine(")");
            vSql.AppendLine("UPDATE dbo.ArticuloInventario");
            vSql.AppendLine("SET PrecioSinIva = CTE_Datos.N1,");
            vSql.AppendLine("	 PrecioConIva = CTE_Datos.N1ConIva,");
            vSql.AppendLine("	 PrecioSinIva2 = CTE_Datos.N2,");
            vSql.AppendLine("	 PrecioConIva2 = CTE_Datos.N2ConIva,");
            vSql.AppendLine("	 PrecioSinIva3 = CTE_Datos.N3,");
            vSql.AppendLine("	 PrecioConIva3 = CTE_Datos.N3ConIva,");
            vSql.AppendLine("	 PrecioSinIva4 = CTE_Datos.N4,");
            vSql.AppendLine("	 PrecioConIva4 = CTE_Datos.N4ConIva,");
            vSql.AppendLine("	 Descripcion = CASE WHEN CTE_Datos.Descripcion = '' THEN ArticuloInventario.Descripcion");
            vSql.AppendLine("	               ELSE CTE_Datos.Descripcion");
            vSql.AppendLine("	               END,");
            vSql.AppendLine("	 NombreOperador = @NombreOperador,");
            vSql.AppendLine("	 FechaUltimaModificacion = @FechaUltimaModificacion");
            vSql.AppendLine("FROM dbo.ArticuloInventario");
            vSql.AppendLine("INNER JOIN CTE_Datos");
            vSql.AppendLine("ON ArticuloInventario.Codigo = CTE_Datos.Codigo");
            vSql.AppendLine("AND ArticuloInventario.ConsecutivoCompania = @ConsecutivoCompania");
            vSql.AppendLine("WHERE ArticuloInventario.Codigo = CTE_Datos.Codigo");
            vSql.AppendLine("AND ArticuloInventario.ConsecutivoCompania = @ConsecutivoCompania");
            vSql.AppendLine("EXEC sp_xml_removedocument @hdoc");
            return vSql.ToString();
        }

        private string SqlParaActualizarPreciosDeArticuloEnMonedaExtranjera() {
            StringBuilder vSql = new StringBuilder();
            QAdvSql InsSql = new QAdvSql(string.Empty);
            vSql.AppendLine("DECLARE @hdoc " + InsSql.NumericTypeForDb(10, 0) + "");
            vSql.AppendLine("EXEC sp_xml_preparedocument @hdoc OUTPUT, @XmlDatos");
            vSql.AppendLine(";WITH CTE_Datos AS(");
            vSql.AppendLine("SELECT Codigo,");
            vSql.AppendLine("	    N1E,");
            vSql.AppendLine("       N1EConIva,");
            vSql.AppendLine("       N2E,");
            vSql.AppendLine("	    N2EConIva,");
            vSql.AppendLine("	    N3E,");
            vSql.AppendLine("	    N3EConIva,");
            vSql.AppendLine("	    N4E,");
            vSql.AppendLine("	    N4EConIva");
            vSql.AppendLine("FROM OPENXML(@hdoc, 'GpData/GpResult', 2)");
            vSql.AppendLine("WITH (");
            vSql.AppendLine("	Codigo " + InsSql.VarCharTypeForDb(30) + ",");
            vSql.AppendLine("	N1E " + InsSql.DecimalTypeForDb(25, 4) + ",");
            vSql.AppendLine("	N1EConIva " + InsSql.DecimalTypeForDb(25, 4) + ",");
            vSql.AppendLine("	N2E " + InsSql.DecimalTypeForDb(25, 4) + ",");
            vSql.AppendLine("	N2EConIva " + InsSql.DecimalTypeForDb(25, 4) + ",");
            vSql.AppendLine("	N3E " + InsSql.DecimalTypeForDb(25, 4) + ",");
            vSql.AppendLine("	N3EConIva " + InsSql.DecimalTypeForDb(25, 4) + ",");
            vSql.AppendLine("	N4E " + InsSql.DecimalTypeForDb(25, 4) + ",");
            vSql.AppendLine("	N4EConIva " + InsSql.DecimalTypeForDb(25, 4) + ")");
            vSql.AppendLine(")");
            vSql.AppendLine("UPDATE dbo.CamposMonedaExtranjera");
            vSql.AppendLine("SET MePrecioSinIva = CTE_Datos.N1E,");
            vSql.AppendLine("	 MePrecioConIva = CTE_Datos.N1EConIva,");
            vSql.AppendLine("	 MePrecioSinIva2 = CTE_Datos.N2E,");
            vSql.AppendLine("	 MePrecioConIva2 = CTE_Datos.N2EConIva,");
            vSql.AppendLine("	 MePrecioSinIva3 = CTE_Datos.N3E,");
            vSql.AppendLine("	 MePrecioConIva3 = CTE_Datos.N3EConIva,");
            vSql.AppendLine("	 MePrecioSinIva4 = CTE_Datos.N4E,");
            vSql.AppendLine("	 MePrecioConIva4 = CTE_Datos.N4EConIva,");
            vSql.AppendLine("	 NombreOperador = @NombreOperador,");
            vSql.AppendLine("	 FechaUltimaModificacion = @FechaUltimaModificacion");
            vSql.AppendLine("FROM dbo.CamposMonedaExtranjera");
            vSql.AppendLine("INNER JOIN CTE_Datos");
            vSql.AppendLine("ON CamposMonedaExtranjera.Codigo = CTE_Datos.Codigo");
            vSql.AppendLine("AND CamposMonedaExtranjera.ConsecutivoCompania = @ConsecutivoCompania");
            vSql.AppendLine("WHERE CamposMonedaExtranjera.Codigo = CTE_Datos.Codigo");
            vSql.AppendLine("AND CamposMonedaExtranjera.ConsecutivoCompania = @ConsecutivoCompania");
            vSql.AppendLine("EXEC sp_xml_removedocument @hdoc");
            return vSql.ToString();
        }

        private string SqlParaDesincorporarArticulosEIncorporarLosDelArchivo() {
            StringBuilder vSql = new StringBuilder();
            QAdvSql InsSql = new QAdvSql(string.Empty);
            vSql.AppendLine("DECLARE @hdoc " + InsSql.NumericTypeForDb(10, 0) + "");
            vSql.AppendLine("EXEC sp_xml_preparedocument @hdoc OUTPUT, @XmlCodigos");

            vSql.AppendLine("BEGIN");
            vSql.AppendLine("UPDATE dbo.ArticuloInventario SET StatusdelArticulo = '1' WHERE ConsecutivoCompania = @ConsecutivoCompania");
            vSql.AppendLine("END");

            vSql.AppendLine("BEGIN");
            vSql.AppendLine(";WITH CTE_Codigos AS(");
            vSql.AppendLine("SELECT Codigo");
            vSql.AppendLine("FROM OPENXML(@hdoc, 'GpData/GpResult', 2)");
            vSql.AppendLine("WITH (");
            vSql.AppendLine("	Codigo " + InsSql.VarCharTypeForDb(30) + ")");
            vSql.AppendLine(")");
            vSql.AppendLine("UPDATE dbo.ArticuloInventario");
            vSql.AppendLine("SET StatusdelArticulo = '0'");
            vSql.AppendLine("FROM dbo.ArticuloInventario");
            vSql.AppendLine("INNER JOIN CTE_Codigos");
            vSql.AppendLine("ON ArticuloInventario.Codigo = CTE_Codigos.Codigo");
            vSql.AppendLine("AND ArticuloInventario.ConsecutivoCompania = @ConsecutivoCompania");
            vSql.AppendLine("WHERE ArticuloInventario.Codigo = CTE_Codigos.Codigo AND ArticuloInventario.ConsecutivoCompania = @ConsecutivoCompania");
            vSql.AppendLine("END");

            vSql.AppendLine("EXEC sp_xml_removedocument @hdoc");
            return vSql.ToString();
        }

        #endregion
    }
}
