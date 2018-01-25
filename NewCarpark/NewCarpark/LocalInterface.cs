using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCarpark
{
    class LocalInterface
    {
        static LocalInterface _local = new LocalInterface(0);

        public static LocalInterface Instance
        {
            get
            {
                return _local;
            }
        }

        public static void LoadCarpark(int carpark)
        {
            _local = new LocalInterface(carpark);
        }

        public static void LoadCarpark(String carpark)
        {
            _local = new LocalInterface(carpark);
        }

        Carpark _carpark;

        LocalInterface(String carparkName)
        {
            _carpark = CarparkManager.Instance.GetCarpark(carparkName);
        }

        LocalInterface(int carparkName)
        {
            _carpark = CarparkManager.Instance.GetCarpark(carparkName);
        }

        public int GetSpaces()
        {
            return Instance._carpark.GetEmptySpaces();
        }
    }
}
