namespace katio.Data.Models;
public class Books 
{
    public int Id;
    public string Title {get; set;}="";

    public string ISBN10{get; set;}="";

    public string ISBN13 {get; set;}="";

    public DateTime Published;

    public string Edition {get; set;}="";

    public string DeweyIndex {get; set;}="";

}