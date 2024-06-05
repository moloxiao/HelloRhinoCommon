using System;
using System.Diagnostics.Tracing;
using Rhino;
using Rhino.Commands;
using Rhino.Input;
using Rhino.Input.Custom;
using Rhino.UI;

namespace HelloRhinoCommon
{
    public class HelloRhinoCommonCommand : Command
    {

        public HelloRhinoCommonCommand()
        {
            // Rhino only creates one instance of each command class defined in a
            // plug-in, so it is safe to store a refence in a static property.
            Instance = this;
        }

        ///<summary>The only instance of this command.</summary>
        public static HelloRhinoCommonCommand Instance { get; private set; }

        ///<returns>The command name as it appears on the Rhino command line.</returns>
        public override string EnglishName => "HelloDrawBox";

        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            // Prompt user for an integer input
            var getInput = new GetInteger();
            getInput.SetCommandPrompt("Enter a number (1 to create, -1 to delete last, -2 to delete all, 0 to show count):");
            if (getInput.Get() != GetResult.Number)
                return getInput.CommandResult();
            int input = getInput.Number();

            try
            {
                AgentCommand(input, doc);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                RhinoApp.WriteLine($"Error: {ex.Message}");
                RhinoApp.WriteLine("The command input is not valid. Please use a supported command.");
            }
            catch (Exception ex)
            {
                RhinoApp.WriteLine($"Unexpected error: {ex.Message}");
                RhinoApp.WriteLine("An unexpected error occurred. Please try again or contact support.");
            }


            doc.Views.Redraw();
            return Result.Success;
        }

        /// <summary>
        /// Executes a command based on the input parameter to manage boxes in the Rhino document.
        /// </summary>
        /// <param name="input">The command input. Valid values are 1 (create a box), -1 (delete the last created box), -2 (delete all boxes), and 0 (show the current number of boxes).</param>
        /// <param name="doc">The Rhino document to apply the commands on.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the input command is not supported.</exception>
        /// <exception cref="ArgumentNullException">Thrown when the Rhino document is null.</exception>
        private void AgentCommand(int input, RhinoDoc doc)
        {
            if (doc == null)
            {
                throw new ArgumentNullException(nameof(doc), "The Rhino document cannot be null.");
            }

            var boxManager = Agent.Instance.BoxManager;

            try
            {
                switch (input)
                {
                    case 1:
                        // Create a new box

                        if (boxManager.CreateBox(doc) == 0)
                        {
                            RhinoApp.WriteLine("create a box");
                        }
                        else
                        {
                            RhinoApp.WriteLine("create a box");
                        }
                        break;
                    case -1:
                        // Delete the last created box

                        if (boxManager.DeleteBox(doc) == 0)
                        {
                            RhinoApp.WriteLine("delete a box");
                        }
                        else
                        {
                            RhinoApp.WriteLine("no box need to delete");
                        }
                        break;
                    case -2:
                        // Delete all created boxes
                        if (boxManager.DeleteAllBox(doc) == 0)
                        {
                            RhinoApp.WriteLine("delete all boxes");
                        }
                        else
                        {
                            RhinoApp.WriteLine("no box need to delete");
                        }
                        break;
                    case 0:
                        // Show the current number of boxes
                        RhinoApp.WriteLine($"current box : {boxManager.GetCurrentBoxNumbers()}");
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(input), "The input command is not supported.");
                }

            }
            catch (Exception ex)
            {
                throw;
            }



        }
    }
}

