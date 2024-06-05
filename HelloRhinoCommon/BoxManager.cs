using System;
using System.Collections.Generic;
using Rhino;
using Rhino.DocObjects;
using Rhino.Geometry;
using Rhino.UI;

namespace HelloRhinoCommon
{
    public class BoxManager
    {
        private static BoxManager _instance;

        private static List<Guid> _boxIds = new List<Guid>();
        private static double _width = 92.0;
        private static double _length = 1200.0;
        private static double _height = 2400.0;
        private static double _spacing = 2000.0;

        private BoxManager() { }

        public static BoxManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new BoxManager();
                }
                return _instance;
            }
        }

        public static void ValidateDoc(RhinoDoc doc)
        {
            if (doc == null)
            {
                throw new ArgumentNullException(nameof(doc), "The Rhino document cannot be null.");
            }
        }

        public int CreateBox(RhinoDoc doc)
        {
            BoxManager.ValidateDoc(doc);

            int count = _boxIds.Count;
            Point3d basePoint = new Point3d(count * (_length + _spacing), 0, 0); // Length along x-axis

            // Check and create block definition
            string blockName = "BoxBlock";
            int blockIndex = CreateBlockDefinition(doc, blockName);

            // Check if block definition was successfully added
            if (blockIndex >= 0)
            {
                Vector3d translationVector = new Vector3d(basePoint);
                Transform transform = Transform.Translation(translationVector);
                Guid instanceId = doc.Objects.AddInstanceObject(blockIndex, transform);
                if (instanceId != Guid.Empty)
                {
                    _boxIds.Add(instanceId);
                }
            }

            return 0;
        }

        private int CreateBlockDefinition(RhinoDoc doc, string blockName)
        {
            BoxManager.ValidateDoc(doc);

            // Check if a block definition with the same name already exists
            var instanceDefinition = doc.InstanceDefinitions.Find(blockName, true);
            if (instanceDefinition != null)
            {
                return instanceDefinition.Index;
            }

            // If not, create a new block definition
            Point3d corner1 = Point3d.Origin;
            Point3d corner2 = new Point3d(_length, _width, _height); // Length along x-axis, Width along y-axis, and Height along z-axis

            // Create a BREP containing the Box
            Box box = new Box(new BoundingBox(corner1, corner2));
            Brep brep = box.ToBrep();

            var attributes = new ObjectAttributes();
            var geometry = new List<GeometryBase> { brep };
            var attributeList = new List<ObjectAttributes> { attributes };

            // Create the block definition
            int blockIndex = doc.InstanceDefinitions.Add(blockName, "Box block definition", corner1, geometry, attributeList);
            return blockIndex;
        }


        public int DeleteBox(RhinoDoc doc)
        {
            BoxManager.ValidateDoc(doc);

            if (_boxIds.Count > 0)
            {
                doc.Objects.Delete(_boxIds[_boxIds.Count - 1], true);
                _boxIds.RemoveAt(_boxIds.Count - 1);
                return 0;
            }
            else
            {
                return 1;
            }
        }

        public int DeleteAllBox(RhinoDoc doc)
        {
            BoxManager.ValidateDoc(doc);
            if (_boxIds.Count > 0)
            {
                foreach (var id in _boxIds)
                {
                    doc.Objects.Delete(id, true);
                }
                _boxIds.Clear();
                return 0;
            }
            else
            {
                return 1;
            }
        }

        public int GetCurrentBoxNumbers()
        {
            return _boxIds.Count;
        }
    }
}