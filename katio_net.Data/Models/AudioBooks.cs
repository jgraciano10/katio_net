using System.ComponentModel.DataAnnotations.Schema;
using katio_net.Data.Models;

namespace katio.Data.Models;

public class AudioBooks : BaseEntity<int> 
{
    public string Name {get; set;}= "";
    public string ISBN10 {get; set;} = "";
    public string ISBN13 {get; set;}= "";
    public DateTime Published;
    public string Genre {get; set;}= "";
    public bool Abridged{get; set;} // resumido o no.
    public int LengthInSeconds {get; set;} // Duracion en segundos
    public string Path {get; set;}= ""; // Donde esta ese libro en el disco.    
    public string Edition {get; set;}= "";
    [ForeignKey("Author")]
    public int AuthorId {get; set;}

    public virtual Author? Author{get;set;}
   
}