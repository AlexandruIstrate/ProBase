using System;
using System.Collections.Generic;
using System.Data;

namespace ProBase.Tests.Substitutes
{
    public class SubstituteFactory
    {
        public static FamousWriter CreateWriter() => new FamousWriter { FirstName = "Ernest", LastName = "Hemmingway", Age = 34 };

        public static List<FamousWriter> CreateWriterList() => new List<FamousWriter>() { CreateWriter() };

        public static DataRow CreateDataRow(FamousWriter writer)
        {
            DataTable dataTable = CreateDataTableInternal();

            DataRow newRow = dataTable.NewRow();
            newRow[nameof(FamousWriter.FirstName)] = writer.FirstName;
            newRow[nameof(FamousWriter.LastName)] = writer.LastName;
            newRow[nameof(FamousWriter.Age)] = writer.Age;

            return newRow;
        }

        public static DataTable CreateDataTable(IEnumerable<FamousWriter> writers)
        {
            DataTable result = CreateDataTableInternal();

            foreach (FamousWriter writer in writers)
            {
                DataRow newRow = result.NewRow();
                newRow[nameof(FamousWriter.FirstName)] = writer.FirstName;
                newRow[nameof(FamousWriter.LastName)] = writer.LastName;
                newRow[nameof(FamousWriter.Age)] = writer.Age;

                result.Rows.Add(newRow);
            }

            return result;
        }

        private static DataTable CreateDataTableInternal()
        {
            DataTable result = new DataTable();

            result.Columns.Add(nameof(FamousWriter.FirstName), GetPropertyType<FamousWriter>(nameof(FamousWriter.FirstName)));
            result.Columns.Add(nameof(FamousWriter.LastName), GetPropertyType<FamousWriter>(nameof(FamousWriter.LastName)));
            result.Columns.Add(nameof(FamousWriter.Age), GetPropertyType<FamousWriter>(nameof(FamousWriter.Age)));

            return result;
        }

        private static Type GetPropertyType<T>(string propertyName)
        {
            return typeof(T).GetProperty(propertyName).PropertyType;
        }
    }
}
