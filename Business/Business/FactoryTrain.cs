using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{ 
    
        /*
         * Author:               40326941 Ismael Souf         
         * Description:          This is the FactoryTrain class which build the type of train
         * Date last modified:   06/12/2018
        */
    [Serializable]
    public class FactoryTrain
    {
        public Train BuildTrain(string type)
        {
            if (type.Contains("Express"))
            {
                return new expressTrain();
            }
            else if (type.Contains("Stopping"))
            {
                return new stoppingTrain();
            }
            else if (type.Contains("Sleeper"))
            {
                return new sleeperTrain();
            }
            else
            {
                throw new ArgumentException("Type does not exist");
            }    

        }
    }
}
