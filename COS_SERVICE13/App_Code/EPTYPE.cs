using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using com.cooshare.os;
using com.cooshare.os.sec;
using System.Collections;

/// <summary>
///EPTYPE 的摘要说明
/// </summary>
[WebService(Namespace = "http://com.cooshare.os")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class EPTYPE : System.Web.Services.WebService {

    public EPTYPE () {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string Service_ReadMe()
    {
        return "COS V1.0 WebService For com.cooshare.os.COS_WEBSERVICE_EPTYPE";
    }

    /// <summary>
    /// 增加EP类型
    /// </summary>
    /// <param name="EP_Type_name">EP类型名称</param>
    /// <param name="EP_Type_Description">EP类型描述</param>
    /// <param name="HCCU_ID">所述HCCU的HCCU_ID</param>
    /// <param name="SK">安全码</param>
    /// <returns>
    /// 返回值为新增的 EP_TYPE_ID 
    /// 返回值为0 说明在当前HCCU_ID下，该EP_TYPE_NAME已经被添加过 
    /// 返回值为-1 说明方法执行异常 
    /// 返回值为-2 说明参数不符合标准 
    /// 返回值为-4 说明安全验证失败
    /// </returns>
    [WebMethod]
    public string EPTYPE_Add(string EP_Type_name, string EP_Type_Description, string HCCU_ID, string SK)
    {
        string[,] p = new string[2, 3];
        p[0, 0] = "EP_Type_name";
        p[1, 0] = EP_Type_name;
        p[0, 1] = "EP_Type_Description";
        p[1, 1] = EP_Type_Description;
        p[0, 2] = "HCCU_ID";
        p[1, 2] = HCCU_ID;

        if (!COS_SECURITY_TOOL.SECURITY_RequestDecrypt(p, SK)) return  COS_SECURITY_TOOL.SECURITY_ContentEncrypt("-4");

        EP_Type_name = COS_SECURITY_TOOL.SECURITY_ContentDecrypt(EP_Type_name);
        EP_Type_Description = COS_SECURITY_TOOL.SECURITY_ContentDecrypt(EP_Type_Description);
        HCCU_ID = COS_SECURITY_TOOL.SECURITY_ContentDecrypt(HCCU_ID);

        COS_WEBSERVICE_EPTYPE cos_w_ep = new COS_WEBSERVICE_EPTYPE();
        return  COS_SECURITY_TOOL.SECURITY_ContentEncrypt(cos_w_ep.EPTYPE_Add(EP_Type_name, EP_Type_Description, HCCU_ID).ToString());
    }

    /// <summary>
    /// 获取EP的类型列表
    /// </summary>
    /// <param name="HCCU_ID">所属的HCCU_ID</param>
    /// <param name="SK">安全码</param>
    /// <returns>
    /// 返回String类型 [String元素格式: {EP_TYPE_ID},{EP_TYPE_NAME},{EP_TYPE_DESCRIPTION}] 
    /// 返回String类型 String=0 说明该HCCU_ID下无EP信息 
    /// 返回String类型 [String元素格式：{-1}] 说明方法执行异常 
    /// 返回String类型 [String元素格式：{-2}] 说明参数不符合标准 
    /// 返回String类型 [String元素格式：{-4}] 说明安全验证失败
    /// </returns>
    [WebMethod]
    public string EPTYPE_GetList(string HCCU_ID,string SK)
    {

        string[,] p = new string[2, 1];
        p[0, 0] = "HCCU_ID";
        p[1, 0] = HCCU_ID;

        if (!COS_SECURITY_TOOL.SECURITY_RequestDecrypt(p, SK)) return  COS_SECURITY_TOOL.SECURITY_ContentEncrypt("-4");

        HCCU_ID = COS_SECURITY_TOOL.SECURITY_ContentDecrypt(HCCU_ID);

        COS_WEBSERVICE_EPTYPE cos_w_ep = new COS_WEBSERVICE_EPTYPE();
        ArrayList a = new ArrayList();
        a = cos_w_ep.EPTYPE_GetList(HCCU_ID);

        if (a.Count == 0)
        {

            return  COS_SECURITY_TOOL.SECURITY_ContentEncrypt("0");
        }
        else {

            if (a[0].ToString().Trim() == "-1") {

                return  COS_SECURITY_TOOL.SECURITY_ContentEncrypt("-1");
            }
            else if (a[0].ToString().Trim() == "-2")
            {

                return  COS_SECURITY_TOOL.SECURITY_ContentEncrypt("-2");
            }
            else {

                string ret = "";
                for (int j = 0; j < a.Count; j++) {

                    ret += a[j].ToString().Trim() + "|";
                }
                ret = ret.Substring(0, ret.Length - 1);
                return  COS_SECURITY_TOOL.SECURITY_ContentEncrypt(ret);
            }
        }
    }
    
}
