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

        public static string PV_ID()
        {
            string PVID = Convert.ToString("56D9DAE6-52AE-4DE2-B185-433D4A4AC2C7");
            return PVID;
        }

        public static string SUPPLIER_ID()
        {
            string SUPPLIERID = Convert.ToString("411BDA0C-9F29-4B9A-AAF2-4758105158A8");
            return SUPPLIERID;
        }

        public static string STAFF_ID()
        {
            string STAFFID = Convert.ToString("D5287042-A8C3-46D8-A64B-6E9F628A3B19");
            return STAFFID;
        }

        public static string CUSTOMER_ID()
        {
            string CUSTOMERID = Convert.ToString("66E62FD7-17FE-4D92-98E1-84167042618E");
            return CUSTOMERID;
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
                cmd.CommandText = "select * from RefTag where RefnoID = '" + Convert.ToString(refnoid) + "'";
                cmd.Connection = new SqlConnection(connection());
                cmd.Connection.Open();
                SqlDataReader Reader;
                Reader = cmd.ExecuteReader();
                if (Reader.Read())
                {
                    if (Reader["TagName"] != null)
                    {
                        Prefix = Reader["TagName"].ToString();
                    }

                    if (Reader["serialNo"] != null)
                    {
                        recount = Convert.ToInt32(Reader["serialNo"]);
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
                    cmd1.CommandText = "update RefTag set serialNo =" + recount + "where RefnoID='" + refnoid + "'";
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
