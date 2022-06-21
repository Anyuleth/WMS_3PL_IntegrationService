using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace WMS_3PL_IntegrationService.DAL.Articulos
{
    public class Articulos
    {
        #region Obtener los articulos pendientes de enviar al SFTP
        public static List<ENTITY.Articulo> ObtenerArticulosPendientes()
        {

            try
            {

              

                var db = new DAL.WMS_3PL_Context();

                var vListaArticulos = db.Articulo.FromSqlRaw("EXECUTE Catalogos.SP_Articulos_Obtener").ToList();

            
                return vListaArticulos;
       


            }
            catch (Exception ex)
            {
                var mensaje = ex.InnerException != null ? ex.Message + ", " + ex.InnerException.Message : ex.Message;
                DAL.Herramientas.GuardarError(new ENTITY.Errores.Errores("Libreria: BLL - Clase: Articulos - Metodo: ObtenerArticulosPendientes", mensaje.ToString()));
            }
            return null;
        }
        #endregion

        #region Obtener los codigos de articulos pendientes de enviar  al SFTP
        public static List<ENTITY.CodigoArticulo> ObtenerCodArticulosPendientes()
        {

            try
            {


                var db = new DAL.WMS_3PL_Context();

                var vListaCodigoArticulos = db.CodigoArticulo.FromSqlRaw("EXECUTE Catalogos.SP_Codigo_Articulos_Obtener").ToList();

             

               
                return vListaCodigoArticulos;



            }
            catch (Exception ex)
            {
                var mensaje = ex.InnerException != null ? ex.Message + ", " + ex.InnerException.Message : ex.Message;
                DAL.Herramientas.GuardarError(new ENTITY.Errores.Errores("Libreria: BLL - Clase: Articulos - Metodo: ObtenerCodArticulosPendientes", mensaje.ToString()));
            }
            return null;
        }
        #endregion
    }


}
