using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCarpark
{
    class Space
    {
        int _id;
        int _floor;
        bool _allocated;

        public Space(int id, int floor)
        {
            _id = id;
            _floor = floor;
            _allocated = false;
        }

        public bool IsAllocated()
        {
            return _allocated;
        }

        public void SetAllocated(bool value)
        {
            _allocated = value;
        }

        public int GetId()
        {
            return _id;
        }
    }
}
