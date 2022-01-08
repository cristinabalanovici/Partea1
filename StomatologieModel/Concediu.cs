namespace StomatologieModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Concediu")]
    public partial class Concediu
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ConcediuId { get; set; }

        public int DoctorId { get; set; }

        public DateTime DataStart { get; set; }

        public DateTime DataFinal { get; set; }

        public virtual Doctor Doctor { get; set; }
    }
}
