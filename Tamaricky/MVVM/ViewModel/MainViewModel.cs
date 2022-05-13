using Tamaricky.MVVM.Model;
using WPFCore.MVVM.Core;

namespace Tamaricky.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        private MainModel _mainModel = new MainModel();
        public MainModel MainModel
        {
            get { return _mainModel; }
            set { _mainModel = value; RaisePropertyChanged(); }
        }
    }
}
