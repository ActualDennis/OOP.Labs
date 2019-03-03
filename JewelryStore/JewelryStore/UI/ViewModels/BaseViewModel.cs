using System.ComponentModel;
using PropertyChanged;

namespace JewelryStore.UI.ViewModels {
    [AddINotifyPropertyChangedInterface]

    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
    }
}
