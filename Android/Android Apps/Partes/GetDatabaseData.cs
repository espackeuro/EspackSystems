using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using static Partes.Values;

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
        public int SPP;
        public int SPA;
        public int STD;
        public int SPS;
        public int SPC;
        public int SPE;
        public int SPX;
        public int SQC;
        public int SEV;
        public int MinGVDBA;
        public string Flags;
        public DateTime? BreakDate;
        public DateTime? LastDeliveryDate;
        public int LastDeliveryQty;
        public DateTime? MinDate;
        public int MinQTY;
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
                    _selectQuery = string.Format(@"select proveedor,Fase4,Descripcion,rd.Embalaje,rd.qty_emb,rd.Lugar_Descarga,r.Loc1,r.Loc2,r.SPP,r.SPA,r.STD,r.SPS,r.SQC,r.SPC,r.SPE,r.SPX,r.SEV,r.MinGVDBA,r.MinGVDBADate,r.flags,BrkDate from referencias r inner join Referencias_Destinos rd on rd.Partnumber=r.partnumber and rd.Servicio=r.Servicio where dbo.CheckFlag(rd.flags,'DEFAULT')=1 and r.Partnumber='{0}' and r.Servicio='{1}'", Partnumber, _Service);
                    _queryLastDel = string.Format("select top 1 Qty=SUM(Qty),Fecha_Salida=Min(CE.Fecha_Salida) from Det_Modulos DM inner join Cab_Modulos CM on CM.CM = DM.CM inner join Cab_Expediciones CE on CE.Expedicion = CM.Expedicion Where PartNumber = '{0}' and dm.Servicio = '{1}' group by CM.Expedicion order by CM.Expedicion desc", Partnumber, _Service);
                    break;
                case "IDC CRA":
                    _DB = "LOGISTICA";
                    _Service = "IDCCRA";
                    _selectQuery = string.Format(@"select proveedor,Fase4,Descripcion,rd.Embalaje,rd.qty_emb,rd.Lugar_Descarga,r.Loc1,r.Loc2,r.SPP,r.SPA,r.STD,r.SPS,r.SQC,r.SPC,r.SPE,r.SPX,r.SEV,r.MinGVDBA,r.MinGVDBADate,r.flags,BrkDate from referencias r inner join Referencias_Destinos rd on rd.Partnumber=r.partnumber and rd.Servicio=r.Servicio where dbo.CheckFlag(rd.flags,'DEFAULT')=1 and r.Partnumber='{0}' and r.Servicio='{1}'", Partnumber, _Service);
                    _queryLastDel = string.Format("select top 1 Qty=SUM(Qty),Fecha_Salida=Min(CE.Fecha_Salida) from Det_Modulos DM inner join Cab_Modulos CM on CM.CM = DM.CM inner join Cab_Expediciones CE on CE.Expedicion = CM.Expedicion Where PartNumber = '{0}' and dm.Servicio = '{1}' group by CM.Expedicion order by CM.Expedicion desc", Partnumber, _Service);
                    break;
                default:
                    _DB = "LOGISTICA_IDC";
                    _Service = "IDC";
                    _selectQuery = string.Format(@"select proveedor,Fase4,Descripcion,rd.Embalaje,rd.qty_emb,rd.Lugar_Descarga,r.Loc1,r.Loc2,r.SPP,r.SPA,r.STD,r.SPS,r.SQC,r.SPC,r.SPE,r.SPX,r.SEV,r.MinGVDBA,r.MinGVDBADate,r.flags,BrkDate from referencias r inner join Referencias_Destinos rd on rd.Partnumber=r.partnumber and rd.Servicio=r.Servicio where dbo.CheckFlag(rd.flags,'DEFAULT')=1 and r.Partnumber='{0}' and r.Servicio='{1}'", Partnumber, _Service);
                    _queryLastDel = string.Format("select top 1 Qty=SUM(Qty),Fecha_Salida=Min(CE.Fecha_Salida) from Det_Modulos DM inner join Cab_Modulos CM on CM.CM = DM.CM inner join Cab_Expediciones CE on CE.Expedicion = CM.Expedicion Where PartNumber = '{0}' and dm.Servicio = '{1}' group by CM.Expedicion order by CM.Expedicion desc", Partnumber, _Service);
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
            if (gDatos.State == ConnectionState.Open)
                gDatos.Close();
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
                            _result.SPP = Convert.ToInt32(_refDR["SPP"]);
                            _result.SPA = Convert.ToInt32(_refDR["SPA"]);
                            _result.STD = Convert.ToInt32(_refDR["STD"]);
                            _result.SPS = Convert.ToInt32(_refDR["SPS"]);
                            _result.SPC = Convert.ToInt32(_refDR["SPC"]);
                            _result.SPE = Convert.ToInt32(_refDR["SPE"]);
                            _result.SPX = Convert.ToInt32(_refDR["SPX"]);
                            _result.SQC = Convert.ToInt32(_refDR["SQC"]);
                            _result.SEV = Convert.ToInt32(_refDR["SEV"]);
                            _result.MinGVDBA =(int)_refDR["MinGVDBA"];
                            _result.Flags = _refDR["flags"].ToString();
                            _result.BreakDate = _refDR["BrkDate"] is DBNull ? null : (DateTime?)_refDR["BrkDate"];
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
            //last delivery and qty
            using (var query = new SqlCommand(_queryLastDel, gDatos))
            {
                try
                {
                    using (SqlDataReader _DelDR = query.ExecuteReader())
                    {
                        if (await _DelDR.ReadAsync())
                        {
                            _result.LastDeliveryDate = _DelDR["Fecha_Salida"] is DBNull? null : (DateTime?)_DelDR["Fecha_Salida"];
                            _result.LastDeliveryQty = (int)_DelDR["Qty"];
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;//control errores TBD
                }
            }
            if (_DB == "LOGISTICA_IDC") //lets get the last min warning alert for the part
            {
                var _queryMin = string.Format("Select Qty,xfec from ReferenciasMinWarnings where Partnumber='{0}' and Service='IDC' and DateOUT is null", Partnumber);
                using (var query = new SqlCommand(_queryMin, gDatos))
                {
                    try
                    {
                        //var _minDR = new SqlDataReader(null);
                        using (SqlDataReader _minDR = query.ExecuteReader())
                        {
                            if (await _minDR.ReadAsync())
                            {
                                _result.MinDate = (DateTime?)_minDR["xfec"];
                                _result.MinQTY = (int)_minDR["Qty"];
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;//control errores TBD
                    }
                }

            }
            gDatos.Close();
            gDatos = null;
            return _result;
        } 
    }
}