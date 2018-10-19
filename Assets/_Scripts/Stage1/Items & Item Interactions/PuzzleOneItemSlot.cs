using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameObjectEventArg
{
    public GameObject item;

    public GameObjectEventArg(GameObject go)
    {
        item = go;
    }
}

public class GameObjectAndBoolEventArg
{
    public GameObject item;
    public bool isThisItemRight;

    public GameObjectAndBoolEventArg(GameObject go, bool boo)
    {
        item = go;
        isThisItemRight = boo;
    }
}

public class PuzzleOneItemSlot : ItemSlot
{
    public PaintingGenre acceptableGenre;

    [System.Serializable] public class MyItemPlacedEvent : UnityEvent<GameObjectEventArg> { }
    [System.Serializable] public class MyItemRemovedEvent : UnityEvent<GameObjectAndBoolEventArg> { }

    [SerializeField] private MyItemPlacedEvent OnRightItemFit;
    [SerializeField] private MyItemPlacedEvent OnWrongItemFit;
    [SerializeField] private MyItemRemovedEvent onItemRemoved;

    public new void FitItem(GameObject item)
    {
        //print("fit");
        base.FitItem(item);

        if (item.GetComponent<Board>()) CheckItem(item.GetComponent<Board>());
    }

    private void CheckItem(Board board)
    {
        //print("check");
        if (IsThisItemRight(board))
        {
            OnRightItemFit.Invoke(new GameObjectEventArg(board.gameObject));
        }
        else
        {
            OnWrongItemFit.Invoke(new GameObjectEventArg(board.gameObject));
        }
    }

    private bool IsThisItemRight(Board board)
    {
        return board.myGenre == acceptableGenre;
    }

    public new void OnItemRemoved(GameObject item)
    {
        if (item.GetComponent<Board>()) onItemRemoved.Invoke(new GameObjectAndBoolEventArg(item, IsThisItemRight(item.GetComponent<Board>())));
    }
}
