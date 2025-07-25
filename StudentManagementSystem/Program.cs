namespace StudentManagementSystem
{
    class Student
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public List<Course> Courses { get; set; } = new List<Course>();

        public bool Enroll(Course course)
        {
            if (!Courses.Contains(course))
            {
                Courses.Add(course);
                return true;
            }
            return false;
        }

        public string PrintDetails()
        {
            string courseList = Courses.Count == 0 ? "No courses" :
                string.Join(", ", Courses.Select(courses => courses.Title));
            return $"ID: {StudentId}, Name: {Name}, Age: {Age}, Courses: {courseList}";
        }
    }


    class Instructor
    {
        public int InstructorId { get; set; }
        public string Name { get; set; }
        public string Specialization { get; set; }

        public string PrintDetails()
        {
            return $"ID: {InstructorId}, Name: {Name}, Specialization: {Specialization}";
        }
    }

    class Course
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public Instructor Instructor { get; set; }

        public string PrintDetails()
        {
            return $"Course ID: {CourseId}, Title: {Title}, Instructor: {Instructor?.Name}";
        }
    }
    class SchoolStudentManager
    {
        public List<Student> Students { get; set; } = new List<Student>();
        public List<Course> Courses { get; set; } = new List<Course>();
        public List<Instructor> Instructors { get; set; } = new List<Instructor>();

        public bool AddStudent(Student student)
        {
            Students.Add(student);
            return true;
        }

        public bool AddCourse(Course course)
        {
            Courses.Add(course);
            return true;
        }

        public bool AddInstructor(Instructor instructor)
        {
            Instructors.Add(instructor);
            return true;
        }

        public Student FindStudent(int studentId)
        {
            return Students.FirstOrDefault(s => s.StudentId == studentId);
        }

        public Course FindCourse(int courseId)
        {
            return Courses.FirstOrDefault(c => c.CourseId == courseId);
        }

        public Instructor FindInstructor(int instructorId)
        {
            return Instructors.FirstOrDefault(i => i.InstructorId == instructorId);
        }

        public bool EnrollStudentInCourse(int studentId, int courseId)
        {
            var student = FindStudent(studentId);
            var course = FindCourse(courseId);
            if (student != null && course != null)
            {
                return student.Enroll(course);
            }
            return false;
        }

        public bool IsStudentEnrolledInCourse(int studentId, int courseId)
        {
            var student = FindStudent(studentId);
            return student?.Courses.Any(c => c.CourseId == courseId) ?? false;
        }

        public string GetInstructorNameByCourseName(string courseName)
        {
            var course = Courses.FirstOrDefault(c => c.Title.Equals(courseName, StringComparison.OrdinalIgnoreCase));
            return course?.Instructor?.Name ?? "Not found";
        }
    }
    class Program
    {
        static void Main()
        {
            var manager = new SchoolStudentManager();
            while (true)
            {
                Console.WriteLine("\n===== Student Management System =====");
                Console.WriteLine("1. Add Student");
                Console.WriteLine("2. Add Instructor");
                Console.WriteLine("3. Add Course");
                Console.WriteLine("4. Enroll Student in Course");
                Console.WriteLine("5. Show All Students");
                Console.WriteLine("6. Show All Courses");
                Console.WriteLine("7. Show All Instructors");
                Console.WriteLine("8. Find Student by ID");
                Console.WriteLine("9. Find Course by ID");
                Console.WriteLine("10. Exit");
                Console.Write("Enter choice: ");

                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Student ID: ");
                        int sid = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Name: ");
                        string sname = Console.ReadLine();
                        Console.WriteLine("Age: ");
                        int age = Convert.ToInt32(Console.ReadLine());
                        manager.AddStudent(new Student { StudentId = sid, Name = sname, Age = age });
                        break;
                    case 2:
                        Console.WriteLine("Instructor ID: ");
                        int iid = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Name: ");
                        string iname = Console.ReadLine();
                        Console.WriteLine("Specialization: ");
                        string spec = Console.ReadLine();
                        manager.AddInstructor(new Instructor { InstructorId = iid, Name = iname, Specialization = spec });
                        break;
                    case 3:
                        Console.WriteLine("Course ID: ");
                        int cid = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Title: ");
                        string title = Console.ReadLine();
                        Console.WriteLine("Instructor ID: ");
                        int insId = Convert.ToInt32(Console.ReadLine());
                        var inst = manager.FindInstructor(insId);
                        if (inst != null)
                            manager.AddCourse(new Course { CourseId = cid, Title = title, Instructor = inst });
                        else
                            Console.WriteLine("Instructor not found");
                        break;
                    case 4:
                        Console.WriteLine("Student ID: ");
                        int studId = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Course ID: ");
                        int crsId = Convert.ToInt32(Console.ReadLine());
                        if (manager.EnrollStudentInCourse(studId, crsId))
                            Console.WriteLine("Enrolled successfully.");
                        else
                            Console.WriteLine("Enrollment failed.");
                        break;
                    case 5:
                        for (int i = 0; i < manager.Students.Count; i++)
                        {
                            Console.WriteLine(manager.Students[i].PrintDetails());
                        }
;
                        break;
                    case 6:
                        for (int i = 0; i < manager.Courses.Count; i++)
                        {
                            Console.WriteLine(manager.Courses[i].PrintDetails());
                        }
                        break;
                    case 7:
                        for (int i = 0; i < manager.Instructors.Count; i++)
                        {
                            Console.WriteLine(manager.Instructors[i].PrintDetails());
                        }
                        break;
                    case 8:
                        Console.WriteLine("Enter Student ID: ");
                        int fid = Convert.ToInt32(Console.ReadLine());
                        var student = manager.FindStudent(fid);
                        Console.WriteLine(student?.PrintDetails() ?? "Student not found");
                        break;
                    case 9:
                        Console.WriteLine("Enter Course ID: ");
                        int fcid = Convert.ToInt32(Console.ReadLine());
                        var course = manager.FindCourse(fcid);
                        Console.WriteLine(course?.PrintDetails() ?? "Course not found");
                        break;
                    case 10:
                        return;
                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            }
        }
    }

}
