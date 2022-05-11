using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;

namespace Med.DB
{
    public class SQL : DataTableEx
    {
        private static SqlConnection connection;
        private static SqlDataAdapter adapter;
        private static SqlCommand command;
        private static string connectString;
        /// <summary>
        /// Возвращает или задает строку подключения и открывает подключение
        /// </summary>
        public static string Connection
        {
            set
            {
                connectString = value;
                connection = new SqlConnection(connectString);
                connection.Open();
            }
        }

        public static DataTableEx Execute(string script)
        {
            DataTableEx Table = new DataTableEx();
            adapter = new SqlDataAdapter(script, connection);
            adapter.Fill(Table);
            return Table;
        }

        public static ObservableCollection<T> Execute<T>(string script, ObservableCollection<T> Source) where T : new()
        {
            
            DataTableEx Table = Execute(script);
            for (int row = 0; row < Table.Rows.Count; row++)
            {
                object s = new T();
                for (int col = 0; col < Table.Columns.Count; col++)
                {
                    string name = Table.Columns[col].ColumnName;
                    var cellValue = Table.Rows[row].ItemArray[col];
                    s.GetType().GetProperty(name).SetValue(s, cellValue);
                }
                Source.Add((T)s);
            }

            return Source;
        }
        public static T Execute<T>(string script) where T : new()
        {
            object s = new T();
            DataTableEx Table = Execute(script);
            for (int row = 0; row < Table.Rows.Count; row++)
            {
                for (int col = 0; col < Table.Columns.Count; col++)
                {
                    string name = Table.Columns[col].ColumnName;
                    var cellValue = Table.Rows[row].ItemArray[col];
                    s.GetType().GetProperty(name).SetValue(s, cellValue);
                }
            }

            return (T)s;
        }

        public static void ExecuteNonQuery(string script)
        {
            command = new SqlCommand(script, connection);
            command.ExecuteNonQuery();
        }

    }
    public class DataTableEx : DataTable
    {
        public object GetData(int indexRow, int indexCol)
        {
            if (this.Rows.Count != 0)
            {
                return this.Rows[indexRow].ItemArray[indexCol];
            }
            else
                return null;
        }
    }
}
