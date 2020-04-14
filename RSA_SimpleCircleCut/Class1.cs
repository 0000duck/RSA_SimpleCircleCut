using System;
using System.Collections.Generic;
using System.Text;

using ABB.Robotics;
using ABB.Robotics.Math;
using ABB.Robotics.RobotStudio;
using ABB.Robotics.RobotStudio.Environment;
using ABB.Robotics.RobotStudio.Stations;
// using ABB.Robotics.Controllers;

namespace RSA_SimpleCircleCut
{
    public class Class1
    {
        static bool _button2State = true;

        // This is the entry point which will be called when the Add-in is loaded
        public static void AddinMain()
        {
            // Handle events from controls defined in CommandBarControls.xml
            RegisterCommand("RSA_SimpleCircleCut.StartButton");
            RegisterCommand("RSA_SimpleCircleCut.CloseButton");
            RegisterCommand("RSA_SimpleCircleCut.Button1");
            RegisterCommand("RSA_SimpleCircleCut.Button2");
            RegisterCommand("RSA_SimpleCircleCut.GalleryButton1");
            RegisterCommand("RSA_SimpleCircleCut.CreateTargetButton1");
            RegisterCommand("RSA_SimpleCircleCut.CustButton1");
            RegisterCommand("RSA_SimpleCircleCut.CustButton2");
            RegisterCommand("RSA_SimpleCircleCut.CustButton3");
            RegisterCommand("RSA_SimpleCircleCut.CustButton4");
            RegisterCommand("RSA_SimpleCircleCut.CustButton5");
            RegisterCommand("RSA_SimpleCircleCut.CustButton6");
            RegisterCommand("RSA_SimpleCircleCut.CustButton7");
            RegisterCommand("RSA_SimpleCircleCut.CustButton8");

            CommandBarComboBox comboBox = CommandBarComboBox.FromID("RSA_SimpleCircleCut.ComboBox1");
            comboBox.SelectionChanged += comboBox_SelectionChanged;

            CommandBarGalleryPopup gallery = CommandBarGalleryPopup.FromID("RSA_SimpleCircleCut.Gallery1");
            gallery.UpdateContent += gallery_UpdateContent;
        }

        static void RegisterCommand(string id)
        {
            var button = CommandBarButton.FromID(id);
            button.UpdateCommandUI += button_UpdateCommandUI;
            button.ExecuteCommand += button_ExecuteCommand;
        }

        static void button_UpdateCommandUI(object sender, UpdateCommandUIEventArgs e)
        {
            switch (e.Id)
            {
                case "RSA_SimpleCircleCut.StartButton":
                case "RSA_SimpleCircleCut.CloseButton":
                    e.Enabled = true;
                    break;
                case "RSA_SimpleCircleCut.Button1":
                    e.Enabled = true;
                    break;
                case "RSA_SimpleCircleCut.CreateTargetButton1":
                    e.Enabled = true;
                    break;
                case "RSA_SimpleCircleCut.Button2":
                    e.Enabled = true;
                    e.Checked = _button2State;
                    break;
                case "RSA_SimpleCircleCut.GalleryButton1":
                    e.Enabled = true;
                    break;
                case "RSA_SimpleCircleCut.CustButton1":
                    e.Enabled = true;
                    break;
                case "RSA_SimpleCircleCut.CustButton2":
                    e.Enabled = true;
                    break;
                case "RSA_SimpleCircleCut.CustButton3":
                    e.Enabled = true;
                    break;
                case "RSA_SimpleCircleCut.CustButton4":
                    e.Enabled = true;
                    break;
                case "RSA_SimpleCircleCut.CustButton5":
                    e.Enabled = true;
                    break;
                case "RSA_SimpleCircleCut.CustButton6":
                    e.Enabled = true;
                    break;
                case "RSA_SimpleCircleCut.CustButton7":
                    e.Enabled = true;
                    break;
                case "RSA_SimpleCircleCut.CustButton8":
                    e.Enabled = true;
                    break;
            }
        }

        static void button_ExecuteCommand(object sender, ExecuteCommandEventArgs e)
        {
            switch (e.Id)
            {
                case "RSA_SimpleCircleCut.StartButton":
                    {
                        // Show and activate the add-in tab
                        RibbonTab ribbonTab = UIEnvironment.RibbonTabs["RSA_SimpleCircleCut.Tab1"];
                        ribbonTab.Visible = true;
                        UIEnvironment.ActiveRibbonTab = ribbonTab;
                    }
                    break;
                case "RSA_SimpleCircleCut.CloseButton":
                    {
                        // Hide the add-in tab
                        RibbonTab ribbonTab = UIEnvironment.RibbonTabs["RSA_SimpleCircleCut.Tab1"];
                        ribbonTab.Visible = false;
                    }
                    break;
                case "RSA_SimpleCircleCut.Button1":
                    Logger.AddMessage("RSA_SimpleCircleCut: Button1 pressed");
                    try
                    {

                        // Create the first robotstudio target.
                        ShowTarget(new Vector3(-0.50629, -3, 0.67950));

                        // Create the second robotstudio target.
                        ShowTarget(new Vector3(0.500, 0, 0.700));

                    }
                    catch (Exception exception)
                    {
                        Project.UndoContext.CancelUndoStep(CancelUndoStepType.Rollback);
                        Logger.AddMessage(new LogMessage(exception.Message.ToString()));
                    }
                    finally
                    {
                        //End UndoStep
                        Project.UndoContext.EndUndoStep();
                    }
                    break;
                case "RSA_SimpleCircleCut.Button2":
                    Logger.AddMessage("RSA_SimpleCircleCut: Button2 pressed");
                    _button2State = !_button2State;
                    break;
                case "RSA_SimpleCircleCut.GalleryButton1":
                    Logger.AddMessage("RSA_SimpleCircleCut: GalleryButton1 pressed");
                    break;
                case "RSA_SimpleCircleCut.CustButton1":
                    Logger.AddMessage("RSA_SimpleCircleCut: Custom Button 1 pressed");
                    break;
                case "RSA_SimpleCircleCut.CustButton2":
                    Logger.AddMessage("RSA_SimpleCircleCut: Custom Button 2 pressed");
                    break;
                case "RSA_SimpleCircleCut.CustButton3":
                    Logger.AddMessage("RSA_SimpleCircleCut: Custom Button 3 pressed");
                    break;
                case "RSA_SimpleCircleCut.CustButton4":
                    Logger.AddMessage("RSA_SimpleCircleCut: Custom Button 4 pressed");
                    break;
                case "RSA_SimpleCircleCut.CustButton5":
                    Logger.AddMessage("RSA_SimpleCircleCut: Custom Button 5 pressed");
                    break;
                case "RSA_SimpleCircleCut.CustButton6":
                    Logger.AddMessage("RSA_SimpleCircleCut: Custom Button 6 pressed");
                    break;
                case "RSA_SimpleCircleCut.CustButton7":
                    Logger.AddMessage("RSA_SimpleCircleCut: Custom Button 7 pressed");
                    break;
                case "RSA_SimpleCircleCut.CustButton8":
                    Logger.AddMessage("RSA_SimpleCircleCut: Custom Button 8 pressed");
                    break;
            }
        }

        private static void ShowTarget(Vector3 position)
        {
            try
            {
                //get the active station
                Station station = Project.ActiveProject as Station;

                //create robtarget
                RsRobTarget robTarget = new RsRobTarget();
                robTarget.Name = station.ActiveTask.GetValidRapidName("Target", "_", 10);

                //translation
                robTarget.Frame.Translation = position;

                //add robtargets to datadeclaration
                station.ActiveTask.DataDeclarations.Add(robTarget);

                //create target
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




        static void gallery_UpdateContent(object sender, EventArgs e)
        {
            var gallery = (CommandBarGalleryPopup)sender;

            // Add a button to the gallery
            var button = new CommandBarButton(null, "Dynamically added button");
            button.HelpText = "The quick brown fox jumps over the lazy dog";
            button.DefaultEnabled = true;
            button.ExecuteCommand += (s1, e1) =>
            {
                Logger.AddMessage("RSA_SimpleCircleCut: Dynamically added button clicked");
            };
            gallery.GalleryControls.Add(button);

            // Unsubscribe from update event
            gallery.UpdateContent -= gallery_UpdateContent;
        }

        static void comboBox_SelectionChanged(object sender, EventArgs e)
        {
            var comboBox = (CommandBarComboBox)sender;
            Logger.AddMessage(string.Format("RSA_SimpleCircleCut: Item '{0}' selected", comboBox.SelectedItem.Tag));
        }

    }
}