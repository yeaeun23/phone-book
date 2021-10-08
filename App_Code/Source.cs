// 취재원
public class Source
{
    // 등록번호
    public string id { get; set; }
    // 이름(한글)
    public string name { get; set; }
    // 이름(영문)
    public string name_en { get; set; }
    // 전화번호1 
    public string tel1 { get; set; }
    // 전화번호2
    public string tel2 { get; set; }
    // 이메일1
    public string email1 { get; set; }
    // 이메일2
    public string email2 { get; set; }
    // 분류
    public string kind { get; set; }
    // 비고
    public string etc { get; set; }
    // 최초 등록자
    public string updateuser_first { get; set; }
    // 최초 등록시간
    public string updatetime_first { get; set; }
    // 마지막 수정자
    public string updateuser_last { get; set; }
    // 마지막 수정시간
    public string updatetime_last { get; set; }
}
