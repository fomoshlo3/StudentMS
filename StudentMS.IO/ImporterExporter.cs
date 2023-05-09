using StudentMS.Models;
using System;
using System.Linq;
using System.Text;

namespace StudentMS.IO
{
    public static class ImporterExporter
    {
        public static List<Student> ImportCSV(string fqpn)
        {
            FileStream fs = File.Open(fqpn, FileMode.Open, FileAccess.Read);

            var students = new List<Student>();

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            using (var sr = new StreamReader(fs, Encoding.GetEncoding(1252), true))
            {
                string? line;
                while ((line = sr.ReadLine()) != null)
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


    }
}

