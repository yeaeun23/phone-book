<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="Detail" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <title>취재원 연락처::상세보기</title>
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
                        <a class="btn btn-sm btn-secondary icon icon-arrow-left-2" href="javascript:history.back();">이전 페이지</a>
                    </div>

                    <div style="float: right;">
                        <asp:Button runat="server" ID="DeleteBtn" CssClass="input_right btn btn-sm btn-danger" Text="삭제" OnClientClick="return confirm('삭제하시겠습니까?');" OnClick="DeleteBtn_Click" />
                        <asp:Button runat="server" ID="SaveBtn" CssClass="input_right btn btn-sm btn-primary" Text="저장" OnClientClick="return SaveBtn_onClick();" OnClick="SaveBtn_Click" />
                        <asp:Button runat="server" ID="EditBtn" CssClass="input_right btn btn-sm btn-primary" Text="수정" OnClick="EditBtn_Click" /> 
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-md-12 col-sm-12">
                    <div class="card h-100">
                        <table class="detail_table">
                            <colgroup>
                                <col width="30%" />
                                <col width="70%" />
                            </colgroup>
                            <thead>
                                <tr class="card-header">
                                    <th colspan="2">상세보기</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <th>
                                        <label for="name">이름 *</label>
                                    </th>
                                    <td>
                                        <asp:TextBox runat="server" ID="name" />
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        <label for="name_en">이름(영문)</label>
                                    </th>
                                    <td>
                                        <asp:TextBox runat="server" ID="name_en" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <hr class="mt-1 mb-1" />
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        <label for="tel1">전화번호1</label>
                                    </th>
                                    <td>
                                        <asp:TextBox runat="server" ID="tel1" />
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        <label for="tel2">전화번호2</label>
                                    </th>
                                    <td>
                                        <asp:TextBox runat="server" ID="tel2" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <hr class="mt-1 mb-1" />
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        <label for="email1">이메일1</label>
                                    </th>
                                    <td>
                                        <asp:TextBox runat="server" ID="email1" />
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        <label for="email2">이메일2</label>
                                    </th>
                                    <td>
                                        <asp:TextBox runat="server" ID="email2" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <hr class="mt-1 mb-1" />
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        <label for="kind">분류 *</label>
                                    </th>
                                    <td>
                                        <asp:DropDownList runat="server" ID="kind" />
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        <label for="etc">비고</label>
                                    </th>
                                    <td>
                                        <asp:TextBox runat="server" ID="etc" TextMode="MultiLine" Rows="3" />
                                    </td>
                                </tr>
                                <tr class="hide">
                                    <td colspan="2">
                                        <hr class="mt-1 mb-1" />
                                    </td>
                                </tr>
                                <tr class="hide">
                                    <th>
                                        <label for="id">등록번호</label>
                                    </th>
                                    <td>
                                        <asp:TextBox runat="server" ID="id" />
                                    </td>
                                </tr>
                                <tr class="hide">
                                    <th>
                                        <label for="updateuser_first">최초 등록자</label>
                                    </th>
                                    <td>
                                        <asp:TextBox runat="server" ID="updateuser_first" />
                                    </td>
                                </tr>
                                <tr class="hide">
                                    <th>
                                        <label for="updatetime_first">최초 등록시간</label>
                                    </th>
                                    <td>
                                        <asp:TextBox runat="server" ID="updatetime_first" />
                                    </td>
                                </tr>
                                <tr class="hide">
                                    <th>
                                        <label for="updateuser_last">마지막 수정자</label>
                                    </th>
                                    <td>
                                        <asp:TextBox runat="server" ID="updateuser_last" />
                                    </td>
                                </tr>
                                <tr class="hide">
                                    <th>
                                        <label for="updatetime_last">마지막 수정시간</label>
                                    </th>
                                    <td>
                                        <asp:TextBox runat="server" ID="updatetime_last" />
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </main>


        <script type="text/javascript" src="js/common.js"></script>
    </form>
</body>
</html>
