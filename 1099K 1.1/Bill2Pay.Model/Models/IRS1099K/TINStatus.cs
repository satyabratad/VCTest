using System.ComponentModel.DataAnnotations;

namespace Bill2Pay.Model
{
    /// <summary>
    /// TIN Status
    /// </summary>
    public class TINStatus
    {
        
        /// <summary>
        /// Database Identity
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Status Name
        /// </summary>
        [MaxLength(255)]
        public string Name { get; set; }
    }
}
