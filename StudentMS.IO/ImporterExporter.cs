using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using DocumentFormat.OpenXml;
using Newtonsoft.Json;
using StudentMS.Models;

namespace StudentMS.IO
{
    public class ImporterExporter
    {
        public static List<Student> ImportCSV(string fqpn)
        {
            FileStream fs = File.Open(fqpn, FileMode.Open, FileAccess.Read);

            var students = new List<Student>();

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            using (var sr = new StreamReader(fs, Encoding.GetEncoding(1252), true))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (!line.Contains("Vorname"))
                    {
                        var objects = line.Split(";");
                        students.Add(new Student (
                            objects[0], objects[1], objects[2]));
                    }
                }
            }
            return students;
        }

        //public static async Task<List<T>> ImportXmlAsync<T>(string fqpn)
        //{
        //    FileStream fs =  File.Open(fqpn, FileMode.Open, FileAccess.Read);
            
        //    var List = new List<Task<T>>();
        //    XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
        //}
    }
}

