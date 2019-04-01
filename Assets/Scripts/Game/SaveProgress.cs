using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class SaveProfiler
{
    public string time;
    public string date;
    public int monstersKilled = 0;

    public override string ToString()
    {
        string result;

        result = time + "|" + date + "|" + monstersKilled + "|";

        return result;
    }

    public void InitFromString(string saveString)
    {
        time = saveString.Substring( 0, 5 );
        date = saveString.Substring( 6, 5 );
        monstersKilled = System.Convert.ToInt32( saveString.Substring( 13, saveString.Length - 13 ) );
    }
}

public class SaveProgress
{
    public void SaveInfo( int monstersKilled )
    {
        SaveProfiler save = new SaveProfiler();

        var timeNow = System.DateTime.Now;

        Debug.Log( "Time is: " + timeNow.ToShortTimeString() );
        Debug.Log( "Date is: " + timeNow.ToShortDateString() );
        Debug.Log( "Monsters killed: " + monstersKilled );

        int timeHours = timeNow.Hour;
        int timeMinutes = timeNow.Minute;
        int dayNow = timeNow.Day;
        int monthNow = timeNow.Month;

        save.date = string.Format( "{0:d2}.{1:d2}", dayNow, monthNow );
        save.time = string.Format( "{0:d2}:{1:d2}", timeHours, timeMinutes );
        save.monstersKilled = monstersKilled;

        using( StreamWriter fileStream = new StreamWriter( "save.data", true, Encoding.Default ) )
        {
            fileStream.WriteLine( save.ToString() );
            fileStream.Close();
        }
    }
}
