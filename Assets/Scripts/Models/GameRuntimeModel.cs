using System.Collections.Generic;
using QFramework;
using UnityEngine;

namespace TruthOrLie_Demo
{
    public class GameRuntimeModel : AbstractModel
    {
        public GameObject[,] Map;
        protected override void OnInit()
        {
            Screen.SetResolution(850, 560, false);
            Map = new GameObject[5, 5];
            InitMap();
        }

        public void InitMap()
        {
            foreach (var node in Map)
            {
                if (node != null)
                    GameObject.Destroy(node);
            }
            Map = new GameObject[5, 5];
            for (int x = 0; x < Map.GetLength(0); x++)
            {
                for (int y = 0; y < Map.GetLength(1); y++)
                {
                    Map[x, y] = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Node"));
                    Map[x, y].transform.position = new Vector3(x + x, y + y, 0);
                    Map[x, y].GetComponent<Node>().Env = Environment.FIRE + Random.Range(0, 6);
                    Map[x, y].transform.Find("Environment").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>($"Images/{Map[x, y].GetComponent<Node>().Env}");
                    if (Map[x, y].GetComponent<Node>().Env > Environment.ROCK) continue;
                    Map[x, y].GetComponent<Node>().EnemyCount = Random.Range(0, 3);
                    if (Map[x, y].GetComponent<Node>().EnemyCount == 0) continue;
                    Map[x, y].GetComponent<Node>().EnemyElement = Element.FIRE + (Map[x, y].GetComponent<Node>().Env - Environment.FIRE);

                }
            }

            Map[0, 0].GetComponent<Node>().Env = Environment.HP_INCREASE;
            Map[0, 0].transform.Find("Environment").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>($"Images/HP_INCREASE");
            Map[0, 0].GetComponent<Node>().EnemyCount = 0;
            Map[0, 0].GetComponent<Node>().EnemyElement = Element.NONE;

            int desX = Random.Range(0, 5);
            int desY = Random.Range(3, 5);
            Debug.Log($"desX = {desX}, desY = {desY}");
            Map[desX, desY].GetComponent<Node>().Env = Environment.DESTINATION;
            Map[desX, desY].transform.Find("Environment").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>($"Images/DESTINATION");
            Map[desX, desY].GetComponent<Node>().EnemyCount = 0;
            Map[desX, desY].GetComponent<Node>().EnemyElement = Element.NONE;

            for (int x = 0; x < Map.GetLength(0); x++)
            {
                for (int y = 0; y < Map.GetLength(1); y++)
                {
                    var list = new List<string[]>();
                    if (x + 1 < Map.GetLength(0))
                    {
                        list.Add(new string[] { "RIGHT", Map[x + 1, y].GetComponent<Node>().Env.ToString(), "EMPTY" });
                        list.Add(new string[] { "RIGHT", "enemy", Map[x + 1, y].GetComponent<Node>().EnemyCount.ToString() });
                        list.Add(new string[] { "RIGHT", "enemy", $"ELEMENT_{Map[x + 1, y].GetComponent<Node>().EnemyElement.ToString()}" });
                    }
                    if (x - 1 >= 0)
                    {
                        list.Add(new string[] { "LEFT", Map[x - 1, y].GetComponent<Node>().Env.ToString(), "EMPTY" });
                        list.Add(new string[] { "LEFT", "enemy", Map[x - 1, y].GetComponent<Node>().EnemyCount.ToString() });
                        list.Add(new string[] { "LEFT", "enemy", $"ELEMENT_{Map[x - 1, y].GetComponent<Node>().EnemyElement.ToString()}" });
                    }
                    if (y + 1 < Map.GetLength(1))
                    {
                        list.Add(new string[] { "UP", Map[x, y + 1].GetComponent<Node>().Env.ToString(), "EMPTY" });
                        list.Add(new string[] { "UP", "enemy", Map[x, y + 1].GetComponent<Node>().EnemyCount.ToString() });
                        list.Add(new string[] { "UP", "enemy", $"ELEMENT_{Map[x, y + 1].GetComponent<Node>().EnemyElement.ToString()}" });
                    }
                    if (y - 1 >= 0)
                    {
                        list.Add(new string[] { "DOWN", Map[x, y - 1].GetComponent<Node>().Env.ToString(), "EMPTY" });
                        list.Add(new string[] { "DOWN", "enemy", Map[x, y - 1].GetComponent<Node>().EnemyCount.ToString() });
                        list.Add(new string[] { "DOWN", "enemy", $"ELEMENT_{Map[x, y - 1].GetComponent<Node>().EnemyElement.ToString()}" });
                    }

                    SeletThreeTipsToNode(x, y, list);
                }
            }
        }

        void SeletThreeTipsToNode(int x, int y, List<string[]> list)
        {
            int index = Random.Range(0, list.Count);
            Map[x, y].GetComponent<Node>().TipsList = new List<string[]>();
            Map[x, y].GetComponent<Node>().TipsList.Add(list[index]);
            list.RemoveAt(index);
            index = Random.Range(0, list.Count);
            Map[x, y].GetComponent<Node>().TipsList.Add(list[index]);
            list.RemoveAt(index);
            index = Random.Range(0, list.Count);
            Map[x, y].GetComponent<Node>().TipsList.Add(list[index]);

            index = Random.Range(0, 3);
            if (Map[x, y].GetComponent<Node>().TipsList[index][2] == "EMPTY")
            {
                if (Map[x, y].GetComponent<Node>().TipsList[index][1] == "FIRE") Map[x, y].GetComponent<Node>().TipsList[index][1] = "WATER";
                if (Map[x, y].GetComponent<Node>().TipsList[index][1] == "WATER") Map[x, y].GetComponent<Node>().TipsList[index][1] = "PLANT";
                if (Map[x, y].GetComponent<Node>().TipsList[index][1] == "PLANT") Map[x, y].GetComponent<Node>().TipsList[index][1] = "ROCK";
                if (Map[x, y].GetComponent<Node>().TipsList[index][1] == "ROCK") Map[x, y].GetComponent<Node>().TipsList[index][1] = "HP_INCREASE";
                if (Map[x, y].GetComponent<Node>().TipsList[index][1] == "HP_INCREASE") Map[x, y].GetComponent<Node>().TipsList[index][1] = "TRAP";
                if (Map[x, y].GetComponent<Node>().TipsList[index][1] == "TRAP") Map[x, y].GetComponent<Node>().TipsList[index][1] = "DESTINATION";
                if (Map[x, y].GetComponent<Node>().TipsList[index][1] == "DESTINATION") Map[x, y].GetComponent<Node>().TipsList[index][1] = "FIRE";
            }
            else if (Map[x, y].GetComponent<Node>().TipsList[index][1] == "enemy")
            {
                if (Map[x, y].GetComponent<Node>().TipsList[index][2] == "0") Map[x, y].GetComponent<Node>().TipsList[index][2] = "1";
                if (Map[x, y].GetComponent<Node>().TipsList[index][2] == "1") Map[x, y].GetComponent<Node>().TipsList[index][2] = "2";
                if (Map[x, y].GetComponent<Node>().TipsList[index][2] == "2") Map[x, y].GetComponent<Node>().TipsList[index][2] = "0";
                if (Map[x, y].GetComponent<Node>().TipsList[index][2] == "ELEMENT_FIRE") Map[x, y].GetComponent<Node>().TipsList[index][2] = "ELEMENT_WATER";
                if (Map[x, y].GetComponent<Node>().TipsList[index][2] == "ELEMENT_WATER") Map[x, y].GetComponent<Node>().TipsList[index][2] = "ELEMENT_PLANT";
                if (Map[x, y].GetComponent<Node>().TipsList[index][2] == "ELEMENT_PLANT") Map[x, y].GetComponent<Node>().TipsList[index][2] = "ELEMENT_ROCK";
                if (Map[x, y].GetComponent<Node>().TipsList[index][2] == "ELEMENT_ROCK") Map[x, y].GetComponent<Node>().TipsList[index][2] = "ELEMENT_NONE";
                if (Map[x, y].GetComponent<Node>().TipsList[index][2] == "ELEMENT_NONE") Map[x, y].GetComponent<Node>().TipsList[index][2] = "ELEMENT_FIRE";
            }
        }
    }
}