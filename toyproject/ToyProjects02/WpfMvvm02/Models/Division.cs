using CommunityToolkit.Mvvm.ComponentModel;

namespace WpfMvvm02.Models {
    public partial class Division : ObservableObject {

        [ObservableProperty]
        private string divCode = string.Empty;

        [ObservableProperty]
        private string divName = string.Empty;
    }
}
