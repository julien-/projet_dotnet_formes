using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Formes
{
    class DAOFactory : AbstractDAOFactory
    {
        public override DAO<Rectangle> getDAORectangle()
        {
            return new DAORectangle();
        }
        public override DAO<Segment> getDAOSegment()
        {
            return new DAOSegment();
        }
        public override DAO<Ellipse> getDAOEllipse()
        {
            return new DAOEllipse();
        }
        public override DAO<Triangle> getDAOTriangle()
        {
            return new DAOTriangle();
        }
        public override DAO<Polygone> getDAOPolygone()
        {
            return new DAOPolygone();
        }
    }
}
