using UnityEngine;
using QFramework;

namespace TruthOrLie_Demo
{
    public class ReachNodeCommand : AbstractCommand
    {
        Player _player;
        Node _node;
        public ReachNodeCommand(Player player, Node node)
        {
            _player = player;
            _node = node;
        }
        protected override void OnExecute()
        {
            Debug.Log(_node.Info());

            _node.IsVisited = true;

            InfoPanel.UpdateHPEvent updateEvent = new InfoPanel.UpdateHPEvent();
            updateEvent.HP = _player.HP;
            this.SendEvent(updateEvent);
        }
    }
}