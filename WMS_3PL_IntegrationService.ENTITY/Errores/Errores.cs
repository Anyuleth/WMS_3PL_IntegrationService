using System;
using System.Collections.Generic;
using System.Text;

namespace WMS_3PL_IntegrationService.ENTITY.Errores
{
    public class Errores
    {
        
        public string Ubicacion { get; set; }
        public string Informacion { get; set; }

        public Errores(string ubicacion, string informacion)
        {
            Ubicacion = ubicacion;
            Informacion = informacion;
        }
        
    }
}
