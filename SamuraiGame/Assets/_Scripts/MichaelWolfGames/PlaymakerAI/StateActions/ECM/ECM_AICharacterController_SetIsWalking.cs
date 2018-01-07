using HutongGames.PlayMaker;
using VGDA.ECM;

namespace MichaelWolfGames.AI.Playmaker.ECM
{
    public class ECM_AICharacterController_SetIsWalking : FsmStateAction
    {
        public FsmOwnerDefault aiControllerGameObject;
        public FsmBool SetWalking;
        public bool ResetOnExit = false;
        private AICharacterController _aiCharacterController;
        private bool _originalWalkingValue = false;

        public override void OnEnter()
        {
            var go = Fsm.GetOwnerDefaultTarget(aiControllerGameObject);
            if (go)
            {
                _aiCharacterController = go.GetComponent<AICharacterController>();
                if (_aiCharacterController)
                {
                    _originalWalkingValue = _aiCharacterController.walking;
                    _aiCharacterController.walking = SetWalking.Value;
                }
            }
            Finish();
        }

        public override void OnExit()
        {
            base.OnExit();
            if (ResetOnExit)
            {
                _aiCharacterController.walking = _originalWalkingValue;
            }
        }

        public override void Reset()
        {
            base.Reset();
            _aiCharacterController = null;
            ResetOnExit = false;
        }
    }
}