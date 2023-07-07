using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusTecsup.Models
{
    public class Conductor
    {
      
            [JsonProperty("perfil")]
            public string Perfil { get; set; }

            [JsonProperty("usuario")]
            public string Usuario { get; set; }

            [JsonProperty("nombre")]
            public string Nombre { get; set; }

            [JsonProperty("apellido")]
            public string Apellido { get; set; }

            [JsonProperty("contraseña")]
            public string Contraseña { get; set; }

            [JsonProperty("telefono")]
            public string Telefono { get; set; }

            [JsonProperty("latitud")]
            public string Latitud { get; set; }

            [JsonProperty("longitud")]
            public string Longitud { get; set; }
        
    }

}
