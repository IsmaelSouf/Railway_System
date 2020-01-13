using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    [Serializable]
    public class Booking
    {
        /*
         * Author:               40326941 Ismael Souf         
         * Description:          This is the booking class which defines the available attributes for each booking
         * Date last modified:   06/12/2018
        */

        // Declare private variables
        private string _name;
        private string _trainRef;
        private bool _firstclass;
        private string _departure;
        private string _arrival;
        private bool _cabin;
        private int _seat;
        private string _coach;

        public Booking()
        {

        }

        //Train ID for booking
        public string TrainID
        {
            get
            {
                return _trainRef;
            }
            set
            {
                if(value == null)
                {
                    throw new ArgumentException("Please enter a train ID");
                }
                else
                {
                    _trainRef = value;
                }
                
            }
        }

        //Name for booking
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if(value != null)
                {
                    _name = value;
                }
                else
                {
                    throw new ArgumentException("Please enter a name for booking");
                }
            }

        }

        //Cabin for booking
        public bool Cabin
        {
            get
            {
                return _cabin;
            }
            set
            {
               _cabin = value;       
            }
        }

        //First class for booking
        public bool FirstClass
        {
            get
            {
                return _firstclass;
            }
            set
            {
                _firstclass = value;
            }
        }

        //Coach for booking
        public string Coach
        {
            get
            {
                return _coach;
            }
            set
            {
                if (value == null)
                {

                    throw new ArgumentException("Not valid coach");
                }
                else
                {
                    
                    _coach = value;
                }

            }
        }

        //Seat for booking must be between 1-60.
        public int Seat
        {
            get
            {
                return _seat;
            }
            set
            {
                if (value < 1 || value > 60)
                {
                    throw new ArgumentException("Seat must be between 1-60");
                }
                else
                {
                    _seat = value;
                }
            }
        }
        //Arrival station for booking
        public string Arrival
        {
            get
            {
                return _arrival;
            }
            set
            {
                
                if (value == null)
                {
                   
                    throw new ArgumentException("Arrival is not valid");
                }
                else
                {
                   
                    _arrival = value;
                }
            }
        }

      //Departure station for booking
        public string Departure
        {
            get
            {
                return _departure;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentException("Departure is not valid");
                }
                else
                {
                    _departure = value;
                }
            }
        }


        public override string ToString()
        {
            return TrainID + " " + Name + " " + Coach + " " + Seat;
        }

    }
}