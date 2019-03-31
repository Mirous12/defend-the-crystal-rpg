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
        StartCoroutine( DelayEndGame() );
    }

    IEnumerator DelayEndGame()
    {
        yield return new WaitForSeconds( WaitSecondsAfterLose );

        SceneManager.LoadScene( "SampleScene" );
    }
}
