using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;

public class SaveComparator : IComparer<SaveProfiler>
{
    public int Compare( SaveProfiler x, SaveProfiler y )
    {
        return x.monstersKilled = y.monstersKilled;
    }
}

public class ScoreTable : MonoBehaviour
{
    public Text[] ScoreFields;
    public Text[] DateFields;
    public GameObject AndMoreComponent;

    private List<SaveProfiler> profilers;

    void Start()
    {
        if( File.Exists( "save.data" ) )
        {
            List<string> dataInfo = new List<string>( File.ReadAllLines( "save.data" ) );

            if( dataInfo.Count < 5 )
            {
                AndMoreComponent.SetActive( false );
            }

            foreach( string item in dataInfo )
            {
                SaveProfiler newProf = new SaveProfiler();

                newProf.InitFromString( item );

                profilers.Add( newProf );
            }

            profilers.Sort( new SaveComparator() );

            for( int i = 0; i < 4; i++ )
            {
                Text scoreText = ScoreFields[ i ];
                Text dateText = DateFields[ i ];

                if( i >= profilers.Count )
                {
                    scoreText.gameObject.SetActive( false );
                    dateText.gameObject.SetActive( false );
                }
                else
                {
                    SaveProfiler currentProfile = profilers[ i ];

                    scoreText.text = "" + currentProfile.monstersKilled + " monsters killed";

                    string date = currentProfile.date + " " + currentProfile.time + " ";
                }
            }
        }
        else
        {
            AndMoreComponent.SetActive( false );

            foreach( var item in ScoreFields )
            {
                item.gameObject.SetActive( false );
            }

            foreach( var item in DateFields )
            {
                item.gameObject.SetActive( false );
            }
        }
    }
}
