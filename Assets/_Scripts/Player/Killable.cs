using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Killable : MonoBehaviour
{
    public void Die()
    {
        GetComponent<CharacterController>().enabled = false;
        GetComponent<FirstPersonController>().enabled = false;
        FindObjectOfType<GameManager>().OnPlayerDied();
    }
}
