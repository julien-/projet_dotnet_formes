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
            this._command.Parameters.AddWithValue("@idgroupe", entry.IdGroupe);

            //Définition des requetes
            List<String> tabRequete = new List<String>();
            //forme
            tabRequete.Add(@"INSERT INTO forme(id, nom, id_groupe) VALUES (@id, @nom, @idgroupe);");
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
            this._command.Parameters.AddWithValue("@idgroupe", entry.IdGroupe);

            //Définition des requetes
            List<String> tabRequete = new List<String>();

            //forme
            tabRequete.Add(@"UPDATE forme SET nom = @nom , id_groupe = @idgroupe WHERE id = @id;");

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

//        public override Forme_composee find(Forme_composee entry)
//        {
//            MySqlDataReader rdr = null;

//            //Définition de la requete
//            this._command.Parameters.Clear();
//            this._command.Parameters.AddWithValue("@id", entry.Id);
//            this._command.CommandText = @"SELECT nom
//                                        FROM forme";

//            try
//            {
//                //Execution de la requete
//                rdr = this._command.ExecuteReader();

//                //Extraction des données
//                rdr.Read();

//                String nom = rdr.GetString(0);

//                //Resultat
//                return new Forme_composee(entry.Id, nom);

//            }
//            catch (MySqlException ex)
//            {
//                Console.WriteLine("Error: {0}", ex.ToString());
//                throw ex;
//            }
//            finally
//            {
//                if (rdr != null)
//                {
//                    rdr.Close();
//                }
//            }
//        }

        public override List<Forme_composee> find()
        {
            MySqlDataReader rdr = null;
            List<Forme_composee> maliste = new List<Forme_composee>();

            try
            {
                List<Forme> malisteforme = new List<Forme>();


                //////////////
                ///// 2 //////
                //////////////

                //Définition de la requete
                this._command.Parameters.Clear();
                this._command.CommandText = @"SELECT fc.id, nom, id_groupe FROM forme f, formecompos fc WHERE f.id = fc.id;";

            
                //Execution de la requete
                rdr = this._command.ExecuteReader();

                //Extraction des données
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        int id = rdr.GetInt32(0);
                        String nom = rdr.GetString(1);
                        int idgroupe = rdr.GetInt32(2);


                        //////////////
                        ///// 1 //////
                        //////////////

                        //definir la liste des formes ici du groupe
                        //Définition de la requete
                        //this._command.Parameters.Clear();
                        //this._command.CommandText = @"SELECT fc.id, nom, id_groupe FROM forme f, formecompos fc WHERE f.id = fc.id;";





                        maliste.Add(new Forme_composee(id, nom, idgroupe, new List<Forme> () ));
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
