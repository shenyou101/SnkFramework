using System;
using System.Collections;
using SnkFramework.Mvvm.Runtime.Base;
using UnityEngine;

namespace SnkFramework.Mvvm.Runtime
{
    namespace View
    {
        [RequireComponent(typeof(Canvas), typeof(CanvasGroup))]
        public abstract partial class SnkWindow : SnkView, ISnkWindow
        {
            public static readonly bool STATE_BROADCAST = false;

            private Canvas _canvas;
            public Canvas Canvas => _canvas ??= GetComponent<Canvas>();

            private CanvasGroup _canvasGroup;
            public CanvasGroup CanvasGroup => _canvasGroup ??= GetComponent<CanvasGroup>();

            public virtual bool Interactable
            {
                get
                {
                    if (this.IsDestroyed() || this.gameObject == null)
                        return false;

                    //if (GlobalSetting.useBlocksRaycastsInsteadOfInteractable)
                    return this.CanvasGroup.blocksRaycasts;
                    //return this.CanvasGroup.interactable;
                }
                set
                {
                    if (this.IsDestroyed() || this.gameObject == null)
                        return;

                    //if (GlobalSetting.useBlocksRaycastsInsteadOfInteractable)
                    this.CanvasGroup.blocksRaycasts = value;
                    //else
                    //    this.CanvasGroup.interactable = value;
                }
            }
            private EventHandler windowStateChanged;

            private WIN_STATE state;
            public WIN_STATE WindowState
            {
                get => this.state;
                set
                {
                    if (this.state.Equals(value))
                        return;

                    WIN_STATE old = this.state;
                    this.state = value;
                    this.RaiseStateChanged(old, this.state);
                }
            }
            public event EventHandler WindowStateChanged
            {
                add { lock (_lock) { this.windowStateChanged += value; } }
                remove { lock (_lock) { this.windowStateChanged -= value; } }
            }
            
            protected override void OnActivatedChanged()
            {
                base.OnActivatedChanged();
                this.Interactable = this.Activated;
            }

            protected void RaiseStateChanged(WIN_STATE oldState, WIN_STATE newState)
            {
                try
                {
                    WindowStateEventArgs eventArgs = new WindowStateEventArgs(this, oldState, newState);
                    windowStateChanged?.Invoke(this, eventArgs);
                }
                catch (Exception e)
                {
                    Debug.LogWarning(e);
                }
            }

            public override void Create(ISnkBundle bundle)
            {
            }

            public virtual SnkTransitionOperation Show(bool animated)
            {
                SnkTransitionOperation operation = new SnkTransitionOperation();
                var routine = onShowTransitioning(operation);
                if (routine != null && animated)
                    StartCoroutine(routine);
                return operation;
            }

            public virtual SnkTransitionOperation Hide(bool animated)
            {
                SnkTransitionOperation operation = new SnkTransitionOperation();
                var routine = onHideTransitioning(operation);
                if (routine != null && animated)
                    StartCoroutine(routine);
                return operation;
            }

            /// <summary>
            /// 显示动画实现
            /// </summary>
            /// <param name="operation"></param>
            /// <returns></returns>
            protected virtual IEnumerator onShowTransitioning(SnkTransitionOperation operation) => default;

            /// <summary>
            /// 隐藏动画实现
            /// </summary>
            /// <param name="operation"></param>
            /// <returns></returns>
            protected virtual IEnumerator onHideTransitioning(SnkTransitionOperation operation) => default;
        }
    }
}