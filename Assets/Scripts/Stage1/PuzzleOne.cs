using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PuzzleOne : MonoBehaviour
{
    [SerializeField] private int numberOfBoardsToFill;
    [SerializeField] private float lightsOffTimeInterval;
    [SerializeField] private TurnLightOnOff[] lights;
    [SerializeField] private UnityEvent onPuzzleFinished;

    private int numberOfBoardsDoneRight = 0, numberOfBoardsDoneWrong = 0;

    public void OnRightItemFit()
    {
        numberOfBoardsDoneRight++;
        CheckPuzzleProgress();
    }

    public void OnWrongItemFit()
    {
        numberOfBoardsDoneWrong++;
        CheckPuzzleProgress();
    }

    private void CheckPuzzleProgress()
    {
        if (numberOfBoardsDoneRight == numberOfBoardsToFill)
        {
            onPuzzleFinished.Invoke();
        }

        if (numberOfBoardsDoneWrong > 0 && numberOfBoardsDoneRight + numberOfBoardsDoneWrong == numberOfBoardsToFill)
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
