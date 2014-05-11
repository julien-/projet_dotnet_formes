using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Formes
{
    class DAOFactory : AbstractDAOFactory
    {
        public override DAOFormeSimple getDAORectangle()
        {
            return new DAORectangle();
        }
        public override DAOFormeSimple getDAOSegment()
        {
            return new DAOSegment();
        }
        public override DAOFormeSimple getDAOEllipse()
        {
            return new DAOEllipse();
        }
        public override DAOFormeSimple getDAOTriangle()
        {
            return new DAOTriangle();
        }
        public override DAOFormeSimple getDAOPolygone()
        {
            return new DAOPolygone();
        }
        public override DAOFormeComposee getDAOFormeComposee()
        {
            return new DAOFormeComposee();
        }
    }
}
