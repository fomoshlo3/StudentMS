using StudentMS.IO;
using StudentMS.Models;
//TODO: Write a Test for one of your Methods

internal class Program
{
    //Since the Task was to code a async Library, and not a async GUI no async identifier on my Main()
    public static void Main(string[] args)
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
        if (ext == ".CSV") students = ImporterExporter.ImportCSV_Async(input).Result;
        //if (ext == ".xml")  Import xml
        //if (ext == ".json") Import json

        StudentsManager.GenerateUniqueMail(students, "tsn.at");

        foreach (var student in students)
        {
            Console.WriteLine(student.ToString());
        }


        var menu = () =>
        {
            string output = "Do you want to save to CSV? \n [Y/N]";

            Console.WriteLine(Environment.NewLine + output);
            string? answer = Console.ReadLine();

            switch (answer)
            {
                case "Y": ExportCSV_Async(,students) break;
                case "N": Environment.Exit(1); break;
                default: Console.WriteLine(); break;
            }
        };
        while(Environment.ExitCode != 1)
        {
            menu();
        }
        //TODO:Anzeigen einer Kurzstatistik (Anzahl d. Schüler bzw. Klassen, Anzahl Schüler pro Klasse, Durchschnitt Schüler
        //in Klassen).
        Console.ReadKey();

    }
}