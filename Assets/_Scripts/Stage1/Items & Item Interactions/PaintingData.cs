using UnityEngine;

[CreateAssetMenu(fileName = "New Painting Data", menuName = "Louvre/Painting Data")]
public class PaintingData: ScriptableObject{
    public string paintingName;
    public string year;
    public string author;
    [TextArea]
    public string description;
}