using System;
using System.Collections.Generic;
using System.Text;
using ABB.Robotics;
using ABB.Robotics.Math;
using ABB.Robotics.RobotStudio;
using ABB.Robotics.RobotStudio.Environment;
using ABB.Robotics.RobotStudio.Stations;
using ABB.Robotics.RobotStudio.Stations.Forms;
using FeaturePathCreate;

public class MyRSTools
{
    public static void toConsole(string message)
    {
        Logger.AddMessage(message);
    }
    public static void toConsole()
    {
        
        Logger.AddMessage("hello");
    }
}


