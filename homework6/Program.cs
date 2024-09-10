using Microsoft.EntityFrameworkCore;

namespace homework6;

class Program
{
    static void Main(string[] args)
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            var list1 = db.Enrollments
                .Where(e => e.CourseId == 1)
                .Select(e => e.Student)
                .ToList();
            
            var list2 = db.Instructors
                .Where(e => e.Id == 2)
                .Select(e => e.Course)
                .ToList();

            var list3 = db.Courses
                .Where(e => e.Instructors.Any(e => e.Id == 1))
                .Select(e => new
                {
                    Course = e,
                    Student = e.Enrollments.Select(e => e.Student).ToList();
                }).ToList();

            var list4 = db.Courses
                .Where(e => e.Enrollments.Select(e => e.Student)
                    .Count() > 5).ToList();
            
            var list5 = db.Students
                .Where(e => (DateTime.Now.Year - e.Birthday.Year) > 25)
                .ToList();
            
            var answer6 = db.Students
                .Average(e => e.Id);

            var answer7 = db.Students
                .OrderBy(e => e.Birthday)
                .FirstOrDefault()!;

            var list8 = db.Enrollments
                .Where(e => e.StudentId == 1)
                .Select(e => e.Course)
                .ToList();

            var list9 = db.Students
                .Select(e => e.Name)
                .ToList();

            var list10 = db.Students
                .GroupBy(e => e.Birthday)
                .ToList();

            var list11 = db.Students
                .OrderBy(e => e.Surname)
                .ToList();

            var list12 = db.Students
                .Join(db.Enrollments, 
                    student => student.Id,
                    enrollment => enrollment.StudentId,
                    (s, e) => new
                    {
                        Student = s,
                        Enrollment = e,
                    })
                .ToList();

            var list13 = db.Students
                .Include(e => e.Enrollments)
                .ThenInclude(e => e.Course)
                .Where(e => e.Enrollments.Any(e => e.CourseId != 1))
                .ToList();

            var list14 = db.Students
                .Include(e => e.Enrollments)
                .ThenInclude(e => e.Course)
                .Where(e => e.Enrollments.Any(e => e.CourseId != 1 && e.CourseId != 2))
                .ToList();

            var list15 = db.Enrollments
                .GroupBy(e => e.Course.Name)
                .Select(e => new
                {
                    CourseName = e.Key,
                    StudentsCount = e.Count(),
                }).ToList();
        }
    }
}

//1) Получить список студентов, зачисленных на определенный курс.
// 2) Получить список курсов, на которых учит определенный преподаватель.
// 3) Получить список курсов, на которых учит определенный преподаватель, вместе с именами студентов, зачисленных на каждый курс.
// 4) Получить список курсов, на которые зачислено более 5 студентов.
// 5) Получить список студентов, старше 25 лет.
// 6) Получить средний возраст всех студентов.
// 7) Получить самого молодого студента.
// 8) Получить количество курсов, на которых учится студент с определенным Id.
// 9) Получить список имен всех студентов.
// 10) Сгруппировать студентов по возрасту.
// 11) Получить список студентов, отсортированных по фамилии в алфавитном порядке.
// 12) Получить список студентов вместе с информацией о зачислениях на курсы.
// 13) Получить список студентов, не зачисленных на определенный курс.
// 14) Получить список студентов, зачисленных одновременно на два определенных курса.
// 15) Получить количество студентов на каждом курсе.