using System.ComponentModel.DataAnnotations;

namespace Demo4NER.Models
{
    public class Link
    {
        [Key]
        public string URL { get; set; }
        public string Name { get; set; }
    }
}