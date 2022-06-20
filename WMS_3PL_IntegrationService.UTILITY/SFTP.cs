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


        public static bool DownloadFile(string archivo, string carpeta, string extension)
        {
            var enviado = false;
            string host = ConfigurationManager.AppSettings["HostSFTP"].ToString();
            string username = ConfigurationManager.AppSettings["Usuario"].ToString();
            string password = ConfigurationManager.AppSettings["pass"].ToString();

            string remoteDirectory = ConfigurationManager.AppSettings["FromWMS"].ToString();

            using (SftpClient sftp = new SftpClient(host, username, password))
            {

                sftp.Connect();

                var files = sftp.ListDirectory(remoteDirectory);
                
                foreach (var file in files.Where(f => f.Name.Contains(archivo)).OrderByDescending(o => o.LastWriteTime).Take(1))
                {
                    using (Stream fileStream = File.Create(carpeta + archivo + extension))
                    {
                        sftp.DownloadFile(file.FullName, fileStream);
                    }
                    return enviado = true;
                  
                }


                sftp.Disconnect();

            }
            return enviado = false;
        }

        public static void MoveFileToProcessed(string archivo)
        {

            string host = ConfigurationManager.AppSettings["HostSFTP"].ToString();
            string username = ConfigurationManager.AppSettings["Usuario"].ToString();
            string password = ConfigurationManager.AppSettings["pass"].ToString();

            string remoteDirectory = ConfigurationManager.AppSettings["FromWMS"].ToString();

            using (SftpClient sftp = new SftpClient(host, username, password))
            {

                sftp.Connect();

                var files = sftp.ListDirectory(remoteDirectory);

                foreach (var file in files.Where(f => f.Name.Contains(archivo)).OrderByDescending(o => o.LastWriteTime).Take(1))
                {
                    file.MoveTo("/FromWMS/Processed/" + file.Name);
                 
                }


                sftp.Disconnect();

            }
        }

    }

}
