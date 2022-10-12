using System;
using System.Collections;

namespace SnkFramework.Mvvm.View
{
    public class SnkUIHideTransition : SnkUITransition
    {
        private bool _dismiss;
        private ISnkUILayer _uiLayer;

        public SnkUIHideTransition(ISnkUILayer uiLayer, ISnkWindowControllable window, bool dismiss) : base(window)
        {
            this._uiLayer = uiLayer;
            this._dismiss = dismiss;
        }

        protected override IEnumerator DoTransition()
        {
            ISnkWindowControllable current = this.Window;
            if (this._uiLayer.IndexOf(current) == 0 && current.mActivated)
            {
                IAsyncResult result = current.Passivate(this.AnimationDisabled);
                while (result.IsCompleted == false)
                    yield return null;
                //yield return result.WaitForDone();
            }

            if (current.mVisibility)
            {
                IAsyncResult result = current.DoHide(this.AnimationDisabled);
                while (result.IsCompleted == false)
                    yield return null;
                //yield return result.WaitForDone();
            }

            if (_dismiss)
                current.DoDismiss();
        }
    }
}