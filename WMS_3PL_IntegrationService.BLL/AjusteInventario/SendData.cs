using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using WMS_3PL_IntegrationService.UTILITY;

namespace WMS_3PL_IntegrationService.BLL.AjusteInventario
{
    public class SendData
    {

        #region Leer el archivo “Ajuste Inventario.xml“ y ajustar el inventario con el procedimiento[WMS].[SP_CrearInventarios] en las bases de datos de Gestión.
        public static void CheckWMS_3PLInventario(string servidorBD, string nombreBD, string usuarioBD, string contrasennaBD)
        {
            try
            {
                var cadenaDeConexion = DAL.Herramientas.CreaCadena(servidorBD, nombreBD, usuarioBD, contrasennaBD);
                string raizArchivo = ConfigurationManager.AppSettings["NombreArchivoXML"].ToString();
                string carpeta = ConfigurationManager.AppSettings["CarpetaPedidos"].ToString();
                string host = ConfigurationManager.AppSettings["HostSFTP"].ToString();
                string username = ConfigurationManager.AppSettings["Usuario"].ToString();
                string password = ConfigurationManager.AppSettings["pass"].ToString();
                string remoteDirectory = ConfigurationManager.AppSettings["FromWMS"].ToString();
                var anno = DateTime.Today.Year;
                var mes = DateTime.Now.ToString("MMMM");
                var dia = DateTime.Today.Day;

                var carpetaInventario = carpeta+ nombreBD + anno + "\\" + mes + "\\" + dia + "\\";
                System.IO.FileInfo filepath = new System.IO.FileInfo(carpetaInventario);
                filepath.Directory.Create();

               
               

                using (SftpClient sftp = new SftpClient(host, username, password))
                {

                    sftp.Connect();

                    var files = sftp.ListDirectory(remoteDirectory);

                    foreach (var file in files.Where(f => f.Name.Contains(raizArchivo)).OrderByDescending(o => o.LastWriteTime))
                    {
                        using (Stream fileStream = File.Create(carpetaInventario+file.Name))
                        {
                            sftp.DownloadFile(file.FullName, fileStream);
                           
                        }

                        ProcesarAjusteInvenatario(carpetaInventario + file.Name, cadenaDeConexion, file.Name);
                    }


                    sftp.Disconnect();

                }



               

            }
            catch (Exception ex)
            {
                var mensaje = ex.InnerException != null ? ex.Message + ", " + ex.InnerException.Message : ex.Message;
                DAL.Herramientas.GuardarError(new ENTITY.Errores.Errores("Libreria: BLL - Clase: SendData - Metodo: CheckWMS_3PLInventario", mensaje.ToString()));

            }

        }

        #endregion

        #region Procesa el archivo de ajuste de inventario ymueve al archivo a otra carpeta para no procesarlo mas 
        public static void ProcesarAjusteInvenatario(string carpeta, string cadenaDeConexion, string archivo)
        {
            try
            {
                var ajusteInventario = XML.DeserializeToObject<ENTITY.AjusteInventario.AjusteInventarios>(carpeta);

                var codAlmacen = ajusteInventario.Lista_ajusteInventario.Select(x => x.Bodega).FirstOrDefault().Split('-').Last();
                var fecha = ajusteInventario.Lista_ajusteInventario.Select(x => x.Fecha).FirstOrDefault();


                var jsonAjusteInventario = Newtonsoft.Json.JsonConvert.SerializeObject(ajusteInventario);

                var existeInventario = DAL.AjusteInventario.AjusteInventario.ValidarInventarioExiste(cadenaDeConexion, fecha);

                if (!existeInventario)
                {

                    DAL.AjusteInventario.AjusteInventario.CrearInventario(cadenaDeConexion, codAlmacen, jsonAjusteInventario, fecha);

                    UTILITY.SFTP.MoveFileToProcessed(archivo,"Processed");
                }
            }
            catch (Exception ex )
            {
                    UTILITY.SFTP.MoveFileToProcessed(archivo, "Error");

                var mensaje = ex.InnerException != null ? ex.Message + ", " + ex.InnerException.Message : ex.Message;
                DAL.Herramientas.GuardarError(new ENTITY.Errores.Errores("Libreria: BLL - Clase: SendData - Metodo: ProcesarAjusteInvenatario", mensaje.ToString()));
            }
           



        }
        #endregion

    }
}
