using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base;
using System.Xml;
using System.Xml.Linq;

namespace Galac.Adm.Ccl.Banco {
   public interface IBeneficiarioPdn : ILibPdn {
      #region Metodos Generados
      XElement FindBeneficiarioBy(int valConsecutivoCompania, string valCodigo);
      #endregion //Metodos Generados

      bool InsertaBeneficiariosDeNomina(XElement valXmlRecord, int valConsecutivoCompania);
      int ConsecutivoBeneficiarioGenerico(int valConsecutivoCompania);
      int ConsecutivoBeneficiarioGenericoParaCrearEmpresa(int valConsecutivoCompania);
      XElement ListadoBeneficiarios(StringBuilder valXmlParamsExpression);
   }
}
