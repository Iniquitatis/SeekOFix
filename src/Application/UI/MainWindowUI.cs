using System.Drawing;
using System.Windows.Forms;

using static SeekOFix.Utils.UI;

namespace SeekOFix.UI
{
    public partial class MainWindow
    {
        private CheckBox _liveCheck;
        private ComboBox _paletteCombo;
        private RadioButton _unitsKRadio;
        private RadioButton _unitsCRadio;
        private RadioButton _unitsFRadio;
        private HistogramPictureBox _histogramPicture;
        private Button _manualRangeSwitchButton;
        private CheckBox _dynSlidersCheck;
        private TextBox _outputPathField;
        private CheckBox _autoSaveCheck;
        private Button _recordVideoButton;
        private Label _gModeLeftLabel;
        private Label _gModeRightLabel;
        private Label _maxTempRawLabel;
        private Panel _picturePanel;
        private FramePictureBox _framePicture;
        private TemperatureGaugePictureBox _tempGaugePicture;
        private Label _mouseLabel;
        private Label _sliderMinTempLabel;
        private Label _sliderMaxTempLabel;
        private TrackBar _minTempSlider;
        private TrackBar _maxTempSlider;

        private void InitializeComponent()
        {
            MinimumSize = new Size(640, 360);
            Padding = new Padding(5);
            Size = new Size(1024, 768);
            Text = "SeekOFix";
            FormClosing += HandleFormClosing;
            Load += HandleLoad;
            Paint += HandlePaint;

            var mainLayout = this.CreateLayout();
            mainLayout.AddColumn(SizeType.Absolute, 250f);
            mainLayout.AddColumn(SizeType.Percent, 100f);
            mainLayout.AddRow(SizeType.Percent, 100f);
            mainLayout.AddRow(SizeType.Absolute, 25f);
            mainLayout.AddRow(SizeType.Absolute, 75f);

            var mainControlLayout = mainLayout.CreateSublayout(0, 0);
            mainControlLayout.AddColumn(SizeType.Percent, 100f);
            mainControlLayout.AddRow(SizeType.Absolute, 30f);
            mainControlLayout.AddRow(SizeType.Absolute, 30f);
            mainControlLayout.AddRow(SizeType.Percent, 100f);

            var mainControlButtons = mainControlLayout.CreateSublayout(0, 0);
            mainControlButtons.AddColumn(SizeType.Percent, 1f / 3f);
            mainControlButtons.AddColumn(SizeType.Percent, 1f / 3f);
            mainControlButtons.AddColumn(SizeType.Percent, 1f / 3f);
            mainControlButtons.AddRow(SizeType.Absolute, 30f);

            var startStopButton = mainControlButtons.CreateInCell<Button>(0, 0);
            startStopButton.Dock = DockStyle.Fill;
            startStopButton.Text = "STOP";
            startStopButton.Click += (sender, e) =>
            {
                ToggleThreadActivity();
                startStopButton.Text = _isRunning ? "STOP" : "START";
            };
            startStopButton.SetToolTip("Start/stop streaming");

            var intCalButton = mainControlButtons.CreateInCell<Button>(1, 0);
            intCalButton.Dock = DockStyle.Fill;
            intCalButton.Text = "INT Cal";
            intCalButton.Click += (sender, e) => _usingExternalCal = false;
            intCalButton.SetToolTip("Do internal calibration");

            var extCalButton = mainControlButtons.CreateInCell<Button>(2, 0);
            extCalButton.Dock = DockStyle.Fill;
            extCalButton.Text = "EXT Cal";
            extCalButton.Click += (sender, e) => _grabExternalReference = true;
            extCalButton.SetToolTip("Do external calibration");

            _liveCheck = mainControlLayout.CreateInCell<CheckBox>(0, 1);
            _liveCheck.Anchor = AnchorStyles.Left;
            _liveCheck.Checked = true;
            _liveCheck.Text = "Live mode";
            _liveCheck.SetToolTip("Display each frame, otherwise display frames only after the calibration");

            var mainControlTabs = mainControlLayout.CreateInCell<TabControl>(0, 2);
            mainControlTabs.Dock = DockStyle.Fill;

            var appearanceTab = mainControlTabs.CreateChild<TabPage>("Appearance");
            appearanceTab.Padding = new Padding(3);

            var appearanceLayout = appearanceTab.CreateLayout();
            appearanceLayout.AddColumn(SizeType.Percent, 35f);
            appearanceLayout.AddColumn(SizeType.Percent, 65f);
            appearanceLayout.AddRow(SizeType.Absolute, 30f);
            appearanceLayout.AddRow(SizeType.Absolute, 30f);
            appearanceLayout.AddRow(SizeType.Absolute, 30f);
            appearanceLayout.AddRow(SizeType.Absolute, 30f);
            appearanceLayout.AddRow(SizeType.Absolute, 100f);
            appearanceLayout.AddRow(SizeType.Absolute, 30f);
            appearanceLayout.AddRow(SizeType.Absolute, 30f);
            appearanceLayout.AddRow(SizeType.Absolute, 30f);

            var paletteLabel = appearanceLayout.CreateInCell<Label>(0, 0);
            paletteLabel.Anchor = AnchorStyles.Left;
            paletteLabel.Text = "Palette:";
            paletteLabel.TextAlign = ContentAlignment.MiddleLeft;

            _paletteCombo = appearanceLayout.CreateInCell<ComboBox>(1, 0);
            _paletteCombo.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            _paletteCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            _paletteCombo.SelectedIndexChanged += HandlePaletteComboSelectedIndexChanged;

            var unitsLabel = appearanceLayout.CreateInCell<Label>(0, 1);
            unitsLabel.Anchor = AnchorStyles.Left;
            unitsLabel.Text = "Units:";
            unitsLabel.TextAlign = ContentAlignment.MiddleLeft;

            var unitsLayout = appearanceLayout.CreateSublayout(1, 1);
            unitsLayout.AddColumn(SizeType.Percent, 1f / 3f);
            unitsLayout.AddColumn(SizeType.Percent, 1f / 3f);
            unitsLayout.AddColumn(SizeType.Percent, 1f / 3f);
            unitsLayout.AddRow(SizeType.Percent, 100f);

            _unitsKRadio = unitsLayout.CreateInCell<RadioButton>(0, 0);
            _unitsKRadio.Anchor = AnchorStyles.Left;
            _unitsKRadio.Checked = true;
            _unitsKRadio.Text = "K";
            _unitsKRadio.CheckedChanged += HandleUnitRadiosCheckedChanged;

            _unitsCRadio = unitsLayout.CreateInCell<RadioButton>(1, 0);
            _unitsCRadio.Anchor = AnchorStyles.Left;
            _unitsCRadio.Text = "°C";
            _unitsCRadio.CheckedChanged += HandleUnitRadiosCheckedChanged;

            _unitsFRadio = unitsLayout.CreateInCell<RadioButton>(2, 0);
            _unitsFRadio.Anchor = AnchorStyles.Left;
            _unitsFRadio.Text = "°F";
            _unitsFRadio.CheckedChanged += HandleUnitRadiosCheckedChanged;

            var applyDenoisingCheck = appearanceLayout.CreateInCell<CheckBox>(0, 2);
            applyDenoisingCheck.Anchor = AnchorStyles.Left;
            applyDenoisingCheck.Checked = true;
            applyDenoisingCheck.Text = "Apply denoising filter";
            applyDenoisingCheck.CheckedChanged += (sender, e) => _framePicture.ApplyDenoising = applyDenoisingCheck.Checked;
            applyDenoisingCheck.SetColumnSpan(2);

            var applySharpenCheck = appearanceLayout.CreateInCell<CheckBox>(0, 3);
            applySharpenCheck.Anchor = AnchorStyles.Left;
            applySharpenCheck.Text = "Apply sharpening filter";
            applySharpenCheck.CheckedChanged += (sender, e) => _framePicture.ApplySharpening = applySharpenCheck.Checked;
            applySharpenCheck.SetColumnSpan(2);

            _histogramPicture = appearanceLayout.CreateInCell<HistogramPictureBox>(0, 4);
            _histogramPicture.BorderStyle = BorderStyle.FixedSingle;
            _histogramPicture.Dock = DockStyle.Fill;
            _histogramPicture.SizeMode = PictureBoxSizeMode.StretchImage;
            _histogramPicture.SetColumnSpan(2);

            _manualRangeSwitchButton = appearanceLayout.CreateInCell<Button>(0, 5);
            _manualRangeSwitchButton.Dock = DockStyle.Fill;
            _manualRangeSwitchButton.Text = "Switch to manual range";
            _manualRangeSwitchButton.Click += HandleManualRangeSwitchButtonClick;
            _manualRangeSwitchButton.SetColumnSpan(2);

            _dynSlidersCheck = appearanceLayout.CreateInCell<CheckBox>(0, 6);
            _dynSlidersCheck.Anchor = AnchorStyles.Left;
            _dynSlidersCheck.Text = "Enable relative sliders";
            _dynSlidersCheck.Visible = false;
            _dynSlidersCheck.CheckedChanged += HandleDynSlidersCheckCheckedChanged;
            _dynSlidersCheck.SetColumnSpan(2);

            var analysisTab = mainControlTabs.CreateChild<TabPage>("Analysis");
            analysisTab.Padding = new Padding(3);

            var analysisLayout = analysisTab.CreateLayout();
            analysisLayout.AddColumn(SizeType.Percent, 35f);
            analysisLayout.AddColumn(SizeType.Percent, 65f);
            analysisLayout.AddRow(SizeType.Absolute, 30f);
            analysisLayout.AddRow(SizeType.Absolute, 30f);
            analysisLayout.AddRow(SizeType.Absolute, 30f);
            analysisLayout.AddRow(SizeType.Absolute, 30f);
            analysisLayout.AddRow(SizeType.Absolute, 30f);
            analysisLayout.AddRow(SizeType.Absolute, 30f);
            analysisLayout.AddRow(SizeType.Absolute, 30f);
            analysisLayout.AddRow(SizeType.Absolute, 30f);

            var enableAnalysisCheck = analysisLayout.CreateInCell<CheckBox>(0, 0);
            enableAnalysisCheck.Anchor = AnchorStyles.Left;
            enableAnalysisCheck.Text = "Enabled";
            enableAnalysisCheck.CheckedChanged += (sender, e) => _framePicture.AnalysisEnabled = enableAnalysisCheck.Checked;
            enableAnalysisCheck.SetColumnSpan(2);

            var showTemperatureCheck = analysisLayout.CreateInCell<CheckBox>(0, 1);
            showTemperatureCheck.Anchor = AnchorStyles.Left;
            showTemperatureCheck.Checked = true;
            showTemperatureCheck.Text = "Show temperature";
            showTemperatureCheck.CheckedChanged += (sender, e) => _framePicture.ShowTemperature = showTemperatureCheck.Checked;
            showTemperatureCheck.SetColumnSpan(2);

            var crossSizeLabel = analysisLayout.CreateInCell<Label>(0, 2);
            crossSizeLabel.Anchor = AnchorStyles.Left;
            crossSizeLabel.Text = "Cross size:";
            crossSizeLabel.TextAlign = ContentAlignment.MiddleLeft;

            var crossSizeSpinner = analysisLayout.CreateInCell<NumericUpDown>(1, 2);
            crossSizeSpinner.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            crossSizeSpinner.Maximum = 64;
            crossSizeSpinner.Minimum = 8;
            crossSizeSpinner.Value = 16;
            crossSizeSpinner.ValueChanged += (sender, e) => _framePicture.CrossSize = (int) crossSizeSpinner.Value;

            var showExtremesCheck = analysisLayout.CreateInCell<CheckBox>(0, 3);
            showExtremesCheck.Anchor = AnchorStyles.Left;
            showExtremesCheck.Checked = true;
            showExtremesCheck.Text = "Show extremes";
            showExtremesCheck.CheckedChanged += (sender, e) => _framePicture.ShowExtremes = showExtremesCheck.Checked;
            showExtremesCheck.SetColumnSpan(2);

            var maxPointsLabel = analysisLayout.CreateInCell<Label>(0, 4);
            maxPointsLabel.Anchor = AnchorStyles.Left;
            maxPointsLabel.Text = "Max points:";
            maxPointsLabel.TextAlign = ContentAlignment.MiddleLeft;

            var maxPointsSpinner = analysisLayout.CreateInCell<NumericUpDown>(1, 4);
            maxPointsSpinner.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            maxPointsSpinner.Maximum = 10;
            maxPointsSpinner.Minimum = 1;
            maxPointsSpinner.Value = 3;
            maxPointsSpinner.ValueChanged += (sender, e) => _framePicture.MaxPoints = (int) maxPointsSpinner.Value;

            var deletePointsButton = analysisLayout.CreateInCell<Button>(0, 5);
            deletePointsButton.Dock = DockStyle.Fill;
            deletePointsButton.Text = "Delete all points";
            deletePointsButton.Click += (sender, e) => _framePicture.DeleteAllPoints();
            deletePointsButton.SetColumnSpan(2);

            var gaugeLabelCountLabel = analysisLayout.CreateInCell<Label>(0, 6);
            gaugeLabelCountLabel.Anchor = AnchorStyles.Left;
            gaugeLabelCountLabel.Text = "Gauge labels:";
            gaugeLabelCountLabel.TextAlign = ContentAlignment.MiddleLeft;

            var gaugeLabelCountSpinner = analysisLayout.CreateInCell<NumericUpDown>(1, 6);
            gaugeLabelCountSpinner.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            gaugeLabelCountSpinner.Maximum = 10;
            gaugeLabelCountSpinner.Minimum = 2;
            gaugeLabelCountSpinner.Value = 2;
            gaugeLabelCountSpinner.ValueChanged += (sender, e) => _tempGaugePicture.LabelCount = (int) gaugeLabelCountSpinner.Value;

            var outputTab = mainControlTabs.CreateChild<TabPage>("Output");
            outputTab.Padding = new Padding(3);

            var outputLayout = outputTab.CreateLayout();
            outputLayout.AddColumn(SizeType.Percent, 50f);
            outputLayout.AddColumn(SizeType.Percent, 50f);
            outputLayout.AddRow(SizeType.Absolute, 25f);
            outputLayout.AddRow(SizeType.Absolute, 30f);
            outputLayout.AddRow(SizeType.Absolute, 30f);
            outputLayout.AddRow(SizeType.Absolute, 30f);
            outputLayout.AddRow(SizeType.Absolute, 30f);

            var outputPathLabel = outputLayout.CreateInCell<Label>(0, 0);
            outputPathLabel.Anchor = AnchorStyles.Left;
            outputPathLabel.Text = "Output path:";
            outputPathLabel.TextAlign = ContentAlignment.MiddleLeft;
            outputPathLabel.SetColumnSpan(2);

            var outputPathLayout = outputLayout.CreateSublayout(0, 1);
            outputPathLayout.AddColumn(SizeType.Percent, 100f);
            outputPathLayout.AddColumn(SizeType.Absolute, 30f);
            outputPathLayout.AddRow(SizeType.Percent, 100f);
            outputPathLayout.SetColumnSpan(2);

            _outputPathField = outputPathLayout.CreateInCell<TextBox>(0, 0);
            _outputPathField.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            _outputPathField.MaxLength = 256;

            var outputPathButton = outputPathLayout.CreateInCell<Button>(1, 0);
            outputPathButton.Dock = DockStyle.Fill;
            outputPathButton.Text = "...";
            outputPathButton.Click += HandleOutputPathButtonClick;

            _autoSaveCheck = outputLayout.CreateInCell<CheckBox>(0, 2);
            _autoSaveCheck.Anchor = AnchorStyles.Left;
            _autoSaveCheck.Text = "Screenshot on each calibration";
            _autoSaveCheck.SetColumnSpan(2);

            var screenshotButton = outputLayout.CreateInCell<Button>(0, 3);
            screenshotButton.Dock = DockStyle.Fill;
            screenshotButton.Text = "Screenshot";
            screenshotButton.Click += HandleScreenshotButtonClick;

            _recordVideoButton = outputLayout.CreateInCell<Button>(1, 3);
            _recordVideoButton.Dock = DockStyle.Fill;
            _recordVideoButton.Text = "Record video";
            _recordVideoButton.Click += HandleRecordVideoButtonClick;

            var debugValueLayout = mainLayout.CreateSublayout(0, 1);
            debugValueLayout.AddColumn(SizeType.Percent, 1f / 3f);
            debugValueLayout.AddColumn(SizeType.Percent, 1f / 3f);
            debugValueLayout.AddColumn(SizeType.Percent, 1f / 3f);
            debugValueLayout.AddRow(SizeType.Percent, 100f);

            _gModeLeftLabel = debugValueLayout.CreateInCell<Label>(0, 0);
            _gModeLeftLabel.Anchor = AnchorStyles.None;
            _gModeLeftLabel.TextAlign = ContentAlignment.MiddleCenter;

            _gModeRightLabel = debugValueLayout.CreateInCell<Label>(1, 0);
            _gModeRightLabel.Anchor = AnchorStyles.None;
            _gModeRightLabel.TextAlign = ContentAlignment.MiddleCenter;

            _maxTempRawLabel = debugValueLayout.CreateInCell<Label>(2, 0);
            _maxTempRawLabel.Anchor = AnchorStyles.None;
            _maxTempRawLabel.TextAlign = ContentAlignment.MiddleCenter;

            _picturePanel = mainLayout.CreateInCell<Panel>(1, 0);
            _picturePanel.Dock = DockStyle.Fill;
            _picturePanel.Padding = new Padding(3);

            _framePicture = _picturePanel.CreateChild<FramePictureBox>();
            _framePicture.SizeMode = PictureBoxSizeMode.StretchImage;
            _framePicture.MouseEnter += HandlePictureMouseEnter;
            _framePicture.MouseLeave += HandlePictureMouseLeave;
            _framePicture.MouseMove += (sender, e) => UpdateMouseLabelFromFramePicture(e.Location);

            _tempGaugePicture = _picturePanel.CreateChild<TemperatureGaugePictureBox>();
            _tempGaugePicture.SizeMode = PictureBoxSizeMode.StretchImage;
            _tempGaugePicture.MouseEnter += HandlePictureMouseEnter;
            _tempGaugePicture.MouseLeave += HandlePictureMouseLeave;
            _tempGaugePicture.MouseMove += (sender, e) => UpdateMouseLabelFromTempGaugePicture(e.Location);

            _mouseLabel = mainLayout.CreateInCell<Label>(1, 1);
            _mouseLabel.Dock = DockStyle.Fill;
            _mouseLabel.TextAlign = ContentAlignment.MiddleCenter;

            var sliderLayout = mainLayout.CreateSublayout(0, 2);
            sliderLayout.AddColumn(SizeType.Absolute, 40f);
            sliderLayout.AddColumn(SizeType.Percent, 100f);
            sliderLayout.AddColumn(SizeType.Absolute, 60f);
            sliderLayout.AddRow(SizeType.Percent, 50f);
            sliderLayout.AddRow(SizeType.Percent, 50f);
            sliderLayout.SetColumnSpan(2);

            var sliderMinLabel = sliderLayout.CreateInCell<Label>(0, 0);
            sliderMinLabel.Dock = DockStyle.Fill;
            sliderMinLabel.Text = "Low";
            sliderMinLabel.TextAlign = ContentAlignment.MiddleLeft;

            var sliderMaxLabel = sliderLayout.CreateInCell<Label>(0, 1);
            sliderMaxLabel.Dock = DockStyle.Fill;
            sliderMaxLabel.Text = "High";
            sliderMaxLabel.TextAlign = ContentAlignment.MiddleLeft;

            _sliderMinTempLabel = sliderLayout.CreateInCell<Label>(2, 0);
            _sliderMinTempLabel.Dock = DockStyle.Fill;
            _sliderMinTempLabel.TextAlign = ContentAlignment.MiddleCenter;

            _sliderMaxTempLabel = sliderLayout.CreateInCell<Label>(2, 1);
            _sliderMaxTempLabel.Dock = DockStyle.Fill;
            _sliderMaxTempLabel.TextAlign = ContentAlignment.MiddleCenter;

            _minTempSlider = sliderLayout.CreateInCell<TrackBar>(1, 0);
            _minTempSlider.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            _minTempSlider.LargeChange = 100;
            _minTempSlider.Maximum = 20000;
            _minTempSlider.Minimum = 4000;
            _minTempSlider.SmallChange = 10;
            _minTempSlider.TickFrequency = 100;
            _minTempSlider.Value = 4000;
            _minTempSlider.Scroll += HandleMinTempSliderScroll;

            _maxTempSlider = sliderLayout.CreateInCell<TrackBar>(1, 1);
            _maxTempSlider.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            _maxTempSlider.LargeChange = 100;
            _maxTempSlider.Maximum = 20000;
            _maxTempSlider.Minimum = 4000;
            _maxTempSlider.SmallChange = 10;
            _maxTempSlider.TickFrequency = 100;
            _maxTempSlider.Value = 4000;
            _maxTempSlider.Scroll += HandleMaxTempSliderScroll;
        }
    }
}
