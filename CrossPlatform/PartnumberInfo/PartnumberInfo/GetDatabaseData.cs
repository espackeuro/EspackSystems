using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Data;
using System.Data.SqlClient;
using static PartnumberInfo.Values;

namespace PartnumberInfo
{
    public partial class MainPage : ContentPage
    {
        public async Task GetDatabaseData()
        {
            string _DB = "";
            string _Service = "";
            string _selectQuery = "";
            string _queryLastDel = "";
            if (txtUser.Text == "DDU VAL")
            {
                //check user
            }
            if (txtPwd.Text == "")
            {
                //check password
            }
            switch (pckDB.SelectedItem)
            {
                case "DDU VAL":
                    _DB = "PRODUCCION";
                    _queryLastDel = string.Format("select top 1 SUM(Qty),CE.Fecha_Salida from Det_Modulos DM inner join Cab_Modulos CM on CM.CM = DM.CM inner join Cab_Expediciones CE on CE.Expedicion = CM.Expedicion Where PartNumber = '{0}' and dm.Servicio = '{1}' group by PartNumber, dm.Servicio, CE.Fecha_Salida order by Fecha_Salida desc", txtPartnumber.Text, _Service);
                    break;
                case "IDC VAL":
                    _DB = "LOGISTICA_IDC";
                    _Service = "IDC";
                    _selectQuery = string.Format(@"select proveedor,Fase4,Descripcion,rd.Embalaje,rd.qty_emb,rd.Lugar_Descarga,r.Loc1,r.Loc2,r.SPP,r.SPA,r.STD,r.SQC,r.SPC,r.SPçe,r.MinGVDBA,r.MinGVDBADate,r.flags,BrkDate from referencias r inner join Referencias_Destinos rd on rd.Partnumber=r.partnumber and rd.Servicio=r.Servicio where dbo.CheckFlag(rd.flags,'DEFAULT')=1 and r.Partnumber='{0}' and r.Servicio='{1}'", txtPartnumber.Text, _Service);
                    _queryLastDel = string.Format("select top 1 Qty=SUM(Qty),CE.Fecha_Salida from Det_Modulos DM inner join Cab_Modulos CM on CM.CM = DM.CM inner join Cab_Expediciones CE on CE.Expedicion = CM.Expedicion Where PartNumber = '{0}' and dm.Servicio = '{1}' group by PartNumber, dm.Servicio, CE.Fecha_Salida order by Fecha_Salida desc", txtPartnumber.Text, _Service);
                    break;
                case "IDC CRA":
                    _DB = "LOGISTICA";
                    _Service = "IDCCRA";
                    _selectQuery = string.Format(@"select proveedor,Fase4,Descripcion,rd.Embalaje,rd.qty_emb,rd.Lugar_Descarga,r.Loc1,r.Loc2,r.SPP,r.SPA,r.STD,r.SQC,r.SPC,r.SPE,r.MinGVDBA,r.MinGVDBADate,r.flags,BrkDate from referencias r inner join Referencias_Destinos rd on rd.Partnumber=r.partnumber and rd.Servicio=r.Servicio where dbo.CheckFlag(rd.flags,'DEFAULT')=1 and r.Partnumber='{0}' and r.Servicio='{1}'", txtPartnumber.Text, _Service);
                    _queryLastDel = string.Format("select top 1 Qty=SUM(Qty),CE.Fecha_Salida from Det_Modulos DM inner join Cab_Modulos CM on CM.CM = DM.CM inner join Cab_Expediciones CE on CE.Expedicion = CM.Expedicion Where PartNumber = '{0}' and dm.Servicio = '{1}' group by PartNumber, dm.Servicio, CE.Fecha_Salida order by Fecha_Salida desc", txtPartnumber.Text, _Service);
                    break;
                default:
                    _DB = "LOGISTICA_IDC";
                    _Service = "IDC";
                    _selectQuery = string.Format(@"select proveedor,Fase4,Descripcion,rd.Embalaje,rd.qty_emb,rd.Lugar_Descarga,r.Loc1,r.Loc2,r.SPP,r.SPA,r.STD,r.SQC,r.SPC,r.SPE,r.MinGVDBA,r.MinGVDBADate,r.flags,BrkDate from referencias r inner join Referencias_Destinos rd on rd.Partnumber=r.partnumber and rd.Servicio=r.Servicio where dbo.CheckFlag(rd.flags,'DEFAULT')=1 and r.Partnumber='{0}' and r.Servicio='{1}'", txtPartnumber.Text, _Service);
                    _queryLastDel = string.Format("select top 1 Qty=SUM(Qty),CE.Fecha_Salida from Det_Modulos DM inner join Cab_Modulos CM on CM.CM = DM.CM inner join Cab_Expediciones CE on CE.Expedicion = CM.Expedicion Where PartNumber = '{0}' and dm.Servicio = '{1}' group by PartNumber, dm.Servicio, CE.Fecha_Salida order by Fecha_Salida desc", txtPartnumber.Text, _Service);
                    break;
            }
            if (gDatos == null)
            {
                string _connectionString = string.Format("Server={0};Initial Catalog={1};User Id={2};Password={3};MultipleActiveResultSets=True;Connection Lifetime=3;Max Pool Size=3", "10.200.10.138", "Sistemas", "procesos", "*seso69*");
                string _User = "";
                string _Pwd = "";
                using (var conn = new SqlConnection(_connectionString))
                {
                    try
                    {
                        await conn.OpenAsync();
                    }
                    catch (Exception ex)
                    {
                        throw;//bad connection
                    }
                    using (var sp = new SqlCommand("pLogonUser", conn) { CommandType = CommandType.StoredProcedure })
                    {
                        SqlCommandBuilder.DeriveParameters(sp);
                        sp.Parameters["@msg"].Value = "";
                        sp.Parameters["@User"].Value = txtUser.Text;
                        sp.Parameters["@Password"].Value = txtPwd.Text;
                        sp.Parameters["@Origin"].Value = "PARTNUMBER INFO";
                        try
                        {
                            await sp.ExecuteNonQueryAsync();
                        }
                        catch (Exception ex)
                        {
                            throw ex;//control errores TBD
                        }
                        if (sp.Parameters["@msg"].Value.ToString() != "OK")
                        {
                            // control errores TBD
                        }
                        _User = sp.Parameters["@User"].Value.ToString();
                        _Pwd = sp.Parameters["@Password"].Value.ToString();
                    }
                }
                _connectionString = string.Format("Server={0};Initial Catalog={1};User Id={2};Password={3};MultipleActiveResultSets=True;Connection Lifetime=3;Max Pool Size=3", "10.200.10.138", _DB, _User, _Pwd);
                gDatos = new SqlConnection(_connectionString);
                try
                {
                    await gDatos.OpenAsync();
                }
                catch (Exception ex)
                {
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
                            lblSupplier.Text = _refDR["Proveedor"].ToString();
                            lbl4fase.Text = _refDR["Fase4"].ToString();
                            lblDesc.Text = _refDR["Descripcion"].ToString();
                            lblPack.Text = _refDR["Embalaje"].ToString();
                            lblQty.Text = _refDR["qty_emb"].ToString();
                            lblDock.Text = _refDR["Lugar_Descarga"].ToString();
                            lblLoc1.Text = _refDR["Loc1"].ToString();
                            lblLoc2.Text = _refDR["Loc2"].ToString();
                            lblSPP.Text = _refDR["SPP"].ToString();
                            lblSPA.Text = _refDR["SPA"].ToString();
                            lblSTD.Text = _refDR["STD"].ToString();
                            lblSQC.Text = _refDR["SQC"].ToString();
                            lblSPC.Text = _refDR["SPC"].ToString();
                            lblSPE.Text = _refDR["SPE"].ToString();
                            lblMinGVDBA.Text = _refDR["MinGVDBA"].ToString();
                            lblFlags.Text = _refDR["flags"].ToString();
                            lblBreakDate.Text = _refDR["BrkDate"].ToString();
                        }
                        else
                        {
                            throw new Exception("Partnumber doesn't exist.");
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
                var _queryMin = string.Format("Select Qty,xfec from ReferenciasMinWarnings where Partnumber='{0}' and Service='IDC' and DateOUT is null", txtPartnumber.Text);
                using (var query = new SqlCommand(_queryMin, gDatos))
                {
                    try
                    {
                        //var _minDR = new SqlDataReader(null);
                        using (SqlDataReader _minDR = query.ExecuteReader())
                        {
                            if (await _minDR.ReadAsync())
                            {
                                lblMinDate.Text = _minDR["xfec"].ToString();
                                lblMinQty.Text = _minDR["Qty"].ToString();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;//control errores TBD
                    }
                }

            }

            using (var query = new SqlCommand(_queryLastDel, gDatos))
            {
                try
                {
                    using (SqlDataReader _DelDR = query.ExecuteReader())
                    {
                        if (await _DelDR.ReadAsync())
                        {
                            lblFechaSalida.Text = _DelDR["Fecha_Salida"].ToString();
                            lblQtyExp.Text = _DelDR["Qty"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;//control errores TBD
                }
            }
            gDatos.Close();
        }
    }
}
