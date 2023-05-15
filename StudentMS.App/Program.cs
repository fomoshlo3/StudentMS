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
        //TODO: Dynamic Relative generation of Paths. for adjustments see StudentMs.App/Properties/launchsettings.json
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

        //TODO:Anzeigen einer Kurzstatistik (Anzahl d. Schüler bzw. Klassen, Anzahl Schüler pro Klasse, Durchschnitt Schüler in Klassen).
        Console.WriteLine($"{students.Count} Schüler");
        Console.WriteLine(students.DistinctBy(s => s.Class).Count() + " Klassen");
        PrintStudentsPerClass(students);
        PrintAverageCount(students);


        Console.ReadKey();

        if (Path.Exists(directory))
        {
            await ImporterExporter.ExportCSV_Async(Path.Combine(directory, "test.CSV"), students);
            //await ImporterExporter.ExportToXml_Async(Path.Combine(directory, "test.xml"), students);
        }

        Console.ReadKey();

    }
    
    public static void PrintStudentsPerClass(List<Student> students)
    {
        var gradeCounts = from s in students
                          group s by s.Class into g
                          select (Class: g.Key, ClassCount: g.Count());

                foreach (var g in gradeCounts)
                {
                    Console.WriteLine($"Klasse: {g.Class}: Schüler: {g.ClassCount}");
                }
    }

    public static void PrintAverageCount(List<Student> students)
    {
        var classCount = from s in students
                         group s by s.Class into g
                         select (Class: g.Key, ClassCount: g.Count());

   
        Console.WriteLine($"Durchschnittliche Klassengröße: {classCount.Average(i => i.ClassCount)}");
    }

}