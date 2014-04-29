using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Text;
using System.Threading.Tasks;



namespace Projet_Formes
{
    class Program
    {
        private static MySqlConnection _connection;
        private static MySqlCommand _cmd;

        private static void InitConnection()
        {
            //details de la connection
            String connectionString = "SERVER=localhost; UID=root; PASSWORD=;";

            //creation de la connection à la base de données (on ne se connect pas encore, on créé seulement l'instance)
            _connection = new MySqlConnection(connectionString);

            //instancie la classe MySqlCommand
            _cmd = new MySqlCommand();

            try
            {
                //CCreation de la connexion
                _cmd.Connection = _connection;

                //On se connecte
                _cmd.Connection.Open();

                Console.WriteLine("Connexion ouverte");
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine("Error: (0)", ex.ToString());
                throw ex;
            }
        }

        private static void creerBDD()
        {
            //Définition de la requete
            _cmd.CommandText = "CREATE DATABASE IF NOT EXISTS db_form;";
            try
            {
                //Execution de la requete
                _cmd.ExecuteNonQuery();

                Console.WriteLine("Base de données créée");
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());
                throw ex;
            }
        }

        private static void creerTables()
        {
            //Définition des requetes
            String [] tabRequete = new String [] { 
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
                                  point1 int(11) NOT NULL,
                                  point2 int(11) NOT NULL,
                                  id_type int(11) NOT NULL,
                                  PRIMARY KEY (id)
                                ) ENGINE=InnoDB DEFAULT CHARSET=latin1;",
                                //point
                                @"CREATE TABLE IF NOT EXISTS point (
                                id int(11) NOT NULL,
                                x int(11) NOT NULL,
                                y int(11) NOT NULL,
                                PRIMARY KEY (id)
                                ) ENGINE=InnoDB DEFAULT CHARSET=latin1;", 
                                //ellipse
                                @"CREATE TABLE IF NOT EXISTS ellipse (
                                id int(11) NOT NULL
                                ) ENGINE=InnoDB DEFAULT CHARSET=latin1;",
                                //polygone
                                @"CREATE TABLE IF NOT EXISTS polygone (
                                id int(11) NOT NULL,
                                id_point int(11) NOT NULL,
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
                                ) ENGINE=InnoDB DEFAULT CHARSET=latin1;"

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
            Console.WriteLine("Tables créées");
           
        }




        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            InitConnection();
            creerBDD();
            creerTables();

            try
            {
                //affiche la version de Mysql dans la console
                Console.WriteLine("MySQL version : " + _cmd.Connection.ServerVersion);
            }

            finally
            {
                //Fermeture de la connexion
                _cmd.Connection.Close();
                Console.WriteLine("Connexion fermée");
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        //static void Main()
        //{
        //    string nomelipse = "elipse1";
        //    string nompoly= "polygone1";
        //    Point point1 = new Point(12, 13);
        //    Point point2 = new Point(14, 15);
        //    Point point3 = new Point(16, 17);
        //    Point point4 = new Point(18, 19);
        //    Point point5 = new Point(20, 21);
        //    Point point6 = new Point(22, 23);
        //    int couleur = 1;
        //    Point[] tabcoord = new Point[]{point5, point6};

        //    //test elipse

        //    Ellipse elipse1 = new Ellipse(nomelipse, point1, point2, couleur);

        //    /*elipse1.Write();

        //    elipse1.translation(2, 2);

        //    elipse1.Write();*/

        //    //test polygone

        //    Polygone pol1 = new Polygone(nompoly, point3, point4, couleur, tabcoord);

        //    /*pol1.Write();

        //    pol1.translation(3, 3);

        //    pol1.Write();*/

        //    //test groupe
        //    Forme_simple []tabformes = new Forme_simple[]{pol1, elipse1};
        //    Forme_composee groupe1 = new Forme_composee("groupe1", tabformes);

        //    groupe1.Write();

        //    groupe1.translation(3, 3);

        //    groupe1.Write();

        //    Console.ReadLine();
        //}
    }
}
