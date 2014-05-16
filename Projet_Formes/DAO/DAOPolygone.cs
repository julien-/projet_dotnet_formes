using System;
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
            Type t = typeof(Polygone);
            Type t2 = entry.GetType();
            if (t.Equals(t2))
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
                    tabRequete_poly.Add(@"INSERT INTO point(id, ordre, x, y) VALUES (@id, " + i + ", " + p.Tableau_points[i - 1].X + ", " + p.Tableau_points[i - 1].Y + ");");
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
            else if (successor != null)
            {
                successor.create(entry);
            }
        }

        public override void delete(Forme_simple entry)
        {
            Type t = typeof(Polygone);
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
            Type t = typeof(Polygone);
            Type t2 = entry.GetType();
            if (t.Equals(t2))
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
            else if (successor != null)
            {
                successor.update(entry);
            }
        }

        public override List<Forme_simple> find()
        {
            MySqlDataReader rdr = null;

            //Recherche du nombre de points
            this._command.CommandText = @"SELECT COUNT(*) AS nb FROM point p, polygone po WHERE p.id = po.id";

            try
            {
                //Execution de la requete
                rdr = this._command.ExecuteReader();
                //Extraction des données
                rdr.Read();
                int nb = rdr.GetInt32(0);   //Nombre de points
                rdr.Close();

                List<Forme_simple> maliste = new List<Forme_simple>();
                if(nb > 0)
                {
                    //Définition de la requete
                    this._command.Parameters.Clear();

                    String condition = @"";
                    String requete = @"SELECT p.id, nom, couleur, id_groupe, x AS x1, y AS y1, x2, y2";
                    for (int i = 3; i <= nb; i++) //Commence a 3 car Point 1 et 2 déjà traités dans la requete principale
                    {
                        requete += ", x" + i + ", y" + i;
                    }


                    requete += " FROM ";
                    for (int i = 2; i <= nb; i++) //Commence a 2 car Sous requete necésaire dès qu'il y a plus de 1 point
                    {
                        condition += @" AND R" + i + @".id = p.id";
                        requete +=  @" (SELECT id, x AS x" + i + ", y AS y" + i + 
                                    @" FROM point "+
                                    @" WHERE ordre = " + i + 
                                    @" ) R" + i + @", ";
                    
                    }
                    requete += @" forme f, formesimple fs, point pt, polygone p "+
                               @" WHERE f.id = fs.id "+
                               @" AND fs.id = pt.id "+
                               @" AND fs.id = p.id " +
                                condition +
                                @" AND ordre = 1;";

                    this._command.CommandText = requete;

           
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
                            int idgroupe = rdr.GetInt32(3);
                            Point[] tab_point = new Point[nb];

                            int j = 0; //j:index dans le tableau de point  i:index dans le tableau des entiers de la requete (X1,Y1,X2,Y2,...)
                            for (int i = 1; i < nb *2; i += 2) //de 2 en 2, car x et y en meme temps
                            {
                                tab_point[j] = new Point(rdr.GetInt32(i + 3), rdr.GetInt32(i + 4));
                                j++;
                            }

                            maliste.Add(new Polygone(id, nom, couleur, tab_point, idgroupe));
                        }
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
            Type t = typeof(Polygone);
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
