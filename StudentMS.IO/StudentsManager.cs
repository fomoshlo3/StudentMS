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
             students.GenerateUniqueMail("tsn.at");

            return students;
        }

        /// <summary>
        /// Checks for duplicate entries using the Default EqualityComparer
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="subjects"></param>
        /// <returns>true if   duplicates are existent</returns>
        private static bool HasDuplicates<T>(this IEnumerable<T> subjects, out int[]? indexOfDuplicates)
        {
            return HasDuplicates(subjects, EqualityComparer<T>.Default, out indexOfDuplicates);
        }
        
        /// <summary>
        /// Checks for duplicate entries using a custom <code>IEqualityComparer<typeparamref name="T"/>></code>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="subjects"></param>
        /// <param name="comparer"></param>
        /// <param name="indexOfDuplicates"></param>
        /// <returns>true if List has duplicates in the columns FirstName and LastName as well as an Array of indexes</returns>
        private static bool HasDuplicates<T>(this IEnumerable<T> subjects, IEqualityComparer<T> comparer, out int[]? indexOfDuplicates)
        {
            if (subjects == null)
                throw new ArgumentNullException(nameof(subjects));
            if (comparer == null)
                throw new ArgumentNullException(nameof(comparer));

            var set = new HashSet<T>(comparer);
            var indexList = new List<int>();
            var i = 0;
            foreach (var s in subjects)
            {
                if (!set.Add(s))
                {
                    indexList.Add(i);
                }
                i++;
            }

            if (indexList.Count > 0)
            {
                indexOfDuplicates = indexList.ToArray();
                return true;
            }
            else
            {
                indexOfDuplicates = null;
                return false;
            }
        }

        //TODO: difficult to unit test
        /// <summary>
        /// Generates unique mail adresses
        /// </summary>
        /// <param name="students"></param>
        /// 
        public static void GenerateUniqueMail(this List<Student> students, string domain)
        {
            foreach (var student in students)
            {
                student.Email = $"{student.FirstName.ToLower().ReplaceUmlauts()}.{student.LastName.ToLower().ReplaceUmlauts()}@{domain}";
            }

            //Custom comparer class
            StudentComparer comparer = new();
            int[]? duplicateIndex;
           
            if(students.HasDuplicates(comparer, out duplicateIndex))
            {
                for (int i = 0; i < duplicateIndex.Length; i++)
                {
                    var duplicate = students.ElementAt(duplicateIndex[i]);
                    duplicate.Email = $"{duplicate.FirstName.ToLower().ReplaceUmlauts()}.{duplicate.LastName.ToLower().ReplaceUmlauts()}{i}@{domain}";
                }
            }
            //TODO: https://www.geekality.net/2010/01/19/how-to-check-for-duplicates/
            //TODO: https://dirask.com/posts/C-NET-remove-duplicates-from-List-using-HashSet-D9zB8p
        }

        /// <summary>
        /// Does what it says
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static string ReplaceUmlauts(this string value)
        {
           return value.Replace("ä", "ae")
                       .Replace("ö", "oe")
                       .Replace("ü", "ue");
        }
    }
}