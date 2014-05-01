using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Projet_Formes
{
    class DAOTriangle : DAO<Triangle>
    {
        public override void create(Triangle entry)
        {
            //Données membres
            this._command.Parameters.Clear();
            this._command.Parameters.AddWithValue("@id", entry.Id);
            this._command.Parameters.AddWithValue("@nom", entry.Nom);
            this._command.Parameters.AddWithValue("@couleur", entry.Couleur);
            this._command.Parameters.AddWithValue("@x1", entry.Liste_points[0].X);
            this._command.Parameters.AddWithValue("@y1", entry.Liste_points[0].Y);
            this._command.Parameters.AddWithValue("@x2", entry.Liste_points[1].X);
            this._command.Parameters.AddWithValue("@y2", entry.Liste_points[1].Y);
            this._command.Parameters.AddWithValue("@x3", entry.Liste_points[2].X);
            this._command.Parameters.AddWithValue("@y3", entry.Liste_points[2].Y);

            //Définition des requetes
            String[] tabRequete = new String[] {
                //forme
                @"INSERT INTO forme(id, nom) VALUES (@id, @nom);", 
                //forme simple
                @"INSERT INTO formesimple(id, couleur) VALUES (@id, @couleur);",
                //triangle
                @"INSERT INTO triangle(id) VALUES (@id);",
                //point
                @"INSERT INTO point(id, ordre, x, y) VALUES (@id, 1, @x1, @y1);",
                @"INSERT INTO point(id, ordre, x, y) VALUES (@id, 2, @x2, @y2);",
                @"INSERT INTO point(id, ordre, x, y) VALUES (@id, 3, @x3, @y3);"
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

        public override void delete(Triangle entry)
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

        public override void update(Triangle entry)
        {
            //Données membres
            this._command.Parameters.Clear();
            this._command.Parameters.AddWithValue("@id", entry.Id);
            this._command.Parameters.AddWithValue("@nom", entry.Nom);
            this._command.Parameters.AddWithValue("@couleur", entry.Couleur);
            this._command.Parameters.AddWithValue("@x1", entry.Liste_points[0].X);
            this._command.Parameters.AddWithValue("@y1", entry.Liste_points[0].Y);
            this._command.Parameters.AddWithValue("@x2", entry.Liste_points[1].X);
            this._command.Parameters.AddWithValue("@y2", entry.Liste_points[1].Y);
            this._command.Parameters.AddWithValue("@x3", entry.Liste_points[2].X);
            this._command.Parameters.AddWithValue("@y3", entry.Liste_points[2].Y);

            //Définition des requetes
            String[] tabRequete = new String[] {
                //forme
                @"UPDATE forme SET nom = @nom WHERE id = @id;", 
                //forme simple
                @"UPDATE formesimple SET couleur = @couleur WHERE id = @id;", 
                //point
                @"UPDATE point SET x = @x1, y = @y1 WHERE id = @id AND ordre = 1;",
                @"UPDATE point SET x = @x2, y = @y2 WHERE id = @id AND ordre = 2;",
                @"UPDATE point SET x = @x3, y = @y3 WHERE id = @id AND ordre = 3;"
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

        public override Triangle  find(int id)
        {
            MySqlDataReader rdr = null;

            //Définition de la requete
            this._command.Parameters.Clear();
            this._command.Parameters.AddWithValue("@id", id);
            this._command.CommandText = @"SELECT nom, couleur, x AS x1, y AS y1, x2, y2, x3, y3 
                                        FROM (
	                                        SELECT x AS x2, y AS y2
                                            FROM point
                                            WHERE id = @id
                                            AND ordre = 2
                                            ) R1, (
	                                        SELECT x AS x3, y AS y3
                                            FROM point
                                            WHERE id = @id
                                            AND ordre = 3
                                            ) R3, forme f, formesimple fs, point p, triangle
                                        WHERE f.id = fs.id 
                                        AND fs.id = p.id
                                        AND fs.id = e.id 
                                        AND triangle.id = @id
                                        AND ordre = 1;";

            try
            {
                //Execution de la requete
                rdr = this._command.ExecuteReader();

                //Extraction des données
                rdr.Read();

                String nom = rdr.GetString(0);
                String couleur = rdr.GetString(1);
                int x1 = rdr.GetInt32(2);
                int y1 = rdr.GetInt32(3);
                int x2 = rdr.GetInt32(4);
                int y2 = rdr.GetInt32(5);
                int x3 = rdr.GetInt32(6);
                int y3 = rdr.GetInt32(7);

                List<Point> listepoint = new List<Point>();
                listepoint.Add(new Point(x1, y1));
                listepoint.Add(new Point(x2, y2));
                listepoint.Add(new Point(x3, y3));
                //Resultat
                return new Triangle(id, nom, listepoint, couleur);

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
