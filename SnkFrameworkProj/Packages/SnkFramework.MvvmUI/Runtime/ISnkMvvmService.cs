using System.Threading.Tasks;
using SnkFramework.Mvvm.Runtime.Base;
using SnkFramework.Mvvm.Runtime.Presenters;
using SnkFramework.Mvvm.Runtime.View;
using SnkFramework.Mvvm.Runtime.ViewModel;

namespace SnkFramework.Mvvm
{
    public interface ISnkMvvmService
    {
    }

    public interface ISnkMvvmNavigation
    {
        //记录历史信息ViewHistory
    }

    public class SnkMvvmService : SnkContainer<ISnkLayer>, ISnkMvvmService
    {
        private ISnkMvvmNavigation _navigation;
        private ISnkViewDispatcher _viewDispatcher { get; }

        public async Task Navigate(SnkViewModelRequest request, ISnkViewModel viewModel)
        {
            await this._viewDispatcher.OpenViewModel(request).ConfigureAwait(false);

            if (viewModel.InitializeTask?.Task != null)
                await viewModel.InitializeTask.Task.ConfigureAwait(false);
        }
    }
}