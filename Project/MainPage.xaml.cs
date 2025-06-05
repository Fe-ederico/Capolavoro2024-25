
namespace Project
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnNavigaClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewPage1());
        }

        private void SvuotaFile(object sender, EventArgs e)
        {
            string baseFolder = AppDomain.CurrentDomain.BaseDirectory;
            string fileName = "squadra.txt";
            string pathFile = Path.Combine(baseFolder, fileName);
            File.WriteAllText(pathFile, string.Empty);
        }
        private async void Pulssquad(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new VisualizzaSquadre());
        }
    }
}
