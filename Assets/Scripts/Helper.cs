using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helper
{
    public static string SecondsToTimeString(float s)
    {
        string hours = ((int) (s / 3600f)).ToString("00");
        string minutes = ((int)(s / 60f)).ToString("00");
        string seconds = ((int)(s % 60f)).ToString("00");
        return $"{hours}:{minutes}:{seconds}";
    }
}
