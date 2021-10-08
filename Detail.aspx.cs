using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

public partial class Detail : Page
{
    DataTable dt;
    string param_fn;
    string param_id;

    string param_kind;
    int param_page;
    string param_name;
    string param_no;
    string param_search;

    protected void Page_Load(object sender, EventArgs e)
    {
        // 리디렉션
        //string strRefer = Request.ServerVariables["HTTP_REFERER"];

        //if (strRefer == null)
        //{
        //    if (Session["empno"] == null)
        //        Page.RegisterClientScriptBlock("done", @"<script>alert('잘못된 방식으로 접근 중입니다! (1)');location.href='http://sis.seoul.co.kr/';</script>");
        //}
        //else if (strRefer.IndexOf("http://sis.seoul.co.kr/SSO/go_newscontact.php") == -1 && strRefer.IndexOf("http://appsvr3.seoul.co.kr/NewsContact/") == -1)
        //{
        //    // 조건 1) ".../SSO/go_newscontact.php"로 하면 Chrome 레퍼에서 못 가져옴 (IE에서는 가능)
        //    // 조건 2) 파일 전송 시 새로고침(Postback)되기 때문에 꼭 있어야 함             
        //    Page.RegisterClientScriptBlock("done", @"<script>alert('잘못된 방식으로 접근 중입니다! (2)');location.href='http://sis.seoul.co.kr/';</script>");
        //}
        //else if (Session["empno"] == null)
        //{
        //    Session["empno"] = Request["userid"];
        //}


        param_fn = Request["fn"];
        param_id = Request["id"];

        if (Request["kind"] == null || Request["kind"] == "" || Request["kind"] == "undefined")
            param_kind = "전체";
        else
            param_kind = Request["kind"];

        if (Request["page"] == null || Request["page"] == "" || Request["page"] == "undefined")
            param_page = 1;
        else
            param_page = Convert.ToInt32(Request["page"]);

        if (Request["name"] == null || Request["name"] == "" || Request["name"] == "undefined")
            param_name = "";
        else
            param_name = Request["name"];

        if (Request["no"] == null || Request["no"] == "" || Request["no"] == "undefined")
            param_no = "";
        else
            param_no = Request["no"];

        if (Request["search"] == null || Request["search"] == "" || Request["search"] == "undefined")
            param_search = "";
        else
            param_search = Request["search"].Replace(" ", "").Replace("-", "").ToUpper();


        try
        {
            if (param_fn == "view")
            {
                EditBtn.Visible = true;         
                SaveBtn.Visible = false;
                DeleteBtn.Visible = true;

                SetKind();
                SetValue();
            }
            else if (param_fn == "edit")
            {
                EditBtn.Visible = false;         
                SaveBtn.Visible = true;
                DeleteBtn.Visible = true;

                if (!IsPostBack)
                {
                    SetKind();
                    SetValue();
                }
            }
            else if (param_fn == "add" || param_fn == null || param_fn == "undefined")
            {
                EditBtn.Visible = false;         
                SaveBtn.Visible = true;
                DeleteBtn.Visible = false;

                SetKind();
            }
        }
        catch (Exception)
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "location.href = 'http://sis.seoul.co.kr/'; alert('오류 발생! IT개발부로 문의해 주세요.');", true);
        }
    }

    // 분류 DropDownList 세팅
    private void SetKind()
    {
        kind.Items.Add("선택");

        dt = Util.ExeQuery(new SqlCommand(string.Format(@"select v_kind as kind from [t_newskind] where b_useflag = 1 order by n_kindcode asc")), "SELECT");

        foreach (DataRow dr in dt.Rows)
        {
            kind.Items.Add(dr["kind"].ToString().Trim());
        }
    }

    private void SetValue()
    {
        List<Source> items = new List<Source>();
        
        dt = Util.ExeQuery(new SqlCommand(string.Format(@"select n_personid as id,
v_name as name,
v_name_en as name_en,
v_tel1 as tel1,
v_tel2 as tel2,
v_email1 as email1,
v_email2 as email2,
(select b.v_kind from [t_newskind] b where a.n_kindcode = b.n_kindcode) as kind,
t_etc as etc,
(select top(1) c.v_updateuser from [t_newsperson_history] c where a.n_personid = c.n_personid order by c.n_seq asc) as updateuser_first,
(select top(1) c.d_updatetime from [t_newsperson_history] c where a.n_personid = c.n_personid order by c.n_seq asc) as updatetime_first,
(select top(1) c.v_updateuser from [t_newsperson_history] c where a.n_personid = c.n_personid order by c.n_seq desc) as updateuser_last,
(select top(1) c.d_updatetime from [t_newsperson_history] c where a.n_personid = c.n_personid order by c.n_seq desc) as updatetime_last
from [t_newsperson] a
where n_personid = {0}", param_id)), "SELECT");

        id.Text = dt.Rows[0]["id"].ToString().Trim();
        name.Text = dt.Rows[0]["name"].ToString().Trim();
        name_en.Text = dt.Rows[0]["name_en"].ToString().Trim();
        tel1.Text = dt.Rows[0]["tel1"].ToString().Trim();
        tel2.Text = dt.Rows[0]["tel2"].ToString().Trim();
        email1.Text = dt.Rows[0]["email1"].ToString().Trim();
        email2.Text = dt.Rows[0]["email2"].ToString().Trim();
        kind.SelectedValue = dt.Rows[0]["kind"].ToString().Trim();
        etc.Text = dt.Rows[0]["etc"].ToString().Trim();
        updateuser_first.Text = dt.Rows[0]["updateuser_first"].ToString().Trim();
        updatetime_first.Text = dt.Rows[0]["updatetime_first"].ToString().Trim();
        updateuser_last.Text = dt.Rows[0]["updateuser_last"].ToString().Trim();
        updatetime_last.Text = dt.Rows[0]["updatetime_last"].ToString().Trim();
    }
    
    protected void EditBtn_Click(object sender, EventArgs e)
    {
        if(param_name=="desc"|| param_name == "asc")
            Response.Redirect("Detail.aspx?fn=edit&id=" + param_id + "&kind=" + param_kind + "&page=" + param_page + "&name=" + param_name + "&search=" + param_search);
        else if (param_no == "desc"|| param_no == "asc")
            Response.Redirect("Detail.aspx?fn=edit&id=" + param_id + "&kind=" + param_kind + "&page=" + param_page + "&no=" + param_no + "&search=" + param_search);
        else
            Response.Redirect("Detail.aspx?fn=edit&id=" + param_id + "&kind=" + param_kind + "&page=" + param_page + "&name=asc&search=" + param_search);
    }

    protected void DeleteBtn_Click(object sender, EventArgs e)
    {
        int input_seq = GetMaxSeq(Convert.ToInt32(param_id)) + 1;

        // 삭제
        dt = Util.ExeQuery(new SqlCommand(string.Format(@"update [t_newsperson] set b_useflag = 0 where n_personid = {0}", param_id)), "UPDATE");

        // 로그
        dt = Util.ExeQuery(new SqlCommand(string.Format(@"insert into [t_newsperson_history] values ({0}, {1}, '{2}', '{3}', getdate())", param_id, input_seq, GetUpdateUser(), Request.UserHostAddress)), "INSERT");

        if (param_name == "desc" || param_name == "asc")
            ClientScript.RegisterStartupScript(GetType(), "alert", "location.href = 'Index.aspx?kind=" + param_kind + "&page=" + param_page + "&name=" + param_name + "&search=" + param_search + "'; alert('삭제되었습니다.');", true);
        else if (param_no == "desc" || param_no == "asc")
            ClientScript.RegisterStartupScript(GetType(), "alert", "location.href = 'Index.aspx?kind=" + param_kind + "&page=" + param_page + "&no=" + param_no + "&search=" + param_search + "'; alert('삭제되었습니다.');", true);
        else
            ClientScript.RegisterStartupScript(GetType(), "alert", "location.href = 'Index.aspx?kind=" + param_kind + "&page=" + param_page + "&name=asc&search=" + param_search + "'; alert('삭제되었습니다.');", true);        
    }
    
    protected void SaveBtn_Click(object sender, EventArgs e)
    {
        string input_name = name.Text.Trim();
        string input_name_en = name_en.Text.Trim() == "" ? null : name_en.Text.Trim();
        string input_tel1 = tel1.Text.Trim().Replace("-", "") == "" ? null : tel1.Text.Trim().Replace("-", "");
        string input_tel2 = tel2.Text.Trim().Replace("-", "") == "" ? null : tel2.Text.Trim().Replace("-", "");
        string input_email1 = email1.Text.Trim() == "" ? null : email1.Text.Trim();
        string input_email2 = email2.Text.Trim() == "" ? null : email2.Text.Trim();
        int input_kindcode = Util.GetKindCode(kind.SelectedValue);
        string input_etc = etc.Text.Trim() == "" ? null : etc.Text.Trim();

        if (param_fn == "edit")
        {
            int input_seq = GetMaxSeq(Convert.ToInt32(param_id)) + 1;

            // 업데이트
            dt = Util.ExeQuery(new SqlCommand(string.Format(@"update [t_newsperson] set v_name = '{0}', v_name_en = '{1}', v_tel1 = '{2}', v_tel2 = '{3}', v_email1 = '{4}', v_email2 = '{5}', n_kindcode = {6}, t_etc = '{7}' where n_personid = {8}", input_name, input_name_en, input_tel1, input_tel2, input_email1, input_email2, input_kindcode, input_etc, param_id)), "UPDATE");

            // 로그
            dt = Util.ExeQuery(new SqlCommand(string.Format(@"insert into [t_newsperson_history] values ({0}, {1}, '{2}', '{3}', getdate())", param_id, input_seq, GetUpdateUser(), Request.UserHostAddress)), "INSERT");

            if (param_name == "desc" || param_name == "asc")
                ClientScript.RegisterStartupScript(GetType(), "alert", "location.href = 'Index.aspx?kind=" + param_kind + "&page=" + param_page + "&name=" + param_name + "&search=" + param_search + "'; alert('수정되었습니다.');", true);
            else if (param_no == "desc" || param_no == "asc")
                ClientScript.RegisterStartupScript(GetType(), "alert", "location.href = 'Index.aspx?kind=" + param_kind + "&page=" + param_page + "&no=" + param_no + "&search=" + param_search + "'; alert('수정되었습니다.');", true);
            else
                ClientScript.RegisterStartupScript(GetType(), "alert", "location.href = 'Index.aspx?kind=" + param_kind + "&page=" + param_page + "&name=asc&search=" + param_search + "'; alert('수정되었습니다.');", true);
        }
        else if (param_fn == "add" || param_fn == null)
        {
            int input_personid = GetMaxId() + 1;

            // 추가
            dt = Util.ExeQuery(new SqlCommand(string.Format(@"insert into [t_newsperson] values ({0}, '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', {7}, '{8}', 1)", input_personid, input_name, input_name_en, input_tel1, input_tel2, input_email1, input_email2, input_kindcode, input_etc)), "INSERT");

            // 로그
            dt = Util.ExeQuery(new SqlCommand(string.Format(@"insert into [t_newsperson_history] values ({0}, 1, '{1}', '{2}', getdate())", input_personid, GetUpdateUser(), Request.UserHostAddress)), "INSERT");

            ClientScript.RegisterStartupScript(GetType(), "alert", "location.href = 'Index.aspx?kind=전체&page=1&no=desc&search='; alert('저장되었습니다.');", true);
        }
    }

    private int GetMaxId()
    {
        dt = Util.ExeQuery(new SqlCommand(string.Format(@"select max(n_personid) as n_personid_max from [t_newsperson]")), "SELECT");

        int max = Convert.ToInt32(dt.Rows[0]["n_personid_max"]);

        dt = Util.ExeQuery(new SqlCommand(string.Format(@"select max(n_personid) as n_personid_max from [t_newsperson_history]")), "SELECT");

        int max_log = Convert.ToInt32(dt.Rows[0]["n_personid_max"]);
        
        return (max > max_log) ? max : max_log;
    }

    private int GetMaxSeq(int param_id)
    {
        dt = Util.ExeQuery(new SqlCommand(string.Format(@"select max(n_seq) as n_seq_max from [t_newsperson_history] where n_personid = {0}", param_id)), "SELECT");

        string max = dt.Rows[0]["n_seq_max"].ToString();

        return (max == "") ? 1 : Convert.ToInt32(max);
    }

    private string GetUpdateUser()
    {
        if (Session["empno"] == null)
            dt = Util.ExeQuery(new SqlCommand(string.Format(@"select ''")), "SELECT");
        else
            dt = Util.ExeQuery(new SqlCommand(string.Format(@"select KORE_NAME from [insa].[dbo].[dh_empl_m] where EMPL_CODE = {0}", Session["empno"])), "SELECT");

        if (dt.Rows.Count == 0)
            return "";
        else
            return dt.Rows[0].ItemArray[0].ToString().Trim();
    }
}
