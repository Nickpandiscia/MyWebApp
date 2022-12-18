using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace MyWebApp.Models
{
	public class Products
	{
		[Key]
		public int ID { get; set; }
		[Required]
		public string Name { get; set; } = string.Empty;
		public string type { get; set; } = string.Empty;
		public float price { get; set; }
		public float review { get; set; }
	}
}
