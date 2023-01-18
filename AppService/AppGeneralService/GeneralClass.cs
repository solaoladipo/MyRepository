using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppService.AppGeneralService
{
    public class GeneralClass
    {
        
        public static string connection()
        {
            string Constr = "Server=NGSL-VI-0531\\SANLAMSERVER;Database=ArcelormittaWebApiDB;User Id=sa;password=sqluser0712$;Trusted_Connection=False;MultipleActiveResultSets=true;";
            return Constr;
        }

        public static bool ifExistinDatabase(string refno, string fieldname, string tablename)
        {
            bool _ans = false;

            if (refno == null || fieldname == null || tablename == null)
            {
                return _ans;
            }


            try
            {
                string sqltext = "select * from " + tablename + " where " + fieldname + "='" + refno + "'";
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = sqltext;
                cmd.Connection = new SqlConnection(connection());
                cmd.Connection.Open();
                SqlDataReader rd;
                rd = cmd.ExecuteReader();

                if (rd.Read())
                {
                    _ans = true;
                }
                else
                {
                    _ans = false;
                }
                rd.Close();

            }


            catch (Exception ex)
            {

                throw ex;
            }

            return _ans;
        }

        public static string GetRefnumber(Guid refnoid, string fieldname, string tablename, bool update)
        {
            string referencnum = null;
            try
            {
                string Prefix = null;

                int recount = 0;
                bool blresult = false;

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "select * from tblRefnoTag where Refnoid = '" + Convert.ToString(refnoid) + "'";
                cmd.Connection = new SqlConnection(connection());
                cmd.Connection.Open();
                SqlDataReader Reader;
                Reader = cmd.ExecuteReader();
                if (Reader.Read())
                {
                    if (Reader["Tag"] != null)
                    {
                        Prefix = Reader["Tag"].ToString();
                    }

                    if (Reader["Serialno"] != null)
                    {
                        recount = Convert.ToInt32(Reader["Serialno"]);
                    }

                }
                Reader.Close();
                cmd.Connection.Close();

                if (Prefix != null)
                {

                    while (blresult == false)
                    {
                        recount += 1;

                        switch ((Convert.ToString(recount)).Length)
                        {
                            case 1:
                                referencnum = Prefix + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year + "/" + "00" + recount;
                                break;
                            case 2:
                                referencnum = Prefix + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year + "/" + "0" + recount;
                                break;
                            case 3:
                                referencnum = Prefix + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year + "/" + "0" + recount;
                                break;
                            case 4:
                                referencnum = Prefix + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year + "/" + "0" + recount;
                                break;
                            case 5:
                                referencnum = Prefix + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year + "/" + "0" + recount;
                                break;
                            case 6:
                                referencnum = Prefix + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year + "/" + "0" + recount;
                                break;
                            case 7:
                                referencnum = Prefix + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year + "/" + "0" + recount;
                                break;
                            default:
                                break;
                        }

                        if (tablename != null && fieldname != null && referencnum != null)
                        {
                            if (ifExistinDatabase(referencnum, fieldname, tablename) == true)
                            {
                                blresult = false;
                            }
                            else
                            {
                                blresult = true;
                            }
                        }
                        else
                        {
                            blresult = true;
                        }
                    }

                }

                if (update == true)
                {
                    SqlCommand cmd1 = new SqlCommand();
                    cmd1.CommandType = System.Data.CommandType.Text;
                    cmd1.CommandText = "update tblRefnoTag set serialno =" + recount + "where refnoid='" + refnoid + "'";
                    cmd1.Connection = new SqlConnection(connection());
                    cmd1.Connection.Open();
                    cmd1.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

            return referencnum;
        }


    }
}
