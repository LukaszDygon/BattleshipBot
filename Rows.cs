using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipBot
{
    public class Rows
    {
        internal static Dictionary<int, char> Row = new Dictionary<int, char>()
        {
            {1, 'A'},
            {2, 'B'},
            {3, 'C'},
            {4, 'D'},
            {5, 'E'},
            {6, 'F'},
            {7, 'G'},
            {8, 'H'},
            {9, 'I'},
            {10, 'J'}
        };

        internal static Dictionary<char, int> RowInt = new Dictionary<char, int>()
        {
            {'A', 1},
            {'B', 2},
            {'C', 3},
            {'D', 4},
            {'E', 5},
            {'F', 6},
            {'G', 7},
            {'H', 8},
            {'I', 9},
            {'J', 10}
        };

        public static char GetRow(int i)
        {
            if (Row.ContainsKey(i)) return Row[i];
            return '\0';
        }

        public static int GetInt(char i)
        {
            if (RowInt.ContainsKey(i)) return RowInt[i];
            return '\0';
        }
    }
}
