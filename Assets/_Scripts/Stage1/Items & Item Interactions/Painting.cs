using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum PaintingGenre { None, Madonna, Portrait, Landscape, Mythology, Altar, LightAndShadows }

public class Painting : MonoBehaviour, Readable
{
    //public Board myBoard;
    //public PuzzleOneItemSlot myBoardSlot;
    public PaintingGenre myGenre;
    [SerializeField] PaintingData data;

    public string GetDescription(){
        string str = data.paintingName + ( data.year != "" ? " (" + data.year + ")" : "" ) + ( data.author != "" ? "\nAutor: " + data.author : "" );
        if(data.description != ""){
            str = str + "\n" + data.description;
        }
        return str;
    }

    //private new void Start()
    //{
        //base.Start();

        //myBoard = transform.parent.GetComponentInChildren<Board>();
        //myBoardSlot = transform.parent.GetComponentInChildren<PuzzleOneItemSlot>();

        //if (myBoard == null && myBoardSlot != null)
        //{
            //myBoard = myBoardSlot.rightItemsToFitHere[0].GetComponent<Board>();
        //}
    //}

    //public void SetUpBoardSlot()
    //{
        //myBoardSlot = transform.parent.GetComponentInChildren<PuzzleOneItemSlot>();
    //}

    /*
  [SerializeField] Texture2D painting;
  MeshRenderer mesh;

  // Use this for initialization
  void Start () {
    mesh = GetComponentInChildren<MeshRenderer>();

    var ratio = new Vector3((float)painting.height/painting.width,(float)painting.width/painting.height, 1);

    
    //var targetSize = new Vector3(painting.width, painting.height, mesh.transform.localScale.z);
    transform.localScale = ratio;
    Debug.Log(mesh.bounds);    
  }
  
  // Update is called once per frame
  void Update () {
    
  }

  void OnValidate(){

  }
  */
}