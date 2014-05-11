using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Projet_Formes
{
    public abstract class DAOForme<T>
    {
        public MySqlConnection _connect = SingletonConnexion.InitConnection();
        public MySqlCommand _command = SingletonConnexion.InitCommand();

        public abstract void create(T entry);

        public abstract void delete(T entry);

        public abstract void update(T entry);

        public abstract T find(int id);
    }
}
