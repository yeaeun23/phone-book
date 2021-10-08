<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Index" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <title>취재원 연락처</title>    
    <script type="text/javascript" src="js/jquery-3.5.0.min.js"></script>
    <script type="text/javascript" src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.0/js/bootstrap.min.js"></script>
    <link rel="stylesheet" type="text/css" href="css/icons.css" />
    <link rel="stylesheet" type="text/css" href="css/common.css" />
    <link rel="stylesheet" type="text/css" href="css/style.css" />
    <link type="text/css" rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.0/css/bootstrap.min.css" />
</head>

<body>
    <form runat="server" id="form1">
        <main class="container">
            <div class="row">
                <div class="col-md-12 col-sm-12">
                    <div style="float: left;">
                        <asp:DropDownList runat="server" ID="kindTab" CssClass="input_left" />
                        <input runat="server" id="searchTB" class="input_left" type="text" placeholder="이름/전화/이메일/비고" style="width: 300px;" />
                        <a id="searchBtn" class="input_left btn btn-sm btn-secondary" onclick="searchBtn_onClick();">검색</a>
                    </div>

                    <div style="float: right;">
                        <a class="btn btn-sm btn-primary" href="Detail.aspx?fn=add">추가</a>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-md-12 col-sm-12">
                    <div class="card h-100">
                        <table class="index_table">
                            <colgroup>
                                <col width="60px" />
                                <col width="55px" />
                                <col width="25%" />
                                <col width="15%" />
                                <col width="15%" />
                                <col width="20%" />
                                <col width="25%" />
                                <col width="30px" />
                            </colgroup>
                            <thead>
                                <tr class="card-header">
                                    <th>
                                        <span class="ellipsis sortTabNo">No</span>
                                    </th>
                                    <th>
                                        <span class="ellipsis">분류</span>
                                    </th>
                                    <th>
                                        <span class="ellipsis sortTabName">이름▲</span>
                                    </th>
                                    <th>
                                        <span class="ellipsis">전화번호1</span>
                                    </th>
                                    <th>
                                        <span class="ellipsis">전화번호2</span>
                                    </th>
                                    <th>
                                        <span class="ellipsis">이메일1</span>
                                    </th>
                                    <th>
                                        <span class="ellipsis">비고</span>
                                    </th>
                                    <th>
                                        <span class="ellipsis"></span>
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater runat="server" ID="sourceRepeater">
                                    <ItemTemplate>
                                        <tr class="link">
                                            <td>
                                                <asp:Label runat="server" CssClass="ellipsis id" Text='<%# Eval("id") %>' />
                                            </td>
                                            <td>
                                                <asp:Label runat="server" CssClass="ellipsis" Text='<%# Eval("kind") %>' />
                                            </td>
                                            <td>
                                                <asp:Label runat="server" CssClass="ellipsis" Text='<%# Eval("name") %>' />
                                            </td>
                                            <td>
                                                <asp:Label runat="server" CssClass="ellipsis" Text='<%# Eval("tel1") %>' />
                                            </td>
                                            <td>
                                                <asp:Label runat="server" CssClass="ellipsis" Text='<%# Eval("tel2") %>' />
                                            </td>
                                            <td>
                                                <asp:Label runat="server" CssClass="ellipsis" Text='<%# Eval("email1") %>' />
                                            </td>
                                            <td>
                                                <asp:Label runat="server" CssClass="ellipsis" Text='<%# Eval("etc") %>' />
                                            </td>
                                            <td>
                                                <asp:Label runat="server" CssClass="ellipsis icon icon-arrow-right" />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>

            <nav aria-label="page-navigation">
                <ul class="pagination pagination-sm justify-content-center">
                    <li class="page-item">
                        <a class="page-link" href="#" aria-label="previous" title="이전 페이지">
                            <span aria-hidden="true">&laquo;</span>
                            <span class="sr-only">Previous</span>
                        </a>
                    </li>
                    <asp:Repeater runat="server" ID="pageRepeater">
                        <ItemTemplate>
                            <li class="page-item">
                                <asp:HyperLink runat="server" CssClass="page-link" NavigateUrl='<%# string.Format("Index.aspx?kind={0}&page={1}&name={2}&no={3}&search={4}", (Request.QueryString["kind"] == null) ? "전체" : Request.QueryString["kind"], Container.DataItem, Request.QueryString["name"], Request.QueryString["no"], Request.QueryString["search"]) %>'><%# Container.DataItem %></asp:HyperLink>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                    <li class="page-item">
                        <a class="page-link" href="#" aria-label="next" title="다음 페이지">
                            <span aria-hidden="true">&raquo;</span>
                            <span class="sr-only">Next</span>
                        </a>
                    </li>
                </ul>
            </nav>
        </main>

        <script type="text/javascript" src="js/common.js"></script>
        <script type="text/javascript">
            // Pagination - 이전 화살표 클릭
            $($(".page-item")[0]).on("click", function (e) {
                if (page_first_now != 1) {
                    if (getUrlParameter("name") == "desc" || getUrlParameter("name") == "asc")
                        location.href = "Index.aspx?kind=" + $("#kindTab").val() + "&page=" + (page_first_now - PAGE_PER_PAGE) + "&name=" + getUrlParameter("name") + "&search=" + $("#searchTB").val();
                    else if (getUrlParameter("no") == "desc" || getUrlParameter("no") == "asc")
                        location.href = "Index.aspx?kind=" + $("#kindTab").val() + "&page=" + (page_first_now - PAGE_PER_PAGE) + "&no=" + getUrlParameter("no") + "&search=" + $("#searchTB").val();
                    else
                        location.href = "Index.aspx?kind=" + $("#kindTab").val() + "&page=" + (page_first_now - PAGE_PER_PAGE) + "&name=asc&search=" + $("#searchTB").val();
                }   
            });

            // Pagination - 다음 화살표 클릭
            $($(".page-item")[$(".page-item").length - 1]).on("click", function (e) {
                if (parseInt((page_last - 1) / PAGE_PER_PAGE) != parseInt((page_first_now - 1) / PAGE_PER_PAGE)) {
                    if (getUrlParameter("name") == "desc" || getUrlParameter("name") == "asc")
                        location.href = "Index.aspx?kind=" + $("#kindTab").val() + "&page=" + (parseInt(page_first_now) + parseInt(PAGE_PER_PAGE)) + "&name=" + getUrlParameter("name") + "&search=" + $("#searchTB").val();
                    else if (getUrlParameter("no") == "desc" || getUrlParameter("no") == "asc")
                        location.href = "Index.aspx?kind=" + $("#kindTab").val() + "&page=" + (parseInt(page_first_now) + parseInt(PAGE_PER_PAGE)) + "&no=" + getUrlParameter("no") + "&search=" + $("#searchTB").val();
                    else
                        location.href = "Index.aspx?kind=" + $("#kindTab").val() + "&page=" + (parseInt(page_first_now) + parseInt(PAGE_PER_PAGE)) + "&name=asc&search=" + $("#searchTB").val();
                }
            });

            // 테이블 tr 클릭
            $("table").on("click", "tr.link", function (e) {
                var param_id = $(e.target).closest(".link").find(".id").text();

                if (getUrlParameter("name") == "desc" || getUrlParameter("name") == "asc")
                    location.href = "Detail.aspx?fn=view&id=" + param_id + "&kind=" + getUrlParameter("kind") + "&page=" + getUrlParameter("page") + "&name=" + getUrlParameter("name") + "&search=" + getUrlParameter("search");
                else if (getUrlParameter("no") == "desc" || getUrlParameter("no") == "asc")
                    location.href = "Detail.aspx?fn=view&id=" + param_id + "&kind=" + getUrlParameter("kind") + "&page=" + getUrlParameter("page") + "&no=" + getUrlParameter("no") + "&search=" + getUrlParameter("search");
                else
                    location.href = "Detail.aspx?fn=view&id=" + param_id + "&kind=전체&page=1&name=asc&search=";
            });

            // 분류 변경
            $("#kindTab").change(function (e) {
                if (getUrlParameter("name") == "desc" || getUrlParameter("name") == "asc")
                    location.href = "Index.aspx?kind=" + $("#kindTab").val() + "&page=1&name=" + getUrlParameter("name") + "&search=" + $("#searchTB").val();
                else if (getUrlParameter("no") == "desc" || getUrlParameter("no") == "asc")
                    location.href = "Index.aspx?kind=" + $("#kindTab").val() + "&page=1&no=" + getUrlParameter("no") + "&search=" + $("#searchTB").val();
                else
                    location.href = "Index.aspx?kind=" + $("#kindTab").val() + "&page=1&name=asc&search=" + $("#searchTB").val();
            });

            // 검색 버튼 클릭
            $("#searchTB").keypress(function (e) {
                if (e.keyCode == 13) {
                    //event.preventDefault();
                    searchBtn_onClick();
                    return false;
                }
            });

            function searchBtn_onClick() {
                if ($("#searchTB").val().length == 1) {
                    alert("2자 이상 입력해 주세요.");
                    $("#searchTB").focus();
                }
                else {
                    if (getUrlParameter("name") == "desc" || getUrlParameter("name") == "asc")
                        location.href = "Index.aspx?kind=" + $("#kindTab").val() + "&page=1&name=" + getUrlParameter("name") + "&search=" + $("#searchTB").val();
                    else if (getUrlParameter("no") == "desc" || getUrlParameter("no") == "asc")
                        location.href = "Index.aspx?kind=" + $("#kindTab").val() + "&page=1&no=" + getUrlParameter("no") + "&search=" + $("#searchTB").val();
                    else
                        location.href = "Index.aspx?kind=" + $("#kindTab").val() + "&page=1&name=asc&search=" + $("#searchTB").val();
                }
            }
        </script>
    </form>
</body>
</html>
