using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Drawing;

namespace Projet_Formes
{
    class DAORectangle : DAOFormeSimple
    {
        public override void create(Forme_simple entry)
        {
            Type t = typeof(Rectangle);
            Type t2 = entry.GetType();
            if (t.Equals(t2))
            {
                base.create(entry);
                Rectangle re = (Rectangle)entry;
                this._command.Parameters.AddWithValue("@x1", re.Point1.X);
                this._command.Parameters.AddWithValue("@y1", re.Point1.Y);
                this._command.Parameters.AddWithValue("@hauteur", re.Hauteur);
                this._command.Parameters.AddWithValue("@largeur", re.Largeur);

                //Définition des requetes
                String[] tabRequete = new String[] {
                    //rectangle
                    @"INSERT INTO rectangle(id, hauteur, largeur) VALUES (@id, @hauteur, @largeur);",
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
            Type t = typeof(Rectangle);
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
            Type t = typeof(Rectangle);
            Type t2 = entry.GetType();
            if (t.Equals(t2))
            {
                base.update(entry);
                Rectangle re = (Rectangle)entry;
                this._command.Parameters.AddWithValue("@x1", re.Point1.X);
                this._command.Parameters.AddWithValue("@y1", re.Point1.Y);
                this._command.Parameters.AddWithValue("@hauteur", re.Hauteur);
                this._command.Parameters.AddWithValue("@largeur", re.Largeur);

                //Définition des requetes
                String[] tabRequete = new String[] {
                    //rectangle
                    @"UPDATE rectangle SET hauteur = @hauteur, largeur = @largeur WHERE id = @id;",
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

        public override Forme_simple find(Forme_simple entry)
        {
            Type t = typeof(Rectangle);
            Type t2 = entry.GetType();
            if (t.Equals(t2))
            {
                MySqlDataReader rdr = null;

                //Définition de la requete
                this._command.Parameters.Clear();
                this._command.Parameters.AddWithValue("@id", entry.Id);
                this._command.CommandText = @"SELECT nom, couleur, x, y, hauteur, largeur
                                            FROM forme f, formesimple fs, point p, rectangle r
                                            WHERE f.id = fs.id 
                                            AND fs.id = p.id
                                            AND fs.id = r.id 
                                            AND r.id = @id
                                            AND p.ordre = 1;";

                try
                {
                    //Execution de la requete
                    rdr = this._command.ExecuteReader();

                    //Extraction des données
                    rdr.Read();

                    String nom = rdr.GetString(0);
                    int couleur = rdr.GetInt32(1);
                    int x = rdr.GetInt32(2);
                    int y = rdr.GetInt32(3);
                    int hauteur = rdr.GetInt32(4);
                    int largeur = rdr.GetInt32(5);
                    //Transmforme les données extraites en données membres
                    Point p1 = new Point(x, y);

                    //Resultat
                    return new Rectangle(entry.Id, nom, couleur, p1, hauteur, largeur);
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
            else if (successor != null)
            {
                successor.find(entry);
                return null;
            }
            else
            {
                return null;
            }            
        }

        public override bool presente(Forme_simple entry)
        {
            Type t = typeof(Rectangle);
            Type t2 = entry.GetType();
            if (t.Equals(t2))
            {
                base.presente(entry);
            }
            else if (successor != null)
            {
                successor.presente(entry);
            }
            return false;
        }
    }
}
