using System.Diagnostics.CodeAnalysis;

namespace StudentMS.Models
{
    /// <summary>
    /// Entity Class
    /// </summary>
    public class Student

    {
        public string? Grade { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; } = null;

        public Student() { }

        public Student(string grade, string lastName, string firstName)
        {
            Grade = grade;
            FirstName = firstName;
            LastName = lastName;
        }

        public Student(string grade, string lastName, string firstName, string email)
        {
            Grade = grade;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public override string ToString()
        {
            if (Email != null)
                return $"{FirstName} {LastName} (Klasse: {Grade}) Email: {Email}";
            return $"{FirstName} {LastName} (Klasse: {Grade})";
        }
    }

    /// <summary>
    /// Compares the Student Entries just for First/LastName and email
    /// </summary>
    public class StudentComparer : IEqualityComparer<Student>
    {
        public bool Equals(Student? x, Student? y)
        {
            if (x == null && y == null) return true;
            if (x == null || y == null) return false;
            if (x.FirstName == y.FirstName && x.LastName == y.LastName && x.Email == y.Email) return true;
            return false;
        }

        /// <summary>
        /// Provides conditional Hashing of FirstName/LastName/Email
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int GetHashCode([DisallowNull] Student obj)
        {
            string code;
            if (obj.Email != null)
            {
                code = $"{obj.Email}";
            }
            else
            {
                code = $"{obj.FirstName},{obj.LastName}";
            }
            return code.GetHashCode();
        }
    }
}