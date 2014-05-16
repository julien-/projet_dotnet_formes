using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Projet_Formes
{
    class DAOFormeComposee : DAOForme<Forme_composee>
    {
        public override void create(Forme_composee entry) 
        {
            //Données membres
            this._command.Parameters.Clear();
            this._command.Parameters.AddWithValue("@id", entry.Id);
            this._command.Parameters.AddWithValue("@nom", entry.Nom);

            //Définition des requetes
            List<String> tabRequete = new List<String>();
            //forme
            tabRequete.Add(@"INSERT INTO forme(id, nom) VALUES (@id, @nom);");
            //forme composee
            tabRequete.Add(@"INSERT INTO formecompos(id) VALUES (@id);");

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

        public override void delete(Forme_composee entry) 
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

        public override void update(Forme_composee entry) 
        {
            //Données membres
            this._command.Parameters.Clear();
            this._command.Parameters.AddWithValue("@id", entry.Id);
            this._command.Parameters.AddWithValue("@nom", entry.Nom);

            //Définition des requetes
            List<String> tabRequete = new List<String>();

            //forme
            tabRequete.Add(@"UPDATE forme SET nom = @nom WHERE id = @id;");

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

        public override Forme_composee find(Forme_composee entry)
        {
            MySqlDataReader rdr = null;

            //Définition de la requete
            this._command.Parameters.Clear();
            this._command.Parameters.AddWithValue("@id", entry.Id);
            this._command.CommandText = @"SELECT nom
                                        FROM forme";

            try
            {
                //Execution de la requete
                rdr = this._command.ExecuteReader();

                //Extraction des données
                rdr.Read();

                String nom = rdr.GetString(0);

                //Resultat
                return new Forme_composee(entry.Id, nom);

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

        public override List<Forme_composee> find()
        {
            MySqlDataReader rdr = null;
            List<Forme_composee> maliste = new List<Forme_composee>();

            //Définition de la requete
            this._command.Parameters.Clear();
            this._command.CommandText = @"SELECT * FROM forme f, formecompos fc
                                        WHERE f.id = fc.id;";

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
                        maliste.Add(new Forme_composee(id, nom));
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

        public override void createorupdate(Forme_composee entry)
        {

        }
    }
}
