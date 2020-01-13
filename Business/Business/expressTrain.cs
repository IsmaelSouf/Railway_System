using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
        /*
         * Author:               40326941 Ismael Souf         
         * Description:          This is a child class of Train which is an expressTrain
         * Date last modified:   06/12/2018
        */
    [Serializable]
    public class expressTrain : Train
    {

        public override List<string> Intermediate
        {
            get
            {
                return null;
            }
        }
    }
}
