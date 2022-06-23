using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace WMS_3PL_IntegrationService.DAL.AjusteInventario
{
    public class AjusteInventario
    {
        #region Valida si existe un albaran para el pedido enviado en la base de gestion
        public static bool ValidarInventarioExiste(string connectionString, DateTime fecha)
        {
            var resultado = false;
            try
            {
                using var context = new DAL.WMS_3PL_Context(connectionString);

                var paramList = new[] {
                    new SqlParameter("@Fecha", fecha),
                    new Microsoft.Data.SqlClient.SqlParameter()
                    {
                        ParameterName = "@Respuesta",
                        SqlDbType = System.Data.SqlDbType.Bit,
                        Direction = System.Data.ParameterDirection.Output
                    } };


                context.Database.ExecuteSqlRaw("EXEC BTOB.SP_ValidarInventario_Existe @Fecha, @Respuesta out", parameters: paramList);
                resultado = Convert.ToBoolean(paramList[1].Value);

            }
            catch (Exception ex)
            {
                var mensajeError = ex.InnerException != null ? ex.Message + ", " + ex.InnerException.Message : ex.Message;
                DAL.Herramientas.GuardarError(new ENTITY.Errores.Errores("Libreria: BLL - Clase: PedidosCompras - Metodo: ModificarEstadoPedido", mensajeError.ToString()));
            }
            return resultado;
        }
        #endregion
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
