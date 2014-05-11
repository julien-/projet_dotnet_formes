using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Drawing;

namespace Projet_Formes
{
    class DAOSegment : DAOFormeSimple
    {
        public override void create(Forme_simple entry)
        {
            base.create(entry);
            Segment s = (Segment)entry;
            this._command.Parameters.AddWithValue("@x1", s.Point1.X);
            this._command.Parameters.AddWithValue("@y1", s.Point1.Y);
            this._command.Parameters.AddWithValue("@x2", s.Point2.X);
            this._command.Parameters.AddWithValue("@y2", s.Point2.Y);

            //Définition des requetes
            String[] tabRequete = new String[] {
                //segment
                @"INSERT INTO segment(id) VALUES (@id);",
                //point
                @"INSERT INTO point(id, ordre, x, y) VALUES (@id, 1, @x1, @y1);",
                @"INSERT INTO point(id, ordre, x, y) VALUES (@id, 2, @x2, @y2);"
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

        public override void update(Forme_simple entry)
        {
            base.update(entry);
            Segment s = (Segment)entry;
            this._command.Parameters.AddWithValue("@x1", s.Point1.X);
            this._command.Parameters.AddWithValue("@y1", s.Point1.Y);
            this._command.Parameters.AddWithValue("@x2", s.Point2.X);
            this._command.Parameters.AddWithValue("@y2", s.Point2.Y);

            //Définition des requetes
            String[] tabRequete = new String[] { 
                //point
                @"UPDATE point SET x = @x1, y = @y1 WHERE id = @id AND ordre = 1;",
                @"UPDATE point SET x = @x2, y = @y2 WHERE id = @id AND ordre = 2;"
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

        public override Forme_simple find(int id)
        {
            MySqlDataReader rdr = null;

            //Définition de la requete
            this._command.Parameters.Clear();
            this._command.Parameters.AddWithValue("@id", id);
            this._command.CommandText = @"SELECT nom, couleur, x AS x1, y AS y1, x2, y2
                                        FROM (
	                                        SELECT x AS x2, y AS y2
                                            FROM point
                                            WHERE id = @id
                                            AND ordre = 2
                                            ) R1, forme f, formesimple fs, point p, segment e
                                        WHERE f.id = fs.id 
                                        AND fs.id = p.id
                                        AND fs.id = e.id 
                                        AND e.id = @id
                                        AND ordre = 1;";

            try
            {
                //Execution de la requete
                rdr = this._command.ExecuteReader();

                //Extraction des données
                rdr.Read();

                String nom = rdr.GetString(0);
                int couleur = rdr.GetInt32(1);
                int x1 = rdr.GetInt32(2);
                int y1 = rdr.GetInt32(3);
                int x2 = rdr.GetInt32(4);
                int y2 = rdr.GetInt32(5);

                Point p1 = new Point(x1, y1);
                Point p2 = new Point(x2, y2);
                //Resultat
                return new Segment(id, nom, couleur, p1, p2);

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
