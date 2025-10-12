using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Globalization;

public class Util
{
    public static float ParseFloat(string s) {
        try {
            return float.Parse(s, CultureInfo.InvariantCulture);
        }

        catch (FormatException)
        {
            Debug.LogWarning("Invalid float format: " + s);
            return 0f; 
        }
        catch (OverflowException)
        {
            Debug.LogWarning("Float value too large or too small: " + s);
            return 0f; 
        }
    }

    public static bool ParseBoolean(string s) {
        return s.ToLower() == "true";
    }

    public static int ParseInt(string s) {
        return int.Parse(s);
    }
}
