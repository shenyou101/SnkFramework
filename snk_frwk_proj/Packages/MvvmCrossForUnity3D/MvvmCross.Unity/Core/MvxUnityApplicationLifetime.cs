using System;
using MvvmCross.Core;

namespace MvvmCross.Unity.Core
{
    public class MvxUnityApplicationLifetime :  IMvxUnityApplicationLifetime
    {
        private EventHandler<MvxLifetimeEventArgs>? _lifetimeChanged;
        public event EventHandler<MvxLifetimeEventArgs>? LifetimeChanged
        {
            add => _lifetimeChanged += value;
            remove => _lifetimeChanged -= value;
        }
    }
}