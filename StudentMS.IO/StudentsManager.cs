using StudentMS.Models;
using System.Text;

namespace StudentMS.IO
{
    /// <summary>
    /// BusinessLogic Class
    /// </summary>
    public static class StudentsManager
    {
        public static List<Student> GetStudents(FileStream fs)
        {
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
                        students.Add(new Student(
                            objects[0], objects[1], objects[2]));
                    }
                }
            }
            SetUniqueMail(students);

            return students;
        }

        /// <summary>
        /// Checks for duplicate entries using the Default EqualityComparer
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="subjects"></param>
        /// <returns>true if duplicates are existent</returns>
        private static bool HasDuplicates<T>(this IEnumerable<T> subjects)
        {
            return HasDuplicates(subjects, EqualityComparer<T>.Default);
        }

        /// <summary>
        /// Checks for duplicate entries using a custom <code>IEqualityComparer<typeparamref name="T"/>></code>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="subjects"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        private static bool HasDuplicates<T>(this IEnumerable<T> subjects, IEqualityComparer<T> comparer)
        {
            if (subjects == null)
                throw new ArgumentNullException("subjects");

            if (comparer == null)
                throw new ArgumentNullException("comparer");

            var set = new HashSet<T>(comparer);

            foreach (var s in subjects)
                if (!set.Add(s))
                    return true;

            return false;
        }
        /// <summary>
        /// decides for a Email Adress Pattern, default {FirstName}.{LastName}@{Url}
        /// </summary>
        /// <param name="students"></param>
        private static void SetUniqueMail(List<Student> students)
        {
            if (students.HasDuplicates())
            {
                //TODO: Pattern for setting uniqueMails 
                foreach (var student in students)
                {
                    
                }
            }
            else
            {
                foreach(var student in students)
                {
                    
                }
            }
            
            //TODO: https://www.geekality.net/2010/01/19/how-to-check-for-duplicates/
            //TODO: https://dirask.com/posts/C-NET-remove-duplicates-from-List-using-HashSet-D9zB8p
        }
    }
}