using System;
namespace PrestoQServicesEncoding
{
    public enum InputType
    {
        /// <summary>
        /// ASCII encoded string, space right-padded
        /// </summary>
        String = 0,

        /// <summary>
        /// An integer value 8-digits long, zero left-padded
        /// </summary>
        Number = 1,

        /// <summary>
        /// US dollar value, where last two digits represent cents. The leading zero will be replaced with a dash if the value is negative
        /// </summary>
        Currency = 2,

        /// <summary>
        /// Array of Y/N values
        /// </summary>
        Flags = 3
    }
}
