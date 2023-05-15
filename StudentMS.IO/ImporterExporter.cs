using StudentMS.Models;
using System.Text;
using System.Xml;

namespace StudentMS.IO
{
    public static class ImporterExporter
    {
        /// <summary>
        /// ValueTask for importing a CSV File
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
                        var subjects = line.Split(";");
                        students.Add(new Student(
                            subjects[0], subjects[1], subjects[2]));
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
                    await sr.WriteLineAsync("Klasse;Vorname(-2.Vorname);Nachame;Email");
                    foreach (var item in dataList)
                    {
                        await sr.WriteLineAsync($"{item.Class};{item.FirstName};{item.LastName};{item.Email}");
                    }
                    sr.Close();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fqpn"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static async ValueTask<List<Student>> ImportXml_Async(string fqpn)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fqpn"></param>
        /// <param name="dataList"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static async Task ExportToXml_Async(string fqpn, List<Student> dataList)
        {
            FileStream fs;
            if (!File.Exists(fqpn)) fs = File.Create(fqpn);
            else using (fs = File.Open(fqpn, FileMode.Append))
                {
                    var settings = new XmlWriterSettings()
                    {
                        Async = true,
                        Indent = true
                    };
                       
                    var writer =  XmlWriter.Create(fs, settings);
                    await writer.WriteStartElementAsync("pf", "root", "http://ns");
                    await writer.WriteStartElementAsync(null, "sub", null);
                    await writer.WriteAttributeStringAsync(null, "att", null, "val");
                    await writer.WriteStringAsync("text");
                    await writer.WriteEndElementAsync();
                    await writer.WriteProcessingInstructionAsync("pName", "pValue");
                    await writer.WriteCommentAsync("cValue");
                    await writer.WriteCDataAsync("cdata value");
                    await writer.WriteEndElementAsync();
                    await writer.FlushAsync();
                    
                    //await writer.WriteStartDocumentAsync();
                    //await writer.XmlSpace  
                    //await writer.WriteStartElementAsync("student");
                    foreach (var item in dataList)
                    {

                    }
                    //await writer.WriteEndElementAsync();
                    //await writer.WriteEndDocumentAsync();
                }

        }
    }
}

