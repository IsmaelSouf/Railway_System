using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;


namespace Business
{
    [Serializable]
    public class Train
    {
        /* 
         * Author:               40326941 Ismael Souf
         * Description:          This is the trains class for the booking which
         *                       defines the trains attributes
         * Date last modified:   06/12/2018
        */

        // Declare private variables
        private string _trainsID;
        private string _departure;
        private string _destination;
        private string _type;
        private string _departureTime;
        private string _departureDay;
        private bool _firstClass;
        private bool _sleeperBerth;
        private List<Booking> _bookings = new List<Booking>();
        private List<string> _intermediate = new List<string>();

        public Train()
        {
            
        }

        //Train ID must be a code.
        public string TrainID
        {
            
            get
            {
                return _trainsID;
            }
            set
            {
                if(value.Length != 4)
                {
                    throw new ArgumentException("Please enter a Train ID");
                }
                else
                {
                    _trainsID = value;
                }
            }
            
        }
   

        //Departure of train
        public string Departure
        {
            get
            {
                return _departure;
            }
            set
            {
                if (value != null)
                {
                    _departure = value;
                }
                
                else
                {
                    throw new ArgumentException("Please verify the departure and destination.");
                }

            }
        }

        //Destination of train
        public string Destination
        {
            get
            {
                return _destination;
            }
            set
            {
                if (value != null)
                {
                    _destination = value;
                }
                
                else
                {
                    throw new ArgumentException("Please verify the departure and destination.");
                }
            }
        }

        
        //First class of train
        public bool FirstClass
        {
            get
            {
                return _firstClass;
            }
            set
            {            
              _firstClass = value;    
            }
        }

        //Departure date of train
        public string DateStart
        {
            get
            {
                return _departureDay;
            }
            set
            {
               
                if (value != null)
                {
                   
                    _departureDay = value;
                }
                else
                {
                  
                    throw new ArgumentException("Trains start date is not valid DD/MM/YYYY");
                }
            }
        }

        //Departure time of train
        public virtual string Time
        {
            get
            {
                return _departureTime;
            }
            set
            {
               
                if (value != null)
                {
                    _departureTime = value;
                }
                else
                {
                   
                    throw new ArgumentException("Departure time is not valid");
                }
            }
        }
        //Type of train
        public string Type
        {
            get
            {
                return _type;
            }
            set
            {
              
                if (value != null)
                { 
                    _type = value;
                }
                else
                {
                  
                    throw new ArgumentException("Type of trains is not valid");
                }
            }
        }
        //Sleeper berth of train
        public virtual bool SleeperBerth
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

 
        //Method to get intermediates of train
        public virtual string getIntermediate
        {
            get
            {
                var intermediateStop = String.Join(",", _intermediate);
                return intermediateStop;
            }
          
        }
        //List of string for intermediates
        public virtual List<string> Intermediate
        {
            get
            {
                {
                    List<string> res = new List<string>();
                    foreach (var v in _intermediate)
                    {
                        res.Add(v);
                    }
                    return res;
                }
            }
           
        }
        //Method to add intermediates in the List
        public virtual void AddIntermediate(string intermediates)
        {
            _intermediate.Add(intermediates);
        }

        //List of bookings for train
        public List<Booking> Bookings
        {
            get
            {
                return _bookings;
            }
            set
            {
               
                if (value == null)
                {
                   
                    throw new ArgumentException("Passenger does not have any bookings");
                }
                else
                {
                    _bookings = value;
                }
            }
        }
        //Method bool to find if a booking is free
        public bool FindBooking(string coach, int seat)
        {

            foreach (Booking p in _bookings)
            {
                if (coach.Equals(p.Coach) && seat == p.Seat)
                {
                    return true;
                }
            }
            
            return false;
        }

        //Method to add a booking
        public void AddBooking(Booking booking)
        {
            _bookings.Add(booking);

        }

        public override string ToString()
        {
            return TrainID + " " + Departure + "-" + Destination + " " + DateStart + " " + Time + " " + Type + " " + FirstClass + " " + SleeperBerth + " " + getIntermediate;
        }


    }
}
