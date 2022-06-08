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
        }
        void Start()
        {
            this.RegisterEvent<MapInfoPanelUpdateEvent>(updateEvent =>
            {
                transform.Find("EnvironmentImage").GetComponent<Image>().sprite = Resources.Load<Sprite>($"Images/{updateEvent.Env}");
                transform.Find("EnemyCountText").GetComponent<UITextMeshPro>().text = updateEvent.EnemyCountText;
                transform.Find("HealthChangeText").GetComponent<UITextMeshPro>().text = updateEvent.HPChangeText;
                switch (updateEvent.EnemyElement)
                {
                    case Element.FIRE:
                        transform.Find("EnemyImage").GetComponent<Image>().color = new Color(253f / 255, 146f / 255, 93f / 255, 1f);
                        break;
                    case Element.WATER:
                        transform.Find("EnemyImage").GetComponent<Image>().color = new Color(33f / 255, 225f / 255, 225f / 255, 1f);
                        break;
                    case Element.PLANT:
                        transform.Find("EnemyImage").GetComponent<Image>().color = new Color(35f / 255, 193f / 255, 38f / 255, 1f);
                        break;
                    case Element.ROCK:
                        transform.Find("EnemyImage").GetComponent<Image>().color = new Color(227f / 255, 179f / 255, 66f / 255, 1f);
                        break;
                    default:
                        transform.Find("EnemyImage").GetComponent<Image>().color = Color.white;
                        break;
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }
    }
}