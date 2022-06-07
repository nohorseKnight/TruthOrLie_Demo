using QFramework;
using UnityEngine;

namespace TruthOrLie_Demo
{
    public class GameRuntimeModel : AbstractModel
    {
        public GameObject[,] Map;
        protected override void OnInit()
        {
            Map = new GameObject[5, 5];
            for (int x = 0; x < Map.GetLength(0); x++)
            {
                for (int y = 0; y < Map.GetLength(1); y++)
                {
                    Map[x, y] = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Node"));
                    Map[x, y].transform.position = new Vector3(x + x, y + y, 0);
                    Map[x, y].GetComponent<Node>().Env = Environment.FIRE + Random.Range(0, 6);
                    if (Map[x, y].GetComponent<Node>().Env > Environment.ROCK) continue;
                    Map[x, y].GetComponent<Node>().EnemyCount = Random.Range(0, 3);
                    if (Map[x, y].GetComponent<Node>().EnemyCount == 0) continue;
                    Map[x, y].GetComponent<Node>().EnemyElement = Element.FIRE + Random.Range(0, 4);
                }
            }

            Map[0, 0].GetComponent<Node>().Env = Environment.HP_INCREASE;
            Map[0, 0].GetComponent<Node>().EnemyCount = 0;
            Map[0, 0].GetComponent<Node>().EnemyElement = Element.NONE;
        }
    }
}