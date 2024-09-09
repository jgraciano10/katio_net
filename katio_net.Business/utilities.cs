using System.Net;
using katio.Data.Dto;
using katio.Data.Models;

namespace katio.Business.Utilities;
public static class Utilities 
{

  #region BaseMessages
  public static BaseMessage<T> BuilResponse<T>(HttpStatusCode statusCode, string message, List<T>? elements = null)
  where T:class
  {
    return new BaseMessage<T>()
    {
      statusCode = statusCode,
      Message = message,
      TotalElements = (elements!=null&&elements.Any())?elements.Count:0,
      ResponseElements = elements ?? new List<T>()

    };
  }
  #endregion
    public static List<Book> createABooksList()
    {
        List<Book> bookList = new List<Book>(){
             new Book( ){
            Title="Cien Años de soledad",
            ISBN10 ="",
            ISBN13 ="",
            Edition = "RAE obra academica",
            DeweyIndex ="800",
            Published = new DateTime().AddDays(1).AddMonths(10).AddYears(1967),
            Id= 1
              },
              new Book( ){
            Title="Huellas",
            ISBN10 ="",
            ISBN13 ="",
            Edition = "Planeta",
            DeweyIndex ="",
            Published = new DateTime().AddDays(1).AddMonths(10).AddYears(2019),
            Id= 2
              },
              new Book( ){
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