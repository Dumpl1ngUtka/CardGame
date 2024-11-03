using System.Collections;
using System.Collections.Generic;
using Units;
using Units.Items;
using UnityEngine;

namespace Battleground
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private int spawnCount = 0;
        [SerializeField] private List<Transform> SpawnPoints;
        [SerializeField] private Player player;

        private void Start()
        {

            var ind = 0;
            player.InstantiatePiece(player.Units[0], SpawnPoints[ind++].position);
            player.InstantiatePiece(player.Units[0], SpawnPoints[ind++].position);
        }
    }
}

