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

namespace Galac.Saw.Brl.Tablas {
    public partial class clsImpuestoBancarioNav: LibBaseNav<IList<ImpuestoBancario>, IList<ImpuestoBancario>>, ILibPdn {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsImpuestoBancarioNav() {
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibDataComponentWithSearch<IList<ImpuestoBancario>, IList<ImpuestoBancario>> GetDataInstance() {
            return new Galac.Saw.Dal.Tablas.clsImpuestoBancarioDat();
        }
        #region Miembros de ILibPdn

        bool ILibPdn.CanBeChoosen(string valCallingModule, eAccionSR valAction, string valExtendedAction, XmlDocument valXmlRow) {
            bool vResult = false;
            ILibDataFKSearch instanciaDal = new Galac.Saw.Dal.Tablas.clsImpuestoBancarioDat();
            switch (valCallingModule) {
                default:
                    vResult = true;
                    break;
            }
            return vResult;
        }

        public string BuscaAlicutoaImpTranscBancarias(DateTime valFecha,bool valAlicDebito) {
            string vResult = "";            
            LibGpParams vParams = new LibGpParams();
            StringBuilder SQL = new StringBuilder();
            try {
                SQL.AppendLine("SELECT * FROM Adm.ImpTransacBancarias ");
                SQL.AppendLine(" WHERE FechaDeInicioDeVigencia = ");
                SQL.AppendLine(" (SELECT MAX(FechaDeInicioDeVigencia) " );
                SQL.AppendLine(" FROM Adm.ImpTransacBancarias ");
                SQL.AppendLine(" WHERE FechaDeInicioDeVigencia <= @FechaDeInicioDeVigencia) ");                                
                vParams.AddInDateTime("FechaDeInicioDeVigencia",valFecha);
                XElement vReq = LibBusiness.ExecuteSelect(SQL.ToString(),vParams.Get(),"",-1);
                if(vReq.HasElements) {
                    if(valAlicDebito) {
                        vResult = LibConvert.NumToString(LibConvert.ToDec (vReq.Element("GpResult").Element("AlicuotaAlDebito")),2);                                                
                    } else {
                        vResult = LibConvert.NumToString(LibConvert.ToDec(vReq.Element("GpResult").Element("AlicuotaAlCredito")),2);                        
                    }
                }                
                return vResult;
            } catch(Exception) {
                throw;
            }          
        }

        bool ILibPdn.GetDataForList(string valCallingModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            ILibDataFKSearch instanciaDal = new Galac.Saw.Dal.Tablas.clsImpuestoBancarioDat();
            return instanciaDal.ConnectFk(ref refXmlDocument, eProcessMessageType.SpName,"Adm.Gp_ImpTransacBancariasSCH", valXmlParamsExpression);
        }

        System.Xml.Linq.XElement ILibPdn.GetFk(string valCallingModule, StringBuilder valParameters) {
            ILibDataComponent<IList<ImpuestoBancario>, IList<ImpuestoBancario>> instanciaDal = new Galac.Saw.Dal.Tablas.clsImpuestoBancarioDat();
            return instanciaDal.QueryInfo(eProcessMessageType.SpName,"Adm.Gp_ImpTransacBancariasGetFk", valParameters);
        }
        #endregion //Miembros de ILibPdn

        protected override bool RetrieveListInfo(string valModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            switch (valModule) {
                case "Alícuota ITF":
                    vResult = ((ILibPdn)this).GetDataForList(valModule, ref refXmlDocument, valXmlParamsExpression);
                    break;
                default: throw new NotImplementedException();
            }
            return vResult;
        }

        protected override void FillWithForeignInfo(ref IList<ImpuestoBancario> refData) {
        }

        internal bool ValidateAll(XElement valRecord,eAccionSR eAccionSR,StringBuilder refErrorMessage) {
            bool vResult = true;
            string vFechaInicioVigencia = LibXml.GetPropertyString(valRecord,"FechaDeInicioDeVigencia");
            string AlicuotaAlDebito = LibXml.GetPropertyString(valRecord,"AlicuotaAlDebito");
            string AlicuotaAlCredito = LibXml.GetPropertyString(valRecord,"AlicuotaAlCredito");
			string AlicuotaC1Al4 = LibXml.GetPropertyString(valRecord,"AlicuotaC1Al4");
			string AlicuotaC5 = LibXml.GetPropertyString(valRecord,"AlicuotaC5");
			string AlicuotaC6 = LibXml.GetPropertyString(valRecord,"AlicuotaC6");

            decimal vDecValue = 0;
            DateTime vDateTimeValue=LibDate.EmptyDate();           
            vResult = DateTime.TryParse(vFechaInicioVigencia,out vDateTimeValue);
            vResult &= decimal.TryParse(AlicuotaAlDebito,out vDecValue);
            vResult &= decimal.TryParse(AlicuotaAlCredito,out vDecValue);
			vResult &= decimal.TryParse(AlicuotaC1Al4,out vDecValue);
			vResult &= decimal.TryParse(AlicuotaC5,out vDecValue);
			vResult &= decimal.TryParse(AlicuotaC6,out vDecValue);
            return vResult;
        }

        #endregion //Metodos Generados
        #region Codigo Ejemplo
        /* Codigo de Ejemplo

        bool IImpuestoBancarioPdn.InsertDefaultRecord() {
            ILibDataComponent<IList<ImpuestoBancario>, IList<ImpuestoBancario>> instanciaDal = new clsImpuestoBancarioDat();
            IList<ImpuestoBancario> vLista = new List<ImpuestoBancario>();
            ImpuestoBancario vCurrentRecord = new Galac.Adm.Dal.BancoImpuestoBancario();
            vCurrentRecord.FechaDeInicioDeVigencia = LibDate.Today();
            vCurrentRecord.AlicuotaAlDebito = 0;
            vCurrentRecord.AlicuotaAlCredito = 0;
            vCurrentRecord.AlicuotaC1Al4 = 0;
            vCurrentRecord.AlicuotaC5 = 0;
            vCurrentRecord.AlicuotaC6 = 0;
            vLista.Add(vCurrentRecord);
            return instanciaDal.Insert(vLista).Success;
        }

        private List<ImpuestoBancario> ParseToListEntity(XElement valXmlEntity) {
            List<ImpuestoBancario> vResult = new List<ImpuestoBancario>();
            var vEntity = from vRecord in valXmlEntity.Descendants("GpResult")
                          select vRecord;
            foreach (XElement vItem in vEntity) {
                ImpuestoBancario vRecord = new ImpuestoBancario();
                vRecord.Clear();
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("FechaDeInicioDeVigencia"), null))) {
                    vRecord.FechaDeInicioDeVigencia = LibConvert.ToDate(vItem.Element("FechaDeInicioDeVigencia"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("AlicuotaAlDebito"), null))) {
                    vRecord.AlicuotaAlDebito = LibConvert.ToDec(vItem.Element("AlicuotaAlDebito"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("AlicuotaAlCredito"), null))) {
                    vRecord.AlicuotaAlCredito = LibConvert.ToDec(vItem.Element("AlicuotaAlCredito"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("AlicuotaC1Al4"), null))) {
                    vRecord.AlicuotaC1Al4 = LibConvert.ToDec(vItem.Element("AlicuotaC1Al4"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("AlicuotaC5"), null))) {
                    vRecord.AlicuotaC5 = LibConvert.ToDec(vItem.Element("AlicuotaC5"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("AlicuotaC6"), null))) {
                    vRecord.AlicuotaC6 = LibConvert.ToDec(vItem.Element("AlicuotaC6"));
                }
                vResult.Add(vRecord);
            }
            return vResult;
        }
        */
        #endregion //Codigo Ejemplo
    } //End of class clsImpuestoBancarioNav

} //End of namespace Galac.Saw.Brl.Tablas

