namespace NIWAServices.Enums;

public enum NIWATidesDatum
{
    LowestAstronomicalTide,
    MeanSeaLevel
}

public static class NIWATidesDatumExtensions
{
    public static string ToNIWAString(this NIWATidesDatum value)
    {
        switch (value)
        {
            case NIWATidesDatum.LowestAstronomicalTide:
                return "LAT";

            case NIWATidesDatum.MeanSeaLevel:
                return "MSL";
        }

        throw new NotSupportedException("Unknown NIWADatum value");
    }
}