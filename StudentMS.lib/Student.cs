using System.Collections;

namespace StudentMS.Models
{
    /// <summary>
    /// Entity Class
    /// </summary>
    //TODO: Implement IComparer, EqualityComparer or something else to only check for duplicate email-urls
    public class Student

    {
        public string? Grade { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? email { get; set; } 

        public Student() { }

        public Student(string grade, string lastName, string firstName)
        {
            Grade = grade;
            FirstName = firstName;
            LastName = lastName;
            email = null;
        }
    }
}