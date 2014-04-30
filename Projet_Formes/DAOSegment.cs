using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Projet_Formes
{
    class DAOSegment : DAO <Segment>
    {
        public override void create(Segment entry)
        {
            String Requete = @"INSERT INTO segment(id) VALUES (@color);";
            this._command.CommandText = Requete;

            this._command.Parameters.AddWithValue("@color", entry.Couleur);


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

        public override void delete(Segment entry)
        {

        }

        public override void update(Segment entry)
        {

        }

        /*Segment find(int id)
        {
            return 
        }*/
    }
}
