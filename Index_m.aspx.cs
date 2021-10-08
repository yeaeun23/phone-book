using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Text;
using System.Web.UI;

public partial class Index_m : Page
{
    DataTable dt;
    string param_kind;
    int param_page;
    string param_search;

    int ROWS_PER_PAGE = Convert.ToInt32(ReadValue("PAGINATION", "ROWS_PER_PAGE_M"));
    int PAGE_PER_PAGE = Convert.ToInt32(ReadValue("PAGINATION", "PAGE_PER_PAGE_M"));

    [DllImport("kernel32.dll")]
    public static extern uint GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, uint nSize, string lpFileName);

    public static string ReadValue(string section, string key)
    {
        StringBuilder tmp = new StringBuilder(255);

        try
        {
            GetPrivateProfileString(section, key, string.Empty, tmp, 255, @"D:\예은\Documents\Visual Studio 2015\Projects\NewsContact\NewsContact.ini");
            //GetPrivateProfileString(section, key, string.Empty, tmp, 255, @"H:\NewsContact\NewsContact.ini");
            return tmp.ToString();
        }
        catch (Exception)
        {
            return "";
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // 리디렉션
        //string strRefer = Request.ServerVariables["HTTP_REFERER"];

        //if (strRefer == null)
        //{
        //    if (Session["empno"] == null)
        //        Page.RegisterClientScriptBlock("done", @"<script>alert('잘못된 방식으로 접근 중입니다! (1)');location.href='http://sis.seoul.co.kr/';</script>");
        //}
        //else if (strRefer.IndexOf("http://sis.seoul.co.kr/") == -1 && strRefer.IndexOf("http://appsvr3.seoul.co.kr/edisplay-major-news-web/") == -1)
        //{
        //    // 조건 1) ".../SSO/go_edisplay_major_news.php"로 하면 Chrome 레퍼에서 못 가져옴 (IE에서는 가능)
        //    // 조건 2) 파일 전송 시 새로고침(Postback)되기 때문에 꼭 있어야 함             
        //    Page.RegisterClientScriptBlock("done", @"<script>alert('잘못된 방식으로 접근 중입니다! (2)');location.href='http://sis.seoul.co.kr/';</script>");
        //}
        //else if (Session["empno"] == null)
        //{
        //    Session["empno"] = Request["userid"];
        //}

        if (Request["kind"] == null || Request["kind"] == "")
            param_kind = "전체";
        else
            param_kind = Request["kind"];

        if (Request["page"] == null || Request["page"] == "")
            param_page = 1;
        else
            param_page = Convert.ToInt32(Request["page"]);

        if (Request["search"] == null || Request["search"] == "")
        {
            param_search = "";
        }
        else
        {
            param_search = Request["search"].Replace(" ", "").Replace("-", "").ToUpper();
            searchTB.Value = param_search;
        }

        //SetPage();

        if (!IsPostBack)
        {
            SetList();
            SetPage();
            SetKind();
        }
    }

    private void SetList()
    {
        string sql = "";
        int selected_kindcode = Util.GetKindCode(param_kind);

        sql += "declare @RowsPerPage int = " + ROWS_PER_PAGE + ", @PageNum int = " + param_page + " ";
        sql += @"select n_personid as id,
v_name as name,
v_name_en as name_en,
v_tel1 as tel1,
v_tel2 as tel2,
v_email1 as email1,
v_email2 as email2,
(select b.v_kind from t_newskind b where a.n_kindcode = b.n_kindcode) as kind,
t_etc as etc,
(select top(1) c.v_updateuser from t_newsperson_history c where a.n_personid = c.n_personid order by c.n_seq asc) as updateuser_first,
(select top(1) c.d_updatetime from t_newsperson_history c where a.n_personid = c.n_personid order by c.n_seq asc) as updatetime_first,
(select top(1) c.v_updateuser from t_newsperson_history c where a.n_personid = c.n_personid order by c.n_seq desc) as updateuser_last,
(select top(1) c.d_updatetime from t_newsperson_history c where a.n_personid = c.n_personid order by c.n_seq desc) as updatetime_last
from [t_newsperson] a where a.b_useflag = 1 ";

        if (param_search == "")
        {
            if (selected_kindcode != 0)
                sql += string.Format(@"and n_kindcode = {0} ", selected_kindcode);
        }
        else
        {
            if (selected_kindcode == 0)
                sql += string.Format(@"and replace(upper(v_name), ' ', '') like '%{0}%' or replace(upper(v_name_en), ' ', '') like '%{0}%' or replace(upper(v_tel1), ' ', '') like '%{0}%' or replace(upper(v_tel2), ' ', '') like '%{0}%' or replace(upper(v_email1), ' ', '') like '%{0}%' or replace(upper(v_email2), ' ', '') like '%{0}%' ", param_search);
            else
                sql += string.Format(@"and n_kindcode = {0} and (replace(upper(v_name), ' ', '') like '%{1}%' or replace(upper(v_name_en), ' ', '') like '%{1}%' or replace(upper(v_tel1), ' ', '') like '%{1}%' or replace(upper(v_tel2), ' ', '') like '%{1}%' or replace(upper(v_email1), ' ', '') like '%{1}%' or replace(upper(v_email2), ' ', '') like '%{1}%') ", selected_kindcode, param_search);
        }

        sql += @"order by n_personid desc ";
        sql += "offset (@PageNum - 1) * @RowsPerPage rows fetch next @RowsPerPage rows only";

        // 데이터 바인딩
        dt = Util.ExeQuery(new SqlCommand(sql), "SELECT");

        List<Source> items = new List<Source>();

        foreach (DataRow dr in dt.Rows)
        {
            items.Add(new Source()
            {
                id = dr["id"].ToString().Trim(),
                name = dr["name"].ToString().Trim(),
                name_en = dr["name_en"].ToString().Trim(),
                tel1 = dr["tel1"].ToString().Trim(),
                tel2 = dr["tel2"].ToString().Trim(),
                email1 = dr["email1"].ToString().Trim(),
                email2 = dr["email2"].ToString().Trim(),
                kind = dr["kind"].ToString().Trim(),
                etc = dr["etc"].ToString().Trim(),
                updateuser_first = dr["updateuser_first"].ToString().Trim(),
                updatetime_first = dr["updatetime_first"].ToString().Trim(),
                updateuser_last = dr["updateuser_last"].ToString().Trim(),
                updatetime_last = dr["updatetime_last"].ToString().Trim(),
            });
        }

        sourceRepeater.DataSource = items;
        sourceRepeater.DataBind();
    }

    // 분류 DropDownList 세팅
    private void SetKind()
    {
        kindTab.Items.Add("전체");

        dt = Util.ExeQuery(new SqlCommand("select v_kind as kind from [t_newskind] where b_useflag = 1 order by n_kindcode asc"), "SELECT");

        foreach (DataRow dr in dt.Rows)
            kindTab.Items.Add(dr["kind"].ToString().Trim());

        kindTab.SelectedValue = param_kind;
    }

    private void SetPage()
    {
        int selected_kindcode = Util.GetKindCode(param_kind);

        if (param_search == "")
        {
            if (selected_kindcode == 0)
                dt = Util.ExeQuery(new SqlCommand("select count(*) from [t_newsperson] where b_useflag = 1"), "SELECT");
            else
                dt = Util.ExeQuery(new SqlCommand(string.Format(@"select count(*) from [t_newsperson] where b_useflag = 1 and n_kindcode = {0}", selected_kindcode)), "SELECT");
        }
        else
        {
            if (selected_kindcode == 0)
                dt = Util.ExeQuery(new SqlCommand(string.Format(@"select count(*) from [t_newsperson] where b_useflag = 1 and replace(upper(v_name), ' ', '') like '%{0}%' or replace(upper(v_name_en), ' ', '') like '%{0}%' or replace(upper(v_tel1), ' ', '') like '%{0}%' or replace(upper(v_tel2), ' ', '') like '%{0}%' or replace(upper(v_email1), ' ', '') like '%{0}%' or replace(upper(v_email2), ' ', '') like '%{0}%'", param_search)), "SELECT");
            else
                dt = Util.ExeQuery(new SqlCommand(string.Format(@"select count(*) from [t_newsperson] where b_useflag = 1 and n_kindcode = {0} and (replace(upper(v_name), ' ', '') like '%{1}%' or replace(upper(v_name_en), ' ', '') like '%{1}%' or replace(upper(v_tel1), ' ', '') like '%{1}%' or replace(upper(v_tel2), ' ', '') like '%{1}%' or replace(upper(v_email1), ' ', '') like '%{1}%' or replace(upper(v_email2), ' ', '') like '%{1}%')", selected_kindcode, param_search)), "SELECT");
        }

        int item_count = Convert.ToInt32(dt.Rows[0].ItemArray[0]);
        List<int> pageList = new List<int>();

        if (item_count > 0 && item_count <= PAGE_PER_PAGE)
        {
            pageList.Add(1);
        }
        else if (item_count != 0)
        {
            for (int i = 1; i < (item_count / ROWS_PER_PAGE) + 2; i++)
                pageList.Add(i);
        }

        pageRepeater.DataSource = pageList;
        pageRepeater.DataBind();
    }
}
