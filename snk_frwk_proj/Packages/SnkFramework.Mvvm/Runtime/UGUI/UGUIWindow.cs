using System.Collections;
using SnkFramework.Mvvm.Core.View;
using SnkFramework.Mvvm.Core.ViewModel;
using UnityEngine;

namespace SnkFramework.Mvvm.Runtime
{
    namespace UGUI
    {
        public abstract class UGUIWindow<TViewModel> : SnkWindow<UGUIViewOwner, UGUILayer, TViewModel>, IUGUIWindow
            where TViewModel : class, ISnkViewModel, new()
        {
            public override string mName
            {
                get => this.mOwner.name;
                set => base.mName = value;
            }

            public float mAlpha
            {
                get => this.mOwner.mCanvasGroup.alpha;
                set => this.mOwner.mCanvasGroup.alpha = value;
            }

            private GameObject _gameObject;
            public GameObject mGameObject => _gameObject ??= this.mOwner.gameObject;

            private Transform _transform;
            public Transform mTransform => _transform ??= this.mOwner.transform;

            private RectTransform _rectTransform;
            public RectTransform mTectTransform => _rectTransform ??= mTransform as RectTransform;

            public override void LoadViewOwner()
            {
                base.LoadViewOwner();
                this.mUILayer.AddChild(mTectTransform);
            }

            public override IEnumerator LoadViewOwnerAsync()
            {
                yield return base.LoadViewOwnerAsync();
                this.mUILayer.AddChild(mTectTransform);
            }

            public override void UnloadViewOwner()
            {
                base.UnloadViewOwner();
                this.mUILayer.RemoveChild(mTectTransform);
            }
        }
    }
}