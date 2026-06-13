using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_ATM
{
    internal class Validador
    {
        public static bool esMontoValido(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;

            int puntos = 0;

            foreach (char c in input)
            {
                if (c == '.')
                {
                    puntos++;
                    if (puntos > 1) return false;
                }
                else if (!char.IsDigit(c))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
