using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Projet_Formes
{
    abstract class DAOForme<T>
    {
        protected MySqlConnection _connect = SingletonConnexion.InitConnection();
        protected MySqlCommand _command = SingletonConnexion.InitCommand();

        public abstract void create(T entry);

        public abstract void delete(T entry);

        public abstract void update(T entry);

        public abstract T find(T entry);

        public abstract void createorupdate(T entry);

    }
}
