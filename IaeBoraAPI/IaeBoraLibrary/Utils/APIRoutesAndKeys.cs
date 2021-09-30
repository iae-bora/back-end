namespace IaeBoraLibrary.Utils
{
    public static class APIRoutesAndKeys
    {
        public static string MachineLearningRoute { get => "https://iae-bora-ml.herokuapp.com/"; }
        public static string MachineLearningEndPointRoute { get => "predict"; }

        public static string GeoCordinateAPIRoute { get => "http://api.positionstack.com/v1/forward"; }
        public static string GeoCordinateAPIKey { get => "1447f0ffb3047e09e2af0f9d6d3024c2"; }
    }

}
