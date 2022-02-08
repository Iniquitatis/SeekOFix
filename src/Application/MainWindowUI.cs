using System;
using System.Drawing;
using System.Windows.Forms;

using static SeekOFix.UIUtils;

namespace SeekOFix
{
    partial class MainWindow
    {
        private CheckBox liveCheck;
        private ComboBox paletteCombo;
        private RadioButton unitsKRadio;
        private RadioButton unitsCRadio;
        private RadioButton unitsFRadio;
        private HistogramPictureBox histogramPicture;
        private Button manualRangeSwitchButton;
        private CheckBox dynSlidersCheck;
        private TextBox outputPathField;
        private CheckBox autoSaveCheck;
        private Button recordVideoButton;
        private Label gModeLeftLabel;
        private Label gModeRightLabel;
        private Label maxTempRawLabel;
        private Panel picturePanel;
        private FramePictureBox framePicture;
        private TemperatureGaugePictureBox tempGaugePicture;
        private Label mouseLabel;
        private Label sliderMinTempLabel;
        private Label sliderMaxTempLabel;
        private TrackBar minTempSlider;
        private TrackBar maxTempSlider;

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
                startStopButton.Text = isRunning ? "STOP" : "START";
            };
            startStopButton.SetToolTip("Start/stop streaming");

            var intCalButton = mainControlButtons.CreateInCell<Button>(1, 0);
            intCalButton.Dock = DockStyle.Fill;
            intCalButton.Text = "INT Cal";
            intCalButton.Click += (sender, e) => usingExternalCal = false;
            intCalButton.SetToolTip("Do internal calibration");

            var extCalButton = mainControlButtons.CreateInCell<Button>(2, 0);
            extCalButton.Dock = DockStyle.Fill;
            extCalButton.Text = "EXT Cal";
            extCalButton.Click += (sender, e) => grabExternalReference = true;
            extCalButton.SetToolTip("Do external calibration");

            liveCheck = mainControlLayout.CreateInCell<CheckBox>(0, 1);
            liveCheck.Anchor = AnchorStyles.Left;
            liveCheck.Checked = true;
            liveCheck.Text = "Live mode";
            liveCheck.SetToolTip("Display each frame, otherwise display frames only after the calibration");

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

            paletteCombo = appearanceLayout.CreateInCell<ComboBox>(1, 0);
            paletteCombo.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            paletteCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            paletteCombo.SelectedIndexChanged += HandlePaletteComboSelectedIndexChanged;

            var unitsLabel = appearanceLayout.CreateInCell<Label>(0, 1);
            unitsLabel.Anchor = AnchorStyles.Left;
            unitsLabel.Text = "Units:";
            unitsLabel.TextAlign = ContentAlignment.MiddleLeft;

            var unitsLayout = appearanceLayout.CreateSublayout(1, 1);
            unitsLayout.AddColumn(SizeType.Percent, 1f / 3f);
            unitsLayout.AddColumn(SizeType.Percent, 1f / 3f);
            unitsLayout.AddColumn(SizeType.Percent, 1f / 3f);
            unitsLayout.AddRow(SizeType.Percent, 100f);

            unitsKRadio = unitsLayout.CreateInCell<RadioButton>(0, 0);
            unitsKRadio.Anchor = AnchorStyles.Left;
            unitsKRadio.Checked = true;
            unitsKRadio.Text = "K";
            unitsKRadio.CheckedChanged += HandleUnitRadiosCheckedChanged;

            unitsCRadio = unitsLayout.CreateInCell<RadioButton>(1, 0);
            unitsCRadio.Anchor = AnchorStyles.Left;
            unitsCRadio.Text = "°C";
            unitsCRadio.CheckedChanged += HandleUnitRadiosCheckedChanged;

            unitsFRadio = unitsLayout.CreateInCell<RadioButton>(2, 0);
            unitsFRadio.Anchor = AnchorStyles.Left;
            unitsFRadio.Text = "°F";
            unitsFRadio.CheckedChanged += HandleUnitRadiosCheckedChanged;

            var applyDenoisingCheck = appearanceLayout.CreateInCell<CheckBox>(0, 2);
            applyDenoisingCheck.Anchor = AnchorStyles.Left;
            applyDenoisingCheck.Checked = true;
            applyDenoisingCheck.Text = "Apply denoising filter";
            applyDenoisingCheck.CheckedChanged += (sender, e) => framePicture.ApplyDenoising = applyDenoisingCheck.Checked;
            applyDenoisingCheck.SetColumnSpan(2);

            var applySharpenCheck = appearanceLayout.CreateInCell<CheckBox>(0, 3);
            applySharpenCheck.Anchor = AnchorStyles.Left;
            applySharpenCheck.Text = "Apply sharpening filter";
            applySharpenCheck.CheckedChanged += (sender, e) => framePicture.ApplySharpening = applySharpenCheck.Checked;
            applySharpenCheck.SetColumnSpan(2);

            histogramPicture = appearanceLayout.CreateInCell<HistogramPictureBox>(0, 4);
            histogramPicture.BorderStyle = BorderStyle.FixedSingle;
            histogramPicture.Dock = DockStyle.Fill;
            histogramPicture.SizeMode = PictureBoxSizeMode.StretchImage;
            histogramPicture.SetColumnSpan(2);

            manualRangeSwitchButton = appearanceLayout.CreateInCell<Button>(0, 5);
            manualRangeSwitchButton.Dock = DockStyle.Fill;
            manualRangeSwitchButton.Text = "Switch to manual range";
            manualRangeSwitchButton.Click += HandleManualRangeSwitchButtonClick;
            manualRangeSwitchButton.SetColumnSpan(2);

            dynSlidersCheck = appearanceLayout.CreateInCell<CheckBox>(0, 6);
            dynSlidersCheck.Anchor = AnchorStyles.Left;
            dynSlidersCheck.Text = "Enable relative sliders";
            dynSlidersCheck.Visible = false;
            dynSlidersCheck.CheckedChanged += HandleDynSlidersCheckCheckedChanged;
            dynSlidersCheck.SetColumnSpan(2);

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
            enableAnalysisCheck.CheckedChanged += (sender, e) => framePicture.AnalysisEnabled = enableAnalysisCheck.Checked;
            enableAnalysisCheck.SetColumnSpan(2);

            var showTemperatureCheck = analysisLayout.CreateInCell<CheckBox>(0, 1);
            showTemperatureCheck.Anchor = AnchorStyles.Left;
            showTemperatureCheck.Checked = true;
            showTemperatureCheck.Text = "Show temperature";
            showTemperatureCheck.CheckedChanged += (sender, e) => framePicture.ShowTemperature = showTemperatureCheck.Checked;
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
            crossSizeSpinner.ValueChanged += (sender, e) => framePicture.CrossSize = (int) crossSizeSpinner.Value;

            var showExtremesCheck = analysisLayout.CreateInCell<CheckBox>(0, 3);
            showExtremesCheck.Anchor = AnchorStyles.Left;
            showExtremesCheck.Checked = true;
            showExtremesCheck.Text = "Show extremes";
            showExtremesCheck.CheckedChanged += (sender, e) => framePicture.ShowExtremes = showExtremesCheck.Checked;
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
            maxPointsSpinner.ValueChanged += (sender, e) => framePicture.MaxPoints = (int) maxPointsSpinner.Value;

            var deletePointsButton = analysisLayout.CreateInCell<Button>(0, 5);
            deletePointsButton.Dock = DockStyle.Fill;
            deletePointsButton.Text = "Delete all points";
            deletePointsButton.Click += (sender, e) => framePicture.DeleteAllAnalyzers();
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
            gaugeLabelCountSpinner.ValueChanged += (sender, e) => tempGaugePicture.LabelCount = (int) gaugeLabelCountSpinner.Value;

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

            outputPathField = outputPathLayout.CreateInCell<TextBox>(0, 0);
            outputPathField.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            outputPathField.MaxLength = 256;

            var outputPathButton = outputPathLayout.CreateInCell<Button>(1, 0);
            outputPathButton.Dock = DockStyle.Fill;
            outputPathButton.Text = "...";
            outputPathButton.Click += HandleOutputPathButtonClick;

            autoSaveCheck = outputLayout.CreateInCell<CheckBox>(0, 2);
            autoSaveCheck.Anchor = AnchorStyles.Left;
            autoSaveCheck.Text = "Screenshot on each calibration";
            autoSaveCheck.SetColumnSpan(2);

            var screenshotButton = outputLayout.CreateInCell<Button>(0, 3);
            screenshotButton.Dock = DockStyle.Fill;
            screenshotButton.Text = "Screenshot";
            screenshotButton.Click += HandleScreenshotButtonClick;

            recordVideoButton = outputLayout.CreateInCell<Button>(1, 3);
            recordVideoButton.Dock = DockStyle.Fill;
            recordVideoButton.Text = "Record video";
            recordVideoButton.Click += HandleRecordVideoButtonClick;

            var debugValueLayout = mainLayout.CreateSublayout(0, 1);
            debugValueLayout.AddColumn(SizeType.Percent, 1f / 3f);
            debugValueLayout.AddColumn(SizeType.Percent, 1f / 3f);
            debugValueLayout.AddColumn(SizeType.Percent, 1f / 3f);
            debugValueLayout.AddRow(SizeType.Percent, 100f);

            gModeLeftLabel = debugValueLayout.CreateInCell<Label>(0, 0);
            gModeLeftLabel.Anchor = AnchorStyles.None;
            gModeLeftLabel.TextAlign = ContentAlignment.MiddleCenter;

            gModeRightLabel = debugValueLayout.CreateInCell<Label>(1, 0);
            gModeRightLabel.Anchor = AnchorStyles.None;
            gModeRightLabel.TextAlign = ContentAlignment.MiddleCenter;

            maxTempRawLabel = debugValueLayout.CreateInCell<Label>(2, 0);
            maxTempRawLabel.Anchor = AnchorStyles.None;
            maxTempRawLabel.TextAlign = ContentAlignment.MiddleCenter;

            picturePanel = mainLayout.CreateInCell<Panel>(1, 0);
            picturePanel.Dock = DockStyle.Fill;
            picturePanel.Padding = new Padding(3);

            framePicture = picturePanel.CreateChild<FramePictureBox>();
            framePicture.SizeMode = PictureBoxSizeMode.StretchImage;
            framePicture.MouseEnter += HandlePictureMouseEnter;
            framePicture.MouseLeave += HandlePictureMouseLeave;
            framePicture.MouseMove += (sender, e) => UpdateMouseLabelFromFramePicture(e.Location);

            tempGaugePicture = picturePanel.CreateChild<TemperatureGaugePictureBox>();
            tempGaugePicture.SizeMode = PictureBoxSizeMode.StretchImage;
            tempGaugePicture.MouseEnter += HandlePictureMouseEnter;
            tempGaugePicture.MouseLeave += HandlePictureMouseLeave;
            tempGaugePicture.MouseMove += (sender, e) => UpdateMouseLabelFromTempGaugePicture(e.Location);

            mouseLabel = mainLayout.CreateInCell<Label>(1, 1);
            mouseLabel.Dock = DockStyle.Fill;
            mouseLabel.TextAlign = ContentAlignment.MiddleCenter;

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

            sliderMinTempLabel = sliderLayout.CreateInCell<Label>(2, 0);
            sliderMinTempLabel.Dock = DockStyle.Fill;
            sliderMinTempLabel.TextAlign = ContentAlignment.MiddleCenter;

            sliderMaxTempLabel = sliderLayout.CreateInCell<Label>(2, 1);
            sliderMaxTempLabel.Dock = DockStyle.Fill;
            sliderMaxTempLabel.TextAlign = ContentAlignment.MiddleCenter;

            minTempSlider = sliderLayout.CreateInCell<TrackBar>(1, 0);
            minTempSlider.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            minTempSlider.LargeChange = 100;
            minTempSlider.Maximum = 20000;
            minTempSlider.Minimum = 4000;
            minTempSlider.SmallChange = 10;
            minTempSlider.TickFrequency = 100;
            minTempSlider.Value = 4000;
            minTempSlider.Scroll += HandleMinTempSliderScroll;

            maxTempSlider = sliderLayout.CreateInCell<TrackBar>(1, 1);
            maxTempSlider.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            maxTempSlider.LargeChange = 100;
            maxTempSlider.Maximum = 20000;
            maxTempSlider.Minimum = 4000;
            maxTempSlider.SmallChange = 10;
            maxTempSlider.TickFrequency = 100;
            maxTempSlider.Value = 4000;
            maxTempSlider.Scroll += HandleMaxTempSliderScroll;
        }
    }
}
