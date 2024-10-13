using System.ComponentModel.DataAnnotations.Schema;
using katio_net.Data.Models;

namespace katio.Data.Models;

public class Narrator : BaseEntity<int>
{
    public string Name{get; set;} = "";
    public string LastName {get; set;} ="";
   
    [ForeignKey("Genres")]
    public int GenresId {get; set;}

    public virtual Genres? Genres{get;set;}
     public Languages Languages;

}