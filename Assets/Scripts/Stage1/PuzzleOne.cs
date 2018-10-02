using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleOne : MonoBehaviour
{
    [SerializeField] private int numberOfBoardsToFill;
    [SerializeField] private float lightsOffTimeInterval;
    [SerializeField] private TurnLightOnOff[] lights;

    private int numberOfBoardsDoneRight = 0, numberOfBoardsDoneWrong = 0;

    public void OnRightItemFit()
    {
        numberOfBoardsDoneRight++;
    }

    public void OnWrongItemFit()
    {
        numberOfBoardsDoneWrong++;
        StartCoroutine(TurnLightsOff());
    }

    private void CheckPuzzleProgress()
    {
        if (numberOfBoardsDoneRight == numberOfBoardsToFill)
        {
            print("acertou");
        }

        if (numberOfBoardsDoneWrong == numberOfBoardsToFill)
        {
            StartCoroutine(TurnLightsOff());
        }
    }

    IEnumerator TurnLightsOff()
    {
        foreach (TurnLightOnOff light in lights)
        {
            light.TurnOff();
        }

        yield return new WaitForSeconds(lightsOffTimeInterval);
        TurnLightsOn();
    }

    private void TurnLightsOn()
    {
        foreach (TurnLightOnOff light in lights)
        {
            light.TurnOn();
        }
    }
}
