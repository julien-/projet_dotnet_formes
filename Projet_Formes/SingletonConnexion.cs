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

        private static MySqlConnection _connection;
        private static MySqlCommand _cmd;

        public static MySqlConnection InitConnection()
        {
            //creation de la connection à la base de données (on ne se connect pas encore, on créé seulement l'instance)
            _connection = new MySqlConnection(connectionString);
            return _connection;
        }

        public static MySqlCommand InitCommand()
        {
            //instancie la classe MySqlCommand
            _cmd = new MySqlCommand();
            try
            {
                //Creation de la connexion
                _cmd.Connection = _connection;

                //On se connecte
                _cmd.Connection.Open();

                creerBDD();
                creerTables();

                return _cmd;
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine("Error: (0)", ex.ToString());
                throw ex;
            }
        }

        public static void CloseConnection()
        {
            _cmd.Connection.Close();
            Console.WriteLine("Connexion fermée");
        }

        public static void creerBDD()
        {
            //Définition de la requete
            _cmd.CommandText = "CREATE DATABASE IF NOT EXISTS db_form;";
            try
            {
                //Execution de la requete
                _cmd.ExecuteNonQuery();

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
                                  couleur varchar(60) NOT NULL,
                                  x1 int(11) NOT NULL,
                                  y1 int(11) NOT NULL,
                                  x2 int(11) NOT NULL,
                                  y2 int(11) NOT NULL,
                                  PRIMARY KEY (id)
                                ) ENGINE=InnoDB DEFAULT CHARSET=latin1;",
                                //ellipse
                                @"CREATE TABLE IF NOT EXISTS ellipse (
                                id int(11) NOT NULL
                                ) ENGINE=InnoDB DEFAULT CHARSET=latin1;",
                                //polygone
                                @"CREATE TABLE IF NOT EXISTS polygone (
                                id int(11) NOT NULL,
                                PRIMARY KEY (id)
                                ) ENGINE=InnoDB DEFAULT CHARSET=latin1;", 
                                //rectangle
                                @"CREATE TABLE IF NOT EXISTS rectangle (
                                id int(11) NOT NULL,
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
                _cmd.CommandText = r;
                try
                {
                    //Execution de la requete
                    _cmd.ExecuteNonQuery();
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
