// 파라미터 값 가져오기
var getUrlParameter = function getUrlParameter(sParam) {
    var sURLVariables = window.location.search.substring(1).split('&');
    var sParameterName;

    for (var i = 0; i < sURLVariables.length; i++) {
        sParameterName = sURLVariables[i].split('=');

        if (sParameterName[0] === sParam)
            return (sParameterName[1] === undefined) ? true : decodeURIComponent(sParameterName[1]);
    }
};


///// Index.aspx /////

// Pagination - 페이지 나눠서 보여주기
if (getUrlParameter("page") == null || getUrlParameter("page") == "") {
    var page_now = 1;
}
else {
    var page_now = getUrlParameter("page");
}

var PAGE_PER_PAGE = 5;
var page_last = ($(".page-item").length == 2) ? 1 : $($(".page-link")[$(".page-link").length - 2]).text();
var page_first_now = ($(".page-item").length == 2) ? 1 : $($(".page-link")[parseInt((page_now - 1) / PAGE_PER_PAGE) * PAGE_PER_PAGE + 1]).text();
var q = parseInt((page_now - 1) / PAGE_PER_PAGE);

for (var i = 1; i < $(".page-item").length - 1; i++) {
    var item = $(".page-item")[i];

    if (parseInt(($(item).find(".page-link").text() - 1) / PAGE_PER_PAGE) == q)
        $(item).removeClass("blind");
    else
        $(item).addClass("blind");
}

// Pagination - 이전 화살표 (비)활성화
if (page_first_now == 1)
    $($(".page-item")[0]).addClass("disabled");
else
    $($(".page-item")[0]).removeClass("disabled");

// Pagination - 다음 화살표 (비)활성화
if (parseInt((page_last - 1) / PAGE_PER_PAGE) == parseInt((page_first_now - 1) / PAGE_PER_PAGE))
    $($(".page-item")[$(".page-item").length - 1]).addClass("disabled");
else
    $($(".page-item")[$(".page-item").length - 1]).removeClass("disabled");

// Pagination - 현재 페이지 css    
for (var i = 1; i < $(".page-item").length - 1; i++) {
    if ($($(".page-item")[i]).text().trim() == page_now)
        $($(".page-item")[i]).addClass("active");
    else
        $($(".page-item")[i]).removeClass("active");
}

// 소트 탭 href
$(".sortTabName").on("click", function (e) {
    if (getUrlParameter("name") == "desc")
        location.href = "Index.aspx?kind=" + $("#kindTab").val() + "&page=1&name=asc&search=" + $("#searchTB").val();
    else if (getUrlParameter("name") == "asc")
        location.href = "Index.aspx?kind=" + $("#kindTab").val() + "&page=1&name=desc&search=" + $("#searchTB").val();
    else
        location.href = "Index.aspx?kind=" + $("#kindTab").val() + "&page=1&name=desc&search=" + $("#searchTB").val();
});

$(".sortTabNo").on("click", function (e) {
    if (getUrlParameter("no") == "desc")
        location.href = "Index.aspx?kind=" + $("#kindTab").val() + "&page=1&no=asc&search=" + $("#searchTB").val();
    else if (getUrlParameter("no") == "asc")
        location.href = "Index.aspx?kind=" + $("#kindTab").val() + "&page=1&no=desc&search=" + $("#searchTB").val();
    else
        location.href = "Index.aspx?kind=" + $("#kindTab").val() + "&page=1&no=desc&search=" + $("#searchTB").val();
});

// 소트 탭 text
if (getUrlParameter("name") == "desc") {
    $(".sortTabName").html("이름▼");
    $(".sortTabNo").html("No");
}
else if (getUrlParameter("name") == "asc") {
    $(".sortTabName").html("이름▲");
    $(".sortTabNo").html("No");
}

if (getUrlParameter("no") == "desc") {
    $(".sortTabName").html("이름");
    $(".sortTabNo").html("No▼");
}
else if (getUrlParameter("no") == "asc") {
    $(".sortTabName").html("이름");
    $(".sortTabNo").html("No▲");
}


///// Detail.aspx /////

// input (비)활성화
if (getUrlParameter("fn") == "view") {
    $(".hide").removeClass("blind");

    $("input[type='text']").each(function () {
        $(this).prop("disabled", true);
    });
    $("select").each(function () {
        $(this).prop("disabled", true);
    });
    $("textarea").each(function () {
        $(this).prop("disabled", true);
    });

    // 비고 textarea 높이
    $("#etc").height($("#etc").prop("scrollHeight"));
}
else {
    $(".hide").addClass("blind");

    $("input[type='text']").each(function () {
        $(this).prop("disabled", false);
    });
    $("select").each(function () {
        $(this).prop("disabled", false);
    });
    $("textarea").each(function () {
        $(this).prop("disabled", false);
    });
}

// 저장 버튼 클릭
function SaveBtn_onClick() {
    var emailReg = /^[A-Z0-9._%+-]+@([A-Z0-9-]+\.)+[A-Z]{2,4}$/i;

    if ($("#name").val() == "") {
        alert("'이름'을(를) 입력해 주세요.");
        $("#name").focus();

        return false;
    }
    else if ($("#email1").val() != "" && !emailReg.test($("#email1").val())) {
        alert("'이메일1' 형식을(를) 확인해 주세요.");
        $("#email1").focus();

        return false;
    }
    else if ($("#email2").val() != "" && !emailReg.test($("#email2").val())) {
        alert("'이메일2' 형식을(를) 확인해 주세요.");
        $("#email2").focus();

        return false;
    }
    else if ($("#kind option").index($("#kind option:selected")) == "0") {
        alert("'분류'을(를) 선택해 주세요.");
        $("#kind").focus();

        return false;
    }

    return true;
}
