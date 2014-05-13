using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Projet_Formes
{
    public class SingletonConnexion
    {
        
        private static String connectionString = "SERVER=localhost; UID=root; PASSWORD=;";

        private static MySqlConnection _connection = null;
        private static MySqlCommand _command = null;

        private static readonly object myLock = new object();

        public static MySqlConnection InitConnection()
        {
            lock (myLock)
            {
                // Si on demande une instance qui n’existe pas, alors on crée notre SqlConnexion.
                if (_connection == null)
                    _connection = new MySqlConnection(connectionString);

                // Dans tous les cas on retourne l’unique instance de notre SqlConnexion.
                return _connection;
            }
        }

        public static MySqlCommand InitCommand()
        {
            lock (myLock)
            {
                // Si on demande une instance qui n’existe pas, alors on crée notre SqlCommand.
                if (_command == null)
                {
                    _command = new MySqlCommand();
                    try
                    {
                        //Creation de la connexion
                        _command.Connection = _connection;

                        //On se connecte
                        _command.Connection.Open();

                        creerBDD();
                        creerTables();
                    }
                    catch (NullReferenceException ex)
                    {
                        Console.WriteLine("Error: (0)", ex.ToString());
                        throw ex;
                    }
                }
                // Dans tous les cas on retourne l’unique instance de notre SqlCommand.
                return _command;
            }
           
        }

        /*public static void CloseConnection()
        {
            _cmd.Connection.Close();
            Console.WriteLine("Connexion fermée");
        }*/

        public static void creerBDD()
        {
            //Définition de la requete
            _command.CommandText = "CREATE DATABASE IF NOT EXISTS db_form;";
            try
            {
                //Execution de la requete
                _command.ExecuteNonQuery();

                Console.WriteLine("Base de données créée");
            }
            catch (MySqlException ex)//erreur
            {
                Console.WriteLine("Error: {0}", ex.ToString());
                throw ex;
            }
        }

        public static void creerTables()
        {
            //Définition des requetes
            String[] tabRequete = new String[] { 
                                @"USE db_form", 
                                //forme
                                @"CREATE TABLE IF NOT EXISTS forme (
                                  id int(11) NOT NULL,
                                  nom varchar(60) NOT NULL,
                                  PRIMARY KEY (id)
                                ) ENGINE=InnoDB DEFAULT CHARSET=latin1;",
                                //forme compose
                                @"CREATE TABLE IF NOT EXISTS formecompos (
                                    id int(11) NOT NULL,
                                    id_forme int(11) NOT NULL,
                                    PRIMARY KEY (id)
                                ) ENGINE=InnoDB DEFAULT CHARSET=latin1;",
                                //forme simple
                                @"CREATE TABLE IF NOT EXISTS formesimple (
                                  id int(11) NOT NULL,
                                  couleur int(11) NOT NULL,
                                  PRIMARY KEY (id)
                                ) ENGINE=InnoDB DEFAULT CHARSET=latin1;",
                                //ellipse
                                @"CREATE TABLE IF NOT EXISTS ellipse (
                                id int(11) NOT NULL,
                                hauteur int(11) NOT NULL,
                                largeur int(11) NOT NULL,
                                PRIMARY KEY (id)
                                ) ENGINE=InnoDB DEFAULT CHARSET=latin1;",
                                //polygone
                                @"CREATE TABLE IF NOT EXISTS polygone (
                                id int(11) NOT NULL,
                                PRIMARY KEY (id)
                                ) ENGINE=InnoDB DEFAULT CHARSET=latin1;", 
                                //rectangle
                                @"CREATE TABLE IF NOT EXISTS rectangle (
                                id int(11) NOT NULL,
                                hauteur int(11) NOT NULL,
                                largeur int(11) NOT NULL,
                                PRIMARY KEY (id)
                                ) ENGINE=InnoDB DEFAULT CHARSET=latin1;",
                                //segment
                                @"CREATE TABLE IF NOT EXISTS segment (
                                id int(11) NOT NULL,
                                PRIMARY KEY (id)
                                ) ENGINE=InnoDB DEFAULT CHARSET=latin1;",
                                //triangle
                                @"CREATE TABLE IF NOT EXISTS triangle (
                                id int(11) NOT NULL,
                                PRIMARY KEY (id)
                                ) ENGINE=InnoDB DEFAULT CHARSET=latin1;",
                                //point
                                @"CREATE TABLE IF NOT EXISTS point (
                                id int(11) NOT NULL,
                                ordre int(11) NOT NULL,
                                x int(11) NOT NULL,
                                y int(11) NOT NULL,
                                PRIMARY KEY (id, ordre)
                                ) ENGINE=InnoDB DEFAULT CHARSET=latin1;",

                                @"ALTER TABLE formesimple ADD CONSTRAINT FOREIGN KEY (id) REFERENCES forme(id) ON DELETE CASCADE ON UPDATE RESTRICT;",
                                @"ALTER TABLE formecompos ADD CONSTRAINT FOREIGN KEY (id) REFERENCES forme(id) ON DELETE CASCADE ON UPDATE RESTRICT;",
                                @"ALTER TABLE ellipse ADD CONSTRAINT FOREIGN KEY (id) REFERENCES formesimple(id) ON DELETE CASCADE ON UPDATE RESTRICT;",
                                @"ALTER TABLE point ADD CONSTRAINT FOREIGN KEY (id) REFERENCES formesimple(id) ON DELETE CASCADE ON UPDATE RESTRICT;",
                                @"ALTER TABLE polygone ADD CONSTRAINT FOREIGN KEY (id) REFERENCES formesimple(id) ON DELETE CASCADE ON UPDATE RESTRICT;",
                                @"ALTER TABLE rectangle ADD CONSTRAINT FOREIGN KEY (id) REFERENCES formesimple(id) ON DELETE CASCADE ON UPDATE RESTRICT;",
                                @"ALTER TABLE segment ADD CONSTRAINT FOREIGN KEY (id) REFERENCES formesimple(id) ON DELETE CASCADE ON UPDATE RESTRICT;",
                                @"ALTER TABLE triangle ADD CONSTRAINT FOREIGN KEY (id) REFERENCES formesimple(id) ON DELETE CASCADE ON UPDATE RESTRICT;",
            };

            foreach (String r in tabRequete)
            {
                //Définition de la requete
                _command.CommandText = r;
                try
                {
                    //Execution de la requete
                    _command.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Error: {0}", ex.ToString());
                    throw ex;
                }

            }
        }
    }
}
