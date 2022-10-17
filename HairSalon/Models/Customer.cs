using System.Collections.Generic;

namespace HairSalon.Models
{
  public class Customer
  {
    public int CustomerId { get; set; }
    public int StylistId { get; set; }
    public string  CustomerName { get; set; }
    public string CustomerDescription {get; set; }
    public virtual Stylist Stylist { get; set; }
  }
}