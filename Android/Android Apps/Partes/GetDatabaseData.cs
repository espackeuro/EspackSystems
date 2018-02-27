using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using static Partes.Values;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Threading;
using Android.Content.PM;
using System.Net;
using System.IO;

namespace Partes
{
    public struct DataRecord
    {
        public string Supplier;
        public string Fase4;
        public string Description;
        public string Pack;
        public int QtyPack;
        public string Dock;
        public string Loc1;
        public string Loc2;
    }
    public static class DataBaseAccess
    {
        public static async Task<DataRecord> GetData(string Partnumber, string System)
        {
            string _DB = "";
            string _Service = "";
            string _selectQuery = "";
            string _queryLastDel = "";
            DataRecord _result = new DataRecord();
            switch (System)
            {
                case "DDU VAL":
                    _DB = "BRASIL";
                    _queryLastDel = string.Format("select top 1 SUM(Qty),CE.Fecha_Salida from Det_Modulos DM inner join Cab_Modulos CM on CM.CM = DM.CM inner join Cab_Expediciones CE on CE.Expedicion = CM.Expedicion Where PartNumber = '{0}' and dm.Servicio = '{1}' group by PartNumber, dm.Servicio, CE.Fecha_Salida order by Fecha_Salida desc", Partnumber, _Service);
                    break;
                case "IDC VAL":
                    _DB = "LOGISTICA_IDC";
                    _Service = "IDC";
                    _selectQuery = string.Format(@"select proveedor,Fase4,Descripcion,rd.Embalaje,rd.qty_emb,rd.Lugar_Descarga,r.Loc1,r.Loc2,r.SPP,r.SPA,r.STD,r.SQC,r.SPC,r.SPE,r.MinGVDBA,r.MinGVDBADate,r.flags,BrkDate from referencias r inner join Referencias_Destinos rd on rd.Partnumber=r.partnumber and rd.Servicio=r.Servicio where dbo.CheckFlag(rd.flags,'DEFAULT')=1 and r.Partnumber='{0}' and r.Servicio='{1}'", Partnumber, _Service);
                    _queryLastDel = string.Format("select top 1 Qty=SUM(Qty),CE.Fecha_Salida from Det_Modulos DM inner join Cab_Modulos CM on CM.CM = DM.CM inner join Cab_Expediciones CE on CE.Expedicion = CM.Expedicion Where PartNumber = '{0}' and dm.Servicio = '{1}' group by PartNumber, dm.Servicio, CE.Fecha_Salida order by Fecha_Salida desc", Partnumber, _Service);
                    break;
                case "IDC CRA":
                    _DB = "LOGISTICA";
                    _Service = "IDCCRA";
                    _selectQuery = string.Format(@"select proveedor,Fase4,Descripcion,rd.Embalaje,rd.qty_emb,rd.Lugar_Descarga,r.Loc1,r.Loc2,r.SPP,r.SPA,r.STD,r.SQC,r.SPC,r.SPE,r.MinGVDBA,r.MinGVDBADate,r.flags,BrkDate from referencias r inner join Referencias_Destinos rd on rd.Partnumber=r.partnumber and rd.Servicio=r.Servicio where dbo.CheckFlag(rd.flags,'DEFAULT')=1 and r.Partnumber='{0}' and r.Servicio='{1}'", Partnumber, _Service);
                    _queryLastDel = string.Format("select top 1 Qty=SUM(Qty),CE.Fecha_Salida from Det_Modulos DM inner join Cab_Modulos CM on CM.CM = DM.CM inner join Cab_Expediciones CE on CE.Expedicion = CM.Expedicion Where PartNumber = '{0}' and dm.Servicio = '{1}' group by PartNumber, dm.Servicio, CE.Fecha_Salida order by Fecha_Salida desc", Partnumber, _Service);
                    break;
                default:
                    _DB = "LOGISTICA_IDC";
                    _Service = "IDC";
                    _selectQuery = string.Format(@"select proveedor,Fase4,Descripcion,rd.Embalaje,rd.qty_emb,rd.Lugar_Descarga,r.Loc1,r.Loc2,r.SPP,r.SPA,r.STD,r.SQC,r.SPC,r.SPE,r.MinGVDBA,r.MinGVDBADate,r.flags,BrkDate from referencias r inner join Referencias_Destinos rd on rd.Partnumber=r.partnumber and rd.Servicio=r.Servicio where dbo.CheckFlag(rd.flags,'DEFAULT')=1 and r.Partnumber='{0}' and r.Servicio='{1}'", Partnumber, _Service);
                    _queryLastDel = string.Format("select top 1 Qty=SUM(Qty),CE.Fecha_Salida from Det_Modulos DM inner join Cab_Modulos CM on CM.CM = DM.CM inner join Cab_Expediciones CE on CE.Expedicion = CM.Expedicion Where PartNumber = '{0}' and dm.Servicio = '{1}' group by PartNumber, dm.Servicio, CE.Fecha_Salida order by Fecha_Salida desc", Partnumber, _Service);
                    break;
            }
            if (gDatos == null)
            {
                string _connectionString = string.Format("Server={0};Initial Catalog={1};User Id={2};Password={3};MultipleActiveResultSets=True;Connection Lifetime=3;Max Pool Size=3", "10.200.10.138", _DB, User, Pwd);
                gDatos = new SqlConnection(_connectionString);
                try
                {
                    await gDatos.OpenAsync();
                }
                catch (Exception ex)
                {
                    gDatos = null;
                    throw ex;//control errores TBD
                }
                gDatos.Close();
            }
            await gDatos.OpenAsync();
            using (var query = new SqlCommand(_selectQuery, gDatos))
            {

                try
                {
                    using (SqlDataReader _refDR = query.ExecuteReader())
                    {
                        if (await _refDR.ReadAsync())
                        { //get the first one
                            _result.Supplier = _refDR["Proveedor"].ToString();
                            _result.Fase4 = _refDR["Fase4"].ToString();
                            _result.Description = _refDR["Descripcion"].ToString();
                            _result.Pack = _refDR["Embalaje"].ToString();
                            _result.QtyPack = Convert.ToInt32(_refDR["qty_emb"]);
                            _result.Dock = _refDR["Lugar_Descarga"].ToString();
                            _result.Loc1 = _refDR["Loc1"].ToString();
                            _result.Loc2 = _refDR["Loc2"].ToString();
                            /*
                            lblSPP.Text = _refDR["SPP"].ToString();
                            lblSPA.Text = _refDR["SPA"].ToString();
                            lblSTD.Text = _refDR["STD"].ToString();
                            lblSQC.Text = _refDR["SQC"].ToString();
                            lblSPC.Text = _refDR["SPC"].ToString();
                            lblSPE.Text = _refDR["SPE"].ToString();
                            lblMinGVDBA.Text = _refDR["MinGVDBA"].ToString();
                            lblFlags.Text = _refDR["flags"].ToString();
                            lblBreakDate.Text = _refDR["BrkDate"].ToString();*/
                        }
                        else
                        {
                            throw new Exception("Partnumber doesn't exist.");
                        }
                    }


                }
                catch (Exception ex)
                {
                    gDatos.Close();
                    gDatos = null;
                    throw ex;//control errores TBD
                }


            }
            gDatos.Close();
            gDatos = null;
            return _result;
        } 
    }
}