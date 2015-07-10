using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using com.cooshare.os;
using com.cooshare.os.sec;
using com.cooshare.os.dev;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;


/// <summary>
///cosh 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class cosh : System.Web.Services.WebService
{

    public cosh()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }


    [WebMethod]
    public string SendCommand(string commandtype, string commandbody, string SK)
    {

        string[,] p = new string[2, 2];
        p[0, 0] = "commandtype";
        p[1, 0] = commandtype;
        p[0, 1] = "commandbody";
        p[1, 1] = commandbody;

        if (!COS_SECURITY_TOOL.SECURITY_RequestDecrypt(p, SK)) return COS_SECURITY_TOOL.SECURITY_ContentEncrypt("-4");

        commandtype = COS_SECURITY_TOOL.SECURITY_ContentDecrypt(commandtype);
        commandbody = COS_SECURITY_TOOL.SECURITY_ContentDecrypt(commandbody);

        if (commandtype.ToLower().Trim() == "i")
        {

            string[] body = commandbody.Split('|');

            string hccu_id = body[1];
            string mac = body[2];
            string ip = body[3];
            string prop = body[4];

            EP ep = new EP();

            string skraw = "EP_TypeId=" + COS_SECURITY_TOOL.SECURITY_ContentEncrypt("1")
                            + "&EP_UserDefined_Alias=" + COS_SECURITY_TOOL.SECURITY_ContentEncrypt("Unknown")
                            + "&EP_ProductId=" + COS_SECURITY_TOOL.SECURITY_ContentEncrypt("1")
                            + "&HCCU_Id=" + COS_SECURITY_TOOL.SECURITY_ContentEncrypt(hccu_id)
                            + "&EP_MAC_Id=" + COS_SECURITY_TOOL.SECURITY_ContentEncrypt(mac)
                            + "&PROPFORMAT=" + COS_SECURITY_TOOL.SECURITY_ContentEncrypt(prop)
                            + "&IP=" + COS_SECURITY_TOOL.SECURITY_ContentEncrypt(ip);

            string sk = COS_SECURITY_TOOL.SECURITY_RequestEncrypt(skraw);

            return ep.EP_Add_20140320(COS_SECURITY_TOOL.SECURITY_ContentEncrypt("1"),
                COS_SECURITY_TOOL.SECURITY_ContentEncrypt("Unknown"),
                COS_SECURITY_TOOL.SECURITY_ContentEncrypt("1"),
                COS_SECURITY_TOOL.SECURITY_ContentEncrypt(hccu_id),
                COS_SECURITY_TOOL.SECURITY_ContentEncrypt(mac),
                COS_SECURITY_TOOL.SECURITY_ContentEncrypt(prop),
                COS_SECURITY_TOOL.SECURITY_ContentEncrypt(ip),
                sk);

        }

        else if (commandtype.ToLower().Trim() == "d")
        {

            string[] body = commandbody.Split('|');

            if (body.Length < 5) return "";

            string hccu_id = body[1];
            string mac = body[2];
            string d_length = body[3];

            if (mac.Length != 16) return "";

            COS_WEBSERVICE_EP c = new COS_WEBSERVICE_EP();
            string epid = Get_EPID_From_MAC(mac);


            ParseDataCommand(commandbody, epid, d_length, hccu_id);


            string skraw = "EP_ID=" + COS_SECURITY_TOOL.SECURITY_ContentEncrypt(epid);
            string sk = COS_SECURITY_TOOL.SECURITY_RequestEncrypt(skraw);

            EXEC_ORDER e = new EXEC_ORDER();
            return e.EXEC_ORDER_Get_WithIP(COS_SECURITY_TOOL.SECURITY_ContentEncrypt(epid), sk);


        }
        else
        {

            return "";
        }

    }

    [WebMethod]
    public string UpdateHCCUStatus(string mac,string SK) {

        string[,] p = new string[2, 1];
        p[0, 0] = "mac";
        p[1, 0] = mac;
        
        if (!COS_SECURITY_TOOL.SECURITY_RequestDecrypt(p, SK)) return COS_SECURITY_TOOL.SECURITY_ContentEncrypt("-4");

        mac = COS_SECURITY_TOOL.SECURITY_ContentDecrypt(mac);

        //UPDATE_STM_HCCU_STATUS
        string query = "UPDATE HCCU_MAC_FACT SET HCCU_LASTUPDATETIME = GETDATE() WHERE HCCU_MAC = '"+mac+"'";
        DataHelper.Query_ExecuteNonQuery(DataHelper.DataBaseFact.CENTRAL, query, null);
        return COS_SECURITY_TOOL.SECURITY_ContentEncrypt("1");
    }

    public string Get_EPID_From_MAC(string mac)
    {

        using (SqlConnection myconn = new SqlConnection(ConfigurationManager.AppSettings["DB_COS_CENTRAL_connstr"].ToString()))
        {

            SqlCommand mysc = new SqlCommand();
            mysc.CommandText = "select ep_id from endpoint_fact where ep_mac_id='" + mac + "'";
            mysc.Connection = myconn;
            myconn.Open();

            try
            {

                return mysc.ExecuteScalar().ToString();

            }
            catch
            {

                return "";
            }

        }




    }

    public void ParseDataCommand(string commandbody, string epid, string d_length, string hccu_id)
    {

        string[] body = commandbody.Split('|');

        string commandprop = "";
        for (int x = 4; x < 4 + int.Parse(d_length); x++) { commandprop += body[x] + "|"; }

        UplinkData(hccu_id, epid, commandprop);

    }

    public void UplinkData(string hccu_id, string _EP_ID, string _PROPERTY_COLLECTION)
    {


        string skraw = "HCCU_ID=" + COS_SECURITY_TOOL.SECURITY_ContentEncrypt(hccu_id)
                + "&EP_ID=" + COS_SECURITY_TOOL.SECURITY_ContentEncrypt(_EP_ID)
                + "&PROPERTY_COLLECTION=" + COS_SECURITY_TOOL.SECURITY_ContentEncrypt(_PROPERTY_COLLECTION);

        string sk = COS_SECURITY_TOOL.SECURITY_RequestEncrypt(skraw);

        PROP p = new PROP();

        p.EP_PROPERTYDATA_Uplink(COS_SECURITY_TOOL.SECURITY_ContentEncrypt(hccu_id),
            COS_SECURITY_TOOL.SECURITY_ContentEncrypt(_EP_ID),
            COS_SECURITY_TOOL.SECURITY_ContentEncrypt(_PROPERTY_COLLECTION), sk);


    }

}
