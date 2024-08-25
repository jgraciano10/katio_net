using katio.Data.Models;

namespace katio.Business.Utilities;
public static class Utilities 
{
    public static List<Books> createABooksList()
    {
        List<Books> bookList = new List<Books>(){
             new Books( ){
            Title="Cien Años de soledad",
            ISBN10 ="",
            ISBN13 ="",
            Edition = "RAE obra academica",
            DeweyIndex ="800",
            Published = new DateTime().AddDays(1).AddMonths(10).AddYears(1967),
            Id= 1
              },
              new Books( ){
            Title="Huellas",
            ISBN10 ="",
            ISBN13 ="",
            Edition = "Planeta",
            DeweyIndex ="",
            Published = new DateTime().AddDays(1).AddMonths(10).AddYears(2019),
            Id= 2
              },
              new Books( ){
            Title="Maria",
            ISBN10 ="",
            ISBN13 ="",
            Edition = "Planeta",
            DeweyIndex ="",
            Published = new DateTime().AddDays(1).AddMonths(10).AddYears(1867),
            Id= 3
              }
        };
       

            
        return bookList;  
    }
}