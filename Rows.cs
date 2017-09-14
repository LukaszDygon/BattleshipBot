using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipBot
{
    public class Rows
    {
        internal static Dictionary<int, char> R = new Dictionary<int, char>()
        {
            {1, 'A'},
            {2, 'B'},
            {3, 'C'},
            {4, 'D'},
            {5, 'E'},
            {6, 'F'},
            {7, 'G'},
            {8, 'H'}
        };

        public static char GetRow(int i)
        {
            if (R.ContainsKey(i)) return R[i];
            return '\0';
        }
    }
}
