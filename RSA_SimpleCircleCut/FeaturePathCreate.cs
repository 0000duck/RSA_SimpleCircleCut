using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using ABB.Robotics;
using ABB.Robotics.Math;
using ABB.Robotics.RobotStudio;
using ABB.Robotics.RobotStudio.Environment;
using ABB.Robotics.RobotStudio.Stations;
using ABB.Robotics.RobotStudio.Stations.Forms;

/// this library contains utilities to create paths from features parsed
/// 
namespace FeaturePathCreate
{
    /// <summary>
    /// Feature is considered to be in the negative Z direction of given center Transfer (vector)
    /// </summary>
    public class Feature
    {
        public Matrix4 Center { get; set; }
        public int Type { get; set; }
        public List<Matrix4> Path = new List<Matrix4>();

        public enum OfType
        {
            Unknown = -1,
            Circle,
            Slot,
            Tbc
        }

        public void PathToTargets()
        {
            foreach (var Point in Path)
            {
                MyRSTools.ShowTarget(Point);
                MyRSTools.toConsole(Point.ToString());
            }
        }

        // Circle() >>> Arc() can be expanded to include arc portions (or parts of circles) to make it a more versitile building block
    }

    /// <summary>
    /// Create a circle at location 'Center' of 'Diameter
    /// </summary>
    public class Circle : Feature
	{
        // Variables of class
        double Diameter { get; set; }

        // Constructor
        public Circle(Matrix4 center, double diameter)
        {
            Center = center;
            Diameter = diameter;
            Type = (int) OfType.Circle; // Circle

            double tolerance = 0.001; // should be swapped out for a user specified amount
            double radius = Diameter / 2;
            Vector3 radiusTransform = new Vector3(0, radius, 0);
            double theta =  2 * Math.Acos((radius-tolerance)/radius);
            double radian = theta;
            MyRSTools.Math.Convert.DegToRad(ref theta);
            Vector3 axis = Center.GetAxisVector(Axis.Z);
            MyRSTools.toConsole(theta.ToString()); // check if in rad or deg
            MyRSTools.toConsole((360 / theta).ToString());

            for (int i = 0; i <= (int)(360 / theta); i++) // TODO: make sure that the right number of points are created and there are no gaps
            {
                Matrix4 newPoint = center;
                newPoint.Translate(radiusTransform);
                MyRSTools.toConsole(radiusTransform.ToString());

                Path.Add(newPoint);
                MyRSTools.toConsole(newPoint.ToString());

                radiusTransform = radiusTransform.Rotate(axis, radian);

            }
        }
    }



    //public Feature Circle(Transform center, double mmDiameter, double degBevel, double mmTolerance, string removal, double mmToolDiam)
    //{
    //// create feature
    //Feature feature = new Feature();

    //// required attributes
    //double tolerance = mmTolerance / SafetyFactor;
    //double radius = mmDiameter / 2;
    //double pointDAngle = Math.Acos((radius - tolerance) / radius) * 2; // angle between points on circle to reach tolerance
    //radius -= mmToolDiam;

    //feature.center = center;

    //Vector3 v3Center = new Vector3(center.X, center.Y, center.Z);

    //    // feature path creation
    //    for (int i = 0; i > Math.Floor(360 / pointDAngle); i++)
    //{
    //    FeatPath.Targets.Add(new Vector3() { });
    //}






    //// start Here, make a list maker of vector3's


    //return feature;
    //}


}
