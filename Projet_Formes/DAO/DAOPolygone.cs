﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Drawing;

namespace Projet_Formes
{
    class DAOPolygone : DAOFormeSimple
    {
        public override void create(Forme_simple entry)
        {
            base.create(entry);
            Polygone p = (Polygone)entry;
       
            //Définition des requetes
            List<String> tabRequete_poly = new List<String>();
            //polygone
            tabRequete_poly.Add(@"INSERT INTO polygone(id) VALUES (@id);");
            //point
            for (int i = 1; i <= p.Tableau_points.Length; i++)
            { 
                tabRequete_poly.Add(@"INSERT INTO point(id, ordre, x, y) VALUES (@id, "+i+", "+p.Tableau_points[i-1].X+", "+p.Tableau_points[i-1].Y+");");
            }

            foreach (String r in tabRequete_poly)
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
            Polygone p = (Polygone)entry;

            //Définition des requetes
            List<String> tabRequete_poly = new List<String>();
 
            //point
            for (int i = 1; i <= p.Tableau_points.Length; i++)
            {
                tabRequete_poly.Add(@"UPDATE point SET x = " + p.Tableau_points[i - 1].X + ", y = " + p.Tableau_points[i - 1].Y + " WHERE id = @id AND ordre = " + i + ";");
            }

            foreach (String r in tabRequete_poly)
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

            //Définition des variables
            this._command.Parameters.Clear();
            this._command.Parameters.AddWithValue("@id", id);

            //Recherche du nombre de points
            this._command.CommandText = @"SELECT COUNT(*) AS nb FROM point WHERE id = @id";

            try
            {
                //Execution de la requete
                rdr = this._command.ExecuteReader();
                //Extraction des données
                rdr.Read();
                int nb = rdr.GetInt32(0);   //Nombre de points
                rdr.Close();

                //Construction de la requete
                //SELECT nom, couleur, x AS x1, y AS y1, x2, y2
                //FROM (
                //    SELECT x AS x2, y AS y2
                //    FROM point
                //    WHERE id = @id
                //    AND ordre = 2
                //    ) R1, ..Sous Requetes.., forme f, formesimple fs, point p, ellipse e
                //WHERE f.id = fs.id 
                //AND fs.id = p.id
                //AND fs.id = e.id 
                //AND e.id = @id
                //AND ordre = 1;
                String requete = @"SELECT nom, couleur, x AS x1, y AS y1, x2, y2";
                for (int i = 3; i <= nb; i++) //Commence a 3 car Point 1 et 2 déjà traités dans la requete principale
                {
                    requete += ", x" + i + ", y" + i;
                }
                requete += " FROM ";
                for (int i = 2; i <= nb; i++) //Commence a 2 car Sous requete necésaire dès qu'il y a plus de 1 point
                {
                    requete += @"(
                                SELECT x AS x" + i + ", y AS y" + i + @"
                                FROM point
                                WHERE id = @id
                                AND ordre = " + i + @"
                                ) R" + i + @", ";
                }
                requete += @"forme f, formesimple fs, point pt, polygone p
                            WHERE f.id = fs.id 
                            AND fs.id = pt.id
                            AND fs.id = p.id 
                            AND p.id = @id
                            AND ordre = 1;";

                this._command.CommandText = requete;

                //Execution de la requete
                rdr = this._command.ExecuteReader();
                //Extraction des données
                rdr.Read();

                String nom = rdr.GetString(0);
                int couleur = rdr.GetInt32(1);
                //int count = rdr.FieldCount; //nombre de points
                Point[] tab_point = new Point[nb];

                int j = 0; //j:index dans le tableau de point  i:index dans le tableau des entiers de la requete (X1,Y1,X2,Y2,...)
                for (int i = 0; i <= nb + 1; i += 2) //de 2 en 2, car x et y en meme temps
                {
                    tab_point[j] = new Point(rdr.GetInt32(i + 2), rdr.GetInt32(i + 3));
                    j++;
                }

                //Resultat
                return new Polygone(id, nom, couleur, tab_point);
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
