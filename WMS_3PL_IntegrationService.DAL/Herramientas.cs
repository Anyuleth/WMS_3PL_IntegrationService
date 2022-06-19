using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WMS_3PL_IntegrationService.DAL
{
    public class Herramientas
    {

        public static string CreaCadena(string servidorBD, string nombreBD, string usuarioBD, string contrasenaBD)
        {
            var cadenaDeConexion = new StringBuilder();
            cadenaDeConexion.Append("Data Source= " + servidorBD);
            cadenaDeConexion.Append("; Initial Catalog= " + nombreBD);
            cadenaDeConexion.Append("; User ID= " + usuarioBD);
            cadenaDeConexion.Append("; Password= " + contrasenaBD);
            return cadenaDeConexion.ToString();
        }
        #region Registra errores en la Capa de Acceso a Datos

        public static void LogException(Exception exc, string source)
        {
            var fecha = string.Format("{0:yyyy-MM-dd}", DateTime.Today);
            string logFile = AppDomain.CurrentDomain.BaseDirectory + fecha + " - LogErrores.txt";

            StreamWriter sw = new StreamWriter(logFile, true);
            sw.WriteLine("********** {0} **********", DateTime.Now);
            if (exc.InnerException != null)
            {
                sw.Write("Inner Exception Type: ");
                sw.WriteLine(exc.InnerException.GetType().ToString());
                sw.Write("Inner Exception: ");
                sw.WriteLine(exc.InnerException.Message);
                sw.Write("Inner Source: ");
                sw.WriteLine(exc.InnerException.Source);
                if (exc.InnerException.StackTrace != null)
                {
                    sw.WriteLine("Inner Stack Trace: ");
                    sw.WriteLine(exc.InnerException.StackTrace);
                }
            }
            sw.Write("Exception Type: ");
            sw.WriteLine(exc.GetType().ToString());
            sw.WriteLine("Exception: " + exc.Message);
            sw.WriteLine("Source: " + source);
            sw.WriteLine("Stack Trace: ");
            if (exc.StackTrace != null)
            {
                sw.WriteLine(exc.StackTrace);
                sw.WriteLine();
            }
            sw.Close();
        }
        #endregion

        public static void GuardarError(ENTITY.Errores.Errores entidad )
        {
            string vcadenaDeConexion = string.Empty;
            try
            {

                using var context = new DAL.WMS_3PL_Context();

                var paramList = new[] {
                    new SqlParameter("@Ubicacion", entidad.Ubicacion),
                    new SqlParameter("@Informacion", entidad.Informacion)};


                context.Database.ExecuteSqlRaw("EXEC Registro.SP_Errores_Guardar @Ubicacion,@Informacion", parameters: paramList);


            }
            catch (Exception ex)
            {
                LogException(ex, "Libreria: DAL - Clase: Herramientas - Metodo: GuardarError");
            }
        }

    }
}
