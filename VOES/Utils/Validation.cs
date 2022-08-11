using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VOES.Utils
{
    class Validation
    {
        public static Boolean isInteger(String input)
        {
            try
            {
                int num = Convert.ToInt32(input);
                return true;
            }
            catch (Exception)
            {
                //Not an integer
            }
            return false;
        }

        public static Boolean isDouble(String input)
        {
            try
            {
                Double num = Convert.ToDouble(input);
                return true;
            }
            catch (Exception)
            {
                //not double
            }
            return false;
        }

    }
}
