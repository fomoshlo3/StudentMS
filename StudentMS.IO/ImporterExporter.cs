using StudentMS.Models;
using System;
using System.Linq;
using System.Text;

namespace StudentMS.IO
{
    public static class ImporterExporter
    {
        /// <summary>
        /// ValueTask for importing 
        /// </summary>
        /// <param name="fqpn"></param>
        /// <returns></returns>
        public static async ValueTask<List<Student>> ImportCSV_Async(string fqpn) // I made this a valuetask, so it only returns if completed.
        {
            FileStream fs = File.Open(fqpn, FileMode.Open, FileAccess.Read);

            var students = new List<Student>();

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            using (var sr = new StreamReader(fs, Encoding.GetEncoding(1252), true))
            {
                string? line;
                while ((line = await sr.ReadLineAsync()) != null)
                {
                    if (!line.Contains("Vorname"))
                    {
                        var objects = line.Split(";");
                        students.Add(new Student(
                            objects[0], objects[1], objects[2]));
                    }
                }
            }
            return students;
        }

        /// <summary>
        /// Exports given <paramref name="dataList"/> at location of <paramref name="fqpn"/> 
        /// </summary>
        /// <param name="fqpn"></param>
        /// <param name="dataList"></param>
        /// <returns></returns>
        public static async Task ExportCSV_Async(string fqpn, List<Student> dataList)
        {
            if (File.Exists(fqpn) != true)
            {
                FileStream fs = File.Create(fqpn);
                using (var sr = new StreamWriter(fs, Encoding.GetEncoding(1252)))
                {
                    await sr.WriteLineAsync("Klasse;Vorname;Name;Email");
                    foreach (var item in dataList)
                    {
                        await sr.WriteLineAsync($"{item.Grade};{item.FirstName};{item.LastName};{item.Email}");
                    }
                    sr.Close();
                }
            }
        }
        //NOTE: Yeah i know no Exception Handling and all, i crunched Tasks all week but time is flying


    }
}

