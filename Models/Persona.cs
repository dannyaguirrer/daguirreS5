using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace daguirreS5.Models
{
    [Table("persona")]
    public class Persona
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(25)]
        public string Nombre { get; set; }
        public int Edad { get; set; }
    }
}
