using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

class GameSessionManager : MonoBehaviour
{
    public int WaitSecondsAfterLose = 4;

    void Start()
    {
        GameObject crystal = GameObject.Find( "Crystal" );

        if( crystal )
        {
            Crystal crysScript = crystal.GetComponent<Crystal>();

            if( crysScript )
            {
                crysScript.CrystalDestroyed += OnCrystalDestroyed;
            }
        }
    }

    private void OnCrystalDestroyed()
    {
        GameObject crystal = GameObject.Find( "Crystal" );

        if (crystal)
        {
            WaveGenerator waveGen = crystal.GetComponent<WaveGenerator>();

            if (waveGen)
            {
                int currentScore = waveGen.CurrentScore;

                SaveProgress saver = new SaveProgress();

                saver.SaveInfo( currentScore );
            }
        }

        StartCoroutine( DelayEndGame() );
    }

    IEnumerator DelayEndGame()
    {
        yield return new WaitForSeconds( WaitSecondsAfterLose );

        SceneManager.LoadScene( "SampleScene" );
    }
}
