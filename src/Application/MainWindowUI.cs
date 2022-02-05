using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using static SeekOFix.UIUtils;

namespace SeekOFix
{
    partial class MainWindow
    {
        private IContainer components = null;
        private ComboBox paletteCombo;
        private RadioButton unitsKRadio;
        private RadioButton unitsCRadio;
        private RadioButton unitsFRadio;
        private CheckBox applySharpenCheck;
        private PictureBox histogramPicture;
        private Button manualRangeSwitchButton;
        private CheckBox dynSlidersCheck;
        private CheckBox enableAnalysisCheck;
        private CheckBox showTemperatureCheck;
        private NumericUpDown crossSizeSpinner;
        private CheckBox showExtremesCheck;
        private NumericUpDown maxCountSpinner;
        private TextBox outputPathField;
        private Button recordVideoButton;
        private Label gModeLeftLabel;
        private Label gModeRightLabel;
        private Label maxTempRawLabel;
        private TabControl pictureTabs;
        private TabPage liveTab;
        private AnalyzablePictureBox livePicture;
        private TabPage firstAfterCalTab;
        private AnalyzablePictureBox firstAfterCalPicture;
        private Label mouseLabel;
        private PictureBox tempGaugePicture;
        private Label tempGaugeMinLabel;
        private Label tempGaugeMaxLabel;
        private Label sliderMinTempLabel;
        private Label sliderMaxTempLabel;
        private TrackBar minTempSlider;
        private TrackBar maxTempSlider;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            AutoScaleDimensions = new SizeF(6f, 13f);
            AutoScaleMode = AutoScaleMode.Font;
            MinimumSize = new Size(640, 360);
            Padding = new Padding(5);
            Size = new Size(1024, 768);
            Text = "SeekOFix";
            FormClosing += new FormClosingEventHandler(HandleFormClosing);
            Load += new EventHandler(HandleLoad);
            Paint += new PaintEventHandler(HandlePaint);

            components = new Container();

            var mainLayout = CreateChild<TableLayoutPanel>(this);
            mainLayout.Dock = DockStyle.Fill;
            mainLayout.Margin = new Padding(0);
            AddColumn(mainLayout, SizeType.Absolute, 250f);
            AddColumn(mainLayout, SizeType.Percent, 100f);
            AddRow(mainLayout, SizeType.Percent, 100f);
            AddRow(mainLayout, SizeType.Absolute, 25f);
            AddRow(mainLayout, SizeType.Absolute, 75f);

            var mainControlLayout = CreateInLayout<TableLayoutPanel>(mainLayout, 0, 0);
            mainControlLayout.Dock = DockStyle.Fill;
            mainControlLayout.Margin = new Padding(0);
            AddColumn(mainControlLayout, SizeType.Percent, 100f);
            AddRow(mainControlLayout, SizeType.Absolute, 30f);
            AddRow(mainControlLayout, SizeType.Percent, 100f);

            var mainControlButtons = CreateInLayout<TableLayoutPanel>(mainControlLayout, 0, 0);
            mainControlButtons.Dock = DockStyle.Fill;
            mainControlButtons.Margin = new Padding(0);
            AddColumn(mainControlButtons, SizeType.Percent, 1f / 3f);
            AddColumn(mainControlButtons, SizeType.Percent, 1f / 3f);
            AddColumn(mainControlButtons, SizeType.Percent, 1f / 3f);
            AddRow(mainControlButtons, SizeType.Absolute, 30f);

            var startStopButton = CreateInLayout<Button>(mainControlButtons, 0, 0);
            startStopButton.Dock = DockStyle.Fill;
            startStopButton.Text = "STOP";
            startStopButton.Click += new EventHandler((sender, e) =>
            {
                ToggleThreadActivity();
                startStopButton.Text = isRunning ? "STOP" : "START";
            });

            var startStopToolTip = new ToolTip(components);
            startStopToolTip.SetToolTip(startStopButton, "Start/stop streaming");

            var intCalButton = CreateInLayout<Button>(mainControlButtons, 1, 0);
            intCalButton.Dock = DockStyle.Fill;
            intCalButton.Text = "INT Cal";
            intCalButton.Click += new EventHandler((sender, e) => usingExternalCal = false);

            var intCalToolTip = new ToolTip(components);
            intCalToolTip.SetToolTip(intCalButton, "Do internal calibration");

            var extCalButton = CreateInLayout<Button>(mainControlButtons, 2, 0);
            extCalButton.Dock = DockStyle.Fill;
            extCalButton.Text = "EXT Cal";
            extCalButton.Click += new EventHandler((sender, e) => grabExternalReference = true);

            var extCalToolTip = new ToolTip(components);
            extCalToolTip.SetToolTip(extCalButton, "Do external calibration");

            var mainControlTabs = CreateInLayout<TabControl>(mainControlLayout, 0, 1);
            mainControlTabs.Dock = DockStyle.Fill;

            var appearanceTab = CreateChild<TabPage>(mainControlTabs, "Appearance");
            appearanceTab.Padding = new Padding(3);
            appearanceTab.UseVisualStyleBackColor = true;

            var appearanceLayout = CreateChild<TableLayoutPanel>(appearanceTab);
            appearanceLayout.Dock = DockStyle.Fill;
            appearanceLayout.Margin = new Padding(0);
            AddColumn(appearanceLayout, SizeType.Percent, 35f);
            AddColumn(appearanceLayout, SizeType.Percent, 65f);
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
            paletteCombo.FormattingEnabled = true;
            paletteCombo.SelectedIndexChanged += new EventHandler(HandlePaletteComboSelectedIndexChanged);

            var unitsLabel = CreateInLayout<Label>(appearanceLayout, 0, 1);
            unitsLabel.Anchor = AnchorStyles.Left;
            unitsLabel.Text = "Temp units:";
            unitsLabel.TextAlign = ContentAlignment.MiddleLeft;

            var unitsLayout = CreateInLayout<TableLayoutPanel>(appearanceLayout, 1, 1);
            unitsLayout.Dock = DockStyle.Fill;
            unitsLayout.Margin = new Padding(0);
            AddColumn(unitsLayout, SizeType.Percent, 1f / 3f);
            AddColumn(unitsLayout, SizeType.Percent, 1f / 3f);
            AddColumn(unitsLayout, SizeType.Percent, 1f / 3f);
            AddRow(unitsLayout, SizeType.Percent, 100f);

            unitsKRadio = CreateInLayout<RadioButton>(unitsLayout, 0, 0);
            unitsKRadio.Anchor = AnchorStyles.Left;
            unitsKRadio.AutoSize = true;
            unitsKRadio.Checked = true;
            unitsKRadio.Text = "K";
            unitsKRadio.CheckedChanged += new EventHandler(HandleUnitRadiosCheckedChanged);
            
            unitsCRadio = CreateInLayout<RadioButton>(unitsLayout, 1, 0);
            unitsCRadio.Anchor = AnchorStyles.Left;
            unitsCRadio.AutoSize = true;
            unitsCRadio.Text = "°C";
            unitsCRadio.CheckedChanged += new EventHandler(HandleUnitRadiosCheckedChanged);
            
            unitsFRadio = CreateInLayout<RadioButton>(unitsLayout, 2, 0);
            unitsFRadio.Anchor = AnchorStyles.Left;
            unitsFRadio.AutoSize = true;
            unitsFRadio.Text = "°F";
            unitsFRadio.CheckedChanged += new EventHandler(HandleUnitRadiosCheckedChanged);
            
            applySharpenCheck = CreateInLayout<CheckBox>(appearanceLayout, 0, 2);
            applySharpenCheck.Anchor = AnchorStyles.Left;
            applySharpenCheck.AutoSize = true;
            applySharpenCheck.Text = "Apply sharpening filter";
            applySharpenCheck.CheckedChanged += new EventHandler((sender, e) => sharpenImage = !sharpenImage);
            SetColumnSpan(applySharpenCheck, 2);

            histogramPicture = CreateInLayout<PictureBox>(appearanceLayout, 0, 3);
            histogramPicture.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            histogramPicture.BackColor = SystemColors.ControlLightLight;
            histogramPicture.BorderStyle = BorderStyle.FixedSingle;
            histogramPicture.SizeMode = PictureBoxSizeMode.StretchImage;
            SetColumnSpan(histogramPicture, 2);

            manualRangeSwitchButton = CreateInLayout<Button>(appearanceLayout, 0, 4);
            manualRangeSwitchButton.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            manualRangeSwitchButton.Text = "Switch to manual range";
            manualRangeSwitchButton.Click += new EventHandler(HandleManualRangeSwitchButtonClick);
            SetColumnSpan(manualRangeSwitchButton, 2);

            dynSlidersCheck = CreateInLayout<CheckBox>(appearanceLayout, 0, 5);
            dynSlidersCheck.Anchor = AnchorStyles.Left;
            dynSlidersCheck.AutoSize = true;
            dynSlidersCheck.Text = "Enable relative sliders";
            dynSlidersCheck.Visible = false;
            dynSlidersCheck.CheckedChanged += new EventHandler(HandleDynSlidersCheckCheckedChanged);
            SetColumnSpan(dynSlidersCheck, 2);

            var analysisTab = CreateChild<TabPage>(mainControlTabs, "Analysis");
            analysisTab.Padding = new Padding(3);
            analysisTab.UseVisualStyleBackColor = true;
            
            var analysisLayout = CreateChild<TableLayoutPanel>(analysisTab);
            analysisLayout.Dock = DockStyle.Fill;
            analysisLayout.Margin = new Padding(0);
            AddColumn(analysisLayout, SizeType.Percent, 35f);
            AddColumn(analysisLayout, SizeType.Percent, 65f);
            AddRow(analysisLayout, SizeType.Absolute, 30f);
            AddRow(analysisLayout, SizeType.Absolute, 30f);
            AddRow(analysisLayout, SizeType.Absolute, 30f);
            AddRow(analysisLayout, SizeType.Absolute, 30f);
            AddRow(analysisLayout, SizeType.Absolute, 30f);
            AddRow(analysisLayout, SizeType.Absolute, 30f);
            AddRow(analysisLayout, SizeType.Absolute, 30f);

            enableAnalysisCheck = CreateInLayout<CheckBox>(analysisLayout, 0, 0);
            enableAnalysisCheck.Anchor = AnchorStyles.Left;
            enableAnalysisCheck.AutoSize = true;
            enableAnalysisCheck.Text = "Enabled";
            enableAnalysisCheck.CheckedChanged += new EventHandler(HandleAnalysisCheckCheckedChanged);
            SetColumnSpan(enableAnalysisCheck, 2);

            showTemperatureCheck = CreateInLayout<CheckBox>(analysisLayout, 0, 1);
            showTemperatureCheck.Anchor = AnchorStyles.Left;
            showTemperatureCheck.AutoSize = true;
            showTemperatureCheck.Checked = true;
            showTemperatureCheck.Text = "Show temperature";
            showTemperatureCheck.CheckedChanged += new EventHandler(HandleShowTemperatureCheckCheckedChanged);
            SetColumnSpan(showTemperatureCheck, 2);

            var crossSizeLabel = CreateInLayout<Label>(analysisLayout, 0, 2);
            crossSizeLabel.Anchor = AnchorStyles.Left;
            crossSizeLabel.Text = "Cross size:";
            crossSizeLabel.TextAlign = ContentAlignment.MiddleLeft;

            crossSizeSpinner = CreateInLayout<NumericUpDown>(analysisLayout, 1, 2);
            crossSizeSpinner.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            crossSizeSpinner.Maximum = 64;
            crossSizeSpinner.Minimum = 8;
            crossSizeSpinner.Value = 16;
            crossSizeSpinner.ValueChanged += new EventHandler(HandleCrossSizeSpinnerValueChanged);
            
            showExtremesCheck = CreateInLayout<CheckBox>(analysisLayout, 0, 3);
            showExtremesCheck.Anchor = AnchorStyles.Left;
            showExtremesCheck.AutoSize = true;
            showExtremesCheck.Checked = true;
            showExtremesCheck.Text = "Show extremes";
            showExtremesCheck.CheckedChanged += new EventHandler(HandleShowExtremesCheckCheckedChanged);
            SetColumnSpan(showExtremesCheck, 2);

            var maxCountLabel = CreateInLayout<Label>(analysisLayout, 0, 4);
            maxCountLabel.Anchor = AnchorStyles.Left;
            maxCountLabel.Text = "Max count:";
            maxCountLabel.TextAlign = ContentAlignment.MiddleLeft;
            
            maxCountSpinner = CreateInLayout<NumericUpDown>(analysisLayout, 1, 4);
            maxCountSpinner.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            maxCountSpinner.Maximum = 10;
            maxCountSpinner.Minimum = 1;
            maxCountSpinner.Value = 3;
            maxCountSpinner.ValueChanged += new EventHandler(HandleMaxCountSpinnerValueChanged);
            
            var deletePointsButton = CreateInLayout<Button>(analysisLayout, 0, 5);
            deletePointsButton.Dock = DockStyle.Fill;
            deletePointsButton.Text = "Delete all points";
            deletePointsButton.Click += new EventHandler(HandleDeletePointsButtonClick);
            SetColumnSpan(deletePointsButton, 2);

            var outputTab = CreateChild<TabPage>(mainControlTabs, "Output");
            outputTab.Padding = new Padding(3);
            outputTab.UseVisualStyleBackColor = true;
            
            var outputLayout = CreateChild<TableLayoutPanel>(outputTab);
            outputLayout.Dock = DockStyle.Fill;
            outputLayout.Margin = new Padding(0);
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

            var outputPathLayout = CreateInLayout<TableLayoutPanel>(outputLayout, 0, 1);
            outputPathLayout.Dock = DockStyle.Fill;
            outputPathLayout.Margin = new Padding(0);
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
            outputPathButton.Click += new EventHandler(HandleOutputPathButtonClick);

            var autoSaveCheck = CreateInLayout<CheckBox>(outputLayout, 0, 2);
            autoSaveCheck.Anchor = AnchorStyles.Left;
            autoSaveCheck.AutoSize = true;
            autoSaveCheck.Text = "Screenshot on each calibration";
            autoSaveCheck.CheckedChanged += new EventHandler((sender, e) => autoSaveImg = !autoSaveImg);
            SetColumnSpan(autoSaveCheck, 2);

            var screenshotButton = CreateInLayout<Button>(outputLayout, 0, 3);
            screenshotButton.Dock = DockStyle.Fill;
            screenshotButton.Text = "Screenshot";
            screenshotButton.Click += new EventHandler(HandleScreenshotButtonClick);

            recordVideoButton = CreateInLayout<Button>(outputLayout, 1, 3);
            recordVideoButton.Dock = DockStyle.Fill;
            recordVideoButton.Text = "Record video";
            recordVideoButton.Click += new EventHandler(HandleRecordVideoButtonClick);
            
            var debugValueLayout = CreateInLayout<TableLayoutPanel>(mainLayout, 0, 1);
            debugValueLayout.Dock = DockStyle.Fill;
            debugValueLayout.Margin = new Padding(0);
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

            var mainPictureLayout = CreateInLayout<TableLayoutPanel>(mainLayout, 1, 0);
            mainPictureLayout.Dock = DockStyle.Fill;
            mainPictureLayout.Margin = new Padding(0);
            AddColumn(mainPictureLayout, SizeType.Percent, 100f);
            AddColumn(mainPictureLayout, SizeType.Absolute, 60f);
            AddRow(mainPictureLayout, SizeType.Percent, 100f);

            pictureTabs = CreateInLayout<TabControl>(mainPictureLayout, 0, 0);
            pictureTabs.Dock = DockStyle.Fill;
            
            liveTab = CreateChild<TabPage>(pictureTabs, "Live");
            liveTab.Padding = new Padding(3);
            liveTab.UseVisualStyleBackColor = true;
            
            livePicture = CreateChild<AnalyzablePictureBox>(liveTab);
            livePicture.BorderStyle = BorderStyle.FixedSingle;
            livePicture.Size = new Size(32, 32);
            livePicture.SizeMode = PictureBoxSizeMode.StretchImage;
            livePicture.MouseEnter += new EventHandler(HandleAnalyzablePictureMouseEnter);
            livePicture.MouseLeave += new EventHandler(HandleAnalyzablePictureMouseLeave);
            livePicture.MouseMove += new MouseEventHandler(HandleAnalyzablePictureMouseMove);
            
            firstAfterCalTab = CreateChild<TabPage>(pictureTabs, "On calibration");
            firstAfterCalTab.Padding = new Padding(3);
            firstAfterCalTab.UseVisualStyleBackColor = true;
            
            firstAfterCalPicture = CreateChild<AnalyzablePictureBox>(firstAfterCalTab);
            firstAfterCalPicture.BorderStyle = BorderStyle.FixedSingle;
            firstAfterCalPicture.Size = new Size(32, 32);
            firstAfterCalPicture.SizeMode = PictureBoxSizeMode.StretchImage;
            firstAfterCalPicture.MouseEnter += new EventHandler(HandleAnalyzablePictureMouseEnter);
            firstAfterCalPicture.MouseLeave += new EventHandler(HandleAnalyzablePictureMouseLeave);
            firstAfterCalPicture.MouseMove += new MouseEventHandler(HandleAnalyzablePictureMouseMove);
            
            mouseLabel = CreateInLayout<Label>(mainLayout, 1, 1);
            mouseLabel.Dock = DockStyle.Fill;
            mouseLabel.TextAlign = ContentAlignment.MiddleCenter;

            var gaugeLayout = CreateInLayout<TableLayoutPanel>(mainPictureLayout, 1, 0);
            gaugeLayout.Dock = DockStyle.Fill;
            gaugeLayout.Margin = new Padding(0);
            AddColumn(gaugeLayout, SizeType.Percent, 100f);
            AddRow(gaugeLayout, SizeType.Absolute, 25f);
            AddRow(gaugeLayout, SizeType.Percent, 100f);
            AddRow(gaugeLayout, SizeType.Absolute, 25f);
            
            tempGaugePicture = CreateInLayout<PictureBox>(gaugeLayout, 0, 1);
            tempGaugePicture.Dock = DockStyle.Fill;
            tempGaugePicture.SizeMode = PictureBoxSizeMode.StretchImage;
            
            tempGaugeMinLabel = CreateInLayout<Label>(gaugeLayout, 0, 2);
            tempGaugeMinLabel.Dock = DockStyle.Fill;
            tempGaugeMinLabel.TextAlign = ContentAlignment.MiddleCenter;
            
            tempGaugeMaxLabel = CreateInLayout<Label>(gaugeLayout, 0, 0);
            tempGaugeMaxLabel.Dock = DockStyle.Fill;
            tempGaugeMaxLabel.TextAlign = ContentAlignment.MiddleCenter;

            var sliderLayout = CreateInLayout<TableLayoutPanel>(mainLayout, 0, 2);
            sliderLayout.Dock = DockStyle.Fill;
            sliderLayout.Margin = new Padding(0);
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
            minTempSlider.CausesValidation = false;
            minTempSlider.LargeChange = 100;
            minTempSlider.Maximum = 20000;
            minTempSlider.Minimum = 4000;
            minTempSlider.SmallChange = 10;
            minTempSlider.TickFrequency = 100;
            minTempSlider.Value = 4000;
            minTempSlider.Scroll += new EventHandler(HandleMinTempSliderScroll);

            maxTempSlider = CreateInLayout<TrackBar>(sliderLayout, 1, 1);
            maxTempSlider.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            maxTempSlider.CausesValidation = false;
            maxTempSlider.LargeChange = 100;
            maxTempSlider.Maximum = 20000;
            maxTempSlider.Minimum = 4000;
            maxTempSlider.SmallChange = 10;
            maxTempSlider.TickFrequency = 100;
            maxTempSlider.Value = 4000;
            maxTempSlider.Scroll += new EventHandler(HandleMaxTempSliderScroll);
        }
    }
}

