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
        public static List<ENTITY.PedidosCompras.PedidosCompra> ObtenerPedidosPendientes()
        {

            try
            {


                var db = new DAL.WMS_3PL_Context();

                var pedido = db.PedidosCompra.FromSqlRaw("EXECUTE Catalogos.SP_PedidoEncabezado_Obtener").ToList();


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
        public static List<ENTITY.PedidosCompras.Lineas> ObtenerPedidosDetallePendientes()
        {

            try
            {


                var db = new DAL.WMS_3PL_Context();

                var pedido = db.Lineas.FromSqlRaw("EXECUTE Catalogos.SP_PedidoDetalle_Obtener").ToList();


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
    }
}
