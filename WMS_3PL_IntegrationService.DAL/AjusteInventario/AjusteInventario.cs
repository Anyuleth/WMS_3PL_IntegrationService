using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace WMS_3PL_IntegrationService.DAL.AjusteInventario
{
    public class AjusteInventario
    {
        #region Crea el inventario en base de datos de gestion
        public static void CrearInventario(string connectionString,string codAlmacen, string inventario, DateTime fecha)
        {
            try
            {
                using var context = new DAL.WMS_3PL_Context(connectionString);

                var paramList = new[] {
                    new SqlParameter("@Inventario", inventario),
                 new SqlParameter("@Fecha", fecha),
                 new SqlParameter("@CodAlmacen", codAlmacen)};


                context.Database.ExecuteSqlRaw("EXEC WMS.SP_CrearInventarios @Inventario, @Fecha, @CodAlmacen", parameters: paramList);


            }
            catch (Exception ex)
            {
                var mensajeError = ex.InnerException != null ? ex.Message + ", " + ex.InnerException.Message : ex.Message;
                DAL.Herramientas.GuardarError(new ENTITY.Errores.Errores("Libreria: BLL - Clase: AjusteInventario - Metodo: CrearInventario", mensajeError.ToString()));
            }

        }
        #endregion
    }
}
