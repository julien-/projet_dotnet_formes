﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Drawing;

namespace Projet_Formes
{
    class DAOEllipse : DAOFormeSimple
    {
        public override void create(Forme_simple entry)
        {
            Type t = typeof(Ellipse);
            Type t2 = entry.GetType();
            if (t.Equals(t2))
            {
                base.create(entry);
                Ellipse e = (Ellipse)entry;

                //Données membres
                this._command.Parameters.AddWithValue("@x1", e.Point1.X);
                this._command.Parameters.AddWithValue("@y1", e.Point1.Y);
                this._command.Parameters.AddWithValue("@hauteur", e.Hauteur);
                this._command.Parameters.AddWithValue("@largeur", e.Largeur);

                //Définition des requetes
                String[] tabRequete = new String[] {
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
            else if (successor != null)
            {
                successor.create(entry);
            }
        }

        public override void delete(Forme_simple entry)
        {
            Type t = typeof(Ellipse);
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
            Type t = typeof(Ellipse);
            Type t2 = entry.GetType();
            if (t.Equals(t2))
            {
                base.update(entry);
                Ellipse e = (Ellipse)entry;
                //Données membres
                this._command.Parameters.AddWithValue("@x1", e.Point1.X);
                this._command.Parameters.AddWithValue("@y1", e.Point1.Y);
                this._command.Parameters.AddWithValue("@hauteur", e.Hauteur);
                this._command.Parameters.AddWithValue("@largeur", e.Largeur);

                //Définition des requetes
                String[] tabRequete = new String[] {
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
            else if (successor != null)
            {
                successor.update(entry);
            }
        }
        
        public override List<Forme_simple> find()
        {
            MySqlDataReader rdr = null;
            List<Forme_simple> maliste = new List<Forme_simple>();

            //Définition de la requete
            this._command.Parameters.Clear();
            this._command.CommandText = @"SELECT e.id, nom, couleur, x, y, largeur, hauteur , id_groupe "+
                                        @"FROM forme f, formesimple fs, ellipse e, point p "+
                                        @"WHERE f.id = fs.id AND fs.id = e.id AND e.id = p.id";

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
                        int largeur = rdr.GetInt32(5);
                        int hauteur = rdr.GetInt32(6);
                        int idgroupe = rdr.GetInt32(7);
                        maliste.Add(new Ellipse(id, nom, couleur, p1, hauteur, largeur, idgroupe));
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

        public override void createorupdate(Forme_simple entry)
        {
            Type t = typeof(Ellipse);
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
    }
}
