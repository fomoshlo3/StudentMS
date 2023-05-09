using StudentMS.IO;
using StudentMS.Models;
//TODO: Write a Test for one of your Methods

internal class Program
{
    private static void Main(string[] args)
    {
        //Console.WriteLine("Hello Please give me a full path to the CSV you would like to load");
        string? input = null;
        if (input == null)
        {
            input = args[0];
        }

        List<Student> students = new();
        //decision logic for ImporterExporter
        var ext = new FileInfo(input).Extension;
        Console.WriteLine(ext);
        if(ext == ".CSV") students = ImporterExporter.ImportCSV(input);
        //if (ext == ".xml")  Import xml
        //if (ext == ".json") Import json

        StudentsManager.GenerateUniqueMail(students, "tsn.at");

        foreach (var student in students)
        {
            Console.WriteLine(student.ToString());
        }

        //TODO:Anzeigen einer Kurzstatistik (Anzahl d. Schüler bzw. Klassen, Anzahl Schüler pro Klasse, Durchschnitt Schüler
        //in Klassen).
        Console.ReadKey();
    }
}