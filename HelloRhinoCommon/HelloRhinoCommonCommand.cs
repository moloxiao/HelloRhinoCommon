using Rhino;
using Rhino.Commands;
using Rhino.Input;
using Rhino.Input.Custom;

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

            var boxManager = Agent.Instance.BoxManager;

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
                    RhinoApp.WriteLine("no support numbers");
                    break;
            }

            doc.Views.Redraw();
            return Result.Success;
        }

    }
}

