namespace StomatologieModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Pontaj")]
    public partial class Pontaj
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PontajId { get; set; }

        public int DoctorId { get; set; }

        [Column(TypeName = "date")]
        public DateTime Data { get; set; }

        public decimal OraStart { get; set; }

        public decimal OraFinal { get; set; }

        public virtual Doctor Doctor { get; set; }
    }
}
