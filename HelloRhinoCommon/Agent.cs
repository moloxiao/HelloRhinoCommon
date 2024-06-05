namespace HelloRhinoCommon
{
    public class Agent
    {
        private static Agent _instance;
        private BoxManager _boxManager;

        private Agent()
        {
            _boxManager = BoxManager.Instance;
        }

        public static Agent Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Agent();
                }
                return _instance;
            }
        }

        public BoxManager BoxManager
        {
            get { return _boxManager; }
        }
    }
}