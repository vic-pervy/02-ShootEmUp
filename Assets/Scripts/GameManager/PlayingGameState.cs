using System;
using System.Linq;
using UnityEngine;

namespace ShootEmUp
{
    public class PlayingGameState : BaseGameState
    {
        private IUpdate[] gameLoopUpdateListeners;
        private IFixedUpdate[] gameLoopFixedUpdatesListeners;
        
        public override void EnterState(GameManager gameManager)
        {
            gameManager.CountDownText.gameObject.SetActive(false);
            gameManager.PauseButton.gameObject.SetActive(true);
            gameManager.PlayButton.gameObject.SetActive(false);
            gameLoopUpdateListeners = Resources.FindObjectsOfTypeAll<MonoBehaviour>().Where(c => c is IUpdate)
                .Cast<IUpdate>().ToArray();
            gameLoopFixedUpdatesListeners = Resources.FindObjectsOfTypeAll<MonoBehaviour>()
                .Where(c => c is IFixedUpdate).Cast<IFixedUpdate>().ToArray();
        }

        public override void ExitState(GameManager gameManager)
        {
        }

        public override void Update()
        {
            foreach (var c in gameLoopUpdateListeners)
            {
                if (!((MonoBehaviour)c) || !((MonoBehaviour)c).enabled ||
                    !((MonoBehaviour)c).gameObject.activeInHierarchy) continue;
                c.Update();
            }
        }

        public override void FixedUpdate()
        {
            foreach (var c in gameLoopFixedUpdatesListeners)
            {
                if (!((MonoBehaviour)c) || !((MonoBehaviour)c).enabled ||
                    !((MonoBehaviour)c).gameObject.activeInHierarchy) continue;
                c.FixedUpdate();
            }
        }
    }
}