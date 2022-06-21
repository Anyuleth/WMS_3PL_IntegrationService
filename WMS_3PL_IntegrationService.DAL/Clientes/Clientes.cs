using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace WMS_3PL_IntegrationService.DAL.Clientes
{
    public class Clientes
    {
        #region Obtener los clientes pendientes de enviar al SFTP
        public static List<ENTITY.Cliente> ObtenerClientesPendientes()
        {

            try
            {


                var db = new DAL.WMS_3PL_Context();

                var vListaClientes = db.Clientes.FromSqlRaw("EXECUTE Catalogos.SP_Clientes_Obtener").ToList();


                return vListaClientes;

            }
            catch (Exception ex)
            {
                var mensaje = ex.InnerException != null ? ex.Message + ", " + ex.InnerException.Message : ex.Message;
                DAL.Herramientas.GuardarError(new ENTITY.Errores.Errores("Libreria: BLL - Clase: Clientes - Metodo: ObtenerClientesPendientes", mensaje.ToString()));
            }
            return null;
        }
        #endregion
    }
}
