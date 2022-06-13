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
        #region Obtener los pedidos pendientes de enviar 
        public static List<ENTITY.PedidosCompras.PedidosCompra> ObtenerPedidosPendientes()
        {

            try
            {


                var db = new DAL.WMS_3PL_Context();

                var pedido = db.PedidosCompra.FromSqlRaw("EXECUTE Catalogos.SP_Clientes_Obtener").ToList();


                return pedido;

            }
            catch (Exception ex)
            {

            }
            return null;
        }
        #endregion
    }
}
