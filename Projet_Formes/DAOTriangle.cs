using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Projet_Formes
{
    class DAOTriangle : DAO<Triangle>
    {
        public override void create(Triangle entry)
        {
            String Requete = @"INSERT INTO triangle(id) VALUES (@color);";
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

        public override void delete(Triangle entry)
        {

        }

        public override void update(Triangle entry)
        {

        }

        /*Triangle find(int id)
        {
            return 
        }*/
    }
}
