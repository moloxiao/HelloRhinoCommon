using System;
using System.Collections.Generic;
using Rhino;
using Rhino.Commands;
using Rhino.Geometry;
using Rhino.Input;
using Rhino.Input.Custom;

namespace HelloRhinoCommon
{
    public class HelloRhinoCommonCommand : Command
    {
        private static List<Guid> _boxIds = new List<Guid>();
        private static double _width = 92.0;
        private static double _length = 1200.0;
        private static double _height = 2400.0;
        private static double _spacing = 2000.0;

        public HelloRhinoCommonCommand()
        {
            // Rhino only creates one instance of each command class defined in a
            // plug-in, so it is safe to store a refence in a static property.
            Instance = this;
        }

        ///<summary>The only instance of this command.</summary>
        public static HelloRhinoCommonCommand Instance { get; private set; }

        ///<returns>The command name as it appears on the Rhino command line.</returns>
        public override string EnglishName => "HelloDrawLine";

        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            // Prompt user for an integer input
            var getInput = new GetInteger();
            getInput.SetCommandPrompt("Enter a number (1 to create, -1 to delete last, -2 to delete all, 0 to show count):");
            if (getInput.Get() != GetResult.Number)
                return getInput.CommandResult();
            int input = getInput.Number();

            switch (input)
            {
                case 1:
                    // Create a new box
                    CreateBox(doc);
                    RhinoApp.WriteLine("create a box");
                    break;
                case -1:
                    // Delete the last created box
                    if (_boxIds.Count > 0)
                    {
                        doc.Objects.Delete(_boxIds[_boxIds.Count - 1], true);
                        _boxIds.RemoveAt(_boxIds.Count - 1);
                        RhinoApp.WriteLine("delete a box");
                    }
                    else
                    {
                        RhinoApp.WriteLine("no box need to delete");
                    }
                    break;
                case -2:
                    // Delete all created boxes
                    if (_boxIds.Count > 0)
                    {
                        foreach (var id in _boxIds)
                        {
                            doc.Objects.Delete(id, true);
                        }
                        _boxIds.Clear();
                        RhinoApp.WriteLine("delete all boxes");
                    }
                    else
                    {
                        RhinoApp.WriteLine("no box need to delete");
                    }
                    break;
                case 0:
                    // Show the current number of boxes
                    RhinoApp.WriteLine($"current box : {_boxIds.Count}");
                    break;
                default:
                    RhinoApp.WriteLine("no support numbers");
                    break;
            }

            doc.Views.Redraw();
            return Result.Success;
        }

        private void CreateBox(RhinoDoc doc)
        {
            int count = _boxIds.Count;
            Point3d basePoint = new Point3d(count * (_length + _spacing), 0, 0); // Length along x-axis
            Point3d corner1 = basePoint;
            Point3d corner2 = new Point3d(basePoint.X + _length, basePoint.Y + _width, basePoint.Z + _height); // Width along y-axis and height along z-axis

            Box box = new Box(new BoundingBox(corner1, corner2));
            Brep brep = box.ToBrep();
            Guid boxId = doc.Objects.AddBrep(brep);
            _boxIds.Add(boxId);
        }
    }
}

