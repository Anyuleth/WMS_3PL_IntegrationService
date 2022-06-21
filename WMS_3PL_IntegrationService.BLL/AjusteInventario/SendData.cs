using System;
using System.Collections.Generic;
using System.Configuration;
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
                    ProcesarAjusteInvenatario(carpetaInventario + archivo + extension, cadenaDeConexion, archivo);
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
            var ajusteInventario = XML.DeserializeToObject<ENTITY.AjusteInventario.AjusteInventarios>(carpeta);

            var codAlmacen = ajusteInventario.Lista_ajusteInventario.Select(x => x.Bodega).FirstOrDefault();
            var fecha = ajusteInventario.Lista_ajusteInventario.Select(x => x.Fecha).FirstOrDefault();


            var jsonAjusteInventario = Newtonsoft.Json.JsonConvert.SerializeObject(ajusteInventario);


            DAL.AjusteInventario.AjusteInventario.CrearInventario(cadenaDeConexion, codAlmacen, jsonAjusteInventario, fecha);

            UTILITY.SFTP.MoveFileToProcessed(archivo);




        }
        #endregion

    }
}
