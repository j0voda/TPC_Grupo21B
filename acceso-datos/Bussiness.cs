using dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace acceso_datos
{
    public abstract class Bussiness<T>
    {
        public string tableName;
        protected string idColumn;
        public List<string> columns;
        public SqlConnection sqlConexion = new SqlConnection();
        public SqlDataReader reader;
        private IDBMapper<T> mapper;

        public Bussiness(string tableName, string idColumn, List<string> rows, IDBMapper<T> mapper)
        {
            this.idColumn = idColumn;
            this.columns = rows;
            this.mapper = mapper;
            this.tableName = tableName;

            SqlConnectionStringBuilder sConnB = new SqlConnectionStringBuilder()
            {
                DataSource = ".\\SQLEXPRESS",
                //DataSource = "localhost",
                InitialCatalog = "PROMOS_DB",
                IntegratedSecurity = true
            };

            sqlConexion.ConnectionString = sConnB.ConnectionString;
        }

        protected List<T> select(string selectedValues, string condition="")
        {
            sqlConexion.Open();

            string query = String.Format("SELECT {0} FROM {1} t {2}", selectedValues, tableName, condition);

            reader = executeCommand(query);

            List<T> list = new List<T>();

            while (reader.Read())
            {
                list.Add(this.mapper.mapToObject(reader));
            }

            sqlConexion.Close();

            return list;
        }

        protected void update(string sets, string condition="")
        {
            sqlConexion.Open();

            string query = String.Format("UPDATE {0} SET {1} {2}", tableName, sets, condition);
            this.executeCommand(query);

            sqlConexion.Close();
        }

        protected void delete(string condition)
        {
            sqlConexion.Open();

            string query = String.Format("DELETE FROM {0} {1}", tableName, condition);
            SqlDataReader reader = this.executeCommand(query);
            sqlConexion.Close();
        }

        virtual public int insert(string values)
        {
            int id = -1;
            sqlConexion.Open();

            string query = String.Format("INSERT INTO {0} ({1}) VALUES ({2});SELECT SCOPE_IDENTITY();", tableName, String.Join(" ,", columns), values);
            reader = this.executeCommand(query);

            if (reader.Read()) { 
                id = Convert.ToInt32(reader[0]);
            }

            sqlConexion.Close();

            return id;
        }

        virtual public List<T> getAllFilterByTextContain(int columnIndex, string text)
        {
            return select($"{idColumn}, {String.Join(" ,", columns)}", $"WHERE {columns[columnIndex]} LIKE '%{text}%'");
        }

        virtual public List<T> getAll()
        {

            return select($"{idColumn}, {String.Join(" ,", columns)}");
        }

        virtual public T getOne(T obj)
        {
            List<T> res = select($"{idColumn}, {String.Join(" ,", columns)}", $"WHERE {idColumn}={this.mapper.getIdentifier(obj)}");

            if (res.Count == 0)
            {
                return default;
            }

            return res[0];
        }

        virtual public void deleteOne(T item)
        {
            delete($"WHERE {idColumn}={this.mapper.getIdentifier(item)}");
        }

        public void deleteMany(List<T> items)
        {
            delete($"WHERE {idColumn} IN ({String.Join(",", items.Select(item => this.mapper.getIdentifier(item)))})");
        }

        public virtual int saveOne(T item)
        {
            List<string> values = this.mapper.mapFromObject(item);

            int id = insert(String.Join(" ,", values));

            return id;
        }

        public void updateOne(T item)
        {
            List<string> values = this.mapper.mapFromObject(item);

            List<string> sets = new List<string>();

            for (int i = 0; i < columns.Count; i++)
            {
                string column = columns[i];
                string value = values[i];

                sets.Add($"{column}={value}");
            }

            update(String.Join(" ,", sets), $"WHERE {idColumn}={this.mapper.getIdentifier(item)}");
        }

        public SqlDataReader executeCommand(string sqlCommand)
        {
            SqlCommand command = new SqlCommand();

            command.CommandType = System.Data.CommandType.Text;
            command.Connection = sqlConexion;
            command.CommandText = sqlCommand;

            return command.ExecuteReader();
        }
    }
}
