using Microsoft.UI;
using Microsoft.UI.Xaml;
using Syncfusion.UI.Xaml.Editors;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace PatientHealthMonitoring
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void segmentedControl_SelectionChanged(object sender, SegmentSelectionChangedEventArgs e)
        {
            if(sender is SfSegmentedControl segmentedControl)
            {
                var segmentedItem = segmentedControl;
                int selectedItem = segmentedItem.SelectedIndex;
                switch (selectedItem)
                {
                    case 1:
                        viewModel.Minimum = 80;
                        viewModel.Maximum = 100;
                        viewModel.Interval = 5;
                        viewModel.BPSeriesVisibility = Visibility.Collapsed;
                        viewModel.OxygenSaturationSeriesVisibility = Visibility.Visible;
                        viewModel.GlucoseLevelSeriesVisibility = Visibility.Collapsed;
                        viewModel.AxisTitle = "Oxygen Saturation (%)";
                        break;
                    case 2:
                        viewModel.Minimum = 0;
                        viewModel.Maximum = 140;
                        viewModel.Interval = 70;
                        viewModel.BPSeriesVisibility = Visibility.Collapsed;
                        viewModel.OxygenSaturationSeriesVisibility = Visibility.Collapsed;
                        viewModel.GlucoseLevelSeriesVisibility = Visibility.Visible;
                        viewModel.AxisTitle = "Blood Glucose (mg/dL)";
                        break;
                    default:
                        viewModel.Minimum = 0;
                        viewModel.Maximum = 160;
                        viewModel.Interval = 40;
                        viewModel.BPSeriesVisibility = Visibility.Visible;
                        viewModel.OxygenSaturationSeriesVisibility = Visibility.Collapsed;
                        viewModel.GlucoseLevelSeriesVisibility = Visibility.Collapsed;
                        viewModel.AxisTitle = "Blood Pressure (mmHg)";
                        break;
                }
            }
        }

    }
}
