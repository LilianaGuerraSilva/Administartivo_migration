using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Threading;
using Galac.Adm.Ccl.Venta;
using Galac.Adm.Dal.Venta;
using LibGalac.Aos.Base;
using LibGalac.Aos.Brl;
//using Galac.Comun.Ccl.SttDef;
using LibGalac.Aos.Base.Dal;
using Galac.Adm.Ccl.Banco;
using Galac.Adm.Brl.Banco;

namespace Galac.Adm.Brl.Venta {
    public class clsMovimientoBancarioPuntoDeVentaNav:IMovimientoBancarioPuntoDeVentaPdn {
        IMovimientoBancarioPdn _MovimientoBancarioDat;
        int IMovimientoBancarioPuntoDeVentaPdn.GenerarProximoConsecutivoMovimiento(int valConsecutivoCompania) {
            int vResult = 1;
            int vMaximo = 0;
            QAdvSql insQAdvSql = new QAdvSql("");
            string vSql = "SELECT MAX(ConsecutivoMovimiento) AS maximo FROM MovimientoBancario WHERE ConsecutivoCompania = " + insQAdvSql.ToSqlValue(valConsecutivoCompania);
            LibGpParams vParams = new LibGpParams();
            XElement xResult = LibBusiness.ExecuteSelect(vSql.ToString(),vParams.Get(),"",0);
            if(xResult != null) {
                vMaximo = LibConvert.ToInt(LibXml.GetPropertyString(xResult,"maximo"));
                if(vMaximo >= 0) {
                    vResult = vMaximo + 1;
                } else {
                    vResult = vMaximo;
                }
            }
            return vResult;
        }

        bool IMovimientoBancarioPuntoDeVentaPdn.ActualizarSaldoCuentaBancariaPuntoDeVenta(int consecutivoCompania,XElement valData) {
            QAdvSql insQAdvSql = new QAdvSql("");
            bool vResult = false;
            decimal vTotalMovimiento = LibImportData.ToDec(LibXml.GetPropertyString(valData,"TotalFactura"));
            string vCodigoCliente = LibXml.GetPropertyString(valData,"CodigoCliente");
            string vNombreCliente = LibXml.GetPropertyString(valData,"NombreCliente");
            string vCodigoVendedor = LibXml.GetPropertyString(valData,"CodigoVendedor");
            string vComprobanteFiscal = LibXml.GetPropertyString(valData,"NumeroComprobanteFiscal");
            string vNumeroFactura = LibXml.GetPropertyString(valData,"Numero");
            string vCodigoCXC = vNumeroFactura + "-" + vNumeroFactura;
            DateTime vFechaFactura = LibConvert.ToDate(LibXml.GetPropertyString(valData,"Fecha"));
            string vNombreOperador = LibXml.GetPropertyString(valData,"NombreOperador");
            string vCodigoMoneda = LibXml.GetPropertyString(valData,"CodigoMoneda");
            string vCodigoCuentaBancaria = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("FacturaRapida","CuentaBancariaCobroDirecto");
            string vConceptoBancario = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("FacturaRapida","ConceptoBancarioCobroDirecto");
            eIngresoEgreso vTipoConcepto = eIngresoEgreso.Ingreso;
            ICuentaBancariaPdn cuentaNav = new clsCuentaBancariaNav();
            vResult = cuentaNav.ActualizaSaldoDisponibleEnCuenta(consecutivoCompania,vCodigoCuentaBancaria,vTotalMovimiento.ToString(),((int)vTipoConcepto).ToString(),((int)eAccionSR.Insertar),"0",false);
            return vResult;
        }  
        
		bool IMovimientoBancarioPuntoDeVentaPdn.Insert(int valConsecutivoCompania,int valConsecutivoMovimiento,string valNumeroCobranza,XElement valData) {
            bool vResult = false;
            _MovimientoBancarioDat = new clsMovimientoBancarioNav();
            List<MovimientoBancario> listMovimientoBancario = BuildDataMovimientoBancarioPuntoDeVenta(valConsecutivoCompania,valConsecutivoMovimiento,valNumeroCobranza,valData);
            vResult = _MovimientoBancarioDat.Insert(listMovimientoBancario);         
            return vResult;
        }
		
		 bool IMovimientoBancarioPuntoDeVentaPdn.Insert(List<MovimientoBancario> list) {
            bool vResult = false;
            _MovimientoBancarioDat = new clsMovimientoBancarioNav();
            vResult = _MovimientoBancarioDat.Insert(list);
            return vResult;
        }

        private List<MovimientoBancario> BuildDataMovimientoBancarioPuntoDeVenta(int valConsecutivoCompania,int valConsecutivoMovimiento,string valNumeroCobranza,XElement valData) {
            List<MovimientoBancario> vResult = new List<MovimientoBancario>();
            vResult.Add(new MovimientoBancario());
            vResult[0].ConsecutivoCompania = valConsecutivoCompania;
            vResult[0].ConsecutivoMovimiento = valConsecutivoMovimiento;
            vResult[0].CodigoCtaBancaria = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("FacturaRapida","CuentaBancariaCobroDirecto"); ;
            vResult[0].CodigoConcepto = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("FacturaRapida","ConceptoBancarioCobroDirecto");
            vResult[0].Fecha = LibConvert.ToDate(LibXml.GetPropertyString(valData,"Fecha"));
            vResult[0].TipoConceptoAsEnum = eIngresoEgreso.Ingreso;
            vResult[0].Monto = LibImportData.ToDec(LibXml.GetPropertyString(valData,"TotalFactura"));
            vResult[0].NumeroDocumento = valNumeroCobranza;
            vResult[0].Descripcion = "Cobro a - " + LibXml.GetPropertyString(valData,"CodigoCliente") + " " + LibXml.GetPropertyString(valData,"NombreCliente");
            vResult[0].GeneraImpuestoBancarioAsBool = false;
            vResult[0].NroMovimientoRelacionado = valNumeroCobranza;
            vResult[0].GeneradoPorAsEnum = eGeneradoPor.Cobranza;
            vResult[0].CambioABolivares = LibImportData.ToDec(LibXml.GetPropertyString(valData,"CambioABolivares"));
            vResult[0].ImprimirChequeAsBool = false;
            vResult[0].ConciliadoSNAsBool = false;
            vResult[0].NroConciliacion = "";
            vResult[0].GenerarAsientoDeRetiroEnCuentaAsBool = false;
            vResult[0].NombreOperador = ((CustomIdentity)Thread.CurrentPrincipal.Identity).Login;
            vResult[0].FechaUltimaModificacion = LibDate.Today();
            return vResult;
        }
    }
}


    






