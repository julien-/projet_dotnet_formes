using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

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
            //ellipse
            tabRequete.Add(@"INSERT INTO polygone(id) VALUES (@id);");
            //point
            int i = 1;
            foreach (Point p in entry.Liste_points)
            {
                tabRequete.Add(@"INSERT INTO point(id, ordre, x, y) VALUES (@id, "+i+", "+p.X+", "+p.Y+");");
                i++;
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
            int i = 1;
            foreach (Point p in entry.Liste_points)
            {
                tabRequete.Add(@"UPDATE point SET x = " + p.X + ", y = " + p.Y + " WHERE id = @id AND ordre = " + i + ";");
                i++;
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
            //public T find(int id) {
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
                int nb = rdr.GetInt32(0);


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
                for (int i = 3; i <= nb; i++) //Commence a 3 car Point 1 et 2 déjà fais
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
                int count = rdr.FieldCount;
                List<Point> list_point = new List<Point>();

                for (int i = 2; i < count; i += 2)
                {
                    list_point.Add(new Point(rdr.GetInt32(i), rdr.GetInt32(i + 1)));
                }

                //Resultat
                return new Polygone(id, nom, list_point, couleur);
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
