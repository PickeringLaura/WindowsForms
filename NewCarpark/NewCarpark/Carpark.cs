using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCarpark
{
    class Carpark
    {
        string _name;
        Space[] spaces;

        //Default constructor
        public Carpark(string Name, int Spaces)
        {
            spaces = new Space[Spaces];

            for (int i = 0; i < Spaces; i++)
            {
                spaces[i] = new Space(i, i % 20);
            }
        }

        //Getter
        public string Name
        {
            get
            {
                return _name;
            }
        }

        public int GetEmptySpaces()
        {
            //Take our list of spaces, check to see if it is not currently allocated, return the length of the list that isn't allocated.
            return spaces.Where(i => !i.IsAllocated()).ToArray().Length;
        }

        public Space nextAvailableCarParkingSpace()
        {
            return spaces.Where(i => !i.IsAllocated()).First();
        }

        public int getAllocatedSpaces()
        {
            return spaces.Where(i => i.IsAllocated() == true).First().GetId();
        }

        public Space GetSpace(int id)
        {
            return spaces[id];
        }

        public bool ResetSpaces()
        {
            foreach (var space in spaces)
            {
                space.SetAllocated(false);
            }
            return true;
        }
    }
}
