using System.ComponentModel.DataAnnotations;

namespace URLShortener
{
    public class ShortenRequest
    {
        [Required]
        public string url;
    }
}
