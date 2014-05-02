using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Projet_Formes
{
    class DAOEllipse : DAO<Ellipse>
    {
        public override void create(Ellipse entry)
        {
            //Données membres
            this._command.Parameters.Clear();
            this._command.Parameters.AddWithValue("@id", entry.Id);
            this._command.Parameters.AddWithValue("@nom", entry.Nom);
            this._command.Parameters.AddWithValue("@couleur", entry.Couleur);
            this._command.Parameters.AddWithValue("@x1", entry.Point1.X);
            this._command.Parameters.AddWithValue("@y1", entry.Point1.Y);
            this._command.Parameters.AddWithValue("@hauteur", entry.Hauteur);
            this._command.Parameters.AddWithValue("@largeur", entry.Largeur);

            //Définition des requetes
            String[] tabRequete = new String[] {
                //forme
                @"INSERT INTO forme(id, nom) VALUES (@id, @nom);", 
                //forme simple
                @"INSERT INTO formesimple(id, couleur) VALUES (@id, @couleur);",
                //ellipse
                @"INSERT INTO ellipse(id, hauteur, largeur) VALUES (@id, @hauteur, @largeur);",
                //point
                @"INSERT INTO point(id, ordre, x, y) VALUES (@id, 1, @x1, @y1);"
            };


            foreach (String r in tabRequete)
            {
                //Définition de la requete
                this._command.CommandText = r;

                try
                {
                    //Execution de la requete
                    this._command.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Error: {0}", ex.ToString());
                    throw ex;
                }
            }

        }

        public override void delete(Ellipse entry)
        {
            
            //Données membres
            this._command.Parameters.Clear();
            this._command.Parameters.AddWithValue("@id", entry.Id);

            //Définition de la requete
            this._command.CommandText = @"DELETE FROM forme WHERE id = @id;";

            try
            {
                //Execution de la requete
                this._command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());
                throw ex;
            }

        }

        public override void update(Ellipse entry)
        {
            //Données membres
            this._command.Parameters.Clear();
            this._command.Parameters.AddWithValue("@id", entry.Id);
            this._command.Parameters.AddWithValue("@nom", entry.Nom);
            this._command.Parameters.AddWithValue("@couleur", entry.Couleur);
            this._command.Parameters.AddWithValue("@x1", entry.Point1.X);
            this._command.Parameters.AddWithValue("@y1", entry.Point1.Y);
            this._command.Parameters.AddWithValue("@hauteur", entry.Hauteur);
            this._command.Parameters.AddWithValue("@largeur", entry.Largeur);
            
            //Définition des requetes
            String[] tabRequete = new String[] {
                //forme
                @"UPDATE forme SET nom = @nom WHERE id = @id;", 
                //forme simple
                @"UPDATE formesimple SET couleur = @couleur WHERE id = @id;", 
                //ellipse
                @"UPDATE ellipse SET hauteur = @hauteur, largeur = @largeur WHERE id = @id;",
                //point
                @"UPDATE point SET x = @x1, y = @y1 WHERE id = @id AND ordre = 1;"
            };


            foreach (String r in tabRequete)
            {
                //Définition de la requete
                this._command.CommandText = r;

                try
                {
                    //Execution de la requete
                    this._command.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Error: {0}", ex.ToString());
                    throw ex;
                }
            }
        }

        public override Ellipse find(int id)
        {
            MySqlDataReader rdr = null;

            //Définition de la requete
            this._command.Parameters.Clear();
            this._command.Parameters.AddWithValue("@id", id);
            this._command.CommandText = @"SELECT nom, couleur, x, y, hauteur, largeur
                                        FROM forme f, formesimple fs, point p, ellipse e
                                        WHERE f.id = fs.id 
                                        AND fs.id = p.id
                                        AND fs.id = e.id 
                                        AND e.id = @id
                                        AND p.ordre = 1;";

            try
            {
                //Execution de la requete
                rdr = this._command.ExecuteReader();

                //Extraction des données
                rdr.Read();

                String nom = rdr.GetString(0);
                String couleur = rdr.GetString(1);
                int x = rdr.GetInt32(2);
                int y = rdr.GetInt32(3);
                int hauteur = rdr.GetInt32(4);
                int largeur = rdr.GetInt32(5);
                //Transmforme les données extraites en données membres
                Point p1 = new Point(x, y);

                //Resultat
                return new Ellipse(id, nom, couleur, p1, hauteur, largeur);

            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());
                throw ex;
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
            }
        }
    }
}
