using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using LibGalac.Aos.Base;


namespace Galac.Saw.Ccl.Tablas {

    public interface IMaquinaFiscalPdn:ILibPdn {
        void GenerarRegistroDeMaquinaFiscal(int valConsecutivoCompania,string valConsecutivoMaquinaFiscal,string valserialMaquinaFiscal,string valDescripcion,int valLongitudNumeroFiscal,string valNombreOperador);        
        IList<MaquinaFiscal> GetDataListMaquinaFiscal(StringBuilder valParameters);
    }
}

