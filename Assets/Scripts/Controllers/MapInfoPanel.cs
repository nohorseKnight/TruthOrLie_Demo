using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using UITextMeshPro = TMPro.TMP_Text;

namespace TruthOrLie_Demo
{
    public class MapInfoPanel : BaseController
    {
        public struct MapInfoPanelUpdateEvent
        {
            public Environment Env;
            public Element EnemyElement;
            public string EnemyCountText;
            public string HPChangeText;
            public List<string[]> TipsList;
        }
        public struct MapInfoPanelInitEvent
        {

        }
        void Start()
        {
            this.RegisterEvent<MapInfoPanelUpdateEvent>(updateEvent =>
            {
                Debug.Log($"this.RegisterEvent<MapInfoPanelUpdateEvent>");
                transform.Find("EnvironmentImage").GetComponent<Image>().sprite = Resources.Load<Sprite>($"Images/{updateEvent.Env}");
                transform.Find("EnemyCountText").GetComponent<UITextMeshPro>().text = updateEvent.EnemyCountText;
                transform.Find("HealthChangeText").GetComponent<UITextMeshPro>().text = updateEvent.HPChangeText;
                switch (updateEvent.EnemyElement)
                {
                    case Element.FIRE: transform.Find("EnemyImage").GetComponent<Image>().color = new Color(236f / 255, 68f / 255, 51f / 255, 1f); break;
                    case Element.WATER: transform.Find("EnemyImage").GetComponent<Image>().color = new Color(1f / 255, 156f / 255, 255f / 255, 1f); break;
                    case Element.PLANT: transform.Find("EnemyImage").GetComponent<Image>().color = new Color(58f / 255, 194f / 255, 47f / 255, 1f); break;
                    case Element.ROCK: transform.Find("EnemyImage").GetComponent<Image>().color = new Color(223f / 255, 166f / 255, 38f / 255, 1f); break;
                    default:
                        transform.Find("EnemyImage").GetComponent<Image>().color = Color.white;
                        break;
                }
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        Debug.Log($"updateEvent.TipsList[{i}][{j}] = {updateEvent.TipsList[i][j]}");
                        transform.Find("TipsPanel").Find($"TipsPanel_{i}").Find($"InfoImage_{j}")
                        .GetComponent<Image>().sprite = Resources.Load<Sprite>($"Images/{updateEvent.TipsList[i][j]}");
                    }
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            this.RegisterEvent<MapInfoPanelInitEvent>(updateEvent =>
            {
                InitMapInfoPanel();
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            InitMapInfoPanel();
        }

        void InitMapInfoPanel()
        {
            GameRuntimeModel gameRuntimeModel = this.GetModel<GameRuntimeModel>();
            transform.Find("EnvironmentImage").GetComponent<Image>().sprite = Resources.Load<Sprite>($"Images/HP_INCREASE");
            transform.Find("EnemyCountText").GetComponent<UITextMeshPro>().text = "0";
            transform.Find("HealthChangeText").GetComponent<UITextMeshPro>().text = "+0";
            transform.Find("EnemyImage").GetComponent<Image>().color = Color.white;
            gameRuntimeModel.Map[0, 0].transform.Find("Environment").gameObject.SetActive(true);
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    transform.Find("TipsPanel").Find($"TipsPanel_{i}").Find($"InfoImage_{j}")
                    .GetComponent<Image>().sprite = Resources.Load<Sprite>($"Images/{gameRuntimeModel.Map[0, 0].GetComponent<Node>().TipsList[i][j]}");
                }
            }
        }
    }
}