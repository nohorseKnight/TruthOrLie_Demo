using UnityEngine;
using QFramework;

namespace TruthOrLie_Demo
{
    public class ChangePlayerATKElementCommand : AbstractCommand
    {
        Element _element;
        public ChangePlayerATKElementCommand(Element element)
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