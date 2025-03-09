using System.ComponentModel.DataAnnotations;

namespace URLShortener
{
    public class UrlEntry
    {
       
        public int Id { get; set; }
        public string ShortCode { get; set; }

        [Required]
        public string OriginalUrl { get; set; }
    }
}
