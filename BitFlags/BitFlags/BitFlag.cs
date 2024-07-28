using System;
using System.Reflection;

namespace BitFlags
{
    public class BitFlag
    {
        // The list flags is made out of uLongs
        // It saves the different Bool values
        // The dict translates from a string to a location

        private List<ulong> flags = new List<ulong>();
        private Dictionary<String, int> flaglocation = new Dictionary<String, int>();
        
        // Constructors
        public BitFlag()
        {
            ulong bitflag = 0;
            flags.Add(bitflag);
        }
        // The bool is the default value of all the Strings inputed
        public BitFlag(bool startBool = false, params String[] keyStrings)
        {
            ulong bitflag = 0;
            flags.Add(bitflag);
            foreach (String s in keyStrings)
            {
                add(s, startBool);
            }
        }
        // Add String with start Value
        public void add(String addString, bool startBool) {
            flaglocation.Add(addString, flaglocation.Count);
            change(addString, startBool);
        }
        // Returns Value
        public bool check(String keyString)
        {
            int[] locationArray;
            locationArray = convertToLocation(flaglocation[keyString]);
            return (flags[locationArray[0]] & (uint)(1 << locationArray[1])) != 0;
        }
        // Change Value to bool
        public void change(String keyString, bool boolToChange = false)
        {
            int[] locationArray;
            locationArray = convertToLocation(flaglocation[keyString]);
            ulong mask = Convert.ToUInt64(boolToChange) << locationArray[1];
            if (boolToChange)
            {
                flags[locationArray[0]] |= (uint)(1 << locationArray[1]);
            }
            else
            {
                flags[locationArray[0]] &= ~(uint)(1 << locationArray[1]);
            }
        }
        // Swap Value
        public void swap(String keyString)
        {
            change(keyString, !check(keyString));
        }
        private int[] convertToLocation(int i)
        {
            int[] returnArray = new int[2];
            returnArray[0] = (int)Math.Truncate((float)(i / 64));
            returnArray[1] = i % 64;
            return returnArray;
        }
    }
}
