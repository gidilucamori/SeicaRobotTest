using System.Windows;
using Seica;

namespace WpfTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Robot _robot;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void CreateNewRobotConnection(object sender, RoutedEventArgs e)
        {
            if (_robot != null) _robot = null;
            _robot = new Robot();
        }

        private void executeCommand(Azioni azione, Pinza pinza, Stazioni stazione, PosizioneSchedaForRobot posizione)
        {
            if (_robot != null && _robot.AreCommandsEnabled && !_robot.CommandPending)
            {
                _robot.WriteCommand(azione, pinza, stazione, posizione);
            }
            else if (_robot != null)
            {
                MessageBox.Show("Definire istanza Robot");
            }
            else if (_robot.CommandPending)
            {
                MessageBox.Show("Attendere la fine del comando in esecuzione dal robot");
            }
        }

        private void pc1p1(object sender, RoutedEventArgs e)
        {
            executeCommand(Azioni.Prelievo, Pinza.Pinza_2, Stazioni.Carico, PosizioneSchedaForRobot.Carico_2);
        }

        private void pc2p2(object sender, RoutedEventArgs e)
        {
            executeCommand(Azioni.Prelievo, Pinza.Pinza_2, Stazioni.Carico, PosizioneSchedaForRobot.Carico_2);
        }

        private void dp1(object sender, RoutedEventArgs e)
        {
            executeCommand(Azioni.Deposito, Pinza.Pinza_1, Stazioni.Scarico, PosizioneSchedaForRobot.Scarico);
        }

        private void dp2(object sender, RoutedEventArgs e)
        {
            executeCommand(Azioni.Deposito, Pinza.Pinza_2, Stazioni.Scarico, PosizioneSchedaForRobot.Scarico);   
        }
    }
}
