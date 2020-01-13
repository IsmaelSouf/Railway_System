using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;
using Business;
using Data;


namespace Presentation
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        FactoryTrain Factory = new FactoryTrain();
        Storage trains = Storage.Instance();

        public MainWindow()
        {

            InitializeComponent();
            trains.DeserializeFile();

            foreach (var s in trains.GetListOfTrains)
            {
                lbxTrain.Items.Add(s);
            }

            foreach (var u in trains.Bookings)
            {
                lbxBooking.Items.Add(u);
            }     

            DateTime time = DateTime.Today;

            //Loop through time and populate to the appropriate combobox
            for (DateTime _time = time.AddHours(0); _time < time.AddHours(24); _time = _time.AddMinutes(30))
            {
                timeBox.Items.Add(_time.ToShortTimeString());
            }

            //Hide visibility of intermediates until type is selected 
            chkPeterborough.Visibility = Visibility.Hidden;
            chkDarlington.Visibility = Visibility.Hidden;
            chkYork.Visibility = Visibility.Hidden;
            chkNewcastle.Visibility = Visibility.Hidden;
            lblintermediate.Visibility = Visibility.Hidden;

            // Populate the seat combo box 1-60
            populateSeatList();

        }

        private void Window_Closed(object sender, CancelEventArgs e)
        {
            // Serialize the trains list including booking
            trains.SerializeFile();
        }



        private void AddTrains_Click(object sender, RoutedEventArgs e)
        {
            //Runs add train method
            AddTrain();
        }


        public void AddTrain()
        {


            try
            {
                //If the type of train is not selected
                if (typeBox.SelectedIndex == -1)
                {
                    throw new ArgumentException("Type undefined");
                }

                //Use type selected to get the right type of train in the factory
                var train = Factory.BuildTrain(typeBox.SelectedItem.ToString());

                if (typeBox.SelectedItem != null)
                {
                    train.Type = typeBox.SelectionBoxItem.ToString();
                }

                if (txtTrainsID.Text.Length != 4)
                {
                    throw new ArgumentException("Invalid Trains ID.");
                }
                else
                {
                    train.TrainID = txtTrainsID.Text;
                }
                 
                if (departureBox.SelectedIndex == 0)
                {
                    train.Departure = "Edinburgh(Waverly)";
                }
                else if (departureBox.SelectedIndex == 1)
                {
                    train.Departure = "London(Kings Cross)";
                }
                else
                {
                    throw new ArgumentException("Invalid data for the departure station!");
                }

                if (destinationBox.SelectedIndex == 0)
                {
                    train.Destination = "Edinburgh(Waverly)";
                }
                else if (destinationBox.SelectedIndex == 1)
                {
                    train.Destination = "London(Kings Cross)";
                }
                else
                {
                    throw new ArgumentException("Invalid data for the destination station!");
                }

                if (train.Departure == train.Destination)
                {
                    throw new ArgumentException("Departure and Destination can not be the same!");
                }

                //If a date is selected in the departure picker, give the date selected
                if (departurePicker.SelectedDate != null)
                {
                    train.DateStart = departurePicker.SelectedDate.Value.ToShortDateString();
                }
                else
                {
                    throw new ArgumentException("Please select a departure day.");
                }

                //If first class is checked the train has first class
                if (chkFirstClass.IsChecked.Value)
                {
                    train.FirstClass = true;
                }
                else
                {
                    train.FirstClass = false;
                }

                //If the type of train is a sleeper and sleeper berth is checked then the train has sleeperberth
                if (chkSleeperBerth.IsChecked.Value && typeBox.SelectedIndex == 2)
                {
                    train.SleeperBerth = true;
                }
                else
                {
                    train.SleeperBerth = false;
                }

                //Convert string time to Datetime 
                if (timeBox.SelectedIndex >= 0)
                {
                    string strTime_Start = timeBox.SelectedItem.ToString();
                    DateTime dateTime_Start = Convert.ToDateTime(strTime_Start);
                    train.Time = dateTime_Start.ToShortTimeString();

                }
                else
                {
                    throw new ArgumentException("Please select a departure time");
                }

                //If station is checked, add it to the train.
                if (chkPeterborough.IsChecked == true)
                {
                    train.AddIntermediate("Peteborough");
                }

                if (chkDarlington.IsChecked == true)
                {
                    train.AddIntermediate("Darlington");
                }

                if (chkYork.IsChecked == true)
                {
                    train.AddIntermediate("York");
                }

                if (chkNewcastle.IsChecked == true)
                {
                    train.AddIntermediate("Newcastle");
                }

                //Add trains
                trains.AddTrain(train.TrainID, train);
                //Display train properties
                lbxTrain.Items.Add(train);

                MessageBox.Show("Train added !");

            }
            catch (ArgumentException excep)
            {              
                MessageBoxButton btnMessageBox = MessageBoxButton.OK;           
                string caption = "Trains Error";
                MessageBox.Show(excep.Message, caption, btnMessageBox);
            }
           
        }




        public void populateSeatList()
        {
            int maxSeat = 60;
            // Loop 60 times
            for (int i = 1; i <= maxSeat; i++)
            {          
                seatBox.Items.Add(i.ToString());
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void timeBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


        }

        private void typeBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //If the type of train is Stopping or Sleeper, show intermediates
            if (typeBox.SelectedIndex >= 1)
            {
                chkPeterborough.Visibility = Visibility.Visible;
                chkDarlington.Visibility = Visibility.Visible;
                chkYork.Visibility = Visibility.Visible;
                chkNewcastle.Visibility = Visibility.Visible;
                lblintermediate.Visibility = Visibility.Visible;

            }
            else
            {
                chkPeterborough.Visibility = Visibility.Hidden;
                chkDarlington.Visibility = Visibility.Hidden;
                chkYork.Visibility = Visibility.Hidden;
                chkNewcastle.Visibility = Visibility.Hidden;
                lblintermediate.Visibility = Visibility.Hidden;

            }

        }



        private void intermediateBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
        //Add booking when button is pressed
        private void btnAddBooking_Click(object sender, RoutedEventArgs e)
        {

            Booking passenger = new Booking();
            Train train = new Train();
            int _interCost = 25;
            int _mainCost = 50;

            try
            {

               //If txtTrainBook is null or empty train ID is invalid
                if(String.IsNullOrEmpty(txtTrainBook.Text))
                {
                    throw new ArgumentException("Train ID is invalid");
                }
                else
                {
                    passenger.TrainID = txtTrainBook.Text;
                }
               
                //If txtName is null or blank name is invalid
                if (String.IsNullOrWhiteSpace(txtName.Text))
                {
                    throw new ArgumentException("Please enter a name for booking");
                }
                else
                {
                    passenger.Name = txtName.Text;
                }
             
                if (coachBox.SelectedIndex == -1)
                {
                    throw new ArgumentException("Please select a Coach for booking");
                }
                else
                {
                    passenger.Coach = coachBox.SelectionBoxItem.ToString();
                }


                if (seatBox.SelectedIndex == -1)
                {
                    throw new ArgumentException("Please select a Seat for booking");
                }
                else
                {
                    passenger.Seat = Convert.ToInt32(seatBox.SelectionBoxItem.ToString());
                }

                if (departBox.SelectedIndex == -1 || arrivalBox.SelectedIndex == -1)
                {
                    throw new ArgumentException("Please select a Departure and Arrival station");
                }
                else if (departBox.SelectionBoxItem.ToString() == arrivalBox.SelectionBoxItem.ToString())
                {
                    throw new ArgumentException("Departure and Arrival must be different");
                }
                else
                {
                    passenger.Departure = departBox.SelectionBoxItem.ToString();
                    passenger.Arrival = arrivalBox.SelectionBoxItem.ToString();
                }

                if (train.SleeperBerth == true && chkCabin.IsChecked == true)
                {
                    passenger.Cabin = true;
                }
                else if (train.SleeperBerth == true && chkCabin.IsChecked == false)
                {
                    passenger.Cabin = false;
                }
                else if (train.SleeperBerth == false)
                {
                    passenger.Cabin = false;
                }
                else
                {
                    throw new ArgumentException("Train or coach does not offer sleeper berth");
                }

                if (train.FirstClass == true && chkFirstClass2.IsChecked == true)
                {
                    passenger.FirstClass = true;
                }
                else if (train.FirstClass == true && chkFirstClass2.IsChecked == false)
                {
                    passenger.FirstClass = false;
                }
                else if (train.FirstClass == false)
                {
                    passenger.FirstClass = false;
                }
                else
                {
                    throw new ArgumentException("Train or coach does not offer first class");
                }
               

                //Set booking fare for trains
                if (departBox.SelectionBoxItem.ToString() == "Edinburgh(Waverly)" && arrivalBox.SelectionBoxItem.ToString() == "London(Kings Cross)")
                {
                    if (chkCabin.IsChecked == true && chkFirstClass2.IsChecked == true)
                    {
                        switch (MessageBox.Show("Booking will cost £" + 90, "Booking Fare", MessageBoxButton.YesNoCancel, MessageBoxImage.Question))
                        {
                            case MessageBoxResult.Yes:
                                MessageBox.Show("Booking purchased");
                                break;

                            case MessageBoxResult.No:
                                return;


                            case MessageBoxResult.Cancel:
                                return;

                        }
                    }

                    if (chkCabin.IsChecked == true && chkFirstClass2.IsChecked == false)
                    {
                        switch (MessageBox.Show("Booking will cost £" + 80, "Booking Fare", MessageBoxButton.YesNoCancel, MessageBoxImage.Question))
                        {
                            case MessageBoxResult.Yes:
                                MessageBox.Show("Booking purchased");
                                break;

                            case MessageBoxResult.No:
                                return;


                            case MessageBoxResult.Cancel:
                                return;

                        }
                    }
                    if (chkFirstClass2.IsChecked == true && chkCabin.IsChecked == false)
                    {
                        switch (MessageBox.Show("Booking will cost £" + 60, "Booking Fare", MessageBoxButton.YesNoCancel, MessageBoxImage.Question))
                        {
                            case MessageBoxResult.Yes:
                                MessageBox.Show("Booking purchased");
                                break;

                            case MessageBoxResult.No:
                                return;


                            case MessageBoxResult.Cancel:
                                return;

                        }
                    }

                    if(chkFirstClass2.IsChecked == false && chkCabin.IsChecked == false)
                    {
                        switch (MessageBox.Show("Booking will cost £" + 50, "Booking Fare", MessageBoxButton.YesNoCancel, MessageBoxImage.Question))
                        {
                            case MessageBoxResult.Yes:
                                MessageBox.Show("Booking purchased");
                                break;

                            case MessageBoxResult.No:
                                return;


                            case MessageBoxResult.Cancel:
                                return;

                        }
                    }


                }
                else if (departBox.SelectionBoxItem.ToString() == "London(Kings Cross)" && arrivalBox.SelectionBoxItem.ToString() == "Edinburgh(Waverly)")
                {
                    if (chkCabin.IsChecked == true && chkFirstClass2.IsChecked == true)
                    {
                        switch (MessageBox.Show("Booking will cost £" + 90, "Booking Fare", MessageBoxButton.YesNoCancel, MessageBoxImage.Question))
                        {
                            case MessageBoxResult.Yes:
                                MessageBox.Show("Booking purchased");
                                break;

                            case MessageBoxResult.No:
                                return;


                            case MessageBoxResult.Cancel:
                                return;

                        }
                    }
                    if (chkFirstClass2.IsChecked == true)
                    {
                        switch (MessageBox.Show("Booking will cost £" + 60, "Booking Fare", MessageBoxButton.YesNoCancel, MessageBoxImage.Question))
                        {
                            case MessageBoxResult.Yes:
                                MessageBox.Show("Booking purchased");
                                break;

                            case MessageBoxResult.No:
                                return;


                            case MessageBoxResult.Cancel:
                                return;

                        }
                    }
                    else
                    {
                        switch (MessageBox.Show("Booking will cost £" + 50, "Booking Fare", MessageBoxButton.YesNoCancel, MessageBoxImage.Question))
                        {
                            case MessageBoxResult.Yes:
                                MessageBox.Show("Booking purchased");
                                break;

                            case MessageBoxResult.No:
                                return;


                            case MessageBoxResult.Cancel:
                                MessageBox.Show("Booking Cancelled");
                                return;

                        }
                    }
                }
               

                if ((departBox.SelectionBoxItem.ToString() == "Peterborough" || departBox.SelectionBoxItem.ToString() == "Darlington" || departBox.SelectionBoxItem.ToString() == "York" || departBox.SelectionBoxItem.ToString() == "NewCastle" || departBox.SelectionBoxItem.ToString() == "Edinburgh(Waverly)" || departBox.SelectionBoxItem.ToString() == "London(Kings Cross)") && (arrivalBox.SelectionBoxItem.ToString() == "Peterborough" || arrivalBox.SelectionBoxItem.ToString() == "Darlington" || arrivalBox.SelectionBoxItem.ToString() == "York" || arrivalBox.SelectionBoxItem.ToString() == "Newcastle"))
                {
                    if (chkCabin.IsChecked == true && chkFirstClass2.IsChecked == true)
                    {
                        switch (MessageBox.Show("Booking will cost £" + 65, "Booking Fare", MessageBoxButton.YesNoCancel, MessageBoxImage.Question))
                        {
                            case MessageBoxResult.Yes:
                                MessageBox.Show("Booking purchased");
                                break;

                            case MessageBoxResult.No:
                                return;


                            case MessageBoxResult.Cancel:
                                return;

                        }
                    }

                    if (chkCabin.IsChecked == true && chkFirstClass2.IsChecked == false)
                    {
                        switch (MessageBox.Show("Booking will cost £" + 55, "Booking Fare", MessageBoxButton.YesNoCancel, MessageBoxImage.Question))
                        {
                            case MessageBoxResult.Yes:
                                MessageBox.Show("Booking purchased");
                                break;

                            case MessageBoxResult.No:
                                return;


                            case MessageBoxResult.Cancel:
                                return;

                        }
                    }

                    if (chkFirstClass2.IsChecked == true && chkCabin.IsChecked == false)
                    {
                        switch (MessageBox.Show("Booking will cost £" + 35, "Booking Fare", MessageBoxButton.YesNoCancel, MessageBoxImage.Question))
                        {
                            case MessageBoxResult.Yes:
                                MessageBox.Show("Booking purchased");
                                break;

                            case MessageBoxResult.No:
                                return;


                            case MessageBoxResult.Cancel:
                                return;

                        }
                    }

                    if(chkFirstClass2.IsChecked == false && chkCabin.IsChecked == false)
                    {
                        switch (MessageBox.Show("Booking will cost £" + 25, "Booking Fare", MessageBoxButton.YesNoCancel, MessageBoxImage.Question))
                        {
                            case MessageBoxResult.Yes:
                                MessageBox.Show("Booking purchased");
                                break;

                            case MessageBoxResult.No:
                                return;


                            case MessageBoxResult.Cancel:
                                MessageBox.Show("Booking Cancelled");
                                return;

                        }
                    }
                }

                else if ((departBox.SelectionBoxItem.ToString() == "Peterborough" || departBox.SelectionBoxItem.ToString() == "Darlington" || departBox.SelectionBoxItem.ToString() == "York" || departBox.SelectionBoxItem.ToString() == "Newcastle") && (arrivalBox.SelectionBoxItem.ToString() == "Peterborough" || arrivalBox.SelectionBoxItem.ToString() == "Darlington" || arrivalBox.SelectionBoxItem.ToString() == "York" || arrivalBox.SelectionBoxItem.ToString() == "Newcastle" || arrivalBox.SelectionBoxItem.ToString() == "Edinburgh(Waverly)" || arrivalBox.SelectionBoxItem.ToString() == "London(Kings Cross)"))
                {
                    if (chkCabin.IsChecked == true && chkFirstClass2.IsChecked == true)
                    {
                        switch (MessageBox.Show("Booking will cost £" + 65, "Booking Fare", MessageBoxButton.YesNoCancel, MessageBoxImage.Question))
                        {
                            case MessageBoxResult.Yes:
                                MessageBox.Show("Booking purchased");
                                break;

                            case MessageBoxResult.No:
                                return;


                            case MessageBoxResult.Cancel:
                                return;

                        }
                    }

                    if (chkCabin.IsChecked == true)
                    {
                        switch (MessageBox.Show("Booking will cost £" + 55, "Booking Fare", MessageBoxButton.YesNoCancel, MessageBoxImage.Question))
                        {
                            case MessageBoxResult.Yes:
                                MessageBox.Show("Booking purchased");
                                break;

                            case MessageBoxResult.No:
                                return;


                            case MessageBoxResult.Cancel:
                                return;

                        }
                    }

                    if (chkFirstClass2.IsChecked == true)
                    {
                        switch (MessageBox.Show("Booking will cost £" + 35, "Booking Fare", MessageBoxButton.YesNoCancel, MessageBoxImage.Question))
                        {
                            case MessageBoxResult.Yes:
                                MessageBox.Show("Booking purchased");
                                break;

                            case MessageBoxResult.No:
                                return;


                            case MessageBoxResult.Cancel:
                                return;

                        }
                    }
                    else
                    {

                        switch (MessageBox.Show("Booking will cost £" + 25, "Booking Fare", MessageBoxButton.YesNoCancel, MessageBoxImage.Question))
                        {
                            case MessageBoxResult.Yes:
                                MessageBox.Show("Booking purchased");
                                break;

                            case MessageBoxResult.No:
                                return;


                            case MessageBoxResult.Cancel:
                                MessageBox.Show("Booking Cancelled");
                                return;

                        }
                    }

                 }

               trains.AddBooking(passenger.TrainID, passenger);
               lbxBooking.Items.Add(passenger);

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }


        }


        private void listbox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
      
            
        }

        private void FindTrain_Click(object sender, RoutedEventArgs e)
        {

            lbxDate.Items.Clear();

            //For each trains if a train has a selected date, display the trainID, the date and time of departure
            foreach (var res in trains.GetListOfTrains)
            {

                if (res.DateStart.Equals(departurePicker.Text))
                {
                    lbxDate.Items.Add(res.TrainID + " " + res.DateStart + " " + res.Time);
                }
            }
                
      
        }

        private void departureBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void timeBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void destinationBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void listbox3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void departBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void arrivalBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void coachBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void seatBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void lbxDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
  
    }
}
