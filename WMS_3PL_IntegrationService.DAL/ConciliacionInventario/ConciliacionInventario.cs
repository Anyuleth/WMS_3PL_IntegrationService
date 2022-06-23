using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace WMS_3PL_IntegrationService.DAL.ConciliacionInventario
{
    public class ConciliacionInventario
    {
        #region Compara el archivo de conciliacion recibido contra el stock en base de datos del btob y devuelve las diferencias
        public static ENTITY.ConciliacionInventarios.RespuestaConciliacionInventarios DireferenciaConciliacionInventario(string conciliacion)
        {
            ENTITY.ConciliacionInventarios.RespuestaConciliacionInventarios resultado = new ENTITY.ConciliacionInventarios.RespuestaConciliacionInventarios();


            try
            {
                using var context = new DAL.WMS_3PL_Context();

                var paramList = new[] {
                    new SqlParameter("@Conciliacion", conciliacion),
                    new Microsoft.Data.SqlClient.SqlParameter()
                    {
                        ParameterName = "@Respuesta",
                        SqlDbType = System.Data.SqlDbType.Bit,
                        Direction = System.Data.ParameterDirection.Output
                    },  new Microsoft.Data.SqlClient.SqlParameter()
                    {
                        ParameterName = "@Diferencias",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Size=9000000,
                        Direction = System.Data.ParameterDirection.Output
                    }
                };


                context.Database.ExecuteSqlRaw("EXEC PedidosCompra.SP_PedidosCompra_Diferencias @Conciliacion,@Respuesta out, @Diferencias output", parameters: paramList);
                resultado.JsonDiferencias = Convert.ToString(paramList[3].Value.ToString());
                resultado.Diferencias = Convert.ToBoolean(paramList[2].Value.ToString());

            }
            catch (Exception ex)
            {
                var mensajeError = ex.InnerException != null ? ex.Message + ", " + ex.InnerException.Message : ex.Message;
                DAL.Herramientas.GuardarError(new ENTITY.Errores.Errores("Libreria: BLL - Clase: ConciliacionInventario - Metodo: DireferenciaConciliacionInventario", mensajeError.ToString()));
            }
            return resultado;
        }
        #endregion
    }
}
