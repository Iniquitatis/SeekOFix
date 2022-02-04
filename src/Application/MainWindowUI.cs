using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SeekOFix
{
    partial class MainWindow
    {
        private IContainer components = null;
        private Button startStopButton;
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
        private Label sliderMinLabel;
        private Label sliderMaxLabel;
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
            FormClosing += new FormClosingEventHandler(MainWindow_FormClosing);
            Load += new System.EventHandler(MainWindow_Load);
            Paint += new PaintEventHandler(MainWindow_Paint);

            components = new Container();

            var startStopToolTip = new ToolTip(components);
            var intCalToolTip = new ToolTip(components);
            var extCalToolTip = new ToolTip(components);

            var mainLayout = new TableLayoutPanel();
            mainLayout.ColumnCount = 3;
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 250f));
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60f));
            mainLayout.Dock = DockStyle.Fill;
            mainLayout.Margin = new Padding(0);
            mainLayout.RowCount = 6;
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 25f));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 25f));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 25f));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 25f));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 25f));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
            Controls.Add(mainLayout);

            var mainControlLayout = new TableLayoutPanel();
            mainControlLayout.ColumnCount = 1;
            mainControlLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            mainControlLayout.Dock = DockStyle.Fill;
            mainControlLayout.Margin = new Padding(0);
            mainControlLayout.RowCount = 2;
            mainControlLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30f));
            mainControlLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
            mainControlLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
            mainLayout.Controls.Add(mainControlLayout, 0, 0);
            mainLayout.SetRowSpan(mainControlLayout, 2);

            var mainControlButtons = new TableLayoutPanel();
            mainControlButtons.ColumnCount = 3;
            mainControlButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333f));
            mainControlButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333f));
            mainControlButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333f));
            mainControlButtons.Dock = DockStyle.Fill;
            mainControlButtons.Margin = new Padding(0);
            mainControlButtons.RowCount = 1;
            mainControlButtons.RowStyles.Add(new RowStyle(SizeType.Absolute, 30f));
            mainControlLayout.Controls.Add(mainControlButtons, 0, 0);

            startStopButton = new Button();
            startStopButton.Dock = DockStyle.Fill;
            startStopButton.Text = "STOP";
            startStopButton.Click += new System.EventHandler(startStopButton_Click);
            startStopToolTip.SetToolTip(startStopButton, "Start/stop streaming");
            mainControlButtons.Controls.Add(startStopButton, 0, 0);

            var intCalButton = new Button();
            intCalButton.Dock = DockStyle.Fill;
            intCalButton.Text = "INT Cal";
            intCalButton.Click += new System.EventHandler((sender, e) => usingExternalCal = false);
            intCalToolTip.SetToolTip(intCalButton, "Do internal calibration");
            mainControlButtons.Controls.Add(intCalButton, 1, 0);

            var extCalButton = new Button();
            extCalButton.Dock = DockStyle.Fill;
            extCalButton.Text = "EXT Cal";
            extCalButton.Click += new System.EventHandler((sender, e) => grabExternalReference = true);
            extCalToolTip.SetToolTip(extCalButton, "Do external calibration");
            mainControlButtons.Controls.Add(extCalButton, 2, 0);

            var mainControlTabs = new TabControl();
            mainControlTabs.Dock = DockStyle.Fill;
            mainControlLayout.Controls.Add(mainControlTabs, 0, 1);

            var appearanceTab = new TabPage("Appearance");
            appearanceTab.Padding = new Padding(3);
            appearanceTab.UseVisualStyleBackColor = true;
            mainControlTabs.Controls.Add(appearanceTab);

            var appearanceLayout = new TableLayoutPanel();
            appearanceLayout.ColumnCount = 2;
            appearanceLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35f));
            appearanceLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65f));
            appearanceLayout.Dock = DockStyle.Fill;
            appearanceLayout.Margin = new Padding(0);
            appearanceLayout.RowCount = 7;
            appearanceLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30f));
            appearanceLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30f));
            appearanceLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30f));
            appearanceLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 100f));
            appearanceLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30f));
            appearanceLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30f));
            appearanceLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30f));
            appearanceTab.Controls.Add(appearanceLayout);

            var paletteLabel = new Label();
            paletteLabel.Anchor = AnchorStyles.Left;
            paletteLabel.Text = "Palette:";
            paletteLabel.TextAlign = ContentAlignment.MiddleLeft;
            appearanceLayout.Controls.Add(paletteLabel, 0, 0);

            paletteCombo = new ComboBox();
            paletteCombo.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            paletteCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            paletteCombo.FormattingEnabled = true;
            paletteCombo.SelectedIndexChanged += new System.EventHandler(paletteCombo_SelectedIndexChanged);
            appearanceLayout.Controls.Add(paletteCombo, 1, 0);

            var unitsLabel = new Label();
            unitsLabel.Anchor = AnchorStyles.Left;
            unitsLabel.Text = "Temp units:";
            unitsLabel.TextAlign = ContentAlignment.MiddleLeft;
            appearanceLayout.Controls.Add(unitsLabel, 0, 1);

            var unitsLayout = new TableLayoutPanel();
            unitsLayout.ColumnCount = 3;
            unitsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333f));
            unitsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333f));
            unitsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333f));
            unitsLayout.Dock = DockStyle.Fill;
            unitsLayout.Margin = new Padding(0);
            unitsLayout.RowCount = 1;
            unitsLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
            appearanceLayout.Controls.Add(unitsLayout, 1, 1);

            unitsKRadio = new RadioButton();
            unitsKRadio.Anchor = AnchorStyles.Left;
            unitsKRadio.AutoSize = true;
            unitsKRadio.Checked = true;
            unitsKRadio.Text = "K";
            unitsKRadio.CheckedChanged += new System.EventHandler(unitRadios_CheckedChanged);
            unitsLayout.Controls.Add(unitsKRadio, 0, 0);

            unitsCRadio = new RadioButton();
            unitsCRadio.Anchor = AnchorStyles.Left;
            unitsCRadio.AutoSize = true;
            unitsCRadio.Text = "°C";
            unitsCRadio.CheckedChanged += new System.EventHandler(unitRadios_CheckedChanged);
            unitsLayout.Controls.Add(unitsCRadio, 1, 0);

            unitsFRadio = new RadioButton();
            unitsFRadio.Anchor = AnchorStyles.Left;
            unitsFRadio.AutoSize = true;
            unitsFRadio.Text = "°F";
            unitsFRadio.CheckedChanged += new System.EventHandler(unitRadios_CheckedChanged);
            unitsLayout.Controls.Add(unitsFRadio, 2, 0);

            applySharpenCheck = new CheckBox();
            applySharpenCheck.Anchor = AnchorStyles.Left;
            applySharpenCheck.AutoSize = true;
            applySharpenCheck.Text = "Apply sharpening filter";
            applySharpenCheck.CheckedChanged += new System.EventHandler((sender, e) => sharpenImage = !sharpenImage);
            appearanceLayout.Controls.Add(applySharpenCheck, 0, 2);
            appearanceLayout.SetColumnSpan(applySharpenCheck, 2);

            histogramPicture = new PictureBox();
            histogramPicture.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            histogramPicture.BackColor = SystemColors.ControlLightLight;
            histogramPicture.BorderStyle = BorderStyle.FixedSingle;
            histogramPicture.SizeMode = PictureBoxSizeMode.StretchImage;
            appearanceLayout.Controls.Add(histogramPicture, 0, 3);
            appearanceLayout.SetColumnSpan(histogramPicture, 2);

            manualRangeSwitchButton = new Button();
            manualRangeSwitchButton.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            manualRangeSwitchButton.Text = "Switch to manual range";
            manualRangeSwitchButton.Click += new System.EventHandler(manualRangeSwitchButton_Click);
            appearanceLayout.Controls.Add(manualRangeSwitchButton, 0, 4);
            appearanceLayout.SetColumnSpan(manualRangeSwitchButton, 2);

            dynSlidersCheck = new CheckBox();
            dynSlidersCheck.Anchor = AnchorStyles.Left;
            dynSlidersCheck.AutoSize = true;
            dynSlidersCheck.Text = "Enable relative sliders";
            dynSlidersCheck.Visible = false;
            dynSlidersCheck.CheckedChanged += new System.EventHandler(dynSlidersCheck_CheckedChanged);
            appearanceLayout.Controls.Add(dynSlidersCheck, 0, 5);
            appearanceLayout.SetColumnSpan(dynSlidersCheck, 2);

            var analysisTab = new TabPage("Analysis");
            analysisTab.Padding = new Padding(3);
            analysisTab.UseVisualStyleBackColor = true;
            mainControlTabs.Controls.Add(analysisTab);

            var analysisLayout = new TableLayoutPanel();
            analysisLayout.ColumnCount = 2;
            analysisLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35f));
            analysisLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65f));
            analysisLayout.Dock = DockStyle.Fill;
            analysisLayout.Margin = new Padding(0);
            analysisLayout.RowCount = 7;
            analysisLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30f));
            analysisLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30f));
            analysisLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30f));
            analysisLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30f));
            analysisLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30f));
            analysisLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30f));
            analysisLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30f));
            analysisTab.Controls.Add(analysisLayout);

            enableAnalysisCheck = new CheckBox();
            enableAnalysisCheck.Anchor = AnchorStyles.Left;
            enableAnalysisCheck.AutoSize = true;
            enableAnalysisCheck.Text = "Enabled";
            enableAnalysisCheck.CheckedChanged += new System.EventHandler(analysisCheck_CheckedChanged);
            analysisLayout.Controls.Add(enableAnalysisCheck, 0, 0);
            analysisLayout.SetColumnSpan(enableAnalysisCheck, 2);

            showTemperatureCheck = new CheckBox();
            showTemperatureCheck.Anchor = AnchorStyles.Left;
            showTemperatureCheck.AutoSize = true;
            showTemperatureCheck.Checked = true;
            showTemperatureCheck.Text = "Show temperature";
            showTemperatureCheck.CheckedChanged += new System.EventHandler(showTemperatureCheck_CheckedChanged);
            analysisLayout.Controls.Add(showTemperatureCheck, 0, 1);
            analysisLayout.SetColumnSpan(showTemperatureCheck, 2);

            var crossSizeLabel = new Label();
            crossSizeLabel.Anchor = AnchorStyles.Left;
            crossSizeLabel.Text = "Cross size:";
            crossSizeLabel.TextAlign = ContentAlignment.MiddleLeft;
            analysisLayout.Controls.Add(crossSizeLabel, 0, 2);

            crossSizeSpinner = new NumericUpDown();
            crossSizeSpinner.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            crossSizeSpinner.Maximum = 64;
            crossSizeSpinner.Minimum = 8;
            crossSizeSpinner.Value = 16;
            crossSizeSpinner.ValueChanged += new System.EventHandler(crossSizeSpinner_ValueChanged);
            analysisLayout.Controls.Add(crossSizeSpinner, 1, 2);

            showExtremesCheck = new CheckBox();
            showExtremesCheck.Anchor = AnchorStyles.Left;
            showExtremesCheck.AutoSize = true;
            showExtremesCheck.Checked = true;
            showExtremesCheck.Text = "Show extremes";
            showExtremesCheck.CheckedChanged += new System.EventHandler(showExtremesCheck_CheckedChanged);
            analysisLayout.Controls.Add(showExtremesCheck, 0, 3);
            analysisLayout.SetColumnSpan(showExtremesCheck, 2);

            var maxCountLabel = new Label();
            maxCountLabel.Anchor = AnchorStyles.Left;
            maxCountLabel.Text = "Max count:";
            maxCountLabel.TextAlign = ContentAlignment.MiddleLeft;
            analysisLayout.Controls.Add(maxCountLabel, 0, 4);

            maxCountSpinner = new NumericUpDown();
            maxCountSpinner.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            maxCountSpinner.Maximum = 10;
            maxCountSpinner.Minimum = 1;
            maxCountSpinner.Value = 3;
            maxCountSpinner.ValueChanged += new System.EventHandler(maxCountSpinner_ValueChanged);
            analysisLayout.Controls.Add(maxCountSpinner, 1, 4);

            var deletePointsButton = new Button();
            deletePointsButton.Dock = DockStyle.Fill;
            deletePointsButton.Text = "Delete all points";
            deletePointsButton.Click += new System.EventHandler(deletePointsButton_Click);
            analysisLayout.Controls.Add(deletePointsButton, 0, 5);
            analysisLayout.SetColumnSpan(deletePointsButton, 2);

            var outputTab = new TabPage("Output");
            outputTab.Padding = new Padding(3);
            outputTab.UseVisualStyleBackColor = true;
            mainControlTabs.Controls.Add(outputTab);

            var outputLayout = new TableLayoutPanel();
            outputLayout.ColumnCount = 2;
            outputLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
            outputLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
            outputLayout.Dock = DockStyle.Fill;
            outputLayout.Margin = new Padding(0);
            outputLayout.RowCount = 5;
            outputLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 25f));
            outputLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30f));
            outputLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30f));
            outputLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30f));
            outputLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30f));
            outputTab.Controls.Add(outputLayout);

            var outputPathLabel = new Label();
            outputPathLabel.Anchor = AnchorStyles.Left;
            outputPathLabel.Text = "Output path:";
            outputPathLabel.TextAlign = ContentAlignment.MiddleLeft;
            outputLayout.Controls.Add(outputPathLabel, 0, 0);
            outputLayout.SetColumnSpan(outputPathLabel, 2);

            var outputPathLayout = new TableLayoutPanel();
            outputPathLayout.ColumnCount = 2;
            outputPathLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            outputPathLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 30f));
            outputPathLayout.Dock = DockStyle.Fill;
            outputPathLayout.Margin = new Padding(0);
            outputPathLayout.RowCount = 1;
            outputPathLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
            outputLayout.Controls.Add(outputPathLayout, 0, 1);
            outputLayout.SetColumnSpan(outputPathLayout, 2);

            outputPathField = new TextBox();
            outputPathField.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            outputPathField.MaxLength = 256;
            outputPathLayout.Controls.Add(outputPathField, 0, 0);

            var outputPathButton = new Button();
            outputPathButton.Dock = DockStyle.Fill;
            outputPathButton.Text = "...";
            outputPathButton.Click += new System.EventHandler(outputPathButton_Click);
            outputPathLayout.Controls.Add(outputPathButton, 1, 0);

            var autoSaveCheck = new CheckBox();
            autoSaveCheck.Anchor = AnchorStyles.Left;
            autoSaveCheck.AutoSize = true;
            autoSaveCheck.Text = "Screenshot on each calibration";
            autoSaveCheck.CheckedChanged += new System.EventHandler((sender, e) => autoSaveImg = !autoSaveImg);
            outputLayout.Controls.Add(autoSaveCheck, 0, 2);
            outputLayout.SetColumnSpan(autoSaveCheck, 2);

            var screenshotButton = new Button();
            screenshotButton.Dock = DockStyle.Fill;
            screenshotButton.Text = "Screenshot";
            screenshotButton.Click += new System.EventHandler(screenshotButton_Click);
            outputLayout.Controls.Add(screenshotButton, 0, 3);

            recordVideoButton = new Button();
            recordVideoButton.Dock = DockStyle.Fill;
            recordVideoButton.Text = "Record video";
            recordVideoButton.Click += new System.EventHandler(recordVideoButton_Click);
            outputLayout.Controls.Add(recordVideoButton, 1, 3);

            var debugValueLayout = new TableLayoutPanel();
            debugValueLayout.ColumnCount = 3;
            debugValueLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333f));
            debugValueLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333f));
            debugValueLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333f));
            debugValueLayout.Dock = DockStyle.Fill;
            debugValueLayout.Margin = new Padding(0);
            debugValueLayout.RowCount = 1;
            debugValueLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
            mainLayout.Controls.Add(debugValueLayout, 0, 2);

            gModeLeftLabel = new Label();
            gModeLeftLabel.Anchor = AnchorStyles.None;
            gModeLeftLabel.TextAlign = ContentAlignment.MiddleCenter;
            debugValueLayout.Controls.Add(gModeLeftLabel, 0, 0);

            gModeRightLabel = new Label();
            gModeRightLabel.Anchor = AnchorStyles.None;
            gModeRightLabel.TextAlign = ContentAlignment.MiddleCenter;
            debugValueLayout.Controls.Add(gModeRightLabel, 1, 0);

            maxTempRawLabel = new Label();
            maxTempRawLabel.Anchor = AnchorStyles.None;
            maxTempRawLabel.TextAlign = ContentAlignment.MiddleCenter;
            debugValueLayout.Controls.Add(maxTempRawLabel, 2, 0);

            pictureTabs = new TabControl();
            pictureTabs.Dock = DockStyle.Fill;
            mainLayout.Controls.Add(pictureTabs, 1, 0);
            mainLayout.SetRowSpan(pictureTabs, 2);

            liveTab = new TabPage("Live");
            liveTab.Padding = new Padding(3);
            liveTab.UseVisualStyleBackColor = true;
            pictureTabs.Controls.Add(liveTab);

            livePicture = new AnalyzablePictureBox();
            livePicture.BorderStyle = BorderStyle.FixedSingle;
            livePicture.Size = new Size(32, 32);
            livePicture.SizeMode = PictureBoxSizeMode.StretchImage;
            livePicture.MouseEnter += new System.EventHandler(analyzablePicture_MouseEnter);
            livePicture.MouseLeave += new System.EventHandler(analyzablePicture_MouseLeave);
            livePicture.MouseMove += new MouseEventHandler(analyzablePicture_MouseMove);
            liveTab.Controls.Add(livePicture);

            firstAfterCalTab = new TabPage("On calibration");
            firstAfterCalTab.Padding = new Padding(3);
            firstAfterCalTab.UseVisualStyleBackColor = true;
            pictureTabs.Controls.Add(firstAfterCalTab);

            firstAfterCalPicture = new AnalyzablePictureBox();
            firstAfterCalPicture.BorderStyle = BorderStyle.FixedSingle;
            firstAfterCalPicture.Size = new Size(32, 32);
            firstAfterCalPicture.SizeMode = PictureBoxSizeMode.StretchImage;
            firstAfterCalPicture.MouseEnter += new System.EventHandler(analyzablePicture_MouseEnter);
            firstAfterCalPicture.MouseLeave += new System.EventHandler(analyzablePicture_MouseLeave);
            firstAfterCalPicture.MouseMove += new MouseEventHandler(analyzablePicture_MouseMove);
            firstAfterCalTab.Controls.Add(firstAfterCalPicture);

            mouseLabel = new Label();
            mouseLabel.Dock = DockStyle.Fill;
            mouseLabel.TextAlign = ContentAlignment.MiddleCenter;
            mainLayout.Controls.Add(mouseLabel, 1, 2);

            tempGaugePicture = new PictureBox();
            tempGaugePicture.Dock = DockStyle.Fill;
            tempGaugePicture.SizeMode = PictureBoxSizeMode.StretchImage;
            mainLayout.Controls.Add(tempGaugePicture, 2, 1);

            tempGaugeMinLabel = new Label();
            tempGaugeMinLabel.Dock = DockStyle.Fill;
            tempGaugeMinLabel.TextAlign = ContentAlignment.MiddleCenter;
            mainLayout.Controls.Add(tempGaugeMinLabel, 2, 2);

            tempGaugeMaxLabel = new Label();
            tempGaugeMaxLabel.Dock = DockStyle.Fill;
            tempGaugeMaxLabel.TextAlign = ContentAlignment.MiddleCenter;
            mainLayout.Controls.Add(tempGaugeMaxLabel, 2, 0);

            sliderMinLabel = new Label();
            sliderMinLabel.Dock = DockStyle.Fill;
            sliderMinLabel.TextAlign = ContentAlignment.MiddleCenter;
            mainLayout.Controls.Add(sliderMinLabel, 2, 3);

            sliderMaxLabel = new Label();
            sliderMaxLabel.Dock = DockStyle.Fill;
            sliderMaxLabel.TextAlign = ContentAlignment.MiddleCenter;
            mainLayout.Controls.Add(sliderMaxLabel, 2, 5);

            minTempSlider = new TrackBar();
            minTempSlider.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            minTempSlider.CausesValidation = false;
            minTempSlider.LargeChange = 100;
            minTempSlider.Maximum = 20000;
            minTempSlider.Minimum = 4000;
            minTempSlider.SmallChange = 10;
            minTempSlider.TickFrequency = 100;
            minTempSlider.Value = 4000;
            minTempSlider.Scroll += new System.EventHandler(minTempSlider_Scroll);
            mainLayout.Controls.Add(minTempSlider, 0, 3);
            mainLayout.SetColumnSpan(minTempSlider, 2);

            maxTempSlider = new TrackBar();
            maxTempSlider.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            maxTempSlider.CausesValidation = false;
            maxTempSlider.LargeChange = 100;
            maxTempSlider.Maximum = 20000;
            maxTempSlider.Minimum = 4000;
            maxTempSlider.SmallChange = 10;
            maxTempSlider.TickFrequency = 100;
            maxTempSlider.Value = 4000;
            maxTempSlider.Scroll += new System.EventHandler(maxTempSlider_Scroll);
            mainLayout.Controls.Add(maxTempSlider, 0, 5);
            mainLayout.SetColumnSpan(maxTempSlider, 2);
        }
    }
}

