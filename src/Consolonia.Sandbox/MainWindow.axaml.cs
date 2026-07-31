using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace Consolonia.Sandbox
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private async void OnShowModalClick(object sender, RoutedEventArgs e)
        {
            var modal = new SampleModal();
            await modal.ShowModalAsync(this);
        }

        private async void OnShowModalClick2(object? sender, RoutedEventArgs e)
        {
            var modal = new SampleModal2();
            await modal.ShowModalAsync(this);
        }
    }
}