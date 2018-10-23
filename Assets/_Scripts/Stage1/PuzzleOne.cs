using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public enum PaintingGenre { None, Madonna, Portrait, Landscape, Mythology, Altar, LightAndShadows }

public class PuzzleOne : MonoBehaviour
{
    [Tooltip("Prefabs dos quadros do tipo Madonna que poderão aparecer na sala puzzle.")]
    [SerializeField] private List<GameObject> madonnaPrefabs;
    [Tooltip("Prefabs dos quadros do tipo Retrato que poderão aparecer na sala puzzle.")]
    [SerializeField] private List<GameObject> portraitPrefabs;
    [Tooltip("Prefabs dos quadros do tipo Paisagismo que poderão aparecer na sala puzzle.")]
    [SerializeField] private List<GameObject> landscapePrefabs;
    [Tooltip("Prefabs dos quadros do tipo Mitologia que poderão aparecer na sala puzzle.")]
    [SerializeField] private List<GameObject> mythologyPrefabs;
    [Tooltip("Prefabs dos quadros do tipo Altar que poderão aparecer na sala puzzle.")]
    [SerializeField] private List<GameObject> altarPrefabs;
    [Tooltip("Prefabs dos quadros do tipo Luz e Sombras que poderão aparecer na sala puzzle.")]
    [SerializeField] private List<GameObject> lightAndShadowsPrefabs;
    [Tooltip("Gêneros de quadros possíveis de aparecerem no puzzle.")]
    [SerializeField] private List<PaintingGenre> listOfGenres;
    [Tooltip("Referência para o GameObject do inimigo.")]
    [SerializeField] private GameObject enemy;

    [Tooltip("Âncoras dos quadros da sala puzzle.")]
    [SerializeField] private List<GameObject> puzzlePaintingAnchors;

    [Tooltip("Número de plaquetas que precisam ser preenchidas no puzzle.")]
    [SerializeField] private int numberOfBoardsToFill;
    [Tooltip("Tempo que as luzes ficam apagadas quando o jogador erra o puzzle.")]
    [SerializeField] private float lightsOffTimeInterval;
    [Tooltip("Quais fontes de luz devem ser apagadas quando o jogador erra o puzzle.")]
    [SerializeField] private TurnLightOnOff[] lights;
    [Tooltip("Qual evento deve ser invocado quando o jogador termina o puzzle.")]
    [SerializeField] private UnityEvent onPuzzleFinished;

    private int numberOfBoardsDoneRight = 0, numberOfBoardsDoneWrong = 0;
    private List<Item> boardsInPuzzleRoom;
    private List<GameObject> paintingsInPuzzleRoom;
    private bool hasPuzzleFinished = false;
    private List<PaintingGenre> genresInTheLastTry;
    private EnemyOneSpawner spawnerScript;

    // DEBUG
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            LosePuzzle();
        }
    }

    private void Awake()
    {
        boardsInPuzzleRoom = new List<Item>();
        paintingsInPuzzleRoom = new List<GameObject>();
        genresInTheLastTry = new List<PaintingGenre>();
        spawnerScript = GetComponent<EnemyOneSpawner>();
        spawnerScript.SetUp(enemy);
    }

    private void Start()
    {
        ResetPaintings();

        if (enemy.activeSelf) enemy.SetActive(false);
    }

    public void OnRightItemFit(GameObjectEventArg arg)
    {
        numberOfBoardsDoneRight++;
        boardsInPuzzleRoom.Add(arg.item.GetComponent<Item>());
        CheckPuzzleProgress();
    }

    public void OnWrongItemFit(GameObjectEventArg arg)
    {
        numberOfBoardsDoneWrong++;
        boardsInPuzzleRoom.Add(arg.item.GetComponent<Item>());
        CheckPuzzleProgress();
    }

    public void OnBoardRemoved(GameObjectAndBoolEventArg arg)
    {
        if (arg.isThisItemRight) numberOfBoardsDoneRight--;
        else numberOfBoardsDoneWrong--;
    }

    private void CheckPuzzleProgress()
    {
        if (!hasPuzzleFinished)
        {
            if (HasWonPuzzle())
            {
                WinPuzzle();
            }

            if (HasLostPuzzle())
            {
                LosePuzzle();
            }
        }
    }

    private bool HasWonPuzzle()
    {
        if (numberOfBoardsDoneRight == numberOfBoardsToFill)
        {
            return true;
        }
        else return false;
    }

    private bool HasLostPuzzle()
    {
        if (numberOfBoardsDoneWrong > 0 && numberOfBoardsDoneRight + numberOfBoardsDoneWrong == numberOfBoardsToFill)
        {
            return true;
        }
        else return false;
    }

    private void WinPuzzle()
    {
        onPuzzleFinished.Invoke();
        hasPuzzleFinished = true;
    }

    private void LosePuzzle()
    {
        StartCoroutine(TurnLightsOff());
        SpawnEnemy();
        //hasPuzzleFinished = true;
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

        ResetPaintings();
    }

    private void ResetPaintings()
    {
        DestroyPaintings();

        // Sorteia os novos gêneros da sala puzzle
        // Porém, deve seguir a seguinte regra: a nova tentativa precisa ter dois gêneros que não estavam presentes na tentativa anterior
        List<PaintingGenre> possibleGenres = listOfGenres.ToList<PaintingGenre>();
        List<PaintingGenre> genres = new List<PaintingGenre>();

        if (genresInTheLastTry.Count > 0)
        {
            // Sorteia dois gêneros que estavam na última tentativa para remover da tentativa atual
            genresInTheLastTry.Remove(genresInTheLastTry[ Random.Range(0, genresInTheLastTry.Count) ]);
            genresInTheLastTry.Remove(genresInTheLastTry[ Random.Range(0, genresInTheLastTry.Count) ]);
            possibleGenres = possibleGenres.Except<PaintingGenre>(genresInTheLastTry).ToList<PaintingGenre>();
            genres = possibleGenres.ToList<PaintingGenre>();
        }
        else
        {
            for (int i = 0; i < numberOfBoardsToFill; i++)
            {
                PaintingGenre newGenre = possibleGenres[Random.Range(0, possibleGenres.Count)];
                genres.Add(newGenre);
                possibleGenres.Remove(newGenre);
            }
        }

        // Sorteia os novos quadros da sala puzzle
        for (int i = 0; i < numberOfBoardsToFill; i++)
        {
            GameObject newPainting = Instantiate(RandomPaintingOfGenre(genres[i]), puzzlePaintingAnchors[i].transform);
            paintingsInPuzzleRoom.Add(newPainting);

            // Ajusta o gênero aceitado pela plaqueta
            newPainting.transform.parent.GetComponentInChildren<PuzzleOneItemSlot>().acceptableGenre = newPainting.GetComponent<Painting>().myGenre;
        }

        // Registra os gêneros que foram sorteados nessa tentativa
        genresInTheLastTry.Clear();
        genresInTheLastTry = genres.ToList<PaintingGenre>();

        // Zera os contadores
        numberOfBoardsDoneRight = numberOfBoardsDoneWrong = 0;
    }

    private void DestroyPaintings()
    {
        foreach(GameObject p in paintingsInPuzzleRoom)
        {
            Destroy(p);
        }
    }

    private GameObject RandomPaintingOfGenre(PaintingGenre genre)
    {
        switch (genre)
        {
            case PaintingGenre.Altar:           return altarPrefabs[Random.Range(0, altarPrefabs.Count)];
            case PaintingGenre.Landscape:       return landscapePrefabs[Random.Range(0, landscapePrefabs.Count)];
            case PaintingGenre.LightAndShadows: return lightAndShadowsPrefabs[Random.Range(0, lightAndShadowsPrefabs.Count)];
            case PaintingGenre.Madonna:         return madonnaPrefabs[Random.Range(0, madonnaPrefabs.Count)];
            case PaintingGenre.Mythology:       return mythologyPrefabs[Random.Range(0, mythologyPrefabs.Count)];
            case PaintingGenre.Portrait:        return portraitPrefabs[Random.Range(0, portraitPrefabs.Count)];
            default:                            return null;
        }
    }

    private void SpawnEnemy()
    {
        spawnerScript.SpawnEnemy();
    }
}
