using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project10_PostgreSQLToDoListApp.ConnectionStrings
{
    public class Connection
    {
      public string GetConnectionString()
        {
            return "Server=localHost;port=5432;Database=DbProject10TodoApp;user ID=postgres;Password=1234";
        }
    }
}
