using Avalonia.Interactivity;
using Consolonia.Modal;

namespace Consolonia.Sandbox
{
    public partial class SampleModal2 : ModalWindow
    {
        public SampleModal2()
        {
            InitializeComponent();
        }

        private void OnCloseClick(object? sender, RoutedEventArgs e)
        {
            CloseModal();
        }
    }
}