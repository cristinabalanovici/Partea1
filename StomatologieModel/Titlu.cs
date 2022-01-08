namespace StomatologieModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Titlu")]
    public partial class Titlu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Titlu()
        {
            Doctors = new HashSet<Doctor>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TitluId { get; set; }

        [Column("Titlu")]
        [Required]
        [StringLength(50)]
        public string Titlu1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Doctor> Doctors { get; set; }
    }
}
