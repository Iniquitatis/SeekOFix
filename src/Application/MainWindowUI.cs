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
        private CheckBox applyDenoisingCheck;
        private CheckBox applySharpenCheck;
        private CustomPictureBox histogramPicture;
        private Button manualRangeSwitchButton;
        private CheckBox dynSlidersCheck;
        private TextBox outputPathField;
        private CheckBox autoSaveCheck;
        private Button recordVideoButton;
        private Label gModeLeftLabel;
        private Label gModeRightLabel;
        private Label maxTempRawLabel;
        private AnalyzablePictureBox picture;
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

            var mainLayout = CreateLayout(this);
            AddColumn(mainLayout, SizeType.Absolute, 250f);
            AddColumn(mainLayout, SizeType.Percent, 100f);
            AddRow(mainLayout, SizeType.Percent, 100f);
            AddRow(mainLayout, SizeType.Absolute, 25f);
            AddRow(mainLayout, SizeType.Absolute, 75f);

            var mainControlLayout = CreateSublayout(mainLayout, 0, 0);
            AddColumn(mainControlLayout, SizeType.Percent, 100f);
            AddRow(mainControlLayout, SizeType.Absolute, 30f);
            AddRow(mainControlLayout, SizeType.Absolute, 30f);
            AddRow(mainControlLayout, SizeType.Percent, 100f);

            var mainControlButtons = CreateSublayout(mainControlLayout, 0, 0);
            AddColumn(mainControlButtons, SizeType.Percent, 1f / 3f);
            AddColumn(mainControlButtons, SizeType.Percent, 1f / 3f);
            AddColumn(mainControlButtons, SizeType.Percent, 1f / 3f);
            AddRow(mainControlButtons, SizeType.Absolute, 30f);

            var startStopButton = CreateInLayout<Button>(mainControlButtons, 0, 0);
            startStopButton.Dock = DockStyle.Fill;
            startStopButton.Text = "STOP";
            startStopButton.Click += (sender, e) =>
            {
                ToggleThreadActivity();
                startStopButton.Text = isRunning ? "STOP" : "START";
            };
            SetToolTip(startStopButton, "Start/stop streaming");

            var intCalButton = CreateInLayout<Button>(mainControlButtons, 1, 0);
            intCalButton.Dock = DockStyle.Fill;
            intCalButton.Text = "INT Cal";
            intCalButton.Click += (sender, e) => usingExternalCal = false;
            SetToolTip(intCalButton, "Do internal calibration");

            var extCalButton = CreateInLayout<Button>(mainControlButtons, 2, 0);
            extCalButton.Dock = DockStyle.Fill;
            extCalButton.Text = "EXT Cal";
            extCalButton.Click += (sender, e) => grabExternalReference = true;
            SetToolTip(extCalButton, "Do external calibration");

            liveCheck = CreateInLayout<CheckBox>(mainControlLayout, 0, 1);
            liveCheck.Anchor = AnchorStyles.Left;
            liveCheck.Checked = true;
            liveCheck.Text = "Live mode";
            SetToolTip(liveCheck, "Display each frame, otherwise display frames only after the calibration");

            var mainControlTabs = CreateInLayout<TabControl>(mainControlLayout, 0, 2);
            mainControlTabs.Dock = DockStyle.Fill;

            var appearanceTab = CreateChild<TabPage>(mainControlTabs, "Appearance");
            appearanceTab.Padding = new Padding(3);

            var appearanceLayout = CreateLayout(appearanceTab);
            AddColumn(appearanceLayout, SizeType.Percent, 35f);
            AddColumn(appearanceLayout, SizeType.Percent, 65f);
            AddRow(appearanceLayout, SizeType.Absolute, 30f);
            AddRow(appearanceLayout, SizeType.Absolute, 30f);
            AddRow(appearanceLayout, SizeType.Absolute, 30f);
            AddRow(appearanceLayout, SizeType.Absolute, 30f);
            AddRow(appearanceLayout, SizeType.Absolute, 100f);
            AddRow(appearanceLayout, SizeType.Absolute, 30f);
            AddRow(appearanceLayout, SizeType.Absolute, 30f);
            AddRow(appearanceLayout, SizeType.Absolute, 30f);

            var paletteLabel = CreateInLayout<Label>(appearanceLayout, 0, 0);
            paletteLabel.Anchor = AnchorStyles.Left;
            paletteLabel.Text = "Palette:";
            paletteLabel.TextAlign = ContentAlignment.MiddleLeft;

            paletteCombo = CreateInLayout<ComboBox>(appearanceLayout, 1, 0);
            paletteCombo.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            paletteCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            paletteCombo.SelectedIndexChanged += HandlePaletteComboSelectedIndexChanged;

            var unitsLabel = CreateInLayout<Label>(appearanceLayout, 0, 1);
            unitsLabel.Anchor = AnchorStyles.Left;
            unitsLabel.Text = "Units:";
            unitsLabel.TextAlign = ContentAlignment.MiddleLeft;

            var unitsLayout = CreateSublayout(appearanceLayout, 1, 1);
            AddColumn(unitsLayout, SizeType.Percent, 1f / 3f);
            AddColumn(unitsLayout, SizeType.Percent, 1f / 3f);
            AddColumn(unitsLayout, SizeType.Percent, 1f / 3f);
            AddRow(unitsLayout, SizeType.Percent, 100f);

            unitsKRadio = CreateInLayout<RadioButton>(unitsLayout, 0, 0);
            unitsKRadio.Anchor = AnchorStyles.Left;
            unitsKRadio.Checked = true;
            unitsKRadio.Text = "K";
            unitsKRadio.CheckedChanged += HandleUnitRadiosCheckedChanged;

            unitsCRadio = CreateInLayout<RadioButton>(unitsLayout, 1, 0);
            unitsCRadio.Anchor = AnchorStyles.Left;
            unitsCRadio.Text = "°C";
            unitsCRadio.CheckedChanged += HandleUnitRadiosCheckedChanged;

            unitsFRadio = CreateInLayout<RadioButton>(unitsLayout, 2, 0);
            unitsFRadio.Anchor = AnchorStyles.Left;
            unitsFRadio.Text = "°F";
            unitsFRadio.CheckedChanged += HandleUnitRadiosCheckedChanged;

            applyDenoisingCheck = CreateInLayout<CheckBox>(appearanceLayout, 0, 2);
            applyDenoisingCheck.Anchor = AnchorStyles.Left;
            applyDenoisingCheck.Checked = true;
            applyDenoisingCheck.Text = "Apply denoising filter";
            SetColumnSpan(applyDenoisingCheck, 2);

            applySharpenCheck = CreateInLayout<CheckBox>(appearanceLayout, 0, 3);
            applySharpenCheck.Anchor = AnchorStyles.Left;
            applySharpenCheck.Text = "Apply sharpening filter";
            SetColumnSpan(applySharpenCheck, 2);

            histogramPicture = CreateInLayout<CustomPictureBox>(appearanceLayout, 0, 4);
            histogramPicture.BorderStyle = BorderStyle.FixedSingle;
            histogramPicture.Dock = DockStyle.Fill;
            histogramPicture.SizeMode = PictureBoxSizeMode.StretchImage;
            SetColumnSpan(histogramPicture, 2);

            manualRangeSwitchButton = CreateInLayout<Button>(appearanceLayout, 0, 5);
            manualRangeSwitchButton.Dock = DockStyle.Fill;
            manualRangeSwitchButton.Text = "Switch to manual range";
            manualRangeSwitchButton.Click += HandleManualRangeSwitchButtonClick;
            SetColumnSpan(manualRangeSwitchButton, 2);

            dynSlidersCheck = CreateInLayout<CheckBox>(appearanceLayout, 0, 6);
            dynSlidersCheck.Anchor = AnchorStyles.Left;
            dynSlidersCheck.Text = "Enable relative sliders";
            dynSlidersCheck.Visible = false;
            dynSlidersCheck.CheckedChanged += HandleDynSlidersCheckCheckedChanged;
            SetColumnSpan(dynSlidersCheck, 2);

            var analysisTab = CreateChild<TabPage>(mainControlTabs, "Analysis");
            analysisTab.Padding = new Padding(3);

            var analysisLayout = CreateLayout(analysisTab);
            AddColumn(analysisLayout, SizeType.Percent, 35f);
            AddColumn(analysisLayout, SizeType.Percent, 65f);
            AddRow(analysisLayout, SizeType.Absolute, 30f);
            AddRow(analysisLayout, SizeType.Absolute, 30f);
            AddRow(analysisLayout, SizeType.Absolute, 30f);
            AddRow(analysisLayout, SizeType.Absolute, 30f);
            AddRow(analysisLayout, SizeType.Absolute, 30f);
            AddRow(analysisLayout, SizeType.Absolute, 30f);
            AddRow(analysisLayout, SizeType.Absolute, 30f);
            AddRow(analysisLayout, SizeType.Absolute, 30f);

            var enableAnalysisCheck = CreateInLayout<CheckBox>(analysisLayout, 0, 0);
            enableAnalysisCheck.Anchor = AnchorStyles.Left;
            enableAnalysisCheck.Text = "Enabled";
            enableAnalysisCheck.CheckedChanged += (sender, e) => picture.AnalysisEnabled = enableAnalysisCheck.Checked;
            SetColumnSpan(enableAnalysisCheck, 2);

            var showTemperatureCheck = CreateInLayout<CheckBox>(analysisLayout, 0, 1);
            showTemperatureCheck.Anchor = AnchorStyles.Left;
            showTemperatureCheck.Checked = true;
            showTemperatureCheck.Text = "Show temperature";
            showTemperatureCheck.CheckedChanged += (sender, e) => picture.ShowTemperature = showTemperatureCheck.Checked;
            SetColumnSpan(showTemperatureCheck, 2);

            var crossSizeLabel = CreateInLayout<Label>(analysisLayout, 0, 2);
            crossSizeLabel.Anchor = AnchorStyles.Left;
            crossSizeLabel.Text = "Cross size:";
            crossSizeLabel.TextAlign = ContentAlignment.MiddleLeft;

            var crossSizeSpinner = CreateInLayout<NumericUpDown>(analysisLayout, 1, 2);
            crossSizeSpinner.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            crossSizeSpinner.Maximum = 64;
            crossSizeSpinner.Minimum = 8;
            crossSizeSpinner.Value = 16;
            crossSizeSpinner.ValueChanged += (sender, e) => picture.CrossSize = (int) crossSizeSpinner.Value;

            var showExtremesCheck = CreateInLayout<CheckBox>(analysisLayout, 0, 3);
            showExtremesCheck.Anchor = AnchorStyles.Left;
            showExtremesCheck.Checked = true;
            showExtremesCheck.Text = "Show extremes";
            showExtremesCheck.CheckedChanged += (sender, e) => picture.ShowExtremes = showExtremesCheck.Checked;
            SetColumnSpan(showExtremesCheck, 2);

            var maxPointsLabel = CreateInLayout<Label>(analysisLayout, 0, 4);
            maxPointsLabel.Anchor = AnchorStyles.Left;
            maxPointsLabel.Text = "Max points:";
            maxPointsLabel.TextAlign = ContentAlignment.MiddleLeft;

            var maxPointsSpinner = CreateInLayout<NumericUpDown>(analysisLayout, 1, 4);
            maxPointsSpinner.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            maxPointsSpinner.Maximum = 10;
            maxPointsSpinner.Minimum = 1;
            maxPointsSpinner.Value = 3;
            maxPointsSpinner.ValueChanged += (sender, e) => picture.MaxPoints = (int) maxPointsSpinner.Value;

            var deletePointsButton = CreateInLayout<Button>(analysisLayout, 0, 5);
            deletePointsButton.Dock = DockStyle.Fill;
            deletePointsButton.Text = "Delete all points";
            deletePointsButton.Click += (sender, e) => picture.DeleteAllAnalyzers();
            SetColumnSpan(deletePointsButton, 2);

            var gaugeLabelCountLabel = CreateInLayout<Label>(analysisLayout, 0, 6);
            gaugeLabelCountLabel.Anchor = AnchorStyles.Left;
            gaugeLabelCountLabel.Text = "Gauge labels:";
            gaugeLabelCountLabel.TextAlign = ContentAlignment.MiddleLeft;

            var gaugeLabelCountSpinner = CreateInLayout<NumericUpDown>(analysisLayout, 1, 6);
            gaugeLabelCountSpinner.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            gaugeLabelCountSpinner.Maximum = 10;
            gaugeLabelCountSpinner.Minimum = 2;
            gaugeLabelCountSpinner.Value = 2;
            gaugeLabelCountSpinner.ValueChanged += (sender, e) => tempGaugePicture.LabelCount = (int) gaugeLabelCountSpinner.Value;

            var outputTab = CreateChild<TabPage>(mainControlTabs, "Output");
            outputTab.Padding = new Padding(3);

            var outputLayout = CreateLayout(outputTab);
            AddColumn(outputLayout, SizeType.Percent, 50f);
            AddColumn(outputLayout, SizeType.Percent, 50f);
            AddRow(outputLayout, SizeType.Absolute, 25f);
            AddRow(outputLayout, SizeType.Absolute, 30f);
            AddRow(outputLayout, SizeType.Absolute, 30f);
            AddRow(outputLayout, SizeType.Absolute, 30f);
            AddRow(outputLayout, SizeType.Absolute, 30f);

            var outputPathLabel = CreateInLayout<Label>(outputLayout, 0, 0);
            outputPathLabel.Anchor = AnchorStyles.Left;
            outputPathLabel.Text = "Output path:";
            outputPathLabel.TextAlign = ContentAlignment.MiddleLeft;
            SetColumnSpan(outputPathLabel, 2);

            var outputPathLayout = CreateSublayout(outputLayout, 0, 1);
            AddColumn(outputPathLayout, SizeType.Percent, 100f);
            AddColumn(outputPathLayout, SizeType.Absolute, 30f);
            AddRow(outputPathLayout, SizeType.Percent, 100f);
            SetColumnSpan(outputPathLayout, 2);

            outputPathField = CreateInLayout<TextBox>(outputPathLayout, 0, 0);
            outputPathField.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            outputPathField.MaxLength = 256;

            var outputPathButton = CreateInLayout<Button>(outputPathLayout, 1, 0);
            outputPathButton.Dock = DockStyle.Fill;
            outputPathButton.Text = "...";
            outputPathButton.Click += HandleOutputPathButtonClick;

            autoSaveCheck = CreateInLayout<CheckBox>(outputLayout, 0, 2);
            autoSaveCheck.Anchor = AnchorStyles.Left;
            autoSaveCheck.Text = "Screenshot on each calibration";
            SetColumnSpan(autoSaveCheck, 2);

            var screenshotButton = CreateInLayout<Button>(outputLayout, 0, 3);
            screenshotButton.Dock = DockStyle.Fill;
            screenshotButton.Text = "Screenshot";
            screenshotButton.Click += HandleScreenshotButtonClick;

            recordVideoButton = CreateInLayout<Button>(outputLayout, 1, 3);
            recordVideoButton.Dock = DockStyle.Fill;
            recordVideoButton.Text = "Record video";
            recordVideoButton.Click += HandleRecordVideoButtonClick;

            var debugValueLayout = CreateSublayout(mainLayout, 0, 1);
            AddColumn(debugValueLayout, SizeType.Percent, 1f / 3f);
            AddColumn(debugValueLayout, SizeType.Percent, 1f / 3f);
            AddColumn(debugValueLayout, SizeType.Percent, 1f / 3f);
            AddRow(debugValueLayout, SizeType.Percent, 100f);

            gModeLeftLabel = CreateInLayout<Label>(debugValueLayout, 0, 0);
            gModeLeftLabel.Anchor = AnchorStyles.None;
            gModeLeftLabel.TextAlign = ContentAlignment.MiddleCenter;

            gModeRightLabel = CreateInLayout<Label>(debugValueLayout, 1, 0);
            gModeRightLabel.Anchor = AnchorStyles.None;
            gModeRightLabel.TextAlign = ContentAlignment.MiddleCenter;

            maxTempRawLabel = CreateInLayout<Label>(debugValueLayout, 2, 0);
            maxTempRawLabel.Anchor = AnchorStyles.None;
            maxTempRawLabel.TextAlign = ContentAlignment.MiddleCenter;

            var picturePanel = CreateInLayout<Panel>(mainLayout, 1, 0);
            picturePanel.Dock = DockStyle.Fill;
            picturePanel.Padding = new Padding(3);

            picture = CreateChild<AnalyzablePictureBox>(picturePanel);
            picture.BorderStyle = BorderStyle.FixedSingle;
            picture.Size = new Size(32, 32);
            picture.SizeMode = PictureBoxSizeMode.StretchImage;
            picture.MouseEnter += HandleAnalyzablePictureMouseEnter;
            picture.MouseLeave += HandleAnalyzablePictureMouseLeave;
            picture.MouseMove += HandleAnalyzablePictureMouseMove;

            tempGaugePicture = CreateChild<TemperatureGaugePictureBox>(picturePanel);
            tempGaugePicture.BorderStyle = BorderStyle.FixedSingle;
            tempGaugePicture.SizeMode = PictureBoxSizeMode.StretchImage;

            mouseLabel = CreateInLayout<Label>(mainLayout, 1, 1);
            mouseLabel.Dock = DockStyle.Fill;
            mouseLabel.TextAlign = ContentAlignment.MiddleCenter;

            var sliderLayout = CreateSublayout(mainLayout, 0, 2);
            AddColumn(sliderLayout, SizeType.Absolute, 40f);
            AddColumn(sliderLayout, SizeType.Percent, 100f);
            AddColumn(sliderLayout, SizeType.Absolute, 60f);
            AddRow(sliderLayout, SizeType.Percent, 50f);
            AddRow(sliderLayout, SizeType.Percent, 50f);
            SetColumnSpan(sliderLayout, 2);

            var sliderMinLabel = CreateInLayout<Label>(sliderLayout, 0, 0);
            sliderMinLabel.Dock = DockStyle.Fill;
            sliderMinLabel.Text = "Low";
            sliderMinLabel.TextAlign = ContentAlignment.MiddleLeft;

            var sliderMaxLabel = CreateInLayout<Label>(sliderLayout, 0, 1);
            sliderMaxLabel.Dock = DockStyle.Fill;
            sliderMaxLabel.Text = "High";
            sliderMaxLabel.TextAlign = ContentAlignment.MiddleLeft;

            sliderMinTempLabel = CreateInLayout<Label>(sliderLayout, 2, 0);
            sliderMinTempLabel.Dock = DockStyle.Fill;
            sliderMinTempLabel.TextAlign = ContentAlignment.MiddleCenter;

            sliderMaxTempLabel = CreateInLayout<Label>(sliderLayout, 2, 1);
            sliderMaxTempLabel.Dock = DockStyle.Fill;
            sliderMaxTempLabel.TextAlign = ContentAlignment.MiddleCenter;

            minTempSlider = CreateInLayout<TrackBar>(sliderLayout, 1, 0);
            minTempSlider.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            minTempSlider.LargeChange = 100;
            minTempSlider.Maximum = 20000;
            minTempSlider.Minimum = 4000;
            minTempSlider.SmallChange = 10;
            minTempSlider.TickFrequency = 100;
            minTempSlider.Value = 4000;
            minTempSlider.Scroll += HandleMinTempSliderScroll;

            maxTempSlider = CreateInLayout<TrackBar>(sliderLayout, 1, 1);
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
