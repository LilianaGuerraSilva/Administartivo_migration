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
using Galac.Saw.Ccl.Tablas;
using LibGalac.Aos.Catching;

namespace Galac.Saw.Brl.Tablas {
    public partial class clsMaquinaFiscalNav: LibBaseNav<IList<MaquinaFiscal>, IList<MaquinaFiscal>>, IMaquinaFiscalPdn {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsMaquinaFiscalNav() {
        }
        #endregion //Constructores
        #region Metodos Generados
        private string DbSchema {
            get { return "Saw"; }
        }
        protected override ILibDataComponentWithSearch<IList<MaquinaFiscal>, IList<MaquinaFiscal>> GetDataInstance() {
            return new Galac.Saw.Dal.Tablas.clsMaquinaFiscalDat();
        }
        #region Miembros de ILibPdn

        bool ILibPdn.CanBeChoosen(string valCallingModule, eAccionSR valAction, string valExtendedAction, XmlDocument valXmlRow) {
            bool vResult = false;
            //ILibDataFKSearch instanciaDal = new Galac.Saw.Dal.Tablas.clsMaquinaFiscalDat();
            switch (valCallingModule) {
                default:
                    vResult = true;
                    break;
            }
            return vResult;
        }

        bool ILibPdn.GetDataForList(string valCallingModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            ILibDataFKSearch instanciaDal = new Galac.Saw.Dal.Tablas.clsMaquinaFiscalDat();
            return instanciaDal.ConnectFk(ref refXmlDocument, eProcessMessageType.SpName, "Saw.Gp_MaquinaFiscalSCH", valXmlParamsExpression);
        }

        System.Xml.Linq.XElement ILibPdn.GetFk(string valCallingModule, StringBuilder valParameters) {
            ILibDataComponent<IList<MaquinaFiscal>, IList<MaquinaFiscal>> instanciaDal = new Galac.Saw.Dal.Tablas.clsMaquinaFiscalDat();
            return instanciaDal.QueryInfo(eProcessMessageType.SpName, "Saw.Gp_MaquinaFiscalGetFk", valParameters);
        }
        #endregion //Miembros de ILibPdn

        protected override bool RetrieveListInfo(string valModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            switch (valModule) {
                case "Máquina Fiscal":
                    vResult = ((ILibPdn)this).GetDataForList(valModule, ref refXmlDocument, valXmlParamsExpression);
                    break;
                default: throw new NotImplementedException();
            }
            return vResult;
        }

        protected override LibResponse InsertRecord(IList<MaquinaFiscal> refRecord) {
            if (LaDescripcionExiste(refRecord)) {
                throw new GalacValidationException("El valor asignado al campo Descripción ya existe");
            }else if (ElNumeroRegistroExiste(refRecord)){
                throw new GalacValidationException("El valor asignado al campo Número de Registro ya existe");
            } else {
                return base.InsertRecord(refRecord);
            }
        }

        protected override LibResponse UpdateRecord(IList<MaquinaFiscal> refRecord) {
                if (LaDescripcionExiste(refRecord)) {
                    throw new GalacValidationException("El valor asignado al campo Descripción ya existe");
                } else if (ElNumeroRegistroExiste(refRecord)) {
                    throw new GalacValidationException("El valor asignado al campo Número de Registro ya existe");
                } else {
                    return base.UpdateRecord(refRecord);
                }
        }
        


        private bool LaDescripcionExiste(IList<MaquinaFiscal> refRecord) {
            bool vResult = false;
            StringBuilder Consulta = new StringBuilder();            
            Consulta.AppendLine(" SELECT COUNT(descripcion) AS Cantidad FROM " + DbSchema + ".MaquinaFiscal");
            Consulta.AppendLine(" WHERE ConsecutivoCompania = @ConsecutivoCompania");
            Consulta.AppendLine(" AND Descripcion = @Descripcion");
            Consulta.AppendLine(" AND ConsecutivoMaquinaFiscal <> @ConsecutivoMaquinaFiscal");
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", refRecord[0].ConsecutivoCompania);
            vParams.AddInString("ConsecutivoMaquinaFiscal", refRecord[0].ConsecutivoMaquinaFiscal,35);
            vParams.AddInString("Descripcion", refRecord[0].Descripcion, 20);
            RegisterClient();
            _Db.QueryInfo(eProcessMessageType.Query, Consulta.ToString(), vParams.Get());
            XElement vData = _Db.QueryInfo(eProcessMessageType.Query, Consulta.ToString(), vParams.Get());
            vResult = LibConvert.ToInt(LibXml.GetPropertyString(vData, "Cantidad")) > 0;
            return vResult;
        }

        private bool ElNumeroRegistroExiste(IList<MaquinaFiscal> refRecord) {
            bool vResult = false;
            StringBuilder Consulta = new StringBuilder();
            Consulta.AppendLine("SET DATEFORMAT dmy");
            Consulta.AppendLine(" SELECT COUNT(NumeroRegistro) AS Cantidad FROM " + DbSchema + ".MaquinaFiscal");
            Consulta.AppendLine(" WHERE ConsecutivoCompania = @ConsecutivoCompania");
            Consulta.AppendLine(" AND NumeroRegistro = @NumeroRegistro");
            Consulta.AppendLine(" AND ConsecutivoMaquinaFiscal <> @ConsecutivoMaquinaFiscal");
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", refRecord[0].ConsecutivoCompania);
            vParams.AddInString("ConsecutivoMaquinaFiscal", refRecord[0].ConsecutivoMaquinaFiscal, (35));
            vParams.AddInString("NumeroRegistro", refRecord[0].NumeroRegistro, (20));
            RegisterClient();
            _Db.QueryInfo(eProcessMessageType.Query, Consulta.ToString(), vParams.Get());
            XElement vData = _Db.QueryInfo(eProcessMessageType.Query, Consulta.ToString(), vParams.Get());
            vResult = LibConvert.ToInt(LibXml.GetPropertyString(vData, "Cantidad")) > 0;
            return vResult;
        }

        void IMaquinaFiscalPdn.GenerarRegistroDeMaquinaFiscal(int valConsecutivoCompania, string valConsecutivoMaquinaFiscal, string valserialMaquinaFiscal,string valDescripcion,int valLongitudNumeroFiscal, string valNombreOperador) {
            IMaquinaFiscalPdn insMaquinaFiscal = new Saw.Brl.Tablas.clsMaquinaFiscalNav();
            IList<MaquinaFiscal> lMaquinaFiscal = new List<MaquinaFiscal>();
            ILibDataComponent<IList<MaquinaFiscal>,IList<MaquinaFiscal>> insMaquinaFiscalDat = new Dal.Tablas.clsMaquinaFiscalDat();
            if(!LibString.IsNullOrEmpty(valConsecutivoMaquinaFiscal)) {
                LibGpParams vParams = new LibGpParams();
                vParams.AddInInteger("ConsecutivoCompania",valConsecutivoCompania);
                vParams.AddInString("ConsecutivoMaquinaFiscal",valConsecutivoMaquinaFiscal,9);
                lMaquinaFiscal = insMaquinaFiscal.GetDataListMaquinaFiscal(vParams.Get());
                lMaquinaFiscal[0].FechaUltimaModificacion = LibDate.Today();
                lMaquinaFiscal[0].NumeroRegistro = valserialMaquinaFiscal;
                lMaquinaFiscal[0].NombreOperador = valNombreOperador;
                lMaquinaFiscal[0].Descripcion = valDescripcion;
                lMaquinaFiscal[0].LongitudNumeroFiscal = valLongitudNumeroFiscal;
                insMaquinaFiscalDat.Update(lMaquinaFiscal);
            } else {
                lMaquinaFiscal.Add(new MaquinaFiscal {
                    ConsecutivoCompania = valConsecutivoCompania,
                    ConsecutivoMaquinaFiscal = GetNextConsecutivoMaquinaFiscal(valConsecutivoCompania),
                    NumeroRegistro = valserialMaquinaFiscal,
                    Descripcion = valDescripcion,
                    LongitudNumeroFiscal = valLongitudNumeroFiscal,
                    Status = "0",
                    FechaUltimaModificacion = LibDate.Today(),
                    NombreOperador = valNombreOperador,                   
                });
                insMaquinaFiscalDat.Insert(lMaquinaFiscal);
            }
        }

        IList<MaquinaFiscal> IMaquinaFiscalPdn.GetDataListMaquinaFiscal(StringBuilder valParameters) {        
            return ((ILibDataComponent<IList<MaquinaFiscal>,IList<MaquinaFiscal>>)new Saw.Dal.Tablas.clsMaquinaFiscalDat()).GetData(eProcessMessageType.SpName,"MaquinaFiscalGET",valParameters);
         }

        private string GetNextConsecutivoMaquinaFiscal(int valConsecutivoCompania) {
            string vResult = "";
            StringBuilder Sql = new StringBuilder();
            LibGpParams vParam = new LibGpParams();
            vParam.AddInInteger("ConsecutivoCompania",valConsecutivoCompania);
            Sql.AppendLine(" SELECT TOP(1)  REPLICATE('0',9-LEN(CONVERT(int,RIGHT(ConsecutivoMaquinaFiscal,4))+1)) + CONVERT(VARCHAR(4),CONVERT(int,RIGHT(ConsecutivoMaquinaFiscal,4))+1) AS ProximoConsecutivo ");
            Sql.AppendLine(" FROM SAW.MaquinaFiscal ");
            Sql.AppendLine(" WHERE ConsecutivoCompania = @ConsecutivoCompania ");
            Sql.AppendLine(" ORDER BY ConsecutivoMaquinaFiscal DESC ");
            XElement xmlResult = LibBusiness.ExecuteSelect(Sql.ToString(),vParam.Get(),"",0);
            if(xmlResult != null && xmlResult.HasElements) {
                vResult = LibXml.GetPropertyString(xmlResult,"ProximoConsecutivo");
            } else {
               vResult = LibText.FillWithCharToLeft(vResult,"0",8) + "1";
            }
            return vResult;
        }       
        #endregion
    } //End of class clsMaquinaFiscalNav

} //End of namespace Galac.Saw.Brl.Tablas

