using Quartz;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Xml.Serialization;
using WMS_3PL_IntegrationService.UTILITY;
using WMS_3PL_IntegrationService.ENTITY.ConfirmacionPedidoCompra;
using System.Linq;
using Renci.SshNet;
using System.IO;

namespace WMS_3PL_IntegrationService.BLL.ConfirmacionPedidoCompra
{
    public class SendData
    {
        #region Compara los pedidos de compra del BTOB con los del  SFTP
        public static void CheckWMS_3PLPedidos(string servidorBD, string nombreBD, string usuarioBD, string contrasennaBD)
        {
            try
            {
                var cadenaDeConexion = DAL.Herramientas.CreaCadena(servidorBD, nombreBD, usuarioBD, contrasennaBD);
                string raizArchivo = ConfigurationManager.AppSettings["NombreArchivoXML"].ToString();
                string carpeta = ConfigurationManager.AppSettings["CarpetaPedidos"].ToString();
                string hostSFTP = ConfigurationManager.AppSettings["HostSFTP"].ToString();
                string usernameSFTP = ConfigurationManager.AppSettings["Usuario"].ToString();
                string passwordSFTP = ConfigurationManager.AppSettings["pass"].ToString();
                string remoteDirectory = ConfigurationManager.AppSettings["FromWMS"].ToString();
                var anno = DateTime.Today.Year;
                var mes = DateTime.Now.ToString("MMMM");
                var dia = DateTime.Today.Day;

                var carpetaPedidos = carpeta + nombreBD + anno + "\\" + mes + "\\" + dia + "\\";
                System.IO.FileInfo filePath = new System.IO.FileInfo(carpetaPedidos);
                filePath.Directory.Create();

              

                using (SftpClient sftp = new SftpClient(hostSFTP, usernameSFTP, passwordSFTP))
                {

                    sftp.Connect();

                    var files = sftp.ListDirectory(remoteDirectory);

                    foreach (var file in files.Where(f => f.Name.Contains(raizArchivo)).OrderByDescending(o => o.LastWriteTime))
                    {
                        using (Stream fileStream = File.Create(carpetaPedidos + file.Name))
                        {
                            sftp.DownloadFile(file.FullName, fileStream);

                        }
                      
                        ProcesarPedidoCompra(carpetaPedidos + file.Name, cadenaDeConexion, file.Name);
                    }


                    sftp.Disconnect();

                }


              

            }
            catch (Exception ex)
            {
                var mensaje = ex.InnerException != null ? ex.Message + ", " + ex.InnerException.Message : ex.Message;
                DAL.Herramientas.GuardarError(new ENTITY.Errores.Errores("Libreria: BLL - Clase: ProcessData - Metodo: SendWMS_3PLPedidos", mensaje.ToString()));

            }

        }
        #endregion

        #region Procesa los archivos si el pedido de confirmacion es correcto, creará albaran de compra
        public static void ProcesarPedidoCompra(string carpeta, string cadenaDeConexion, string archivo)
        {
            var pedido = XML.DeserializeToObject<ENTITY.ConfirmacionPedidoCompra.ConfirmacionPedidoCompras>(carpeta);
            var documento = pedido.Lista_articulos.Select(x => x.Documento).FirstOrDefault();
            var fecha = pedido.Lista_articulos.Select(x => x.Fecha_Transmision).FirstOrDefault();


            var jsonPedido = Newtonsoft.Json.JsonConvert.SerializeObject(pedido);


            var existeAlbaran = DAL.ConfirmacionPedidoCompra.ConfirmacionPedidoCompra.ValidarAlbaranExiste(cadenaDeConexion, documento);

            if (!existeAlbaran)
            {
                var diferenciasPedido = DAL.ConfirmacionPedidoCompra.ConfirmacionPedidoCompra.DireferenciaPedidoCompras(jsonPedido, documento);

                if (!diferenciasPedido.Diferencias)
                {
                    DAL.ConfirmacionPedidoCompra.ConfirmacionPedidoCompra.CrearAlbaranCompra(cadenaDeConexion, documento, fecha);
                    UTILITY.SFTP.MoveFileToProcessed(archivo, "Processed");
                }
                else
                {
                    UTILITY.Notificacion.MailNotification("Pedido con diferencias", "El archivo de confirmación tiene diferencias, revisar por favor" + documento);
                    UTILITY.SFTP.MoveFileToProcessed(archivo, "Error");
                }

            }
            else
            {
                UTILITY.Notificacion.MailNotification("Pedido ya existe", "Ya existe un albaran para el pedido " + documento);
                UTILITY.SFTP.MoveFileToProcessed(archivo, "Processed");
            }

        }
        #endregion

       

    }
}
