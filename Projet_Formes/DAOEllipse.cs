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
            String Requete = @"INSERT INTO ellipse(id) VALUES (1);";
            this._command.CommandText = Requete;


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
