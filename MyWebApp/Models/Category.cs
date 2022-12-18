using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyWebApp.Models
{
    public class Category
    {
        //key tells Entity that this is a primary key and is needed.
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [DisplayName("Display Order")]
        public int DisplayOrder { get; set; }       
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        //This is going to create the contents inside our category table.
        //Search for sqldb using Command Prompt, type in sqllocaldb/ sqllocaldb versions. Or type sqllocaldb create to make new.
    }
}
