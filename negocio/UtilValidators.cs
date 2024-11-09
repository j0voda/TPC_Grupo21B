using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class UtilValidators
    {
        private UtilValidators() {
            throw new Exception("Utility class");
        }

        public static bool isNotNullOrEmpty(params string[] values)
        {
            foreach (var value in values)
            {
                if (value == null && value.Length == 0)
                {
                    return false;
                }
            }
            return true;
        }

    }
}
