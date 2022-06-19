using Microsoft.Extensions.Configuration;
using Renci.SshNet;
using Renci.SshNet.Sftp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Configuration;
using Microsoft.IdentityModel.Protocols;
using System.Linq;

namespace WMS_3PL_IntegrationService.UTILITY
{
    public class SFTP
    {

        public static void SendSFTP(string XMLfile, string NameFile, out bool enviadoSFTP, out string mensaje)
        {

            try
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                IConfiguration configuration = builder.Build();

                var enviado = false;
                var notificacion = string.Empty;
                var hostName = ConfigurationManager.AppSettings["HostSFTP"].ToString();
                var userName = ConfigurationManager.AppSettings["Usuario"].ToString();
                var portNumber = ConfigurationManager.AppSettings["Puerto"].ToString();
                var password = ConfigurationManager.AppSettings["pass"].ToString();
                var sftpDestinationUrl  = ConfigurationManager.AppSettings["ToWMS"].ToString();

                SftpClient client = new SftpClient(hostName, userName, password);
             
                client.Connect();

                var files = Directory.GetFiles(XMLfile);
                foreach (var file in files)
                {
                    //string remoteFileName = file;
                    using (Stream file1 = new FileStream(file, FileMode.Open))
                    {
                        //remoteFileName = Path.GetFileName(remoteFileName);

                        var fileSftp = sftpDestinationUrl + NameFile;
                        if (!client.Exists(fileSftp))
                        {
                            client.UploadFile(file1, fileSftp, null);
                            enviado = true;
                            notificacion = "Envio al SFTP con éxito";
                        }
                        else
                        {
                            enviado = false;
                            notificacion = "Enviado anteriormente al SFTP";
                        }
                    }
                }
                enviadoSFTP = enviado;
                mensaje = notificacion;
            }
            catch (Exception ex)
            {
                enviadoSFTP = false;
                mensaje = ex.ToString();
                // Error.SaveError(new ENTITY.Error("Library: Class: UploadSFTP - Method: SFTP ", ex.ToString()));

            }

        }



        /// <summary>
        /// Enumere un directorio remoto en la consola.
        /// </summary>
        private void listFiles()
        {
            string host = ConfigurationManager.AppSettings["HostSFTP"].ToString();
            string username = ConfigurationManager.AppSettings["Usuario"].ToString();
            string password = ConfigurationManager.AppSettings["pass"].ToString();
            string archivo = ConfigurationManager.AppSettings["NombreArchivoXML"].ToString();
            

            string remoteDirectory = ConfigurationManager.AppSettings["FromWMS"].ToString(); ;

            using (SftpClient sftp = new SftpClient(host, username, password))
            {
                try
                {
                    sftp.Connect();

                    var files = sftp.ListDirectory(remoteDirectory);

                    var x=files.Where(f => f.Name.Contains(archivo)).OrderByDescending(o => o.LastWriteTime).Take(1);

                    sftp.Disconnect();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Se ha detectado una excepción " + e.ToString());
                }
            }
        }
    }
}
