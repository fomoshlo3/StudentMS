using StudentMS.IO;
using StudentMS.Models;
//TODO: Write a Test for one of your Methods

internal class Program
{
    public static async Task Main(string[] args)
    {
        string? input = null;
        if (input == null)
        {
            input = args[0];
        }
        //---------
        string? directory = Path.GetDirectoryName(input);
        var ext = new FileInfo(input).Extension;
        //---------
        List<Student> students = new();

        //decision logic for ImporterExporter
        if (ext == ".CSV") students = await ImporterExporter.ImportCSV_Async(input);
        //TODO: if (ext == ".xml") students = await ImporterExporter.ImportXml_Async(input);
        //TODO: if (ext == ".json") students = await ImporterExporter.ImportJson_Async(input);

        StudentsManager.GenerateUniqueMail(students, "tsn.at");

        foreach (var student in students)
        {
            Console.WriteLine(student.ToString());
        }
        if (Path.Exists(directory))
        {
            await ImporterExporter.ExportCSV_Async(Path.Combine(directory, "test.CSV"), students);
            await ImporterExporter.ExportToXml_Async(Path.Combine(directory, "test.xml"), students);
        }
        

        //TODO:Anzeigen einer Kurzstatistik (Anzahl d. Schüler bzw. Klassen, Anzahl Schüler pro Klasse, Durchschnitt Schüler in Klassen).

        Console.ReadKey();

    }
}