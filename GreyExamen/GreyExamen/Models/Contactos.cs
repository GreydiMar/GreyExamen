using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace GreyExamen.Models
{
    public class Contactos
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(60)]
        public string Nombres { get; set; }

        [MaxLength(60)]
        public string Apellidos { get; set; }

        [MaxLength(70)]
        public int Edad { get; set; }

        [MaxLength(30)]
        public string Pais { get; set; }

        [MaxLength(90)]
        public string Nota { get; set; }


        public double Latitud { get; set; }

        public double Longitud { get; set; }
    
}

}
