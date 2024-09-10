using Microsoft.EntityFrameworkCore;

namespace homework6;

class Program
{
    static void Main(string[] args)
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            //1) Получить список студентов, зачисленных на определенный курс.
            var list1 = db.Enrollments
                .Where(e => e.CourseId == 1)
                .Select(e => e.Student)
                .ToList();
            
            // 2) Получить список курсов, на которых учит определенный преподаватель.
            var list2 = db.Instructors
                .Where(e => e.Id == 2)
                .Select(e => e.Course)
                .ToList();

            // 3) Получить список курсов, на которых учит определенный преподаватель, вместе с именами студентов, зачисленных на каждый курс.
            var list3 = db.Courses
                .Where(e => e.Instructors.Any(e => e.Id == 1))
                .Select(e => new
                {
                    Student = e.Enrollments.Select(e => e.Student.Name).ToList(),
                    Course = e
                }).ToList();

            // 4) Получить список курсов, на которые зачислено более 5 студентов
            var list4 = db.Courses
                .Where(e => e.Enrollments.Select(e => e.Student)
                    .Count() > 5).ToList();
            
            // 5) Получить список студентов, старше 25 лет.
            var list5 = db.Students
                .Where(e => (DateTime.Now.Year - e.Birthday.Year) > 25)
                .ToList();
            
            // 6) Получить средний возраст всех студентов.
            var answer6 = db.Students
                .Average(e => e.Id);

            // 7) Получить самого молодого студента.
            var answer7 = db.Students
                .OrderBy(e => e.Birthday)
                .LastOrDefault()!;

            // 8) Получить количество курсов, на которых учится студент с определенным Id.
            var list8 = db.Enrollments
                .Where(e => e.StudentId == 1)
                .Select(e => e.Course)
                .Count();

            // 9) Получить список имен всех студентов.
            var list9 = db.Students
                .Select(e => e.Name)
                .ToList();

            // 10) Сгруппировать студентов по возрасту.
            var list10 = db.Students
                .GroupBy(e => e.Birthday)
                .ToList();

            // 11) Получить список студентов, отсортированных по фамилии в алфавитном порядке.
            var list11 = db.Students
                .OrderBy(e => e.Surname)
                .ToList();

            // 12) Получить список студентов вместе с информацией о зачислениях на курсы.
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

            // 13) Получить список студентов, не зачисленных на определенный курс.
            var list13 = db.Students
                .Include(e => e.Enrollments)
                .ThenInclude(e => e.Course)
                .Where(e => e.Enrollments.Any(e => e.CourseId != 1))
                .ToList();

            // 14) Получить список студентов, зачисленных одновременно на два определенных курса.
            var list14 = db.Students
                .Include(e => e.Enrollments)
                .ThenInclude(e => e.Course)
                .Where(e => e.Enrollments.Any(e => e.CourseId == 1 && e.CourseId == 2))
                .ToList();

            // 15) Получить количество студентов на каждом курсе.
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

