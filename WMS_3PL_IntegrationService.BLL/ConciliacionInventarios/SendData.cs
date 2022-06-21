using System;
using System.Collections.Generic;
using System.Configuration;
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
                string archivo = ConfigurationManager.AppSettings["NombreArchivoXML"].ToString();
                string carpeta = ConfigurationManager.AppSettings["CarpetaPedidos"].ToString();

                var anno = DateTime.Today.Year;
                var mes = DateTime.Now.ToString("MMMM");
                var dia = DateTime.Today.Day;

                var carpetaInventario = carpeta + anno + "\\" + mes + "\\" + dia + "\\";
                System.IO.FileInfo file = new System.IO.FileInfo(carpetaInventario);
                file.Directory.Create();



                var readFile = UTILITY.SFTP.DownloadFile(archivo, carpetaInventario, extension);
                if (readFile)
                {
                    ProcesarConciliacionInventario(carpetaInventario + archivo + extension, cadenaDeConexion, archivo);
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


            //llamar procedimiento almacenado para comprar el stock del btob contra el del archivo

            UTILITY.SFTP.MoveFileToProcessed(archivo);




        }
        #endregion
    }
}
