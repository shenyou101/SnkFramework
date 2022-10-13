using System;
using SnkFramework.Mvvm.Core.Extensions;

namespace SnkFramework.Mvvm.Core
{
    namespace Command
    {
        public class SnkSimpleCommand : SnkCommand
        {
            private bool enabled = true;
            private readonly Action execute;

            public SnkSimpleCommand(Action execute, bool keepStrongRef = false)
            {
                if (execute == null)
                    throw new ArgumentNullException("execute");

                this.execute = keepStrongRef ? execute : execute.AsWeak();
            }

            public bool Enabled
            {
                get { return this.enabled; }
                set
                {
                    if (this.enabled == value)
                        return;

                    this.enabled = value;
                    this.RaiseCanExecuteChanged();
                }
            }

            public override bool CanExecute(object parameter)
            {
                return this.Enabled;
            }

            public override void Execute(object parameter)
            {
                if (this.CanExecute(parameter) && this.execute != null)
                    this.execute();
            }
        }

        public class SnkSimpleCommand<T> : SnkCommand
        {
            private bool enabled = true;
            private readonly Action<T> execute;

            public SnkSimpleCommand(Action<T> execute, bool keepStrongRef = false)
            {
                if (execute == null)
                    throw new ArgumentNullException("execute");

                this.execute = keepStrongRef ? execute : execute.AsWeak();
            }

            public bool Enabled
            {
                get { return this.enabled; }
                set
                {
                    if (this.enabled == value)
                        return;

                    this.enabled = value;
                    this.RaiseCanExecuteChanged();
                }
            }

            public override bool CanExecute(object parameter)
            {
                return this.Enabled;
            }

            public override void Execute(object parameter)
            {
                if (this.CanExecute(parameter) && this.execute != null)
                    this.execute((T) Convert.ChangeType(parameter, typeof(T)));
            }
        }
    }
}