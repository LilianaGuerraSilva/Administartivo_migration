using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Galac.Saw.Ccl.Inventario {
    public interface IImportarPreciosDeArticuloPdn {
        /// <summary>
        /// Importa los Precios de Articulos en el Archivo de Texto.
        /// </summary>
        /// <param name="valPathFile">Indica la ruta del Archivo</param>
        /// <param name="valDesincorporarArticulos">Indica si se desea Desincorporar todos los Articulos e Incorporar solo los que vienen en el Archivo</param>
        /// <returns>Respuesta con Información sobre el Proceso</returns>
        string ImportFile(string valPathFile, bool valDesincorporarArticulos);
        /// <summary>
        /// Indica si la extención del Archivo es correcta (.txt).
        /// </summary>
        bool ValidarExtensionDeArchivo(string valPathFile);
    }
}
