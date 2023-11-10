using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppBodega.Models
{
    public class Item
    {
        public string Id { get; set; }
        public int RUT { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string FechadeNacimiento { get; set; }
        public string MedicoTratante { get; set; }
        public string Fechahora { get; set; }
    }

}