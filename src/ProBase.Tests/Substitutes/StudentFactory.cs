using System;
using System.Collections.Generic;
using System.Data;

namespace ProBase.Tests.Substitutes
{
    public class StudentFactory
    {
        public static Student CreateStudent() => new Student { FirstName = "Ernest", LastName = "Hemmingway", Age = 34 };

        public static List<Student> CreateStudentList() => new List<Student>() { CreateStudent() };

        public static DataRow CreateDataRow(Student student)
        {
            DataTable dataTable = CreateDataTableInternal();

            DataRow newRow = dataTable.NewRow();
            newRow["Prenume"] = student.FirstName;
            newRow["Nume"] = student.LastName;
            newRow["Varsta"] = student.Age;

            return newRow;
        }

        public static DataTable CreateDataTable(IEnumerable<Student> writers)
        {
            DataTable result = CreateDataTableInternal();

            foreach (Student student in writers)
            {
                DataRow newRow = result.NewRow();
                newRow["Prenume"] = student.FirstName;
                newRow["Nume"] = student.LastName;
                newRow["Varsta"]  = student.Age;

                result.Rows.Add(newRow);
            }

            return result;
        }

        private static DataTable CreateDataTableInternal()
        {
            DataTable result = new DataTable();

            result.Columns.Add("Prenume", GetPropertyType<Student>(nameof(Student.FirstName)));
            result.Columns.Add("Nume", GetPropertyType<Student>(nameof(Student.LastName)));
            result.Columns.Add("Varsta", GetPropertyType<Student>(nameof(Student.Age)));

            return result;
        }

        private static Type GetPropertyType<T>(string propertyName)
        {
            return typeof(T).GetProperty(propertyName).PropertyType;
        }
    }
}
