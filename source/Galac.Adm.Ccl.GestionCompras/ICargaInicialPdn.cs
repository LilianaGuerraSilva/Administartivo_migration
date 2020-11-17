using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using LibGalac.Aos.Base;
namespace Galac.Adm.Ccl.GestionCompras {

    public interface ICargaInicialPdn : ILibPdn {
        #region Metodos Generados
        XElement FindByConsecutivoCompaniaCodigoFecha(int valConsecutivoCompania, string valCodigo, DateTime valFecha);
        #endregion //Metodos Generados


    } //End of class ICargaInicialPdn

} //End of namespace Galac.Adm.Ccl.GestionCompras

