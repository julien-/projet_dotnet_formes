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
        public abstract DAOFormeSimple getDAORectangle();
        public abstract DAOFormeSimple getDAOSegment();
        public abstract DAOFormeSimple getDAOEllipse();
        public abstract DAOFormeSimple getDAOTriangle();
        public abstract DAOFormeSimple getDAOPolygone();
        public abstract DAOFormeComposee getDAOFormeComposee();

        public static AbstractDAOFactory getFactory(FactoryType type)
        {

            if (type.Equals(FactoryType.DAO_FACTORY))
                return new DAOFactory();
            return null;
        }
    }
}
