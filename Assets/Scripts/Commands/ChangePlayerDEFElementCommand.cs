using UnityEngine;
using QFramework;

namespace TruthOrLie_Demo
{
    public class ChangePlayerDEFElementCommand : AbstractCommand
    {
        Element _element;
        public ChangePlayerDEFElementCommand(Element element)
        {
            _element = element;
        }
        protected override void OnExecute()
        {
            Player.ChangeSwordElemntEvent updateEvent = new Player.ChangeSwordElemntEvent();
            updateEvent.element = _element;
            this.SendEvent(updateEvent);
        }
    }
}