using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCarpark
{
    class CarparkManager
    {
        static CarparkManager _instance = new CarparkManager();

        Carpark[] carparks;
        readonly string[] CARPARKNAMES = { "South-West", "Mayfair", "North-East", "Wheatfield" };
        readonly string[] DISCOUNTCODES = { "BN123", "TH589", "CK490" };

        //Getter for the static instance of the carpark, to allow accessibility for other "screens"
        public static CarparkManager Instance
        {
            get
            {
                return _instance;
            }
        }

        CarparkManager()
        {
            carparks = new Carpark[4];
            for (int i = 0; i < 4; i++)
            {
                carparks[i] = new Carpark(CARPARKNAMES[i], 5);
            }
        }

        public Carpark GetCarpark(int index)
        {
            return Instance.carparks[index];
        }

        public Carpark GetCarpark(string carpark)
        {
            int index = Array.IndexOf(CARPARKNAMES, carpark);
            return Instance.carparks[index];
        }

        public bool ValidateDiscountCode(string code)
        {
            if (DISCOUNTCODES.Contains(code))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
