using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Projet_Formes
{
    class DAOEllipse : DAO<Ellipse>
    {
        public override void create(Ellipse entry)
        {

            this._command.CommandText = @"INSERT INTO ellipse(id) VALUES (@id);";
            this._command.Parameters.AddWithValue("@id", entry.Id);

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

        public override void delete(Ellipse entry)
        {

        }

        public override void update(Ellipse entry)
        {

        }

        /*Ellipse find(int id)
        {
            return 
        }*/
    }
}
