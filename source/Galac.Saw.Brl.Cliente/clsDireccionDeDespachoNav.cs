using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security;
using System.Security.Permissions;
using System.Xml;
using System.Xml.Linq;
using LibGalac.Aos.Base;
using LibGalac.Aos.Brl;
using LibGalac.Aos.Base.Dal;
using Galac.Saw.Ccl.Cliente;

namespace Galac.Saw.Brl.Cliente {
    public partial class clsDireccionDeDespachoNav: LibBaseNavDetail<IList<DireccionDeDespacho>, IList<DireccionDeDespacho>> {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsDireccionDeDespachoNav() {
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibDataDetailComponent<IList<DireccionDeDespacho>, IList<DireccionDeDespacho>> GetDataInstance() {
            return new Galac.Saw.Dal.Cliente.clsDireccionDeDespachoDat();
        }

        private void FillWithForeignInfo(ref IList<DireccionDeDespacho> refData) {
            FillWithForeignInfoDireccionDeDespacho(ref refData);
        }
        #region DireccionDeDespacho

        private void FillWithForeignInfoDireccionDeDespacho(ref IList<DireccionDeDespacho> refData) {
            XElement vInfoConexionCliente = FindInfoCliente(refData);
            var vListCliente = (from vRecord in vInfoConexionCliente.Descendants("GpResult")
                                      select new {
                                          ConsecutivoCompania = LibConvert.ToInt(vRecord.Element("ConsecutivoCompania")),
                                          Consecutivo = LibConvert.ToInt(vRecord.Element("Consecutivo")), 
                                          Codigo = vRecord.Element("Codigo").Value, 
                                          Nombre = vRecord.Element("Nombre").Value, 
                                          NumeroRIF = vRecord.Element("NumeroRIF").Value, 
                                          NumeroNIT = vRecord.Element("NumeroNIT").Value, 
										  Status = vRecord.Element("Status").Value, 
                                          SeccionGenerales = vRecord.Element("SeccionGenerales").Value, 
                                          Direccion = vRecord.Element("Direccion").Value, 
                                          Ciudad = vRecord.Element("Ciudad").Value, 
                                          ZonaPostal = vRecord.Element("ZonaPostal").Value, 
                                          Telefono = vRecord.Element("Telefono").Value, 
                                          FAX = vRecord.Element("FAX").Value, 
                                          Contacto = vRecord.Element("Contacto").Value, 
                                          ZonaDeCobranza = vRecord.Element("ZonaDeCobranza").Value, 
                                          ConsecutivoVendedor = LibConvert.ToInt(vRecord.Element("ConsecutivoVendedor")), 
                                          CodigoVendedor = vRecord.Element("CodigoVendedor").Value, 
                                          //NombreVendedor = vRecord.Element("NombreVendedor").Value, 
                                          SeccionAdvertencias = vRecord.Element("SeccionAdvertencias").Value, 
                                          RazonInactividad = vRecord.Element("RazonInactividad").Value, 
                                          Email = vRecord.Element("Email").Value, 
                                          ActivarAvisoAlEscoger = vRecord.Element("ActivarAvisoAlEscoger").Value, 
                                          TextoDelAviso = vRecord.Element("TextoDelAviso").Value, 
                                          SeccionDirDespacho = vRecord.Element("SeccionDirDespacho").Value, 
                                          SeccionReglasContables = vRecord.Element("SeccionReglasContables").Value, 
                                          CuentaContableCxC = vRecord.Element("CuentaContableCxC").Value, 
                                          DescripcionCuentaContableCxC = vRecord.Element("DescripcionCuentaContableCxC").Value, 
                                          CuentaContableIngresos = vRecord.Element("CuentaContableIngresos").Value, 
                                          DescripcionCuentaContableIngresos = vRecord.Element("DescripcionCuentaContableIngresos").Value, 
                                          CuentaContableAnticipo = vRecord.Element("CuentaContableAnticipo").Value, 
                                          DescripcionCuentaContableAnticipo = vRecord.Element("DescripcionCuentaContableAnticipo").Value, 
                                          InfoGalac = vRecord.Element("InfoGalac").Value, 
                                          SectorDeNegocio = vRecord.Element("SectorDeNegocio").Value, 
                                          CodigoLote = vRecord.Element("CodigoLote").Value, 
                                          NivelDePrecio = vRecord.Element("NivelDePrecio").Value, 
                                          Origen = vRecord.Element("Origen").Value, 
                                          DiaCumpleanos = LibConvert.ToInt(vRecord.Element("DiaCumpleanos")), 
                                          MesCumpleanos = LibConvert.ToInt(vRecord.Element("MesCumpleanos")), 
                                          CorrespondenciaXEnviar = vRecord.Element("CorrespondenciaXEnviar").Value, 
                                          EsExtranjero = vRecord.Element("EsExtranjero").Value, 
                                          ClienteDesdeFecha = vRecord.Element("ClienteDesdeFecha").Value, 
                                          AQueSeDedicaElCliente = vRecord.Element("AQueSeDedicaElCliente").Value, 
                                          TipoDocumentoIdentificacion = vRecord.Element("TipoDocumentoIdentificacion").Value, 
                                          TipoDeContribuyente = vRecord.Element("TipoDeContribuyente").Value, 
                                          CampoDefinible1 = vRecord.Element("CampoDefinible1").Value
                                      }).Distinct();
            XElement vInfoConexionCiudad = FindInfoCiudad(refData);
            var vListCiudad = (from vRecord in vInfoConexionCiudad.Descendants("GpResult")
                                      select new {
                                          NombreCiudad = vRecord.Element("NombreCiudad").Value, 
                                          fldOrigen = vRecord.Element("fldOrigen").Value
                                      }).Distinct();

            foreach (DireccionDeDespacho vItem in refData) {
            }
        }

        private XElement FindInfoCliente(IList<DireccionDeDespacho> valData) {
            XElement vXElement = new XElement("GpData");
            foreach(DireccionDeDespacho vItem in valData) {
                vXElement.Add(FilterDireccionDeDespachoByDistinctCliente(vItem).Descendants("GpResult"));
            }
            ILibPdn insCliente = new Galac.Saw.Brl.Cliente.clsClienteNav();
            XElement vXElementResult = insCliente.GetFk("DireccionDeDespacho", ParametersGetFKClienteForXmlSubSet(valData[0].ConsecutivoCompania, vXElement));
            return vXElementResult;
        }

        private XElement FilterDireccionDeDespachoByDistinctCliente(DireccionDeDespacho valMaster) {
            XElement vXElement = new XElement("GpData",
               new XElement("GpResult",
                    new XElement("CodigoCliente", valMaster.CodigoCliente)));
            return vXElement;
        }

        private StringBuilder ParametersGetFKClienteForXmlSubSet(int valConsecutivoCompania, XElement valXElement) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInXml("XmlData", valXElement);
            vResult = vParams.Get();
            return vResult;
        }

        private XElement FindInfoCiudad(IList<DireccionDeDespacho> valData) {
            XElement vXElement = new XElement("GpData");
            foreach(DireccionDeDespacho vItem in valData) {
                vXElement.Add(FilterDireccionDeDespachoByDistinctCiudad(vItem).Descendants("GpResult"));
            }
            ILibPdn insCiudad = new Galac.Comun.Brl.TablasGen.clsCiudadNav();
            XElement vXElementResult = insCiudad.GetFk("DireccionDeDespacho", ParametersGetFKCiudadForXmlSubSet(vXElement));
            return vXElementResult;
        }

        private XElement FilterDireccionDeDespachoByDistinctCiudad(DireccionDeDespacho valMaster) {
            XElement vXElement = new XElement("GpData",
                new XElement("GpResult",
                    new XElement("Ciudad", valMaster.Ciudad)));
            return vXElement;
        }

        private StringBuilder ParametersGetFKCiudadForXmlSubSet(XElement valXElement) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInXml("XmlData", valXElement);
            vResult = vParams.Get();
            return vResult;
        }
        #endregion //DireccionDeDespacho
        #endregion //Metodos Generados
        #region Codigo Ejemplo
        /* Codigo de Ejemplo

        bool IDireccionDeDespachoPdn.InsertDefaultRecord(int valConsecutivoCompania) {
            ILibDataComponent<IList<DireccionDeDespacho>, IList<DireccionDeDespacho>> instanciaDal = new clsDireccionDeDespachoDat();
            IList<DireccionDeDespacho> vLista = new List<DireccionDeDespacho>();
            DireccionDeDespacho vCurrentRecord = new Galac.Saw.Dal.ClienteDireccionDeDespacho();
            vCurrentRecord.ConsecutivoCompania = valConsecutivoCompania;
            vCurrentRecord.ConsecutivoCompania = 0;
            vCurrentRecord.CodigoCliente = "";
            vCurrentRecord.ConsecutivoDireccion = 0;
            vCurrentRecord.PersonaContacto = "";
            vCurrentRecord.Direccion = "";
            vCurrentRecord.Ciudad = "";
            vCurrentRecord.ZonaPostal = "";
            vLista.Add(vCurrentRecord);
            return instanciaDal.Insert(vLista).Success;
        }

        private List<DireccionDeDespacho> ParseToListEntity(XElement valXmlEntity) {
            List<DireccionDeDespacho> vResult = new List<DireccionDeDespacho>();
            var vEntity = from vRecord in valXmlEntity.Descendants("GpResult")
                          select vRecord;
            foreach (XElement vItem in vEntity) {
                DireccionDeDespacho vRecord = new DireccionDeDespacho();
                vRecord.Clear();
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoCompania"), null))) {
                    vRecord.ConsecutivoCompania = LibConvert.ToInt(vItem.Element("ConsecutivoCompania"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CodigoCliente"), null))) {
                    vRecord.CodigoCliente = vItem.Element("CodigoCliente").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoDireccion"), null))) {
                    vRecord.ConsecutivoDireccion = LibConvert.ToInt(vItem.Element("ConsecutivoDireccion"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("PersonaContacto"), null))) {
                    vRecord.PersonaContacto = vItem.Element("PersonaContacto").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Direccion"), null))) {
                    vRecord.Direccion = vItem.Element("Direccion").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Ciudad"), null))) {
                    vRecord.Ciudad = vItem.Element("Ciudad").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ZonaPostal"), null))) {
                    vRecord.ZonaPostal = vItem.Element("ZonaPostal").Value;
                }
                vResult.Add(vRecord);
            }
            return vResult;
        }
        */
        #endregion //Codigo Ejemplo


    } //End of class clsDireccionDeDespachoNav

} //End of namespace Galac.Saw.Brl.Cliente

