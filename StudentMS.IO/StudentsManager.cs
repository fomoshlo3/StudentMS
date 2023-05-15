using StudentMS.Models;
using System.Text.RegularExpressions;

namespace StudentMS.IO
{
    /// <summary>
    /// BusinessLogic Class
    /// </summary>
    public static class StudentsManager
    {
        /// <summary>
        /// Generates a unique Mail in format <code>"john.doe@<paramref name="domain"/>"</code>
        /// Implements a check for duplicates & a string cleaner.
        /// </summary>
        /// <param name="students"></param>
        /// <param name="domain"></param>
        public static void GenerateUniqueMail(List<Student> students, string domain)
        {
            //Task führt nur aus wenn es IO blockiert.
            foreach (var student in students)
            {
                if (student.FirstName != null && student.LastName != null)
                    student.Email = $"{student.FirstName.GetCleanString()}.{student.LastName.GetCleanString()}@{domain}";
            }

            //Custom comparer class
            StudentComparer comparer = new();
            int[]? duplicateIndex;

            if (students.HasDuplicates(comparer, out duplicateIndex))
            {
                for (int i = 0; i < duplicateIndex?.Length; i++)
                {
                    var duplicate = students.ElementAt(duplicateIndex[i]);
                    duplicate.Email =  $"{duplicate.FirstName?.GetCleanString()}.{duplicate.LastName?.GetCleanString()}{i}@{domain}";
                }
            }
            //NOTE: https://www.geekality.net/2010/01/19/how-to-check-for-duplicates/
            //NOTE: https://dirask.com/posts/C-NET-remove-duplicates-from-List-using-HashSet-D9zB8p
        }

        /// <summary>
        /// Checks for duplicate entries using a custom <code>IEqualityComparer</code>
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

        /// <summary>
        /// Does what it says
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static string GetCleanString(this string value)
        {
            //TODO: unit test
            Regex cleanStr = new(@"(\b\w+\b)");  //Since .Match() returns only first match this is effective enough

            return cleanStr.Match(value).Value.ToLower(System.Globalization.CultureInfo.CurrentCulture)
                                              .Replace("ä", "ae")
                                              .Replace("ö", "oe")
                                              .Replace("ü", "ue");
        }
    }
}