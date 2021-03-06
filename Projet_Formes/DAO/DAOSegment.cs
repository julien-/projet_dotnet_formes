﻿using System;
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
            Type t = typeof(Segment);
            Type t2 = entry.GetType();
            if (t.Equals(t2))
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
            else if (successor != null)
            {
                successor.create(entry);
            }
        }

        public override void delete(Forme_simple entry)
        {
            Type t = typeof(Segment);
            Type t2 = entry.GetType();
            if (t.Equals(t2))
            {
                base.delete(entry);
            }
            else if (successor != null)
            {
                successor.delete(entry);
            }
        }

        public override void update(Forme_simple entry)
        {
            Type t = typeof(Segment);
            Type t2 = entry.GetType();
            if (t.Equals(t2))
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
            else if (successor != null)
            {
                successor.update(entry);
            }
        }

        public override void createorupdate(Forme_simple entry)
        {
            Type t = typeof(Segment);
            Type t2 = entry.GetType();
            if (t.Equals(t2))
            {
                base.createorupdate(entry);
            }
            else if (successor != null)
            {
                successor.createorupdate(entry);
            }
        }

        public override List<Forme_simple> find()
        {
            MySqlDataReader rdr = null;
            List<Forme_simple> maliste = new List<Forme_simple>();

            //Définition de la requete
            this._command.Parameters.Clear();
            this._command.CommandText = @"SELECT s.id, nom, couleur, x AS x1, y AS y1, x2, y2, id_groupe
                                        FROM (SELECT id, x AS x2, y AS y2 
                                                FROM point 
                                                WHERE ordre = 2)
                                                R1, forme f, formesimple fs, point p, segment s
                                        WHERE f.id = fs.id 
                                        AND fs.id = R1.id
                                        AND fs.id = p.id
                                        AND fs.id = s.id 
                                        AND ordre = 1;";

            try
            {
                //Execution de la requete
                rdr = this._command.ExecuteReader();

                //Extraction des données
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        int id = rdr.GetInt32(0);
                        String nom = rdr.GetString(1);
                        int couleur = rdr.GetInt32(2);
                        Point p1 = new Point(rdr.GetInt32(3), rdr.GetInt32(4));
                        Point p2 = new Point(rdr.GetInt32(5), rdr.GetInt32(6));
                        int idgroupe = rdr.GetInt32(7);
                        maliste.Add(new Segment(id, nom, couleur, p1, p2, idgroupe));
                    }
                }

                //Resultat
                return maliste;
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
