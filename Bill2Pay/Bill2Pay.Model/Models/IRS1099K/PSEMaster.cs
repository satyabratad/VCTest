using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bill2Pay.Model
{
    public class PSEMaster
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [MaxLength(255)]
        public string Name { get; set; }
        [MaxLength(512)]
        public string Address { get; set; }

        public virtual ICollection<ImportDetail> ImportDetails { get; set; }

        public virtual ICollection<SubmissionDetail> SubmissionDetails { get; set; }
    }
}
