using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WMS_3PL_IntegrationService.DAL.ConfirmacionPedidoCompra
{
    public class ConfirmacionPedidoCompra
    {
        #region Crea el albaran de compra
        public static void CrearAlbaranCompra(string connectionString,string pedido, DateTime fecha)
        {
            var resultado = false;
            try
            {
                using var context = new DAL.WMS_3PL_Context(connectionString);

                var paramList = new[] {
                    new SqlParameter("@SuPedido", pedido),
                    new SqlParameter("@Fecha", fecha)};


                context.Database.ExecuteSqlRaw("EXEC BTOB.SP_CrearAlbaranes @SuPedido, @Fecha", parameters: paramList);


            }
            catch (Exception ex)
            {
                var mensajeError = ex.InnerException != null ? ex.Message + ", " + ex.InnerException.Message : ex.Message;
                DAL.Herramientas.GuardarError(new ENTITY.Errores.Errores("Libreria: BLL - Clase: PedidosCompras - Metodo: ModificarEstadoPedido", mensajeError.ToString()));
            }
          
        }
        #endregion

        #region Guardar el archivo procesado en base datos de BTOB
        public static bool GuardarArchivoProcesado(ENTITY.ConfirmacionPedidoCompra.ConfirmacionPedidoCompras pedido)
        {
            var resultado = false;
            try
            {
                using var context = new DAL.WMS_3PL_Context();

                var paramList = new[] {
                    new SqlParameter("@Pedido", pedido)};


                context.Database.ExecuteSqlRaw("EXEC PedidosCompra.SP_CrearAlbaran_PedidoCompra @Pedido", parameters: paramList);


            }
            catch (Exception ex)
            {
                var mensajeError = ex.InnerException != null ? ex.Message + ", " + ex.InnerException.Message : ex.Message;
                DAL.Herramientas.GuardarError(new ENTITY.Errores.Errores("Libreria: BLL - Clase: PedidosCompras - Metodo: ModificarEstadoPedido", mensajeError.ToString()));
            }
            return resultado;
        }
        #endregion

        #region Valida si existe un albaran para el pedido enviado
        public static bool ValidarAlbaranExiste(string connectionString, string documento)
        {
            var resultado = false;
            try
            {
                using var context = new DAL.WMS_3PL_Context(connectionString);

                var paramList = new[] {
                    new SqlParameter("@Documento", documento),
                    new Microsoft.Data.SqlClient.SqlParameter()
                    {
                        ParameterName = "@Respuesta",
                        SqlDbType = System.Data.SqlDbType.Bit,
                        Direction = System.Data.ParameterDirection.Output
                    } };


                context.Database.ExecuteSqlRaw("EXEC BTOB.SP_ValidarAlbaran_Existe @Documento, @Respuesta out", parameters: paramList);
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

        #region Compara el pedido del archivo recibido contra el pedido en base de datos del btob
        public static ENTITY.ConfirmacionPedidoCompra.RespuestaConfirmacionPedidoCompra DireferenciaPedidoCompras(string pedido, string documento)
        {
            ENTITY.ConfirmacionPedidoCompra.RespuestaConfirmacionPedidoCompra resultado= new ENTITY.ConfirmacionPedidoCompra.RespuestaConfirmacionPedidoCompra();


            try
            {
                using var context = new DAL.WMS_3PL_Context();

                var paramList = new[] {
                    new SqlParameter("@Pedido", pedido),
                    new SqlParameter("@Documento", documento),
                    new Microsoft.Data.SqlClient.SqlParameter()
                    {
                        ParameterName = "@Respuesta",
                        SqlDbType = System.Data.SqlDbType.Bit,
                        Direction = System.Data.ParameterDirection.Output
                    },  new Microsoft.Data.SqlClient.SqlParameter()
                    {
                        ParameterName = "@Diferencias",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Size=9000000,
                        Direction = System.Data.ParameterDirection.Output
                    }
                };


                context.Database.ExecuteSqlRaw("EXEC PedidosCompra.SP_PedidosCompra_Diferencias @Pedido, @Documento,@Respuesta out, @Diferencias output", parameters: paramList);
                resultado.JsonDiferencias = Convert.ToString(paramList[3].Value.ToString());
                resultado.Diferencias = Convert.ToBoolean(paramList[2].Value.ToString());

            }
            catch (Exception ex)
            {
                var mensajeError = ex.InnerException != null ? ex.Message + ", " + ex.InnerException.Message : ex.Message;
                DAL.Herramientas.GuardarError(new ENTITY.Errores.Errores("Libreria: BLL - Clase: PedidosCompras - Metodo: ModificarEstadoPedido", mensajeError.ToString()));
            }
            return resultado;
        }
        #endregion
    }
}
