using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using LibGalac.Aos.Base;
using LibGalac.Aos.Cnf;
using LibGalac.Aos.DefGen;
using System.Data;
using Galac.Saw.Brl.Inventario;
using Galac.Saw.Ccl.Inventario;

namespace Galac.Saw.Brl.Inventario {
    public class clsLibroElectronicoHelper {

        private const string _Separador = "|";

        public string Separador {
            get { return _Separador; }
        }

        public clsLibroElectronicoHelper() {

        }

        public string PathLibroElectronico(string valGrupoDeLibroPLE) {
            string vResult = LibGalac.Aos.DefGen.LibDefGen.DataPathUser(LibDefGen.ProgramInfo.ProgramInitials);
            if (!LibAppConfig.UseExternalConfig) {
                vResult = System.IO.Path.Combine(vResult, "Galac Software", LibDefGen.ProgramInfo.ProgramInitials);
                vResult = System.IO.Path.Combine(vResult, "Libros Electronicos");
                vResult = System.IO.Path.Combine(vResult, valGrupoDeLibroPLE);
            }
            if (!LibFile.DirExists(vResult)) {
                LibFile.CreateDir(vResult);
            }
            return vResult;
        }

        public string NombreDelArchivoConFormatoSunat(string valIdentificadorLibro, string valRucEmpresa, string valAno, string valMes, int valCantidadRegistros, bool valSoloMonedaLocal) {
            const string vCodigoOportunidadesEdosFinacieros = "00";
            const string vEsLibroElectronicoPLE = "1";
            const string vIndicadorOperaciones = "1";
            string vContieneRegistro = "0";
            string vIndicadorMoneda = "2";
            string vFechaPerido = valAno + valMes + "00";
            StringBuilder vResult = new StringBuilder();

            vResult.Append("LE");//Identificador fijo 'LE' 
            if (valCantidadRegistros > 0) {
                vContieneRegistro = "1";
            }
            if (valSoloMonedaLocal) {
                vIndicadorMoneda = "1";
            }
            vResult.Append(valRucEmpresa);
            vResult.Append(vFechaPerido);
            vResult.Append(valIdentificadorLibro);
            vResult.Append(vCodigoOportunidadesEdosFinacieros);
            vResult.Append(vIndicadorOperaciones);
            vResult.Append(vContieneRegistro);
            vResult.Append(vIndicadorMoneda);
            vResult.Append(vEsLibroElectronicoPLE);
            return vResult.ToString();
        }

        public string DarFormatoMonetarioParaPLE(string valMontoIn, int valCantidadDecimales) {
            string vResult;
            vResult = valMontoIn; 
            vResult = DarFormatoNumericoConDecimal(vResult, valCantidadDecimales);
            return vResult;
        }

        private string SetDecimalSeparator(string valNumero) {
            string vResult = "";
            string TokenSeparator = "";
            TokenSeparator = LibConvert.CurrentDecimalSeparator();
            if (LibText.S1IsInS2(".", valNumero) && TokenSeparator.Equals(",")) {
                vResult = LibText.Replace(valNumero, ".", ",");
            } else if (LibText.S1IsInS2(",", valNumero) && TokenSeparator.Equals(".")) {
                vResult = LibText.Replace(valNumero, ",", ".");
            } else {
                vResult = valNumero;
            }
            return vResult;
        }

        private string DarFormatoNumericoConDecimal(string valNumero, int valCantidadDecimales) {
            string vValorFinal = "";
            string vParteEntera = "";
            string vParteDecimal = "";
            int TokenPosition = 0;
            byte vCantidadDecimales = LibConvert.ToByte(valCantidadDecimales);
            string dubCeros = "";

            valNumero = LibText.Trim(valNumero);
            valNumero = SetDecimalSeparator(valNumero);
            TokenPosition = LibText.InStr(valNumero, LibConvert.CurrentDecimalSeparator());
            if (TokenPosition > 0) {
                vParteEntera = LibText.Left(valNumero, TokenPosition);
                vParteDecimal = LibText.Right(valNumero, LibText.Len(valNumero) - TokenPosition - 1);
                vParteDecimal = LibText.FillWithCharToRight(vParteDecimal, "0", vCantidadDecimales);
                vValorFinal = vParteEntera + "." + vParteDecimal;
            } else {
                dubCeros = new string('0', vCantidadDecimales);
                vValorFinal = vValorFinal + "." + dubCeros;
            }
            vValorFinal = LibText.Left(vValorFinal, 8);
            return vValorFinal;
        }

        public string IdentificadorRegistroPLE(eTipoDeRegistroPLE eTipoDeRegistro) {
            Dictionary<eTipoDeRegistroPLE, string> ConstantesLibroElectronico = new Dictionary<eTipoDeRegistroPLE, string> {
            {eTipoDeRegistroPLE.RegistroDeCompras,"080100"},
            {eTipoDeRegistroPLE.RegistroDeComprasSujtNoDomiciliado,"080200"},
            {eTipoDeRegistroPLE.RegistroDeComprasSimple,"080300"},
            {eTipoDeRegistroPLE.RegistroDeVentas,"140100"},
            {eTipoDeRegistroPLE.RegistroDeVentasSimple,"140200"},
            {eTipoDeRegistroPLE.RegistroDeInventPermanValorizado,"130100"},
            {eTipoDeRegistroPLE.RegistroDeInventUnidFiscas,"120100"}};
            return ConstantesLibroElectronico[eTipoDeRegistro];
        }
    }
}
