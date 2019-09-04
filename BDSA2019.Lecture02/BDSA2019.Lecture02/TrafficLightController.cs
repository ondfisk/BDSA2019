using System;
using static BDSA2019.Lecture02.TrafficLightColor;

namespace BDSA2019.Lecture02
{
    public class TrafficLightController : ITrafficLightController
    {
        public bool MayIGo(TrafficLightColor color)
        {
            switch (color)
            {
                default:
                    return false;
                case Green:
                    return true;
            }
        }
    }
}