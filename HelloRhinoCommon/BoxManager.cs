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



        public int CreateBox(RhinoDoc doc)
        {
            int count = _boxIds.Count;
            Point3d basePoint = new Point3d(count * (_length + _spacing), 0, 0); // Length along x-axis
            Point3d corner1 = basePoint;
            Point3d corner2 = new Point3d(basePoint.X + _length, basePoint.Y + _width, basePoint.Z + _height); // Width along y-axis and height along z-axis

            // create box( BREP )
            Box box = new Box(new BoundingBox(corner1, corner2));
            Brep brep = box.ToBrep();

            // create block define
            string blockName = "BoxBlock_" + count;
            while (doc.InstanceDefinitions.Find(blockName) != null)
            {
                blockName = "BoxBlock_" + Guid.NewGuid().ToString("N");
            }

            var attributes = new ObjectAttributes();
            var geometry = new List<GeometryBase> { brep };
            var attributeList = new List<ObjectAttributes> { attributes };

            int blockIndex = doc.InstanceDefinitions.Add(blockName, "Box block definition", basePoint, geometry, attributeList);

            // check if success add block
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







        public int DeleteBox(RhinoDoc doc)
        {
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