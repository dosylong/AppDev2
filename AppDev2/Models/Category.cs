using System.ComponentModel.DataAnnotations;

namespace AppDev2.Models
{
	public class Category
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
	}
}