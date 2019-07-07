using System;
using System.Collections;
using System.Linq;
using System.Text.RegularExpressions;
using GradeBook.Enums;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name) : base(name)
        {
            Type = GradeBookType.Ranked;
        }

        public override char GetLetterGrade(double averageGrade)
        {
            if (Students.Count < 5)
            {
                throw new InvalidOperationException("Ranked-grading requires a minimum of 5 students to work");
            }
            else
            {
                //This number corresponds to 20% of students 
                var threshold = (int) Math.Ceiling(Students.Count * 0.2);

                //We then have a sorted list of average grades 
                var grades = Students.OrderByDescending(e => e.AverageGrade).Select(e => e.AverageGrade).ToList();


                //We then check if the grade inputted is more than or equal to the grade that is occupying the 80% percentile point 
                if (grades[threshold - 1] <= averageGrade)
                {
                    return 'A';
                }
                else if (grades[(threshold * 2) - 1] <= averageGrade)
                {
                    return 'B';
                }
                else if (grades[(threshold * 3) - 1] <= averageGrade)
                {
                    return 'C';
                }
                else if (grades[(threshold * 4) - 1] <= averageGrade)
                {
                    return 'D';
                }
                else
                {
                    return 'F';
                }
            }
        }

        public override void CalculateStatistics()
        {
            if (Students.Count < 5)
            {
                Console.WriteLine(
                    "Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }

            base.CalculateStatistics();
        }


        public override void CalculateStudentStatistics(string name)
        {
            if (Students.Count < 5)
            {
                Console.WriteLine(
                        "Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }
            base.CalculateStudentStatistics(name);
        }
    }
}