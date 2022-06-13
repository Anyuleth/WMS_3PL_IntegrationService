using Renci.SshNet;
using Renci.SshNet.Sftp;
using System;
using System.Collections.Generic;
using System.IO;

namespace WMS_3PL_IntegrationService.UTILITY
{
    public class SFTP
    {

        public static void SendSFTP(string XMLfile, string NameFile )
        {

            try
            {
              


                var hostName = @"172.23.24.54";
                var userName = "anyuleth.cortes";
                var portNumber = "22";
                var password = @"_vNNtL%x;rkb9/G}";
                var sftpDestinationUrl = @"/FromWMS/";

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
                        }
                        else
                        {
                          //  Notification.SaveNotification(new ENTITY.Notification("Library: Class: UploadSFTP - Method: SFTP", "El archivo: " + fileSftp + " ya fue enviado.  "));
                        }
                    }
                }

            }
            catch (Exception ex)
            {
               // Error.SaveError(new ENTITY.Error("Library: Class: UploadSFTP - Method: SFTP ", ex.ToString()));

            }

        }



        /// <summary>
        /// Enumere un directorio remoto en la consola.
        /// </summary>
        private void listFiles()
        {
            string host = @"172.23.24.54";
            string username = "anyuleth.cortes";
            string password = @"cc3s3f1126..";
    
            string remoteDirectory = "/toWMS";

            using (SftpClient sftp = new SftpClient(host, username, password))
            {
                try
                {
                    sftp.Connect();

                    var files = sftp.ListDirectory(remoteDirectory);

                    foreach (var file in files)
                    {
                        Console.WriteLine(file.Name);
                    }

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
