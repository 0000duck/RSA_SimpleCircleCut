using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;
using ABB.Robotics;
using ABB.Robotics.Math;
using ABB.Robotics.RobotStudio;
using ABB.Robotics.RobotStudio.Environment;
using ABB.Robotics.RobotStudio.Stations;
using ABB.Robotics.RobotStudio.Stations.Forms;
using FeaturePathCreate;
using RobotStudio.API.Internal;

public class MyRSTools
{
    public static void toConsole(string message)
    {
        Logger.AddMessage(message);
    }

    public static void toConsole(Vector3 input, string name)
    {
        Logger.AddMessage("Vector3 " + name + " has contents:");
        Logger.AddMessage("Vector3.x = " + input.x.ToString());
        Logger.AddMessage("Vector3.y = " + input.y.ToString());
        Logger.AddMessage("Vector3.z = " + input.z.ToString());
        Logger.AddMessage("Vector3.x = " + input.IsNaN.ToString());
    }

    public static void toConsole(Matrix4 input, string name)
    {
        Logger.AddMessage("Matrix4 " + name + " has contents:");
        Logger.AddMessage("Matrix4.x = " + input.x.ToString());
        Logger.AddMessage("Matrix4.y = " + input.y.ToString());
        Logger.AddMessage("Matrix4.z = " + input.z.ToString());
        Logger.AddMessage("Matrix4.t = " + input.t.ToString());
        Logger.AddMessage("Matrix4 = " + input.ToString());
        Logger.AddMessage("Matrix4.IsNaN = " + input.IsNaN.ToString());
    }

    public static void toConsole(RsWorkObject input, string name)
    {
        Logger.AddMessage("RsWorkObject " + name + " has contents:");
        Logger.AddMessage("RsWorkObject.Name = " + input.Name.ToString());
        Logger.AddMessage("RsWorkObject.ObjectFrame = " + input.ObjectFrame.ToString());
        MyRSTools.toConsole(input.ObjectFrame, "above");
        Logger.AddMessage("RsWorkObject.UserFrame = " + input.UserFrame.ToString());
        MyRSTools.toConsole(input.UserFrame, "above");
        Logger.AddMessage("RsWorkObject.Children = " + input.Children.ToString());
        Logger.AddMessage("RsWorkObject = " + input.ToString());
    }

    public static void toConsole(Transform input, string name)
    {
        Logger.AddMessage("Transform " + name + " has contents:");
        Logger.AddMessage("Transform.Translation = " + input.Translation.ToString());
        Logger.AddMessage("Transform.GlobalMatrix = " + input.GlobalMatrix.ToString());
        Logger.AddMessage("Transform.Matrix = " + input.Matrix.ToString());
        Logger.AddMessage("Transform = " + input.ToString());
    }


    /// <summary>
    /// This takes the object given by mouse click and resets the bias introduced by the moved workObj
    /// </summary>
    /// <param Vector or Trans="input"></param>
    /// <returns></returns>
    public static Vector3 TranslateToWobj(Vector3 input)
    {
        toConsole(input, "input");

        // get current workobj frame
        Station currentStation = Station.ActiveStation;
        RsWorkObject currentWorkObject = currentStation.ActiveTask.ActiveWorkObject;
        Vector3 objectTranslation = currentWorkObject.ObjectFrame.GlobalMatrix.Translation;
        toConsole(objectTranslation, "Object Translation");
        Vector3 invObjectTranslation = new Vector3(-objectTranslation.x, -objectTranslation.y, -objectTranslation.z);
        toConsole(invObjectTranslation, "Inverse Object Translation");

        toConsole(input + invObjectTranslation, "Translated input");
        return (input - objectTranslation);
    }

    public static void TranslateToWobj(ref Vector3 input)
    {
        toConsole(input, "input");

        // get current workobj frame
        Station currentStation = Station.ActiveStation;
        RsWorkObject currentWorkObject = currentStation.ActiveTask.ActiveWorkObject;
        Vector3 objectTranslation = currentWorkObject.ObjectFrame.GlobalMatrix.Translation;
        toConsole(objectTranslation, "Object Translation");
        Vector3 invObjectTranslation = new Vector3(-objectTranslation.x, -objectTranslation.y, -objectTranslation.z);
        toConsole(invObjectTranslation, "Inverse Object Translation");

        input += invObjectTranslation;
        toConsole(input, "Translated input");
    }

    public static Matrix4 TranslateToWobj(Matrix4 input)
    {
        toConsole(input, "input");

        // get current workobj frame
        Station currentStation = Station.ActiveStation;
        RsWorkObject currentWorkObject = currentStation.ActiveTask.ActiveWorkObject;
        Vector3 objectTranslation = currentWorkObject.ObjectFrame.GlobalMatrix.Translation;
        toConsole(objectTranslation, "Object Translation");
        Vector3 invObjectTranslation = new Vector3(-objectTranslation.x, -objectTranslation.y, -objectTranslation.z);
        toConsole(invObjectTranslation, "Inverse Object Translation");

        // translate output
        input.Translate(invObjectTranslation);
        toConsole(input, "Translated input");
        return input;
    }

    public static void TranslateToWobj(ref Matrix4 input)
    {
        toConsole(input, "input");

        // get current workobj frame
        Station currentStation = Station.ActiveStation;
        RsWorkObject currentWorkObject = currentStation.ActiveTask.ActiveWorkObject;
        Vector3 objectTranslation = currentWorkObject.ObjectFrame.GlobalMatrix.Translation;
        toConsole(objectTranslation, "Object Translation");
        Vector3 invObjectTranslation = new Vector3(-objectTranslation.x, -objectTranslation.y, -objectTranslation.z);
        toConsole(invObjectTranslation, "Inverse Object Translation");

        // translate output
        input.Translate(invObjectTranslation);
        toConsole(input, "Translated input");
    }

    /// <summary>
    /// Place target at the location given
    /// </summary>
    /// <param name="position"></param>
    public static void ShowTarget(Matrix4 position)
    {
        MyRSTools.ShowTarget(position.Translation, Matrix4.Identity);
    }
    public static void ShowTarget(Vector3 position)
    {
        MyRSTools.ShowTarget(position, Matrix4.Identity);
    }
    public static void ShowTarget(Vector3 position, Matrix4 orientation) // TODO: deal with orientation
    {
        try
        {
            //get the active station
            Station station = Project.ActiveProject as Station;

            //create robtarget
            RsRobTarget robTarget = new RsRobTarget();
            robTarget.Name = station.ActiveTask.GetValidRapidName("CustomTarget", "_", 10);

            //translation
            MyRSTools.TranslateToWobj(ref position);  // - if placing relative to workObj
            MyRSTools.toConsole(position, "To control");
            robTarget.Frame.Translation = position;

            //add robtargets to datadeclaration
            station.ActiveTask.DataDeclarations.Add(robTarget);

            //create target
            // RsWorkObject Current = station.ActiveTask.ActiveWorkObject;
            RsTarget target = new RsTarget(station.ActiveTask.ActiveWorkObject, robTarget);
            target.Name = robTarget.Name;
            target.Attributes.Add(target.Name, true);


            //add targets to active task
            station.ActiveTask.Targets.Add(target);
        }
        catch (Exception exception)
        {
            Logger.AddMessage(new LogMessage(exception.Message.ToString()));
        }
    }

    public class Math
    {
        public class Convert
        {
            public static double DegToRad(ref double input)
            {
                input *= 180 / 3.14159265;
                return input;
            }
            public static float DegToRad(ref float input)
            {
                input *= (float)(180 / 3.14159265);
                return input;
            }
        }
    }
}




//    Bash
public class BashTesting
{
    public static void Execute(Vector3 clickVector3)
    {
        Circle testCircle = new Circle(new Matrix4(clickVector3), 0.3);
        testCircle.PathToTargets();
        MyRSTools.toConsole("finished making circle :)");


    }

    public static void Button8Test()
    {

        double pi = Math.PI;
        Vector3 testVector3 = new Vector3(100,200,300);
        Matrix4 testMatrix4 = new Matrix4(testVector3);
        Matrix4 testRadius = new Matrix4(new Vector3(100,0,0));
        double degrees = pi/4;

        Matrix4 output = testRadius;

        MyRSTools.toConsole(testRadius.ToString());
        for (int i = 0; i < 20; i++)
        {
            MyRSTools.toConsole("rotated RZ "+degrees.ToString()+" deg ");
            output.Rotate(Vector3.ZVector, degrees);
            MyRSTools.toConsole(output.ToString());
        }
    }
}


//*/