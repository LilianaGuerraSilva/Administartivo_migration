using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base;
using System.Xml.Linq;
using LibGalac.Aos.UI.Mvvm.Messaging;
using LibGalac.Aos.Brl.Backup;

namespace Galac.Saw {
    class clsRestoreSettings: ILibRestoreSettings {
        public LibBackupMfcController Controller { get; set; }

        public string ControllerCode { get; set; }

        public string ControllerCodeFieldName { get; set; }

        public string ControllerName { get; set; }

        public string ControllerNameFieldName { get; set; }

        public LibCloneableList<LibBackupMfcController> OthersControllers { get; set; }

        public string ProgramInitials { get; set; }

        public XElement RestoreTree { get; set; }

        public List<LibRestoreValidation> Validators { get; set; }

        public ILibRestoreSettings LastRestoredController { get; set; }

        public string MinVersionDBAllowedForRestore {
            get { return "5.67"; }
        }

        public XElement CreateRestoreTree() {
            return new XElement("GpData",
               new XElement("Lib",
                   new XElement("GUser", new XAttribute("ShortMsgName", "Usuario"), new XAttribute("Scheme", "Lib")),
                   new XElement("PrinterSettings", new XAttribute("ShortMsgName", "PrinterSettings"), new XAttribute("Scheme", "Lib"))
                   //,
                   //new XElement("InstalledVersion", new XAttribute("ShortMsgName", "InstalledVersion"), new XAttribute("Scheme", "dbo"))
                ),
                new XElement("Tablas",
                    new XElement("TipoDeComprobante", new XAttribute("ShortMsgName", "Tipo De Comprobante"), new XAttribute("Scheme", "Contab")),
                    new XElement("MonedaLocal", new XAttribute("ShortMsgName", "Moneda Local"), new XAttribute("Scheme", "Comun")),
                    new XElement("Moneda", new XAttribute("ShortMsgName", "Moneda"), new XAttribute("Scheme", "Comun")),
                    new XElement("Pais", new XAttribute("ShortMsgName", "País"), new XAttribute("Scheme", "Comun"))
                ),
                new XElement("Parametro",
                    new XElement("ParametrosGen", new XAttribute("ShortMsgName", "Parámetros Generales"), new XAttribute("Scheme", "Contab"))
                ),                    
                new XElement("Compania", new XAttribute("ShortMsgName", "Compañia"), new XAttribute("Scheme", "Emp"),
                    new XElement("CompaniaDetalleSeguridad", new XAttribute("ShortMsgName", "Seguridad x Compañía"), new XAttribute("Scheme", "Emp")),
                    new XElement("ParametrosActivoFijo", new XAttribute("ShortMsgName", "Parametros Activo Fijo"), new XAttribute("Scheme", "Contab")),
                    new XElement("Lote", new XAttribute("ShortMsgName", "Lote"), new XAttribute("Scheme", "Comun")),
                    new XElement("ParametrosConciliacion", new XAttribute("ShortMsgName", "Parametros Conciliación"), new XAttribute("Scheme", "Contab")),
                    new XElement("CompaniaDetDefCtaFlujoEfectivo", new XAttribute("ShortMsgName", "Compania Definicion Cuentas Flujo de Efectivo"), new XAttribute("Scheme", "Contab")),
                    new XElement("Producto", new XAttribute("ShortMsgName", "Producto"), new XAttribute("Scheme", "Comun")),
                    new XElement("CriterioDeDistribucion", new XAttribute("ShortMsgName", "Criterios De Distribucion"), new XAttribute("Scheme", "Comun")),
                    new XElement("CriterioDeDistribucionDetalle", new XAttribute("ShortMsgName", "Criterio De Distribucion Detalle"), new XAttribute("Scheme", "Comun")),
                    new XElement("ElementoDelCosto", new XAttribute("ShortMsgName", "Elemento Del Costo"), new XAttribute("Scheme", "Comun")),
                    new XElement("Periodo", new XAttribute("ShortMsgName", "Periodo"), new XAttribute("Scheme", "Contab"),
                        new XElement("PeriodoDetalleDefinicionCuenta", new XAttribute("ShortMsgName", "Periodo Renglon"), new XAttribute("Scheme", "Contab")),                        
                        new XElement("Auxiliar", new XAttribute("ShortMsgName", "Auxiliar"), new XAttribute("Scheme", "Contab")),
                        new XElement("CentroDeCostos", new XAttribute("ShortMsgName", "Centro De Costos"), new XAttribute("Scheme", "Contab")),
                        new XElement("GrupoDeActivos", new XAttribute("ShortMsgName", "Grupo De Activos"), new XAttribute("Scheme", "Contab")),
                        new XElement("Cuenta", new XAttribute("ShortMsgName", "Cuenta"), new XAttribute("Scheme", "Contab")),
                        new XElement("PeriodoDetDefGrupoDeInventario", new XAttribute("ShortMsgName", "Grupo De Inventario"), new XAttribute("Scheme", "Contab")),
                        new XElement("ActivoFijo", new XAttribute("ShortMsgName", "Activo Fijo"), new XAttribute("Scheme", "Contab")),
                        new XElement("Comprobante", new XAttribute("ShortMsgName", "Comprobante"), new XAttribute("Scheme", "Contab")),
                        new XElement("ComprobanteDetalle", new XAttribute("ShortMsgName", "Comprobante Detalle"), new XAttribute("Scheme", "Contab")),
                        new XElement("ComprobanteSaldoInicial", new XAttribute("ShortMsgName", "Comprobante Saldo Inicial"), new XAttribute("Scheme", "Contab")),
                         new XElement("ComprobanteSaldoInicialDetalle", new XAttribute("ShortMsgName", "Comprobante Saldo Inicial Detalle"), new XAttribute("Scheme", "Contab")),
                        new XElement("ConciliacionContable", new XAttribute("ShortMsgName", "Conciliacion Contable"), new XAttribute("Scheme", "Contab")),
                        new XElement("ConciliacionContableDetalle", new XAttribute("ShortMsgName", "Conciliacion Contable Detalle"), new XAttribute("Scheme", "Contab"))                        
                    )
                    //,
                    //new XElement("EsquemaDeBalance", new XAttribute("ShortMsgName", "Esquema De Balance"), new XAttribute("Scheme", "dbo"),
                    //    new XElement("EsquemaDeBalanceMov", new XAttribute("ShortMsgName", "Esquema De Balance Mov"), new XAttribute("Scheme", "dbo"))
                    //)
                )
            );
        }       

        public void Initialize() {
            LibGalac.Aos.Dal.LibDbo vDbo = new LibGalac.Aos.Dal.LibDbo();
            ProgramInitials = LibGalac.Aos.DefGen.LibDefGen.ProgramInfo.ProgramInitials;
            ControllerCodeFieldName = "Codigo";
            ControllerNameFieldName = "Nombre";

            Controller = new LibBackupMfcController();
            if (vDbo.Exists("Emp.Compania", LibGalac.Aos.Dal.eDboType.Tabla)) {
                Controller.TableName = "Emp.Compania";
            } else {
                Controller.TableName = "Compania";
            }
            Controller.FieldName = "ConsecutivoCompania";
            Controller.CurrentValue = 0;

            RestoreTree = CreateRestoreTree();
        }

        public void InitializeValidators() {
            throw new NotImplementedException();
        }

        public void RefreshController(System.Data.DataRow valRow) {
            ControllerName = "";
            ControllerCode = "";
            Controller.CurrentValue = 0;
            if (valRow != null) {
                if (!LibConvert.IsNull(valRow["Nombre"])) {
                    ControllerName = valRow["Nombre"].ToString();
                }
                if (!LibConvert.IsNull(valRow["Codigo"])) {
                    ControllerCode = valRow["Codigo"].ToString();
                }
                if (!LibConvert.IsNull(valRow["ConsecutivoCompania"])) {
                    Controller.CurrentValue = LibConvert.ToInt(valRow["ConsecutivoCompania"].ToString()); ;
                }
            }
        }     

        object ICloneable.Clone() {
            ILibRestoreSettings vResult = this.MemberwiseClone() as ILibRestoreSettings;
            vResult.OthersControllers = this.OthersControllers.Clone();
            vResult.Controller = this.Controller.Clone() as LibBackupMfcController;
            return vResult;
        }


        public void ExecuteProcessAfterRestoreData() {
            throw new NotImplementedException();
        }

        public void GenerateController(System.Data.DataRow valRow, bool valHasToGenerateCode) {
            throw new NotImplementedException();
        }

        public void GenerateOtherController(string valTableName, System.Data.DataRow valDataRow, bool valFirstRecord) {
            throw new NotImplementedException();
        }

        public bool IsRestoreDataAllowed() {
            throw new NotImplementedException();
        }

        public List<string> SqlDeleteBackupCompany(object valNameMFCForDelete) {
            throw new NotImplementedException();
        }

        public string SqlUpdateMFC(System.Data.DataRow valRow, ref string refNameMFCForDelete) {
            throw new NotImplementedException();
        }

        public bool IsValidBackupCountry() {
            throw new NotImplementedException();
        }
    }
}
