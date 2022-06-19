using Quartz;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Xml.Serialization;
using WMS_3PL_IntegrationService.UTILITY;
using WMS_3PL_IntegrationService.ENTITY.ConfirmacionPedidoCompra;
using System.Linq;

namespace WMS_3PL_IntegrationService.BLL.ConfirmacionPedidoCompra
{
    public class SendData
    {

        public static void CheckWMS_3PLPedidos(string servidorBD, string nombreBD, string usuarioBD, string contrasennaBD)
        {
            try
            {
                //CONECTARSE AL SFTP PARA LEER ESE ARCHIVO

                string carpeta = ConfigurationManager.AppSettings["CarpetaPedidos"].ToString();
             

                var cadenaDeConexion = DAL.Herramientas.CreaCadena(servidorBD, nombreBD, usuarioBD, contrasennaBD);

                var pedido = XML.DeserializeToObject<ENTITY.ConfirmacionPedidoCompra.ConfirmacionPedidoCompras>(carpeta) ;
                var documento = pedido.Lista_articulos.Select(x => x.Documento).FirstOrDefault();
                var fecha = pedido.Lista_articulos.Select(x => x.Fecha_Transmision).FirstOrDefault();


                var jsonPedido = Newtonsoft.Json.JsonConvert.SerializeObject(pedido);


                //1.validar que el pedido no tenga un albaran creado
                var existeAlbaran = DAL.ConfirmacionPedidoCompra.ConfirmacionPedidoCompra.ValidarAlbaranExiste(cadenaDeConexion, documento);
               
                if (!existeAlbaran)
                {
                    //2.validar lineas del pedido recibido contra las lineas del pedido decompra enviado
                    var diferenciasPedido= DAL.ConfirmacionPedidoCompra.ConfirmacionPedidoCompra.DireferenciaPedidoCompras(jsonPedido, documento);

                    if (!diferenciasPedido.Diferencias)
                    {
                        //Crear el albarán de compra para ese pedido
                        DAL.ConfirmacionPedidoCompra.ConfirmacionPedidoCompra.CrearAlbaranCompra(cadenaDeConexion, documento, fecha);

                        //Hacer tabla para insertar los archivos procesados y Marcar el pedido de compra como procesado para no leerlo mas .
                        DAL.ConfirmacionPedidoCompra.ConfirmacionPedidoCompra.GuardarArchivoProcesado(pedido);

                       
                    }
                    else
                    {
                        UTILITY.Notificacion.MailNotification("Pedido con diferencias", "Ya existe un albaran para el pedido "+ documento);
                    }
                
                }
                else
                {
                    //sino envia notificacion con el error de que ya existe albaran para ese pedi
                    UTILITY.Notificacion.MailNotification("Pedido ya existe","Ya existe un albaran para el pedido " + documento);
                }


                
            }
            catch (Exception ex)
            {
                var mensaje = ex.InnerException != null ? ex.Message + ", " + ex.InnerException.Message : ex.Message;
                DAL.Herramientas.GuardarError(new ENTITY.Errores.Errores("Libreria: BLL - Clase: ProcessData - Metodo: SendWMS_3PLPedidos", mensaje.ToString()));

            }

        }
      

    }
}
