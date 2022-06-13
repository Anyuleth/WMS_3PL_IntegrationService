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
        #region Obtener los articulos pendientes de enviar 
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

            }
            return null;
        }
        #endregion
    }
}
