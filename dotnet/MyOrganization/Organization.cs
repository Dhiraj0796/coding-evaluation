using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOrganization
{
    internal abstract class Organization
    {
        private Position root;
        private int uniqueIdentifier = 1;

        public Organization()
        {
            root = CreateOrganization();
        }

        protected abstract Position CreateOrganization();

        /**
         * hire the given person as an employee in the position that has that title
         * 
         * @param person
         * @param title
         * @return the newly filled position or empty if no position has that title
         */
        public Position? Hire(Name person, string title)
        {
            //My changes
            var positionToFill = FindPositionByTitle(root, title);

            if (positionToFill != null)
            {
                var newEmployee = new Employee(GenerateUniqueIdentifier(), person);
                positionToFill.SetEmployee(newEmployee);
                return positionToFill;
            }
            else
            {
                return null;
            }
        }
        private Position? FindPositionByTitle(Position p, string title)
        {
            if (p != null)
            {
                if (p.GetTitle() == title)
                {
                    return p;
                }

                foreach (Position position in p.GetDirectReports())
                {
                    var foundPosition = FindPositionByTitle(position, title);
                    if (foundPosition != null)
                    {
                        return foundPosition;
                    }
                }
            }

            return null;
        }

        private int GenerateUniqueIdentifier()
        {

            return uniqueIdentifier++;
        }

        override public string ToString()
        {
            return PrintOrganization(root, "");
        }

        private string PrintOrganization(Position pos, string prefix)
        {
            StringBuilder sb = new StringBuilder(prefix + "+-" + pos.ToString() + "\n");
            foreach (Position p in pos.GetDirectReports())
            {
                sb.Append(PrintOrganization(p, prefix + "  "));
            }
            return sb.ToString();
        }
    }
}
