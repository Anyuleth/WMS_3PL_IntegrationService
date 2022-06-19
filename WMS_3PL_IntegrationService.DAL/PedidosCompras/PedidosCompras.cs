using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace WMS_3PL_IntegrationService.DAL.PedidosCompras
{
    public class PedidosCompras
    {
        #region Obtener los encabezados de los pedidos pendientes de enviar 
        public static List<ENTITY.PedidosCompras.Encabezaddo> ObtenerPedidosPendientes()
        {

            try
            {


                var db = new DAL.WMS_3PL_Context();

                var pedido = db.PedidosCompraEncabezado.FromSqlRaw("EXECUTE Catalogos.SP_PedidoEncabezado_Obtener").ToList();


                return pedido;

            }
            catch (Exception ex)
            {
                var mensaje = ex.InnerException != null ? ex.Message + ", " + ex.InnerException.Message : ex.Message;
                DAL.Herramientas.GuardarError(new ENTITY.Errores.Errores("Libreria: BLL - Clase: PedidosCompras - Metodo: ObtenerPedidosPendientes", mensaje.ToString()));
            }
            return null;
        }
        #endregion

        #region Obtener los pedidos pendientes de enviar 
        public static List<ENTITY.PedidosCompras.Lineas> ObtenerPedidosDetallePendientes(string idPedido)
        {

            try
            {


                var db = new DAL.WMS_3PL_Context();
                var paramList = new[] {
                    new SqlParameter("@idPedido", idPedido)};
                var pedido = db.Detalle.FromSqlRaw("EXECUTE Catalogos.SP_PedidoCompraDetalle_Pendientes @idPedido", parameters: paramList).ToList();

                

                return pedido;

            }
            catch (Exception ex)
            {
                var mensaje = ex.InnerException != null ? ex.Message + ", " + ex.InnerException.Message : ex.Message;
                DAL.Herramientas.GuardarError(new ENTITY.Errores.Errores("Libreria: BLL - Clase: PedidosCompras - Metodo: ObtenerPedidosDetallePendientes", mensaje.ToString()));
            }
            return null;
        }
        #endregion

        #region Validar si existe factura
        public static bool ModificarEstadoPedido(string idPedido, string estado, string mensaje)
        {
            var resultado = false;
            try
            {
                using var context =  new DAL.WMS_3PL_Context();

                var paramList = new[] {
                    new SqlParameter("@IdPedido", idPedido),
                    new SqlParameter("@Estado", estado), 
                    new SqlParameter("@Mensaje", mensaje)};


                context.Database.ExecuteSqlRaw("EXEC PedidosCompra.SP_CambiarEstado_PedidoCompra @IdPedido,@Estado, @Mensaje", parameters: paramList);


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
