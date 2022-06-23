using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using WMS_3PL_IntegrationService.ENTITY.PedidosCompras;

namespace WMS_3PL_IntegrationService.BLL.PedidosCompras
{
    public class SendData
    {
        #region Enviar los pedidos de compras creados en ICG Manager y enviarlos al BTOB para enviarlos al SFTP
        public static void SendWMS_3PLPedidos()
        {

           
            try
            {
                List<ENTITY.PedidosCompras.Encabezaddo> encabezados = new List<ENTITY.PedidosCompras.Encabezaddo>();
                encabezados = DAL.PedidosCompras.PedidosCompras.ObtenerPedidosPendientes();
                
                if (encabezados.Count > 0)
                {

                    var carpeta = ConfigurationManager.AppSettings["CarpetaPedidos"].ToString();
                    var nombreArchivoXML = ConfigurationManager.AppSettings["NombreArchivoXML"].ToString();
                    var extencionArchivoXML = ConfigurationManager.AppSettings["ExtencionArchivoXML"].ToString();

                    var anno = DateTime.Today.Year;
                    var mes = DateTime.Now.ToString("MMMM");
                    var dia = DateTime.Today.Day;

                    var carpetaPedidos = carpeta + anno + "\\" + mes + "\\" + dia + "\\";
                    System.IO.FileInfo file = new System.IO.FileInfo(carpetaPedidos);
                    file.Directory.Create();

                    List<ENTITY.PedidosCompras.Lineas> lineas = new List<ENTITY.PedidosCompras.Lineas>();
                    foreach (var item in encabezados)
                    {

                        var enviadoSFTP = false;
                        var mensaje = string.Empty;
                        lineas = DAL.PedidosCompras.PedidosCompras.ObtenerPedidosDetallePendientes(item.IdPedido);

                        Encabezaddo encabezado = new Encabezaddo();
                        encabezado.Empresa = item.Empresa;
                        encabezado.Documento = item.Documento;
                        encabezado.Tipo = item.Tipo;
                        encabezado.Fecha_Entrada = item.Fecha_Entrada;
                        encabezado.Fecha_transmision = item.Fecha_transmision;

                        Detalle detalle = new Detalle();
                        detalle.Lineas = lineas;

                        ENTITY.PedidosCompras.PedidosCompras pedidos = new ENTITY.PedidosCompras.PedidosCompras();
                        pedidos.Encabezado = encabezado;
                        pedidos.Detalle = detalle;

                        XmlSerializer serialiser = new XmlSerializer(typeof(ENTITY.PedidosCompras.PedidosCompras));
                        UTILITY.XML.CreateXML(carpetaPedidos + nombreArchivoXML+ item.Empresa + item.Serie + extencionArchivoXML, pedidos, serialiser);

                        UTILITY.SFTP.SendSFTP(carpetaPedidos, nombreArchivoXML + item.Empresa + item.Serie + extencionArchivoXML, out enviadoSFTP, out mensaje);

                        if (enviadoSFTP)
                        {
                            DAL.PedidosCompras.PedidosCompras.ModificarEstadoPedido(item.IdPedido, "ENVIADO", mensaje);

                        }
                        else
                        {
                            DAL.PedidosCompras.PedidosCompras.ModificarEstadoPedido(item.IdPedido, "ENVIADO", mensaje);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                var mensaje = ex.InnerException != null ? ex.Message + ", " + ex.InnerException.Message : ex.Message;
                DAL.Herramientas.GuardarError(new ENTITY.Errores.Errores("Libreria: BLL - Clase: ProcessData - Metodo: SendWMS_3PLPedidos", mensaje.ToString()));

            }


        }
        #endregion


    }
}
