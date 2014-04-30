using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Formes
{
    public enum FactoryType
    {
        DAO_FACTORY
    }

    abstract class AbstractDAOFactory
    {
        public abstract DAO<Rectangle> getDAORectangle();
        public abstract DAO<Segment> getDAOSegment();
        public abstract DAO<Ellipse> getDAOEllipse();
        public abstract DAO<Triangle> getDAOTriangle();
        public abstract DAO<Polygone> getDAOPolygone();

        public static AbstractDAOFactory getFactory(FactoryType type)
        {

            if (type.Equals(FactoryType.DAO_FACTORY))
                return new DAOFactory();
            return null;
        }
    }
}
