
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Business;


namespace UnitTestProject1
{
    [TestClass]
    public class TrainTest
    { 

        private Dictionary<string, Train> _trainsDictionary = new Dictionary<string, Train>();
        Train example1 = new Train();
        Train example2 = new Train();


        [TestMethod]
        public void DepartureTest()
        {

            _trainsDictionary.Add("1H34", example1);
            _trainsDictionary.Add("1E32", example2);

            string departure1 = "Edinburgh(Waverly)";
            string departure2 = "London(Kings Cross)";

            example1.Departure = departure1;
            example2.Departure = departure2;

            Assert.AreEqual(departure1, example1.Departure, "Departure Test");
            Assert.AreEqual(departure2, example2.Departure, "Departure Test 2");
         
        }

        [TestMethod()]
        public void DestinationTest()
        {
            _trainsDictionary.Add("1H34", example1);
            _trainsDictionary.Add("1E32", example2);
            string destination1 = "Edinburgh(Waverly)";
            string destination2 = "London(Kings Cross)";
            example1.Destination = destination1;
            example2.Destination = destination2;

            Assert.AreEqual(destination1, example1.Destination, "Destination Test");
            Assert.AreEqual(destination2, example2.Destination, "Destination Test 2");
        }

        [TestMethod()]
        public void DepartureDayTest()
        {
            _trainsDictionary.Add("1H34", example1);
            _trainsDictionary.Add("1E32", example2);

            example1.DateStart = "01/11/2018";
            example2.DateStart = "20/12/2018";

            Assert.AreEqual("01/11/2018", example1.DateStart, "DepartureDay Test");
            Assert.AreEqual("20/12/2018", example2.DateStart, "DepartureDay Test 2");
        }

        [TestMethod()]
        public void DepartureTimeTest()
        {
            _trainsDictionary.Add("1H34", example1);
            _trainsDictionary.Add("1E32", example2);

            string time1 = "11:00";
            string time2 = "21:00";
            example1.Time = time1;
            example2.Time = time2;

            Assert.AreEqual(time1, example1.Time, "DepartureTime Test");
            Assert.AreEqual(time2, example2.Time, "DepartureTime Test 2");
        }

        [TestMethod()]
        public void TypeTest()
        {
            _trainsDictionary.Add("1H34", example1);
            _trainsDictionary.Add("1E32", example2);
            string type1 = "Stopping";
            string type2 = "Sleeper";
            example1.Type = "Stopping";
            example2.Type = "Sleeper";

            Assert.AreEqual(type1, example1.Type, "Type Test");
            Assert.AreEqual(type2, example2.Type, "Type Test 2");
        }

        [TestMethod()]
        public void FirstClassTest()
        {
            _trainsDictionary.Add("1H34", example1);
            _trainsDictionary.Add("1E32", example2);
        
            example1.FirstClass = true;
            example2.FirstClass = false;

            Assert.IsTrue(example1.FirstClass, "FirstClass Test");
            Assert.IsFalse(example2.FirstClass, "FirstClass Test 2");       
        }

        [TestMethod()]
        public void SleeperBerthTest()
        {
            _trainsDictionary.Add("1H34", example1);
            _trainsDictionary.Add("1E32", example2);

            example1.SleeperBerth = false;
            example2.SleeperBerth = true;

            Assert.IsFalse(example1.SleeperBerth, "SleeperBerth Test");
            Assert.IsTrue(example2.SleeperBerth, "SleeperBerth Test 2");
          
        }

        //This test should fail...
        [TestMethod()]
        public void IntermediateTest()
        {
            _trainsDictionary.Add("1H34", example1);
            _trainsDictionary.Add("1E32", example2);


            List<string> _intermediate = new List<string>();
            List<string> _intermediate1 = new List<string>();

           

            _intermediate.Add("Peterbourgh");
            _intermediate = example1.Intermediate;
            _intermediate1.Add("Darlington"+ "York");
            _intermediate1 = example2.Intermediate;

            CollectionAssert.AreEqual(_intermediate, example1.Intermediate);
            CollectionAssert.AreEqual(_intermediate1, example2.Intermediate);
           
        }

    }
}
