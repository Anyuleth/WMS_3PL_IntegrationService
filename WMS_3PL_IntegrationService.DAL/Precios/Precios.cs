using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
namespace WMS_3PL_IntegrationService.DAL.Precios
{
    public class Precios
    {
        #region Obtener los precios pendientes de enviar 
        public static List<ENTITY.Precios.PreciosU> ObtenerPreciosPendientes()
        {

            try
            {


                var db = new DAL.WMS_3PL_Context();

                var vListaPrecios = db.Precio.FromSqlRaw("EXECUTE Catalogos.SP_Precios_Obtener").ToList();


                return vListaPrecios;

            }
            catch (Exception ex)
            {

            }
            return null;
        }
        #endregion
    }
}
