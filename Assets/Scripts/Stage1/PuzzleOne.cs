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

    private List<Item> boardsInPuzzleRoom;

    private void Awake()
    {
        boardsInPuzzleRoom = new List<Item>();
    }

    public void OnRightItemFit(ItemPlacedInSlotEventArg arg)
    {
        //print("certo");
        numberOfBoardsDoneRight++;
        boardsInPuzzleRoom.Add(arg.item.GetComponent<Item>());
        CheckPuzzleProgress();
    }

    public void OnWrongItemFit(ItemPlacedInSlotEventArg arg)
    {
        //print("errado");
        numberOfBoardsDoneWrong++;
        boardsInPuzzleRoom.Add(arg.item.GetComponent<Item>());
        CheckPuzzleProgress();
    }

    private void CheckPuzzleProgress()
    {
        if (numberOfBoardsDoneRight == numberOfBoardsToFill)
        {
            onPuzzleFinished.Invoke();
            numberOfBoardsDoneRight = 0;
        }

        if (numberOfBoardsDoneWrong > 0 && numberOfBoardsDoneRight + numberOfBoardsDoneWrong == numberOfBoardsToFill)
        {
            StartCoroutine(TurnLightsOff());
            numberOfBoardsDoneWrong = 0;
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

        ResetBoards();
    }

    private void ResetBoards()
    {
        foreach (Item board in boardsInPuzzleRoom)
        {
            board.ResetItem();
        }
    }
}
