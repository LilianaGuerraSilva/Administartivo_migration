using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Galac.Adm.Ccl.GestionCompras {
   public interface ILibroDeComprasPdn {
       void GenerarLibroDeCompras(int valConsecutivoCompania, string valMes, string valAno, string valNombreCompaniaParaInformes, string valNumeroRIF,  BackgroundWorker valBWorker);
       void GenerarPLELibroDeCompras(int valConsecutivoCompania, string valMes, string valAno, string valNombreCompaniaParaInformes, string valNumeroRIF,  bool RegistroDeComprasCompleto);       
   }
}
