using StudentMS.IO;
//TODO: Write a Test for one of your Methods

internal class Program
{
    private static void Main(string[] args)
    {
        //Console.WriteLine("Hello Please give me a full path to the CSV you would like to load");
        string input = args[0];
        if (input == null)
        {
            input = args[0];
        }
        FileStream fs = File.Open(input, FileMode.Open);
        
        var students = StudentsManager.GetStudents(fs);

        foreach (var student in students)
        {
            Console.WriteLine(student.ToString());
        }

        //TODO:Anzeigen einer Kurzstatistik (Anzahl d. Schüler bzw. Klassen, Anzahl Schüler pro Klasse, Durchschnitt Schüler
        //in Klassen).
        


        
        Console.ReadKey();

    }
}