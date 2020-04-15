using System;
using System.Collections.Generic;
using System.Text;
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
	public class Feature
	{
		/// <summary>
		/// Feature is considered to be in the negative Z direction of given center Transfer (vector)
		/// </summary>
		 

		// FeaturePath fields
		public Transform center;
		public FeatPath featPath;
		public double SafetyFactor = 1.5;
		// feature constructor

		

		public Feature Circle(Transform center, double mmDiameter, double mmToolDiam)
		{
			// assume 90deg Bevel (flat wall) & 0.1mm path tolerance
			// internal removal of material (inside) and current tool data
			return Circle(center, mmDiameter,0.1);
		}
		public Feature Circle(Transform center, double mmDiameter, double mmTolerance, double mmToolDiam)
		{
			// assume 90deg Bevel (flat wall)

			return Circle(center, mmDiameter, 90, mmTolerance, "inside", mmToolDiam);
		}
		public Feature Circle(Transform center, double mmDiameter, double degBevel, double mmTolerance, string removal, double mmToolDiam) 
		{
			// create feature
			Feature feature = new Feature();

			// required attribs
			double tolerance = mmTolerance / SafetyFactor;
			double radius = mmDiameter / 2;
			double pointDAngle = Math.Acos((radius - tolerance) / radius);
			radius -= mmToolDiam;

			feature.center = center;

			// feature path creation
			//feature.featPath += 
			
			// start Here, make a list maker of vector3's


			return feature;
		}
		
		// Circle() >>> Arc() can be expanded to include arc portions (or parts of circles) to make it a more versitile building block


		// feature constructor
		public void Square()
		{

		}

		public void SimpleCircle() // might make into an object
		{

		}

		public class FeatPath
		{
			List<Vector3> Targets;
		}



	}

}
