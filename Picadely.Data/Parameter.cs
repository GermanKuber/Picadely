using System;
using System.Data;

namespace Picadely.Services
{
    public class Parameter
    {
        public string ColumnName { get; }
        public string Value { get; }
        public SqlDbType Type { get; }

        public Parameter(string columnName, string value, SqlDbType type)
        {
            ColumnName = columnName ?? throw new ArgumentNullException(nameof(columnName));
            Value = value ?? throw new ArgumentNullException(nameof(value));
            Type = type;
        }
    }
}
