using System.Collections.Generic;
using QFramework;
using UnityEngine;
using UITextMeshPro = TMPro.TMP_Text;

namespace TruthOrLie_Demo
{
    public class UISystem : AbstractSystem
    {
        private Transform canvasTrans;
        private Dictionary<string, GameObject> openedWindow = new Dictionary<string, GameObject>();
        protected override void OnInit()
        {
            // canvasTrans = Object.FindObjectOfType<Canvas>().transform;
            canvasTrans = GameObject.Find("Canvas").transform;
        }

        public GameObject OpenUI(string name, string layoutName = "MiddleLayout")
        {
            if (openedWindow.ContainsKey(name)) return null;
            var go = Object.Instantiate(Resources.Load<GameObject>($"UIPrefabs/{name}"), canvasTrans.Find(layoutName));
            openedWindow[name] = go;

            return (GameObject)go;
        }

        public GameObject OpenPopup(string ContentText)
        {
            if (openedWindow.ContainsKey("PopupPanel")) return null;
            var go = Object.Instantiate(Resources.Load<GameObject>("UIPrefabs/PopupPanel"), canvasTrans.Find("TopLayout"));
            go.transform.Find("ContentText").GetComponent<UITextMeshPro>().text = ContentText;
            openedWindow["PopupPanel"] = go;

            return (GameObject)go;
        }


        public void CloseUI(string name)
        {
            if (!openedWindow.TryGetValue(name, out var window)) return;
            Object.Destroy(window);
            openedWindow.Remove(name);
        }

    }
}