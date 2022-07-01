using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : LocalSingleton<GameManager>
{
    public Camera cam;

    public Transform player;
    public Transform garbage;

    public ParticleSystem confetti;

    public EndgameController endgameController;

    public LayerMask groundLayer;
}
