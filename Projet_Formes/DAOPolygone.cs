using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Drawing;

namespace Projet_Formes
{
    class DAOPolygone : DAO<Polygone>
    {
        public override void create(Polygone entry)
        {
            //Données membres
            this._command.Parameters.Clear();
            this._command.Parameters.AddWithValue("@id", entry.Id);
            this._command.Parameters.AddWithValue("@nom", entry.Nom);
            this._command.Parameters.AddWithValue("@couleur", entry.Couleur);
         
            //Définition des requetes
            List<String> tabRequete = new List<String>();
            //forme
            tabRequete.Add(@"INSERT INTO forme(id, nom) VALUES (@id, @nom);");
            //forme simple
            tabRequete.Add(@"INSERT INTO formesimple(id, couleur) VALUES (@id, @couleur);");
            //polygone
            tabRequete.Add(@"INSERT INTO polygone(id) VALUES (@id);");
            //point
            for (int i = 1; i <= entry.Tableau_points.Length; i++)
            { 
                tabRequete.Add(@"INSERT INTO point(id, ordre, x, y) VALUES (@id, "+i+", "+entry.Tableau_points[i-1].X+", "+entry.Tableau_points[i-1].Y+");");
            }

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


        public override void delete(Polygone entry)
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


        public override void update(Polygone entry)
        {
            //Données membres
            this._command.Parameters.Clear();
            this._command.Parameters.AddWithValue("@id", entry.Id);
            this._command.Parameters.AddWithValue("@nom", entry.Nom);
            this._command.Parameters.AddWithValue("@couleur", entry.Couleur);


            //Définition des requetes
            List<String> tabRequete = new List<String>();
            //forme
            tabRequete.Add(@"UPDATE forme SET nom = @nom WHERE id = @id;");
            //forme simple
            tabRequete.Add(@"UPDATE formesimple SET couleur = @couleur WHERE id = @id;");
            //point
            for (int i = 1; i <= entry.Tableau_points.Length; i++)
            {
                tabRequete.Add(@"UPDATE point SET x = " + entry.Tableau_points[i - 1].X + ", y = " + entry.Tableau_points[i - 1].Y + " WHERE id = @id AND ordre = " + i + ";");
            }
            


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

        public override Polygone find(int id)
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
                requete += @"forme f, formesimple fs, point p, polygone 
                            WHERE f.id = fs.id 
                            AND fs.id = p.id
                            AND fs.id = e.id 
                            AND polygone.id = @id
                            AND ordre = 1;";


                String nom = rdr.GetString(0);
                String couleur = rdr.GetString(1);
                //int count = rdr.FieldCount; //nombre de points
                Point[] tab_point = new Point[nb];

                int j = 0; //j:index dans le tableau de point  i:index dans le tableau des entiers de la requete (X1,Y1,X2,Y2,...)
                for (int i = 0; i <= nb; i += 2) //de 2 en 2, car x et y en meme temps
                {
                    tab_point[j] = new Point(rdr.GetInt32(i), rdr.GetInt32(i + 1));
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
