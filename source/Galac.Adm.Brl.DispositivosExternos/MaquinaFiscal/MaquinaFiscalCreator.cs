using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
namespace Galac.Adm.Brl.DispositivosExternos.MaquinaFiscal {
    public class MaquinaFiscalCreator {

        public virtual clsMaquinaFiscal Crear(string valXmlMaquinaFiscal) {
            XElement vXmlMaquinaFiscal = XElement.Parse(valXmlMaquinaFiscal);
            string vModelo = clsMaquinaFiscalUtil.ObtenerValorNodoUnico(vXmlMaquinaFiscal, "MODELO");
            switch(vModelo.ToUpper()) {
                case "ELEPOSVMAX":
                    return new clsEleposVMAX(valXmlMaquinaFiscal);
                default:
                    throw new ArgumentException("Modelo de impresora aun no implementado");
            }
        }
    }
}
