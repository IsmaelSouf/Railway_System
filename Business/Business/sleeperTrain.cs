using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
       /*
         * Author:               40326941 Ismael Souf         
         * Description:          This is a child class of Train which is an sleeperTrain and override methods
         * Date last modified:   06/12/2018
        */

    [Serializable]
    public class sleeperTrain : Train
    {
        private bool _sleeperBerth;
        private string _departureTime;

        public override bool SleeperBerth
        {
            get
            {
                return _sleeperBerth;
            }
            set
            {
                _sleeperBerth = value;
            }
        }

        public override string Time
        {
            get
            {
                return _departureTime;
            }
            set
            {
                string strTime_Start = value;
                DateTime dateTime_Start = Convert.ToDateTime(strTime_Start);
                if (dateTime_Start.Hour >= 21)
                {
                    _departureTime = value;
                }
                else
                {
                    throw new ArgumentException("Sleeper departs after 21:00");
                }
            }
        }

      
    }
}
