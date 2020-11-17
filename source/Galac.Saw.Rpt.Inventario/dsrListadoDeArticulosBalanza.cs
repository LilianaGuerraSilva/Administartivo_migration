using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DataDynamics.ActiveReports;
using DataDynamics.ActiveReports.Document;
using System.Data;
using LibGalac.Aos.ARRpt;
using LibGalac.Aos.Base;
using LibGalac.Aos.DefGen;
namespace Galac.Saw.Rpt.Inventario {
    /// <summary>
    /// ESTE ARCHIVO NO ES PARA SER AGREGADO DIRECTO AL PROYECTO
    /// </summary>

    public partial class dsrListadoDeArticulosBalanza:DataDynamics.ActiveReports.ActiveReport {
        #region Variables
        private bool _UseExternalRpx;
        private static string _RpxFileName;
        private bool _FiltrarPorLineaDeProducto;
        #endregion //Variables
        #region Constructores
        //LEE PROGRAMADOR: Deja que ActiveReports cree el designer correctamente, 
        //LEE PROGRAMADOR: este archivo es solo para pasar lineas AFTER, no para tomarlo en su totalidad 
        public dsrListadoDeArticulosBalanza()
            : this(false,string.Empty) {
        }

        public dsrListadoDeArticulosBalanza(bool initUseExternalRpx,string initRpxFileName) {
            InitializeComponent();
            _UseExternalRpx = initUseExternalRpx;
            if(_UseExternalRpx) {
                _RpxFileName = initRpxFileName;
            }
        }
        #endregion //Constructores
        #region Metodos Generados

        public string ReportTitle() {
            return "Listado de Artículos con Balanza";
        }

        public bool ConfigReport(DataTable valDataSource,Dictionary<string,string> valParameters) {
            if(_UseExternalRpx) {
                string vRpxPath = LibWorkPaths.PathOfRpxFile(_RpxFileName,ReportTitle(),false,LibDefGen.ProgramInfo.ProgramInitials);//acá se indicaría si se busca en ULS, por defecto buscaría en app.path... Tip: Una función con otro nombre.
                if(!LibString.IsNullOrEmpty(vRpxPath,true)) {
                    LibReport.LoadLayout(this,vRpxPath);
                }
            }
            _FiltrarPorLineaDeProducto = LibConvert.SNToBool(valParameters["FiltrarPorLineaDeProducto"]);
            if(LibReport.ConfigDataSource(this,valDataSource)) {
                LibReport.ConfigFieldStr(this,"txtNombreCompania",valParameters["NombreCompania"],string.Empty);
                LibReport.ConfigLabel(this,"lblTituloInforme",ReportTitle());
                LibReport.ConfigLabel(this,"lblFechaInicialYFinal","");
                LibReport.ConfigLabel(this,"lblFechaYHoraDeEmision",LibReport.PromptEmittedOnDateAtHour);
                LibReport.ConfigHeader(this,"txtNombreCompania","lblFechaYHoraDeEmision","lblTituloInforme","txtNroDePagina","lblFechaInicialYFinal",LibGalac.Aos.ARRpt.LibGraphPrnSettings.PrintPageNumber,LibGalac.Aos.ARRpt.LibGraphPrnSettings.PrintEmitDate);
                LibReport.ConfigFieldStr(this,"txtCodigo",string.Empty,"Codigo");
                LibReport.ConfigFieldStr(this,"txtDescripcion",string.Empty,"Descripcion");
                LibReport.ConfigFieldDec(this,"txtExistencia",string.Empty,"Existencia");
                if(!_FiltrarPorLineaDeProducto) {
                    LibReport.ConfigFieldStr(this,"txtLineaDeProductoHide",string.Empty,"LineaDeProducto");
                    LibReport.ConfigLabel(this,"lblLineaDeProductoHide","Línea de Producto");
                    LibReport.ConfigFieldStr(this,"txtLineaDeProducto",string.Empty,"");
                    LibReport.ConfigLabel(this,"lblLineaDeProducto","");
                    LibReport.ConfigGroupHeader(this,"GHSecLineaDeProducto","LineaDeProducto",GroupKeepTogether.FirstDetail,RepeatStyle.OnPage,true,NewPage.None);
                } else {
                    LibReport.ConfigFieldStr(this,"txtLineaDeProducto",string.Empty,"LineaDeProducto");
                    LibReport.ConfigLabel(this,"lblLineaDeProducto","Línea de Producto");
                    LibReport.ConfigFieldStr(this,"txtLineaDeProductoHide",string.Empty,"");
                    LibReport.ConfigLabel(this,"lblLineaDeProductoHide","");
                }
                LibGraphPrnMargins.SetGeneralMargins(this,DataDynamics.ActiveReports.Document.PageOrientation.Portrait);
                return true;
            }
            return false;
        }
    }
        #endregion //Metodos Generados
} //End of class dsrListadoDeArticulosBalanza}} //End of namespace Galac.Saw.Rpt.Inventario

