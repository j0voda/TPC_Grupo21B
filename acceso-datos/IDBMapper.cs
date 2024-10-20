using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace acceso_datos
{
    public interface IDBMapper<T>
    {

        // Devuelve el identificador del objeto list para consulta SQL;
        string getIdentifier(T obj);

        // Devuelve una instancia del objeto mapeado del cursor
        T mapToObject(SqlDataReader reader);

        // Devuelve una lista de argumentos listo para la consulta SQL
        List<string> mapFromObject(T obj);
    }
}
