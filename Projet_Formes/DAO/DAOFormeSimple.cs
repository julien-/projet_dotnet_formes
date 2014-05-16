using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Projet_Formes
{
    abstract class DAOFormeSimple : DAOForme<Forme_simple>
    {
        protected DAOFormeSimple successor;

        public void SetSuccessor(DAOFormeSimple successor)
        {
            this.successor = successor;
        }

        public override void create(Forme_simple entry)
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

        public override void delete(Forme_simple entry) 
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

        public override void update(Forme_simple entry) 
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

        //public override List<Forme_simple> find()
        //{
        //    return null;
        //}

        public override void createorupdate(Forme_simple entry)
        {
            MySqlDataReader rdr = null;

            //Définition de la requete
            this._command.Parameters.Clear();
            this._command.Parameters.AddWithValue("@id", entry.Id);
            this._command.CommandText = @"SELECT COUNT(*)
                                            FROM forme f
                                            WHERE f.id = @id";

            try
            {
                //Execution de la requete
                rdr = this._command.ExecuteReader();

                rdr.Read();

                int val = rdr.GetInt32(0);

                rdr.Close();

                if (val == 0)
                    create(entry);
                else
                    update(entry);
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());
                throw ex;
            }
        }
    }
}
