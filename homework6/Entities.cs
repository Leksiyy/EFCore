namespace homework6;

public class Student
{
    public int Id {get; set;}
    public string Name {get; set;}
    public string Surname {get; set;}
    public DateTime Birthday {get; set;}
    public ICollection<Enrollment> Enrollments {get; set;}
}
public class Course
{
    public int Id {get; set;}
    public string Name {get; set;}
    public string Description {get; set;}
    public ICollection<Enrollment> Enrollments {get; set;}
    public ICollection<Instructor> Instructors {get; set;}
}
public class Enrollment
{
    public int Id {get; set;}
    public int StudentId {get; set;}
    public Student Student {get; set;}
    public int CourseId {get; set;}
    public Course Course {get; set;}
    public DateTime EnrollmentDate {get; set;}
}
public class Instructor
{
    public int Id {get; set;}
    public string Name {get; set;}
    public string Surname {get; set;}
    public List<Course> Courses = new List<Course>();
    public Course Course {get; set;}
}

// ■ Enrollment (Зачисление): Информация о зачислении студентов на курсы, включая идентификатор зачисления, идентификатор студента, идентификатор курса и дату зачисления.
// ■ Instructor (Преподаватель): Информация о преподавателях, включая их идентификатор, имя и фамилию.
