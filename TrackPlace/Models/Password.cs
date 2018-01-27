using System.ComponentModel.DataAnnotations;

namespace TrackPlace.Models
{
    /// <summary>
    /// Password entity for useracconts
    /// </summary>
    public class Password
    {
        public int PasswordId { get; set; }
        [Required]
        public string PasswordName { get; set; }
        
    }
}
