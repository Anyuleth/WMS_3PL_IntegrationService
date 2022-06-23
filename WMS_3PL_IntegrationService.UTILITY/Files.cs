using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace WMS_3PL_IntegrationService.UTILITY
{
    public class Files
    {

        public static void LogInformation(string source, string log)
        {
            var fecha = string.Format("{0:yyyy-MM-dd}", DateTime.Today);
            string logFile = AppDomain.CurrentDomain.BaseDirectory + fecha + " - LogInformation.txt";

            StreamWriter sw = new StreamWriter(logFile, true);
            sw.WriteLine("********** {0} **********", DateTime.Now);
            
            sw.Write(log);
            sw.WriteLine(source);
           
            sw.Close();
        }
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

        public static string FileName(string reportName, string dateFormatted)
        {

            string carpeta = AppDomain.CurrentDomain.BaseDirectory + @"\FTP\Resources\FromWMS";
            string logFile = carpeta + @"\" + reportName + "_" + dateFormatted + ".xml";
            return logFile;
        }

        private void SerializeElement(string filename)
        {
            XmlSerializer ser = new XmlSerializer(typeof(XmlElement));
            XmlElement myElement =
            new XmlDocument().CreateElement("MyElement", "ns");
            myElement.InnerText = "Hello World";
            TextWriter writer = new StreamWriter(filename);
            ser.Serialize(writer, myElement);
            writer.Close();
        }

        public static void CreateImportDataCsv(List<string> csvData, string reportName)
        {
            DateTime date = DateTime.Now;
            string dateFormatted = date.ToString("yyyyMMdd");
            try
            {


                string csvFilePath = FileName(reportName, dateFormatted);
                System.IO.File.WriteAllLines(csvFilePath, csvData);

                SplitCsv(csvFilePath, reportName);
            }
            catch (Exception ex)
            {


                
            }

        }

        public static List<string> ImportDataCsv(object reportObject)
        {
            IEnumerable<string> csvGenerated = new List<string>();
            try
            {
                var enumerable = reportObject as IEnumerable;
                var list = enumerable.Cast<object>().ToList();

                csvGenerated = from invoice in list
                               let dataLine = string.Join(";", invoice.GetType()
                               .GetProperties().Select(p => p.GetValue(invoice)))
                               select dataLine;

            }
            catch (Exception ex)
            {


            
            }
            return csvGenerated.ToList();
        }



        public static void SplitCsv(string csvFilePath, string reportName)
        {
            using (System.IO.StreamReader sr = new System.IO.StreamReader(csvFilePath))
            {
                var fecha = string.Format("{0:yyyyMMdd}", DateTime.Today);
                var anno = DateTime.Today.Year;
                var mes = DateTime.Now.ToString("MMMM");
                var dia = DateTime.Today.Day;
                string PathSFTP_CSV = AppDomain.CurrentDomain.BaseDirectory + "\\FTP\\Resources\\FromWMS\\" + anno + "\\" + mes + "\\" + dia + "\\";
                System.IO.FileInfo file = new System.IO.FileInfo(PathSFTP_CSV);
                file.Directory.Create(); // If the directory already exists, this method does nothing.

                string logFile = PathSFTP_CSV + @"\" + reportName + "_" + fecha + "_";

                int fileNumber = 0;



                while (!sr.EndOfStream)
                {
                    int count = -1;
                    using (System.IO.StreamWriter sw = new System.IO.StreamWriter(logFile + string.Format("{0:000}", ++fileNumber) + ".xml"))
                    {
                        sw.AutoFlush = true;

                        while (!sr.EndOfStream && ++count < 10000)
                        {
                            sw.WriteLine(sr.ReadLine());

                        }

                    }

                }
              //  UploadSFTP.SFTP(PathSFTP_CSV);
            }

        }

    }
}