using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using WMS_3PL_IntegrationService.UTILITY;

namespace WMS_3PL_IntegrationService.BLL.ConciliacionInventarios
{
    public class SendData
    {
       
        #region comparar el archivo recibido contra el stock si hay diferencias enviar reporte por correo
        public static void CheckWMS_3PLConciliacionInventario(string servidorBD, string nombreBD, string usuarioBD, string contrasennaBD)
        {
            try
            {
                var cadenaDeConexion = DAL.Herramientas.CreaCadena(servidorBD, nombreBD, usuarioBD, contrasennaBD);
                string extension = ConfigurationManager.AppSettings["ExtencionArchivoXML"].ToString();
                string raizArchivo = ConfigurationManager.AppSettings["NombreArchivoXML"].ToString();
                string carpeta = ConfigurationManager.AppSettings["CarpetaPedidos"].ToString();
                string hostSFTP = ConfigurationManager.AppSettings["HostSFTP"].ToString();
                string usernameSFTP = ConfigurationManager.AppSettings["Usuario"].ToString();
                string passwordSFTP = ConfigurationManager.AppSettings["pass"].ToString();
                string remoteDirectory = ConfigurationManager.AppSettings["FromWMS"].ToString();
                var anno = DateTime.Today.Year;
                var mes = DateTime.Now.ToString("MMMM");
                var dia = DateTime.Today.Day;

                var carpetaInventario = carpeta + anno + "\\" + mes + "\\" + dia + "\\";
                System.IO.FileInfo filePath = new System.IO.FileInfo(carpetaInventario);
                filePath.Directory.Create();

                using (SftpClient sftp = new SftpClient(hostSFTP, usernameSFTP, passwordSFTP))
                {

                    sftp.Connect();

                    var files = sftp.ListDirectory(remoteDirectory);

                    foreach (var file in files.Where(f => f.Name.Contains(raizArchivo)).OrderByDescending(o => o.LastWriteTime))
                    {
                        using (Stream fileStream = File.Create(carpetaInventario + file.Name))
                        {
                            sftp.DownloadFile(file.FullName, fileStream);

                        }

                        ProcesarConciliacionInventario(carpetaInventario + file.Name, cadenaDeConexion, file.Name);
                    }


                    sftp.Disconnect();

                }
            }
            catch (Exception ex)
            {
                var mensaje = ex.InnerException != null ? ex.Message + ", " + ex.InnerException.Message : ex.Message;
                DAL.Herramientas.GuardarError(new ENTITY.Errores.Errores("Libreria: BLL - Clase: SendData - Metodo: CheckWMS_3PLConciliacionInventario", mensaje.ToString()));

            }

        }

        #endregion

        #region Procesa el archivo de ajuste de inventario ymueve al archivo a otra carpeta para no procesarlo mas 
        public static void ProcesarConciliacionInventario(string carpeta, string cadenaDeConexion, string archivo)
        {
            var conciliacionInventario = XML.DeserializeToObject<ENTITY.ConciliacionInventarios.ConciliacionInventarios>(carpeta);

            var codAlmacen = conciliacionInventario.Lista_ConciliacionInventario.Select(x => x.Bodega).FirstOrDefault();
            var fecha = conciliacionInventario.Lista_ConciliacionInventario.Select(x => x.Fecha).FirstOrDefault();


            var jsonAjusteInventario = Newtonsoft.Json.JsonConvert.SerializeObject(conciliacionInventario);

            var diferenciasConciliacion = DAL.ConciliacionInventario.ConciliacionInventario.DireferenciaConciliacionInventario(jsonAjusteInventario);

          
            if (!diferenciasConciliacion.Diferencias)
            {
                UTILITY.Notificacion.MailNotification("Pedido con diferencias", "El archivo de confirmación tiene diferencias, revisar por favor");
                UTILITY.SFTP.MoveFileToProcessed(archivo, "Processed");
            }
            else
            {
                UTILITY.Notificacion.MailNotification("Conciliación de Inventario", "El archivo de confirmación tiene diferencias, revisar por favor");
                UTILITY.SFTP.MoveFileToProcessed(archivo, "Error");
            }

            

            UTILITY.Notificacion.MailNotification("Conciliación de Inventario", "El archivo de conciliación de inventario tiene diferencias, revisar por favor");

            UTILITY.SFTP.MoveFileToProcessed(archivo, "Processed");




        }
        #endregion
    }
}
