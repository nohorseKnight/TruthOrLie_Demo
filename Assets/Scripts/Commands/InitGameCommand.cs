using UnityEngine;
using QFramework;

namespace TruthOrLie_Demo
{
    public class InitGameCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            GameObject.Find("Player").GetComponent<Player>().PlayerInit();
            this.GetModel<GameRuntimeModel>().InitMap();
        }
    }
}