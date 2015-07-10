using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using com.cooshare.os;
using com.cooshare.os.sec;
using com.cooshare.os.dev;

/// <summary>
///serv 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class serv : System.Web.Services.WebService
{

    public serv()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="HCCU_ID">HCCU ID</param>
    /// <param name="HCCU_MAC_ID">MAC ID</param>
    /// <param name="EXECORDER_EP_ID_PAKG">EXEORDER接口参数序列</param>
    /// <param name="EVENT_ID_Collection">EVENT更新ID集</param>
    /// <param name="TRANS_PAKG">TRANS接口参数序列</param>
    /// <param name="REQLOGS_PAKG">REQLOGS接口参数序列</param>
    /// <param name="SK">安全码</param>
    /// <returns>
    /// return_sync_hccu_ep_info 单元
    /// 单组返回{返回值为 MAC,IP,PORT,HCCU_ACC_STATUS^EP_ID,EP_TYPEID,EP_USERDEFINED_ALIAS,EP_PRODUCTID,EP_MAC_ID|{Repeart}
    /// 返回值为 -1 说明方法执行异常 
    /// 返回值为 -2 说明参数不符合标准 
    /// 返回值为 -4 说明安全验证失败}
    /// 
    /// 
    /// return_sync_exec_order 单元
    /// 多组返回{返回值为{EP_ID},{PROP},{VALUE}|
    /// 返回值为 -1 说明没有待执行指令
    /// 返回值为 -2 说明参数不符合标准
    /// 返回值为 -4 说明安全验证失败}^
    /// 
    /// 
    /// return_sync_events 单元
    /// 单组返回
    /// {分隔符1：=======A308899ASFFFDSDFAALSDFSFCXXCVASDFASDFAAASDFFD=====
    /// 分隔符2：=======B5526421ASFFFDSDFAALSDFSFCXXCVASDFASDFAAJJID=====
    /// 返回内容格式：EP_ID{分隔符1}PROPERTYNAME{分隔符1}TRIGGERCONTENT{分隔符1}EVENTID{分隔符1}STATUS{分隔符2}
    /// 返回值为 -1 说明无任何EP事件
    /// 返回值为-2 说明参数不符合标准
    /// 返回值为-4 说明安全验证失败}
    /// 
    /// 
    /// return_sync_notifyeventupdate 单元
    /// 单组返回
    /// {1：说明通知成功
    /// -1：说明通知失败
    /// -2：说明参数不符合规范
    /// -4: 说明安全验证失败}
    /// 
    /// 
    /// return_sync_prop 单元
    /// 单组返回
    /// {返回内容格式：{EP_PROPERTY_ID},{EP_PROPERTY_NAME}|
    /// 返回值为 -1 说明无注册信息}
    /// 
    /// 
    /// return_sync_trans 单元
    /// 多组返回
    /// {返回值为1 说明添加成功 
    /// 返回值为-1 说明方法执行异常 
    /// 返回值为-2 说明参数不符合标准 
    /// 返回值为-4 说明安全验证失败},
    /// 
    /// 
    /// SYNC_REQLOGS 单元
    /// 多组返回
    /// {返回值为1  说明添加成功
    /// 返回值为-1 说明方法执行异常
    /// 返回值为-2 说明参数不符合标准},
    /// 
    /// </returns>
    [WebMethod]
    public string Sync(string HCCU_ID, string HCCU_MAC_ID, string EXECORDER_EP_ID_PAKG, string EVENT_ID_Collection, string TRANS_PAKG, string REQLOGS_PAKG, string SK)
    {

        string[,] p = new string[2, 6];
        p[0, 0] = "HCCU_ID";
        p[1, 0] = HCCU_ID;
        p[0, 1] = "HCCU_MAC_ID";
        p[1, 1] = HCCU_MAC_ID;
        p[0, 2] = "EXECORDER_EP_ID_PAKG";
        p[1, 2] = EXECORDER_EP_ID_PAKG;
        p[0, 3] = "EVENT_ID_Collection";
        p[1, 3] = EVENT_ID_Collection;
        p[0, 4] = "TRANS_PAKG";
        p[1, 4] = TRANS_PAKG;
        p[0, 5] = "REQLOGS_PAKG";
        p[1, 5] = REQLOGS_PAKG;

        if (!COS_SECURITY_TOOL.SECURITY_RequestDecrypt(p, SK)) return COS_SECURITY_TOOL.SECURITY_ContentEncrypt("-4");

        /*
         * SYNC_HCCU_EP_INFO
         * 
         * */
        #region output:return_sync_hccu_ep_info
        HCCU _HCCU = new HCCU();

        string para = "HCCU_ID=" + HCCU_ID + "&HCCU_MAC=" + HCCU_MAC_ID;
        string sk = COS_SECURITY_TOOL.SECURITY_RequestEncrypt(para);

        string return_sync_hccu_ep_info = _HCCU.HCCU_EP_Get_Info(HCCU_ID, HCCU_MAC_ID, sk);
        return_sync_hccu_ep_info = "<SYNC_HCCU_EP_INFO>" + return_sync_hccu_ep_info + "</SYNC_HCCU_EP_INFO>";
        #endregion

        /*
         * SYNC_EXECORDER
         * 
         * */
        #region output:return_sync_exec_order
        EXEC_ORDER _EXEC_ORDER = new EXEC_ORDER();
        string return_sync_exec_order = "";
        if (EXECORDER_EP_ID_PAKG != "")
        {
            string[] EXECORDER_EP_ID_PAKG_ARRAY = EXECORDER_EP_ID_PAKG.Split('|');
            for (int x = 0; x < EXECORDER_EP_ID_PAKG_ARRAY.Length; x++) {

                if (EXECORDER_EP_ID_PAKG_ARRAY[x] != "") {

                    string[] EXECORDER_EP_ID_PAKG_SINGLE_ARRAY = EXECORDER_EP_ID_PAKG_ARRAY[x].Split(',');
                    return_sync_exec_order += _EXEC_ORDER.EXEC_ORDER_Get(EXECORDER_EP_ID_PAKG_SINGLE_ARRAY[0], EXECORDER_EP_ID_PAKG_SINGLE_ARRAY[1]) + "^";
                }
            }

            return_sync_exec_order = "<SYNC_EXECORDER>" + return_sync_exec_order + "</SYNC_EXECORDER>";

        }
        else {

            return_sync_exec_order = "<SYNC_EXECORDER></SYNC_EXECORDER>";
        }

        #endregion


        /*
        * SYNC_EP_EVENTS
        * 
        * */
        #region output:return_sync_events
        EVENTS _EVENTS = new EVENTS();

        string para3 = "HCCU_ID=" + HCCU_ID;
        string sk3 = COS_SECURITY_TOOL.SECURITY_RequestEncrypt(para3);

        string return_sync_events = _EVENTS.EVENTS_Sync(HCCU_ID, sk3);
        return_sync_events = "<SYNC_EVENTS>" + return_sync_events + "</SYNC_EVENTS>";
        #endregion

        /*
        * UPLOAD: SYNC_NOTIFYEVENTUPDATE(1,-1,-2,-4)
        * 
        * 
        * */
        #region output:return_sync_notifyeventupdate
        string para35 = "EVENT_ID_Collection=" + EVENT_ID_Collection;
        string sk35 = COS_SECURITY_TOOL.SECURITY_RequestEncrypt(para35);

        string return_sync_notifyeventupdate = _EVENTS.NotifyEventUpdate(EVENT_ID_Collection, sk35);
        return_sync_notifyeventupdate = "<SYNC_NOTIFYEVENTUPDATE>" + return_sync_notifyeventupdate + "</SYNC_NOTIFYEVENTUPDATE>";
        #endregion

        /*
        * SYNC_PROP
        * 
        * */
        #region output:return_sync_prop
        PROP _PROP = new PROP();

        string para4 = "HCCU_ID=" + HCCU_ID;
        string sk4 = COS_SECURITY_TOOL.SECURITY_RequestEncrypt(para4);

        string return_sync_prop = _PROP.EP_PROPERTYFACT_Sync(HCCU_ID, sk4);
        return_sync_prop = "<SYNC_PROP>" + return_sync_prop + "</SYNC_PROP>";
        #endregion

        /*
         * UPLOAD: SYNC_TRANS(1,-1,-2,-4)
         * 
         * */
        #region output:return_sync_trans
        string return_sync_trans = "";
        TRANS _TRANS = new TRANS();

        if (TRANS_PAKG.Trim() != "")
        {
            string[] TRANS_PAKG_ARRAY = TRANS_PAKG.Split('|');
            for (int x = 0; x < TRANS_PAKG_ARRAY.Length; x++)
            {

                if (TRANS_PAKG_ARRAY[x] != "")
                {

                    string[] TRANS_PAKG_SINGLE_ARRAY = TRANS_PAKG_ARRAY[x].Split(',');
                    return_sync_trans += _TRANS.TRANS_Add(TRANS_PAKG_SINGLE_ARRAY[0], TRANS_PAKG_SINGLE_ARRAY[1], TRANS_PAKG_SINGLE_ARRAY[2], TRANS_PAKG_SINGLE_ARRAY[3], TRANS_PAKG_SINGLE_ARRAY[4]) + ",";
                }
            }
            return_sync_trans = "<SYNC_TRANS>" + return_sync_trans + "</SYNC_TRANS>";
        }
        else
        {

            return_sync_trans = "<SYNC_TRANS></SYNC_TRANS>";
        }
        #endregion


        /*
        * UPLOAD: SYNC_REQLOGS(1,-1,-2,-4)
        * 
        * */
        #region output:return_sync_requestlogs
        string return_sync_requestlogs = "";
        REQUESTLOGS _REQUESTLOGS = new REQUESTLOGS();

        if (REQLOGS_PAKG.Trim() != "")
        {
            string[] REQLOGS_PAKG_ARRAY = REQLOGS_PAKG.Split('|');
            for (int x = 0; x < REQLOGS_PAKG_ARRAY.Length; x++)
            {

                if (REQLOGS_PAKG_ARRAY[x] != "")
                {

                    string[] REQLOGS_PAKG_SINGLE_ARRAY = REQLOGS_PAKG_ARRAY[x].Split(',');
                    return_sync_requestlogs += _REQUESTLOGS.REQUESTLOGS_Add(REQLOGS_PAKG_SINGLE_ARRAY[0],
                        REQLOGS_PAKG_SINGLE_ARRAY[1],
                        REQLOGS_PAKG_SINGLE_ARRAY[2],
                        REQLOGS_PAKG_SINGLE_ARRAY[3],
                        REQLOGS_PAKG_SINGLE_ARRAY[4],
                        REQLOGS_PAKG_SINGLE_ARRAY[5],
                        REQLOGS_PAKG_SINGLE_ARRAY[6]) + ",";

                }
            }

            return_sync_requestlogs = "<SYNC_REQLOGS>" + return_sync_requestlogs + "</SYNC_REQLOGS>";

        }
        else
        {

            return_sync_requestlogs = "<SYNC_REQLOGS></SYNC_REQLOGS>";
        }
        #endregion


        return return_sync_hccu_ep_info + return_sync_exec_order + return_sync_events + return_sync_notifyeventupdate + return_sync_prop + return_sync_trans + return_sync_requestlogs;
    }

}