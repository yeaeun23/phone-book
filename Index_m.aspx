<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Index_m.aspx.cs" Inherits="Index_m" %>

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
    <link rel="stylesheet" type="text/css" href="css/style_m.css" />
    <link type="text/css" rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.0/css/bootstrap.min.css" />
</head>

<body>
    <form runat="server" id="form1">
        <main class="container">
            <div class="row">
                <div class="col-md-12 col-sm-12">
                    <asp:DropDownList runat="server" ID="kindTab" />
                    <input runat="server" id="searchTB" type="text" placeholder="이름/전화/이메일/비고" />
                    <a id="searchBtn" class="btn btn-sm btn-secondary" onclick="searchBtn_onClick();">검색</a>
                    <a id="addBtn" class="btn-primary" href="Detail_m.aspx?fn=add">+</a>
                </div>
            </div>

            <div class="row">
                <div class="col-md-12 col-sm-12">
                    <div class="card h-100">
                        <table class="index_table">
                            <colgroup>
                                <col width="0px" />
                                <col width="55px" />
                                <col width="*" />
                                <col width="30px" />
                            </colgroup>
                            <tbody>
                                <asp:Repeater runat="server" ID="sourceRepeater">
                                    <ItemTemplate>
                                        <tr class="link">
                                            <td class="blind">
                                                <asp:Label runat="server" CssClass="ellipsis id" Text='<%# Eval("id") %>' />
                                            </td>
                                            <td>
                                                <asp:Label runat="server" CssClass="ellipsis" Text='<%# Eval("kind") %>' />
                                            </td>
                                            <td>
                                                <asp:Label runat="server" CssClass="ellipsis" Text='<%# Eval("name") %>' />
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
                <ul class="pagination justify-content-center">
                    <li class="page-item">
                        <a class="page-link" href="#" aria-label="previous" title="이전 페이지">
                            <span aria-hidden="true">&laquo;</span>
                            <span class="sr-only">Previous</span>
                        </a>
                    </li>
                    <asp:Repeater runat="server" ID="pageRepeater">
                        <ItemTemplate>
                            <li class="page-item">
                                <asp:HyperLink runat="server" CssClass="page-link" NavigateUrl='<%# string.Format("Index_m.aspx?kind={0}&page={1}&search={2}", (Request.QueryString["kind"] == null) ? "전체" : Request.QueryString["kind"], Container.DataItem, Request.QueryString["search"]) %>'><%# Container.DataItem %></asp:HyperLink>
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
                if (page_first_now != 1)
                    location.href = "Index_m.aspx?kind=" + $("#kindTab").val() + "&page=" + (page_first_now - PAGE_PER_PAGE);
            });

            // Pagination - 다음 화살표 클릭
            $($(".page-item")[$(".page-item").length - 1]).on("click", function (e) {
                if (parseInt((page_last - 1) / PAGE_PER_PAGE) != parseInt((page_first_now - 1) / PAGE_PER_PAGE))
                    location.href = "Index_m.aspx?kind=" + $("#kindTab").val() + "&page=" + (parseInt(page_first_now) + parseInt(PAGE_PER_PAGE));
            });

            // 테이블 tr 클릭
            $("table").on("click", "tr.link", function (e) {
                var param_id = $(e.target).closest(".link").find(".id").text();
                location.href = "Detail_m.aspx?fn=view&id=" + param_id;
            });

            // 분류 변경
            $("#kindTab").change(function (e) {
                location.href = "Index_m.aspx?kind=" + $("#kindTab").val() + "&page=1&search=" + $("#searchTB").val();
            });

            // 검색 버튼 클릭
            $("#searchTB").keypress(function (e) {
                if (e.keyCode == 13) {
                    event.preventDefault();
                }
            });

            function searchBtn_onClick() {
                if ($("#searchTB").val().length == 1) {
                    alert("2자 이상 입력해 주세요.");
                    $("#searchTB").focus();
                }
                else {
                    location.href = "Index_m.aspx?kind=" + $("#kindTab").val() + "&page=1&search=" + $("#searchTB").val();
                }
            }
        </script>
    </form>
</body>
</html>
