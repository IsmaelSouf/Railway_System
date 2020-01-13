using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Business;

namespace Data
{
    public class Storage
    {
        /* 
         * Author:               40326941 Ismael Souf
         * Description:          This is my storage class which add trains to dictionary.
         *                       This class also save and load to a bin file using singleton and BinaryFormatter
         * Date last modified:   06/12/2018
        */


        private static Storage store;
       
        private Dictionary<string, Train> _trainDictionary;
        private BinaryFormatter _formatter;

        private const string filename = "trainsList.bin";

        private static readonly object padlock = new object();

        //Initializing instance of the class if there isn't one already
        public static Storage Instance()
        {
            if (store == null)
            {
                lock (padlock)
                {
                    if (store == null)
                    {
                        store = new Storage();
                    }
                }
            }

            return store;
        }

        //private constructor
        private Storage()
        {
            _trainDictionary = new Dictionary<string, Train>();
            _formatter = new BinaryFormatter();
        }

        //Method to add train into Dictionary
        public void AddTrain(string trainID, Train train)
        {
            // Check if train with that ID already exists in the trainDictionary
            if (_trainDictionary.ContainsKey(trainID))
            {
                throw new ArgumentException("Train with that ID already exists");


            }
            else
            {
                // Add train in the dictionary
                _trainDictionary.Add(trainID, train);
            }
        }

        //Method to add booking train
        public void AddBooking(string trainID, Booking booking)
        {
            // Check if train with that ID already exists in the trainDictionary
            if (_trainDictionary.ContainsKey(trainID))
            {
                //Check if the Seat and Coach are free in order to book, if yes add booking
                if (!_trainDictionary[trainID].FindBooking(booking.Coach, booking.Seat))
                {

                    _trainDictionary[trainID].AddBooking(booking);

                }
                else
                {
                    throw new ArgumentException("Booking with that ID already exists");

                }
            }
            else
            {
                throw new ArgumentException("Train with that ID not found");
            }
        }


        public void SerializeFile()
        {   
            try
            {
                // Create a FileStream that will write data to file.
                FileStream writer = new FileStream(filename, FileMode.Create, FileAccess.Write);
                // Save our dictionary of trains to file
                this._formatter.Serialize(writer, _trainDictionary);
                // Close the writer FileStream when we are done.
                writer.Close();
            }
            catch (Exception)
            {
                throw new ArgumentException("Unable to save our trains informations.");
            } 
        } 

        public void DeserializeFile()
        {

            // Check if we had previously save informations of our trains
            if (File.Exists(filename))
            {

                try
                {
                    // Create a FileStream that will read access to the data file.
                    FileStream reader = new FileStream(filename, FileMode.Open, FileAccess.Read);
                    // Reconstruct from file.
                    _trainDictionary = (Dictionary<String, Train>)
                    _formatter.Deserialize(reader);
                    // Close the reader FileStream when we are done
                    reader.Close();

                }
                catch (Exception)
                {
                    throw new ArgumentException("Problem occurred with the file.");
                }

            }

        }

        //Get our list of trains
        public List<Train> GetListOfTrains
        {
            get
            {
                // Create a new list of type train
                List<Train> trains = new List<Train>();

                if (_trainDictionary.Count > 0)
                {
                    // Loop through each trains in the dictionary
                    foreach (Train train in _trainDictionary.Values)
                    {
                        trains.Add(train);
                    }
                }
                return trains;

            }
        }

        //Get our list of bookings
        public List<Booking> Bookings
        {
            get
            {
                // Create a new list of type booking
                List<Booking> bookings = new List<Booking>();

                if (_trainDictionary.Count > 0)
                {
                    // Loop through each trains in the dictionary
                    foreach (Train c in _trainDictionary.Values)
                    {
                        // Loop through each booking in the current trains bookings list
                        foreach (Booking b in c.Bookings)
                        {
                            // Add the current booking to the list of bookings
                            bookings.Add(b);
                        }
                    }
                }       
                return bookings;
            }

        }
    }
}

