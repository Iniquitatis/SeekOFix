namespace TestSeek
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.extCalButton = new System.Windows.Forms.Button();
            this.maxTempSlider = new System.Windows.Forms.TrackBar();
            this.minTempSlider = new System.Windows.Forms.TrackBar();
            this.manualRangeSwitchButton = new System.Windows.Forms.Button();
            this.autoSaveCheck = new System.Windows.Forms.CheckBox();
            this.dynSlidersCheck = new System.Windows.Forms.CheckBox();
            this.applySharpenCheck = new System.Windows.Forms.CheckBox();
            this.sliderMinLabel = new System.Windows.Forms.Label();
            this.sliderMaxLabel = new System.Windows.Forms.Label();
            this.startStopButton = new System.Windows.Forms.Button();
            this.startStopToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.extCalToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.intCalButton = new System.Windows.Forms.Button();
            this.unitsLabel = new System.Windows.Forms.Label();
            this.unitsFRadio = new System.Windows.Forms.RadioButton();
            this.histogramPicture = new System.Windows.Forms.PictureBox();
            this.unitsKRadio = new System.Windows.Forms.RadioButton();
            this.unitsCRadio = new System.Windows.Forms.RadioButton();
            this.paletteCombo = new System.Windows.Forms.ComboBox();
            this.paletteLabel = new System.Windows.Forms.Label();
            this.gModeLeftLabel = new System.Windows.Forms.Label();
            this.gModeRightLabel = new System.Windows.Forms.Label();
            this.temperatureGaugePicture = new System.Windows.Forms.PictureBox();
            this.maxTempRawLabel = new System.Windows.Forms.Label();
            this.maxTempLabel = new System.Windows.Forms.Label();
            this.minTempLabel = new System.Windows.Forms.Label();
            this.mainLayout = new System.Windows.Forms.TableLayoutPanel();
            this.mainControlLayout = new System.Windows.Forms.TableLayoutPanel();
            this.mainControlButtons = new System.Windows.Forms.TableLayoutPanel();
            this.mainControlTabs = new System.Windows.Forms.TabControl();
            this.appearanceTab = new System.Windows.Forms.TabPage();
            this.appearanceLayout = new System.Windows.Forms.TableLayoutPanel();
            this.unitsLayout = new System.Windows.Forms.TableLayoutPanel();
            this.analysisTab = new System.Windows.Forms.TabPage();
            this.analysisLayout = new System.Windows.Forms.TableLayoutPanel();
            this.enableAnalysisCheck = new System.Windows.Forms.CheckBox();
            this.showTemperatureCheck = new System.Windows.Forms.CheckBox();
            this.crossSizeLabel = new System.Windows.Forms.Label();
            this.crossSizeSpinner = new System.Windows.Forms.NumericUpDown();
            this.showExtremesCheck = new System.Windows.Forms.CheckBox();
            this.maxCountLabel = new System.Windows.Forms.Label();
            this.maxCountSpinner = new System.Windows.Forms.NumericUpDown();
            this.outputTab = new System.Windows.Forms.TabPage();
            this.outputLayout = new System.Windows.Forms.TableLayoutPanel();
            this.outputPathLabel = new System.Windows.Forms.Label();
            this.outputPathLayout = new System.Windows.Forms.TableLayoutPanel();
            this.outputPathField = new System.Windows.Forms.TextBox();
            this.outputPathButton = new System.Windows.Forms.Button();
            this.screenshotButton = new System.Windows.Forms.Button();
            this.recordVideoButton = new System.Windows.Forms.Button();
            this.pictureTabs = new System.Windows.Forms.TabControl();
            this.liveTab = new System.Windows.Forms.TabPage();
            this.firstAfterCalTab = new System.Windows.Forms.TabPage();
            this.debugValueLayout = new System.Windows.Forms.TableLayoutPanel();
            this.mouseLabel = new System.Windows.Forms.Label();
            this.intCalToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.deletePointsButton = new System.Windows.Forms.Button();
            this.livePicture = new TestSeek.AnalyzablePictureBox();
            this.firstAfterCalPicture = new TestSeek.AnalyzablePictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.maxTempSlider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minTempSlider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.histogramPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.temperatureGaugePicture)).BeginInit();
            this.mainLayout.SuspendLayout();
            this.mainControlLayout.SuspendLayout();
            this.mainControlButtons.SuspendLayout();
            this.mainControlTabs.SuspendLayout();
            this.appearanceTab.SuspendLayout();
            this.appearanceLayout.SuspendLayout();
            this.unitsLayout.SuspendLayout();
            this.analysisTab.SuspendLayout();
            this.analysisLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.crossSizeSpinner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxCountSpinner)).BeginInit();
            this.outputTab.SuspendLayout();
            this.outputLayout.SuspendLayout();
            this.outputPathLayout.SuspendLayout();
            this.pictureTabs.SuspendLayout();
            this.liveTab.SuspendLayout();
            this.firstAfterCalTab.SuspendLayout();
            this.debugValueLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.livePicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.firstAfterCalPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // extCalButton
            // 
            this.extCalButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.extCalButton.Location = new System.Drawing.Point(169, 3);
            this.extCalButton.Name = "extCalButton";
            this.extCalButton.Size = new System.Drawing.Size(78, 24);
            this.extCalButton.TabIndex = 0;
            this.extCalButton.Text = "EXT Cal";
            this.extCalToolTip.SetToolTip(this.extCalButton, "Do external calibration");
            this.extCalButton.UseVisualStyleBackColor = true;
            this.extCalButton.Click += new System.EventHandler(this.extCalButton_Click);
            // 
            // maxTempSlider
            // 
            this.maxTempSlider.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.maxTempSlider.CausesValidation = false;
            this.mainLayout.SetColumnSpan(this.maxTempSlider, 2);
            this.maxTempSlider.LargeChange = 100;
            this.maxTempSlider.Location = new System.Drawing.Point(3, 896);
            this.maxTempSlider.Maximum = 20000;
            this.maxTempSlider.Minimum = 4000;
            this.maxTempSlider.Name = "maxTempSlider";
            this.maxTempSlider.Size = new System.Drawing.Size(1159, 19);
            this.maxTempSlider.SmallChange = 10;
            this.maxTempSlider.TabIndex = 11;
            this.maxTempSlider.TickFrequency = 100;
            this.maxTempSlider.Value = 4000;
            this.maxTempSlider.Scroll += new System.EventHandler(this.maxTempSlider_Scroll);
            // 
            // minTempSlider
            // 
            this.minTempSlider.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.minTempSlider.CausesValidation = false;
            this.mainLayout.SetColumnSpan(this.minTempSlider, 2);
            this.minTempSlider.LargeChange = 100;
            this.minTempSlider.Location = new System.Drawing.Point(3, 846);
            this.minTempSlider.Maximum = 20000;
            this.minTempSlider.Minimum = 4000;
            this.minTempSlider.Name = "minTempSlider";
            this.minTempSlider.Size = new System.Drawing.Size(1159, 19);
            this.minTempSlider.SmallChange = 10;
            this.minTempSlider.TabIndex = 12;
            this.minTempSlider.TickFrequency = 100;
            this.minTempSlider.Value = 4000;
            this.minTempSlider.Scroll += new System.EventHandler(this.minTempSlider_Scroll);
            // 
            // manualRangeSwitchButton
            // 
            this.manualRangeSwitchButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.appearanceLayout.SetColumnSpan(this.manualRangeSwitchButton, 2);
            this.manualRangeSwitchButton.Location = new System.Drawing.Point(3, 193);
            this.manualRangeSwitchButton.Name = "manualRangeSwitchButton";
            this.manualRangeSwitchButton.Size = new System.Drawing.Size(224, 24);
            this.manualRangeSwitchButton.TabIndex = 13;
            this.manualRangeSwitchButton.Text = "Switch to manual range";
            this.manualRangeSwitchButton.UseVisualStyleBackColor = true;
            this.manualRangeSwitchButton.Click += new System.EventHandler(this.manualRangeSwitchButton_Click);
            // 
            // autoSaveCheck
            // 
            this.autoSaveCheck.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.autoSaveCheck.AutoSize = true;
            this.outputLayout.SetColumnSpan(this.autoSaveCheck, 2);
            this.autoSaveCheck.Location = new System.Drawing.Point(3, 61);
            this.autoSaveCheck.Name = "autoSaveCheck";
            this.autoSaveCheck.Size = new System.Drawing.Size(173, 17);
            this.autoSaveCheck.TabIndex = 14;
            this.autoSaveCheck.Text = "Screenshot on each calibration";
            this.autoSaveCheck.UseVisualStyleBackColor = true;
            this.autoSaveCheck.CheckedChanged += new System.EventHandler(this.autoSaveCheck_CheckedChanged);
            // 
            // dynSlidersCheck
            // 
            this.dynSlidersCheck.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.dynSlidersCheck.AutoSize = true;
            this.appearanceLayout.SetColumnSpan(this.dynSlidersCheck, 2);
            this.dynSlidersCheck.Location = new System.Drawing.Point(3, 226);
            this.dynSlidersCheck.Name = "dynSlidersCheck";
            this.dynSlidersCheck.Size = new System.Drawing.Size(128, 17);
            this.dynSlidersCheck.TabIndex = 16;
            this.dynSlidersCheck.Text = "Enable relative sliders";
            this.dynSlidersCheck.UseVisualStyleBackColor = true;
            this.dynSlidersCheck.Visible = false;
            this.dynSlidersCheck.CheckedChanged += new System.EventHandler(this.dynSlidersCheck_CheckedChanged);
            // 
            // applySharpenCheck
            // 
            this.applySharpenCheck.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.applySharpenCheck.AutoSize = true;
            this.appearanceLayout.SetColumnSpan(this.applySharpenCheck, 2);
            this.applySharpenCheck.Location = new System.Drawing.Point(3, 66);
            this.applySharpenCheck.Name = "applySharpenCheck";
            this.applySharpenCheck.Size = new System.Drawing.Size(129, 17);
            this.applySharpenCheck.TabIndex = 20;
            this.applySharpenCheck.Text = "Apply sharpening filter";
            this.applySharpenCheck.UseVisualStyleBackColor = true;
            this.applySharpenCheck.CheckedChanged += new System.EventHandler(this.applySharpenCheck_CheckedChanged);
            // 
            // sliderMinLabel
            // 
            this.sliderMinLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sliderMinLabel.Location = new System.Drawing.Point(1168, 843);
            this.sliderMinLabel.Name = "sliderMinLabel";
            this.sliderMinLabel.Size = new System.Drawing.Size(54, 25);
            this.sliderMinLabel.TabIndex = 22;
            this.sliderMinLabel.Text = "MIN";
            this.sliderMinLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // sliderMaxLabel
            // 
            this.sliderMaxLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sliderMaxLabel.Location = new System.Drawing.Point(1168, 893);
            this.sliderMaxLabel.Name = "sliderMaxLabel";
            this.sliderMaxLabel.Size = new System.Drawing.Size(54, 25);
            this.sliderMaxLabel.TabIndex = 23;
            this.sliderMaxLabel.Text = "MAX";
            this.sliderMaxLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // startStopButton
            // 
            this.startStopButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.startStopButton.Location = new System.Drawing.Point(3, 3);
            this.startStopButton.Name = "startStopButton";
            this.startStopButton.Size = new System.Drawing.Size(77, 24);
            this.startStopButton.TabIndex = 27;
            this.startStopButton.Text = "STOP";
            this.startStopToolTip.SetToolTip(this.startStopButton, "Start/stop streaming");
            this.startStopButton.UseVisualStyleBackColor = true;
            this.startStopButton.Click += new System.EventHandler(this.startStopButton_Click);
            // 
            // intCalButton
            // 
            this.intCalButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.intCalButton.Location = new System.Drawing.Point(86, 3);
            this.intCalButton.Name = "intCalButton";
            this.intCalButton.Size = new System.Drawing.Size(77, 24);
            this.intCalButton.TabIndex = 43;
            this.intCalButton.Text = "INT Cal";
            this.intCalToolTip.SetToolTip(this.intCalButton, "Do internal calibration");
            this.intCalButton.UseVisualStyleBackColor = true;
            this.intCalButton.Click += new System.EventHandler(this.intCalButton_Click);
            // 
            // unitsLabel
            // 
            this.unitsLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.unitsLabel.AutoSize = true;
            this.unitsLabel.Location = new System.Drawing.Point(3, 38);
            this.unitsLabel.Name = "unitsLabel";
            this.unitsLabel.Size = new System.Drawing.Size(62, 13);
            this.unitsLabel.TabIndex = 33;
            this.unitsLabel.Text = "Temp units:";
            this.unitsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // unitsFRadio
            // 
            this.unitsFRadio.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.unitsFRadio.AutoSize = true;
            this.unitsFRadio.Location = new System.Drawing.Point(101, 6);
            this.unitsFRadio.Name = "unitsFRadio";
            this.unitsFRadio.Size = new System.Drawing.Size(35, 17);
            this.unitsFRadio.TabIndex = 26;
            this.unitsFRadio.Text = "°F";
            this.unitsFRadio.UseVisualStyleBackColor = true;
            this.unitsFRadio.CheckedChanged += new System.EventHandler(this.unitRadios_CheckedChanged);
            // 
            // histogramPicture
            // 
            this.histogramPicture.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.histogramPicture.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.histogramPicture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.appearanceLayout.SetColumnSpan(this.histogramPicture, 2);
            this.histogramPicture.Location = new System.Drawing.Point(3, 93);
            this.histogramPicture.Name = "histogramPicture";
            this.histogramPicture.Size = new System.Drawing.Size(224, 94);
            this.histogramPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.histogramPicture.TabIndex = 40;
            this.histogramPicture.TabStop = false;
            // 
            // unitsKRadio
            // 
            this.unitsKRadio.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.unitsKRadio.AutoSize = true;
            this.unitsKRadio.Checked = true;
            this.unitsKRadio.Location = new System.Drawing.Point(3, 6);
            this.unitsKRadio.Name = "unitsKRadio";
            this.unitsKRadio.Size = new System.Drawing.Size(32, 17);
            this.unitsKRadio.TabIndex = 32;
            this.unitsKRadio.TabStop = true;
            this.unitsKRadio.Text = "K";
            this.unitsKRadio.UseVisualStyleBackColor = true;
            this.unitsKRadio.CheckedChanged += new System.EventHandler(this.unitRadios_CheckedChanged);
            // 
            // unitsCRadio
            // 
            this.unitsCRadio.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.unitsCRadio.AutoSize = true;
            this.unitsCRadio.Location = new System.Drawing.Point(52, 6);
            this.unitsCRadio.Name = "unitsCRadio";
            this.unitsCRadio.Size = new System.Drawing.Size(36, 17);
            this.unitsCRadio.TabIndex = 25;
            this.unitsCRadio.Text = "°C";
            this.unitsCRadio.UseVisualStyleBackColor = true;
            this.unitsCRadio.CheckedChanged += new System.EventHandler(this.unitRadios_CheckedChanged);
            // 
            // paletteCombo
            // 
            this.paletteCombo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.paletteCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.paletteCombo.FormattingEnabled = true;
            this.paletteCombo.Location = new System.Drawing.Point(83, 4);
            this.paletteCombo.Name = "paletteCombo";
            this.paletteCombo.Size = new System.Drawing.Size(144, 21);
            this.paletteCombo.TabIndex = 31;
            this.paletteCombo.SelectedIndexChanged += new System.EventHandler(this.paletteCombo_SelectedIndexChanged);
            // 
            // paletteLabel
            // 
            this.paletteLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.paletteLabel.AutoSize = true;
            this.paletteLabel.Location = new System.Drawing.Point(3, 8);
            this.paletteLabel.Name = "paletteLabel";
            this.paletteLabel.Size = new System.Drawing.Size(43, 13);
            this.paletteLabel.TabIndex = 30;
            this.paletteLabel.Text = "Palette:";
            this.paletteLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // gModeLeftLabel
            // 
            this.gModeLeftLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.gModeLeftLabel.AutoSize = true;
            this.gModeLeftLabel.Location = new System.Drawing.Point(12, 6);
            this.gModeLeftLabel.Name = "gModeLeftLabel";
            this.gModeLeftLabel.Size = new System.Drawing.Size(58, 13);
            this.gModeLeftLabel.TabIndex = 35;
            this.gModeLeftLabel.Text = "gModeLeft";
            this.gModeLeftLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gModeRightLabel
            // 
            this.gModeRightLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.gModeRightLabel.AutoSize = true;
            this.gModeRightLabel.Location = new System.Drawing.Point(92, 6);
            this.gModeRightLabel.Name = "gModeRightLabel";
            this.gModeRightLabel.Size = new System.Drawing.Size(65, 13);
            this.gModeRightLabel.TabIndex = 36;
            this.gModeRightLabel.Text = "gModeRight";
            this.gModeRightLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // temperatureGaugePicture
            // 
            this.temperatureGaugePicture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.temperatureGaugePicture.Location = new System.Drawing.Point(1168, 28);
            this.temperatureGaugePicture.Name = "temperatureGaugePicture";
            this.temperatureGaugePicture.Size = new System.Drawing.Size(54, 787);
            this.temperatureGaugePicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.temperatureGaugePicture.TabIndex = 37;
            this.temperatureGaugePicture.TabStop = false;
            // 
            // maxTempRawLabel
            // 
            this.maxTempRawLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.maxTempRawLabel.AutoSize = true;
            this.maxTempRawLabel.Location = new System.Drawing.Point(170, 6);
            this.maxTempRawLabel.Name = "maxTempRawLabel";
            this.maxTempRawLabel.Size = new System.Drawing.Size(75, 13);
            this.maxTempRawLabel.TabIndex = 39;
            this.maxTempRawLabel.Text = "maxTempRaw";
            this.maxTempRawLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // maxTempLabel
            // 
            this.maxTempLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.maxTempLabel.Location = new System.Drawing.Point(1168, 0);
            this.maxTempLabel.Name = "maxTempLabel";
            this.maxTempLabel.Size = new System.Drawing.Size(54, 25);
            this.maxTempLabel.TabIndex = 41;
            this.maxTempLabel.Text = "30.5";
            this.maxTempLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // minTempLabel
            // 
            this.minTempLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.minTempLabel.Location = new System.Drawing.Point(1168, 818);
            this.minTempLabel.Name = "minTempLabel";
            this.minTempLabel.Size = new System.Drawing.Size(54, 25);
            this.minTempLabel.TabIndex = 42;
            this.minTempLabel.Text = "20.5";
            this.minTempLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // mainLayout
            // 
            this.mainLayout.ColumnCount = 3;
            this.mainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250F));
            this.mainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.mainLayout.Controls.Add(this.mainControlLayout, 0, 0);
            this.mainLayout.Controls.Add(this.minTempSlider, 0, 3);
            this.mainLayout.Controls.Add(this.maxTempSlider, 0, 5);
            this.mainLayout.Controls.Add(this.pictureTabs, 1, 0);
            this.mainLayout.Controls.Add(this.temperatureGaugePicture, 2, 1);
            this.mainLayout.Controls.Add(this.minTempLabel, 2, 2);
            this.mainLayout.Controls.Add(this.maxTempLabel, 2, 0);
            this.mainLayout.Controls.Add(this.debugValueLayout, 0, 2);
            this.mainLayout.Controls.Add(this.sliderMaxLabel, 2, 5);
            this.mainLayout.Controls.Add(this.sliderMinLabel, 2, 3);
            this.mainLayout.Controls.Add(this.mouseLabel, 1, 2);
            this.mainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainLayout.Location = new System.Drawing.Point(5, 5);
            this.mainLayout.Margin = new System.Windows.Forms.Padding(0);
            this.mainLayout.Name = "mainLayout";
            this.mainLayout.RowCount = 6;
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.mainLayout.Size = new System.Drawing.Size(1225, 918);
            this.mainLayout.TabIndex = 43;
            // 
            // mainControlLayout
            // 
            this.mainControlLayout.ColumnCount = 1;
            this.mainControlLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainControlLayout.Controls.Add(this.mainControlButtons, 0, 0);
            this.mainControlLayout.Controls.Add(this.mainControlTabs, 0, 1);
            this.mainControlLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainControlLayout.Location = new System.Drawing.Point(0, 0);
            this.mainControlLayout.Margin = new System.Windows.Forms.Padding(0);
            this.mainControlLayout.Name = "mainControlLayout";
            this.mainControlLayout.RowCount = 2;
            this.mainLayout.SetRowSpan(this.mainControlLayout, 2);
            this.mainControlLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.mainControlLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainControlLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.mainControlLayout.Size = new System.Drawing.Size(250, 818);
            this.mainControlLayout.TabIndex = 49;
            // 
            // mainControlButtons
            // 
            this.mainControlButtons.AutoSize = true;
            this.mainControlButtons.ColumnCount = 3;
            this.mainControlButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33332F));
            this.mainControlButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.mainControlButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.mainControlButtons.Controls.Add(this.startStopButton, 0, 0);
            this.mainControlButtons.Controls.Add(this.intCalButton, 1, 0);
            this.mainControlButtons.Controls.Add(this.extCalButton, 2, 0);
            this.mainControlButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainControlButtons.Location = new System.Drawing.Point(0, 0);
            this.mainControlButtons.Margin = new System.Windows.Forms.Padding(0);
            this.mainControlButtons.Name = "mainControlButtons";
            this.mainControlButtons.RowCount = 1;
            this.mainControlButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.mainControlButtons.Size = new System.Drawing.Size(250, 30);
            this.mainControlButtons.TabIndex = 43;
            // 
            // mainControlTabs
            // 
            this.mainControlTabs.Controls.Add(this.appearanceTab);
            this.mainControlTabs.Controls.Add(this.analysisTab);
            this.mainControlTabs.Controls.Add(this.outputTab);
            this.mainControlTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainControlTabs.Location = new System.Drawing.Point(3, 33);
            this.mainControlTabs.Name = "mainControlTabs";
            this.mainControlTabs.SelectedIndex = 0;
            this.mainControlTabs.Size = new System.Drawing.Size(244, 782);
            this.mainControlTabs.TabIndex = 48;
            // 
            // appearanceTab
            // 
            this.appearanceTab.Controls.Add(this.appearanceLayout);
            this.appearanceTab.Location = new System.Drawing.Point(4, 22);
            this.appearanceTab.Name = "appearanceTab";
            this.appearanceTab.Padding = new System.Windows.Forms.Padding(3);
            this.appearanceTab.Size = new System.Drawing.Size(236, 756);
            this.appearanceTab.TabIndex = 0;
            this.appearanceTab.Text = "Appearance";
            this.appearanceTab.UseVisualStyleBackColor = true;
            // 
            // appearanceLayout
            // 
            this.appearanceLayout.ColumnCount = 2;
            this.appearanceLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.appearanceLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.appearanceLayout.Controls.Add(this.paletteLabel, 0, 0);
            this.appearanceLayout.Controls.Add(this.paletteCombo, 1, 0);
            this.appearanceLayout.Controls.Add(this.unitsLabel, 0, 1);
            this.appearanceLayout.Controls.Add(this.unitsLayout, 1, 1);
            this.appearanceLayout.Controls.Add(this.applySharpenCheck, 0, 2);
            this.appearanceLayout.Controls.Add(this.histogramPicture, 0, 3);
            this.appearanceLayout.Controls.Add(this.manualRangeSwitchButton, 0, 4);
            this.appearanceLayout.Controls.Add(this.dynSlidersCheck, 0, 5);
            this.appearanceLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.appearanceLayout.Location = new System.Drawing.Point(3, 3);
            this.appearanceLayout.Margin = new System.Windows.Forms.Padding(0);
            this.appearanceLayout.Name = "appearanceLayout";
            this.appearanceLayout.RowCount = 7;
            this.appearanceLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.appearanceLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.appearanceLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.appearanceLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.appearanceLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.appearanceLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.appearanceLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.appearanceLayout.Size = new System.Drawing.Size(230, 750);
            this.appearanceLayout.TabIndex = 0;
            // 
            // unitsLayout
            // 
            this.unitsLayout.ColumnCount = 3;
            this.unitsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.unitsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.unitsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.unitsLayout.Controls.Add(this.unitsKRadio, 0, 0);
            this.unitsLayout.Controls.Add(this.unitsCRadio, 1, 0);
            this.unitsLayout.Controls.Add(this.unitsFRadio, 2, 0);
            this.unitsLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.unitsLayout.Location = new System.Drawing.Point(80, 30);
            this.unitsLayout.Margin = new System.Windows.Forms.Padding(0);
            this.unitsLayout.Name = "unitsLayout";
            this.unitsLayout.RowCount = 1;
            this.unitsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.unitsLayout.Size = new System.Drawing.Size(150, 30);
            this.unitsLayout.TabIndex = 44;
            // 
            // analysisTab
            // 
            this.analysisTab.Controls.Add(this.analysisLayout);
            this.analysisTab.Location = new System.Drawing.Point(4, 22);
            this.analysisTab.Name = "analysisTab";
            this.analysisTab.Padding = new System.Windows.Forms.Padding(3);
            this.analysisTab.Size = new System.Drawing.Size(236, 756);
            this.analysisTab.TabIndex = 1;
            this.analysisTab.Text = "Analysis";
            this.analysisTab.UseVisualStyleBackColor = true;
            // 
            // analysisLayout
            // 
            this.analysisLayout.ColumnCount = 2;
            this.analysisLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.analysisLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.analysisLayout.Controls.Add(this.enableAnalysisCheck, 0, 0);
            this.analysisLayout.Controls.Add(this.showTemperatureCheck, 0, 1);
            this.analysisLayout.Controls.Add(this.crossSizeLabel, 0, 2);
            this.analysisLayout.Controls.Add(this.crossSizeSpinner, 1, 2);
            this.analysisLayout.Controls.Add(this.showExtremesCheck, 0, 3);
            this.analysisLayout.Controls.Add(this.maxCountLabel, 0, 4);
            this.analysisLayout.Controls.Add(this.maxCountSpinner, 1, 4);
            this.analysisLayout.Controls.Add(this.deletePointsButton, 0, 5);
            this.analysisLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.analysisLayout.Location = new System.Drawing.Point(3, 3);
            this.analysisLayout.Margin = new System.Windows.Forms.Padding(0);
            this.analysisLayout.Name = "analysisLayout";
            this.analysisLayout.RowCount = 7;
            this.analysisLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.analysisLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.analysisLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.analysisLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.analysisLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.analysisLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.analysisLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.analysisLayout.Size = new System.Drawing.Size(230, 750);
            this.analysisLayout.TabIndex = 0;
            // 
            // enableAnalysisCheck
            // 
            this.enableAnalysisCheck.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.enableAnalysisCheck.AutoSize = true;
            this.analysisLayout.SetColumnSpan(this.enableAnalysisCheck, 2);
            this.enableAnalysisCheck.Location = new System.Drawing.Point(3, 6);
            this.enableAnalysisCheck.Name = "enableAnalysisCheck";
            this.enableAnalysisCheck.Size = new System.Drawing.Size(65, 17);
            this.enableAnalysisCheck.TabIndex = 45;
            this.enableAnalysisCheck.Text = "Enabled";
            this.enableAnalysisCheck.UseVisualStyleBackColor = true;
            this.enableAnalysisCheck.CheckedChanged += new System.EventHandler(this.analysisCheck_CheckedChanged);
            // 
            // showTemperatureCheck
            // 
            this.showTemperatureCheck.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.showTemperatureCheck.AutoSize = true;
            this.showTemperatureCheck.Checked = true;
            this.showTemperatureCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.analysisLayout.SetColumnSpan(this.showTemperatureCheck, 2);
            this.showTemperatureCheck.Location = new System.Drawing.Point(3, 36);
            this.showTemperatureCheck.Name = "showTemperatureCheck";
            this.showTemperatureCheck.Size = new System.Drawing.Size(112, 17);
            this.showTemperatureCheck.TabIndex = 51;
            this.showTemperatureCheck.Text = "Show temperature";
            this.showTemperatureCheck.UseVisualStyleBackColor = true;
            this.showTemperatureCheck.CheckedChanged += new System.EventHandler(this.showTemperatureCheck_CheckedChanged);
            // 
            // crossSizeLabel
            // 
            this.crossSizeLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.crossSizeLabel.AutoSize = true;
            this.crossSizeLabel.Location = new System.Drawing.Point(3, 68);
            this.crossSizeLabel.Name = "crossSizeLabel";
            this.crossSizeLabel.Size = new System.Drawing.Size(57, 13);
            this.crossSizeLabel.TabIndex = 49;
            this.crossSizeLabel.Text = "Cross size:";
            this.crossSizeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // crossSizeSpinner
            // 
            this.crossSizeSpinner.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.crossSizeSpinner.Location = new System.Drawing.Point(83, 65);
            this.crossSizeSpinner.Maximum = new decimal(new int[] {
            64,
            0,
            0,
            0});
            this.crossSizeSpinner.Minimum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.crossSizeSpinner.Name = "crossSizeSpinner";
            this.crossSizeSpinner.Size = new System.Drawing.Size(144, 20);
            this.crossSizeSpinner.TabIndex = 50;
            this.crossSizeSpinner.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.crossSizeSpinner.ValueChanged += new System.EventHandler(this.crossSizeSpinner_ValueChanged);
            // 
            // showExtremesCheck
            // 
            this.showExtremesCheck.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.showExtremesCheck.AutoSize = true;
            this.showExtremesCheck.Checked = true;
            this.showExtremesCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.analysisLayout.SetColumnSpan(this.showExtremesCheck, 2);
            this.showExtremesCheck.Location = new System.Drawing.Point(3, 96);
            this.showExtremesCheck.Name = "showExtremesCheck";
            this.showExtremesCheck.Size = new System.Drawing.Size(98, 17);
            this.showExtremesCheck.TabIndex = 47;
            this.showExtremesCheck.Text = "Show extremes";
            this.showExtremesCheck.UseVisualStyleBackColor = true;
            this.showExtremesCheck.CheckedChanged += new System.EventHandler(this.showExtremesCheck_CheckedChanged);
            // 
            // maxCountLabel
            // 
            this.maxCountLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.maxCountLabel.AutoSize = true;
            this.maxCountLabel.Location = new System.Drawing.Point(3, 128);
            this.maxCountLabel.Name = "maxCountLabel";
            this.maxCountLabel.Size = new System.Drawing.Size(60, 13);
            this.maxCountLabel.TabIndex = 46;
            this.maxCountLabel.Text = "Max count:";
            this.maxCountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // maxCountSpinner
            // 
            this.maxCountSpinner.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.maxCountSpinner.Location = new System.Drawing.Point(83, 125);
            this.maxCountSpinner.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.maxCountSpinner.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.maxCountSpinner.Name = "maxCountSpinner";
            this.maxCountSpinner.Size = new System.Drawing.Size(144, 20);
            this.maxCountSpinner.TabIndex = 48;
            this.maxCountSpinner.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.maxCountSpinner.ValueChanged += new System.EventHandler(this.maxCountSpinner_ValueChanged);
            // 
            // outputTab
            // 
            this.outputTab.Controls.Add(this.outputLayout);
            this.outputTab.Location = new System.Drawing.Point(4, 22);
            this.outputTab.Name = "outputTab";
            this.outputTab.Padding = new System.Windows.Forms.Padding(3);
            this.outputTab.Size = new System.Drawing.Size(236, 756);
            this.outputTab.TabIndex = 2;
            this.outputTab.Text = "Output";
            this.outputTab.UseVisualStyleBackColor = true;
            // 
            // outputLayout
            // 
            this.outputLayout.ColumnCount = 2;
            this.outputLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.outputLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.outputLayout.Controls.Add(this.outputPathLabel, 0, 0);
            this.outputLayout.Controls.Add(this.outputPathLayout, 0, 1);
            this.outputLayout.Controls.Add(this.autoSaveCheck, 0, 2);
            this.outputLayout.Controls.Add(this.screenshotButton, 0, 3);
            this.outputLayout.Controls.Add(this.recordVideoButton, 1, 3);
            this.outputLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outputLayout.Location = new System.Drawing.Point(3, 3);
            this.outputLayout.Margin = new System.Windows.Forms.Padding(0);
            this.outputLayout.Name = "outputLayout";
            this.outputLayout.RowCount = 5;
            this.outputLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.outputLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.outputLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.outputLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.outputLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.outputLayout.Size = new System.Drawing.Size(230, 750);
            this.outputLayout.TabIndex = 0;
            // 
            // outputPathLabel
            // 
            this.outputPathLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.outputPathLabel.AutoSize = true;
            this.outputLayout.SetColumnSpan(this.outputPathLabel, 2);
            this.outputPathLabel.Location = new System.Drawing.Point(3, 6);
            this.outputPathLabel.Name = "outputPathLabel";
            this.outputPathLabel.Size = new System.Drawing.Size(66, 13);
            this.outputPathLabel.TabIndex = 48;
            this.outputPathLabel.Text = "Output path:";
            this.outputPathLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // outputPathLayout
            // 
            this.outputPathLayout.ColumnCount = 2;
            this.outputLayout.SetColumnSpan(this.outputPathLayout, 2);
            this.outputPathLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.outputPathLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.outputPathLayout.Controls.Add(this.outputPathField, 0, 0);
            this.outputPathLayout.Controls.Add(this.outputPathButton, 1, 0);
            this.outputPathLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outputPathLayout.Location = new System.Drawing.Point(0, 25);
            this.outputPathLayout.Margin = new System.Windows.Forms.Padding(0);
            this.outputPathLayout.Name = "outputPathLayout";
            this.outputPathLayout.RowCount = 1;
            this.outputPathLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.outputPathLayout.Size = new System.Drawing.Size(230, 30);
            this.outputPathLayout.TabIndex = 47;
            // 
            // outputPathField
            // 
            this.outputPathField.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.outputPathField.Location = new System.Drawing.Point(3, 5);
            this.outputPathField.MaxLength = 256;
            this.outputPathField.Name = "outputPathField";
            this.outputPathField.Size = new System.Drawing.Size(194, 20);
            this.outputPathField.TabIndex = 46;
            this.outputPathField.Text = "export";
            // 
            // outputPathButton
            // 
            this.outputPathButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outputPathButton.Location = new System.Drawing.Point(203, 3);
            this.outputPathButton.Name = "outputPathButton";
            this.outputPathButton.Size = new System.Drawing.Size(24, 24);
            this.outputPathButton.TabIndex = 47;
            this.outputPathButton.Text = "...";
            this.outputPathButton.UseVisualStyleBackColor = true;
            this.outputPathButton.Click += new System.EventHandler(this.outputPathButton_Click);
            // 
            // screenshotButton
            // 
            this.screenshotButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.screenshotButton.Location = new System.Drawing.Point(3, 88);
            this.screenshotButton.Name = "screenshotButton";
            this.screenshotButton.Size = new System.Drawing.Size(109, 24);
            this.screenshotButton.TabIndex = 44;
            this.screenshotButton.Text = "Screenshot";
            this.screenshotButton.UseVisualStyleBackColor = true;
            this.screenshotButton.Click += new System.EventHandler(this.screenshotButton_Click);
            // 
            // recordVideoButton
            // 
            this.recordVideoButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.recordVideoButton.Location = new System.Drawing.Point(118, 88);
            this.recordVideoButton.Name = "recordVideoButton";
            this.recordVideoButton.Size = new System.Drawing.Size(109, 24);
            this.recordVideoButton.TabIndex = 45;
            this.recordVideoButton.Text = "Record video";
            this.recordVideoButton.UseVisualStyleBackColor = true;
            this.recordVideoButton.Click += new System.EventHandler(this.recordVideoButton_Click);
            // 
            // pictureTabs
            // 
            this.pictureTabs.Controls.Add(this.liveTab);
            this.pictureTabs.Controls.Add(this.firstAfterCalTab);
            this.pictureTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureTabs.Location = new System.Drawing.Point(253, 3);
            this.pictureTabs.Name = "pictureTabs";
            this.mainLayout.SetRowSpan(this.pictureTabs, 2);
            this.pictureTabs.SelectedIndex = 0;
            this.pictureTabs.Size = new System.Drawing.Size(909, 812);
            this.pictureTabs.TabIndex = 44;
            // 
            // liveTab
            // 
            this.liveTab.Controls.Add(this.livePicture);
            this.liveTab.Location = new System.Drawing.Point(4, 22);
            this.liveTab.Name = "liveTab";
            this.liveTab.Padding = new System.Windows.Forms.Padding(3);
            this.liveTab.Size = new System.Drawing.Size(901, 786);
            this.liveTab.TabIndex = 0;
            this.liveTab.Text = "Live";
            this.liveTab.UseVisualStyleBackColor = true;
            // 
            // firstAfterCalTab
            // 
            this.firstAfterCalTab.Controls.Add(this.firstAfterCalPicture);
            this.firstAfterCalTab.Location = new System.Drawing.Point(4, 22);
            this.firstAfterCalTab.Name = "firstAfterCalTab";
            this.firstAfterCalTab.Padding = new System.Windows.Forms.Padding(3);
            this.firstAfterCalTab.Size = new System.Drawing.Size(901, 786);
            this.firstAfterCalTab.TabIndex = 1;
            this.firstAfterCalTab.Text = "On calibration";
            this.firstAfterCalTab.UseVisualStyleBackColor = true;
            // 
            // debugValueLayout
            // 
            this.debugValueLayout.ColumnCount = 3;
            this.debugValueLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.debugValueLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.debugValueLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.debugValueLayout.Controls.Add(this.gModeLeftLabel, 0, 0);
            this.debugValueLayout.Controls.Add(this.gModeRightLabel, 1, 0);
            this.debugValueLayout.Controls.Add(this.maxTempRawLabel, 2, 0);
            this.debugValueLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.debugValueLayout.Location = new System.Drawing.Point(0, 818);
            this.debugValueLayout.Margin = new System.Windows.Forms.Padding(0);
            this.debugValueLayout.Name = "debugValueLayout";
            this.debugValueLayout.RowCount = 1;
            this.debugValueLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.debugValueLayout.Size = new System.Drawing.Size(250, 25);
            this.debugValueLayout.TabIndex = 45;
            // 
            // mouseLabel
            // 
            this.mouseLabel.AutoSize = true;
            this.mouseLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mouseLabel.Location = new System.Drawing.Point(253, 818);
            this.mouseLabel.Name = "mouseLabel";
            this.mouseLabel.Size = new System.Drawing.Size(909, 25);
            this.mouseLabel.TabIndex = 46;
            this.mouseLabel.Text = "mouse";
            this.mouseLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // deletePointsButton
            // 
            this.analysisLayout.SetColumnSpan(this.deletePointsButton, 2);
            this.deletePointsButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.deletePointsButton.Location = new System.Drawing.Point(3, 153);
            this.deletePointsButton.Name = "deletePointsButton";
            this.deletePointsButton.Size = new System.Drawing.Size(224, 24);
            this.deletePointsButton.TabIndex = 52;
            this.deletePointsButton.Text = "Delete all points";
            this.deletePointsButton.UseVisualStyleBackColor = true;
            this.deletePointsButton.Click += new System.EventHandler(this.deletePointsButton_Click);
            // 
            // livePicture
            // 
            this.livePicture.AnalysisEnabled = false;
            this.livePicture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.livePicture.CrossSize = 16;
            this.livePicture.Location = new System.Drawing.Point(8, 8);
            this.livePicture.MaxCount = 3;
            this.livePicture.Name = "livePicture";
            this.livePicture.RawValues = null;
            this.livePicture.ShowExtremes = true;
            this.livePicture.ShowTemperature = true;
            this.livePicture.Size = new System.Drawing.Size(32, 32);
            this.livePicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.livePicture.TabIndex = 34;
            this.livePicture.TabStop = false;
            this.livePicture.TempUnit = "K";
            this.livePicture.MouseEnter += new System.EventHandler(this.analyzablePicture_MouseEnter);
            this.livePicture.MouseLeave += new System.EventHandler(this.analyzablePicture_MouseLeave);
            this.livePicture.MouseMove += new System.Windows.Forms.MouseEventHandler(this.analyzablePicture_MouseMove);
            // 
            // firstAfterCalPicture
            // 
            this.firstAfterCalPicture.AnalysisEnabled = false;
            this.firstAfterCalPicture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.firstAfterCalPicture.CrossSize = 16;
            this.firstAfterCalPicture.Location = new System.Drawing.Point(8, 8);
            this.firstAfterCalPicture.MaxCount = 3;
            this.firstAfterCalPicture.Name = "firstAfterCalPicture";
            this.firstAfterCalPicture.RawValues = null;
            this.firstAfterCalPicture.ShowExtremes = true;
            this.firstAfterCalPicture.ShowTemperature = true;
            this.firstAfterCalPicture.Size = new System.Drawing.Size(32, 32);
            this.firstAfterCalPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.firstAfterCalPicture.TabIndex = 38;
            this.firstAfterCalPicture.TabStop = false;
            this.firstAfterCalPicture.TempUnit = "K";
            this.firstAfterCalPicture.MouseEnter += new System.EventHandler(this.analyzablePicture_MouseEnter);
            this.firstAfterCalPicture.MouseLeave += new System.EventHandler(this.analyzablePicture_MouseLeave);
            this.firstAfterCalPicture.MouseMove += new System.Windows.Forms.MouseEventHandler(this.analyzablePicture_MouseMove);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1235, 928);
            this.Controls.Add(this.mainLayout);
            this.MinimumSize = new System.Drawing.Size(640, 360);
            this.Name = "MainWindow";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Text = "SeekOFix v0.4";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MainWindow_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.maxTempSlider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minTempSlider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.histogramPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.temperatureGaugePicture)).EndInit();
            this.mainLayout.ResumeLayout(false);
            this.mainLayout.PerformLayout();
            this.mainControlLayout.ResumeLayout(false);
            this.mainControlLayout.PerformLayout();
            this.mainControlButtons.ResumeLayout(false);
            this.mainControlTabs.ResumeLayout(false);
            this.appearanceTab.ResumeLayout(false);
            this.appearanceLayout.ResumeLayout(false);
            this.appearanceLayout.PerformLayout();
            this.unitsLayout.ResumeLayout(false);
            this.unitsLayout.PerformLayout();
            this.analysisTab.ResumeLayout(false);
            this.analysisLayout.ResumeLayout(false);
            this.analysisLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.crossSizeSpinner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxCountSpinner)).EndInit();
            this.outputTab.ResumeLayout(false);
            this.outputLayout.ResumeLayout(false);
            this.outputLayout.PerformLayout();
            this.outputPathLayout.ResumeLayout(false);
            this.outputPathLayout.PerformLayout();
            this.pictureTabs.ResumeLayout(false);
            this.liveTab.ResumeLayout(false);
            this.firstAfterCalTab.ResumeLayout(false);
            this.debugValueLayout.ResumeLayout(false);
            this.debugValueLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.livePicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.firstAfterCalPicture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button extCalButton;
        private System.Windows.Forms.TrackBar maxTempSlider;
        private System.Windows.Forms.TrackBar minTempSlider;
        private System.Windows.Forms.Button manualRangeSwitchButton;
        private System.Windows.Forms.CheckBox autoSaveCheck;
        private System.Windows.Forms.CheckBox dynSlidersCheck;
        private System.Windows.Forms.CheckBox applySharpenCheck;
        private System.Windows.Forms.Label sliderMinLabel;
        private System.Windows.Forms.Label sliderMaxLabel;
        private System.Windows.Forms.Button startStopButton;
        private System.Windows.Forms.ToolTip startStopToolTip;
        private System.Windows.Forms.ToolTip extCalToolTip;
        private System.Windows.Forms.Label paletteLabel;
        private System.Windows.Forms.ComboBox paletteCombo;
        private AnalyzablePictureBox livePicture;
        private System.Windows.Forms.Label gModeLeftLabel;
        private System.Windows.Forms.Label gModeRightLabel;
        private System.Windows.Forms.PictureBox temperatureGaugePicture;
        private AnalyzablePictureBox firstAfterCalPicture;
        private System.Windows.Forms.Label maxTempRawLabel;
        private System.Windows.Forms.PictureBox histogramPicture;
        private System.Windows.Forms.Label maxTempLabel;
        private System.Windows.Forms.Label minTempLabel;
        private System.Windows.Forms.Label unitsLabel;
        private System.Windows.Forms.RadioButton unitsFRadio;
        private System.Windows.Forms.RadioButton unitsKRadio;
        private System.Windows.Forms.RadioButton unitsCRadio;
        private System.Windows.Forms.Button intCalButton;
        private System.Windows.Forms.TableLayoutPanel mainLayout;
        private System.Windows.Forms.TableLayoutPanel mainControlButtons;
        private System.Windows.Forms.TableLayoutPanel unitsLayout;
        private System.Windows.Forms.ToolTip intCalToolTip;
        private System.Windows.Forms.TabControl pictureTabs;
        private System.Windows.Forms.TabPage liveTab;
        private System.Windows.Forms.TabPage firstAfterCalTab;
        private System.Windows.Forms.TableLayoutPanel debugValueLayout;
        private System.Windows.Forms.Label mouseLabel;
        private System.Windows.Forms.CheckBox enableAnalysisCheck;
        private System.Windows.Forms.TableLayoutPanel analysisLayout;
        private System.Windows.Forms.CheckBox showExtremesCheck;
        private System.Windows.Forms.Label maxCountLabel;
        private System.Windows.Forms.NumericUpDown maxCountSpinner;
        private System.Windows.Forms.CheckBox showTemperatureCheck;
        private System.Windows.Forms.Label crossSizeLabel;
        private System.Windows.Forms.NumericUpDown crossSizeSpinner;
        private System.Windows.Forms.TableLayoutPanel outputLayout;
        private System.Windows.Forms.Button screenshotButton;
        private System.Windows.Forms.Button recordVideoButton;
        private System.Windows.Forms.TableLayoutPanel outputPathLayout;
        private System.Windows.Forms.Button outputPathButton;
        private System.Windows.Forms.TextBox outputPathField;
        private System.Windows.Forms.TableLayoutPanel mainControlLayout;
        private System.Windows.Forms.TabControl mainControlTabs;
        private System.Windows.Forms.TabPage appearanceTab;
        private System.Windows.Forms.TableLayoutPanel appearanceLayout;
        private System.Windows.Forms.TabPage analysisTab;
        private System.Windows.Forms.TabPage outputTab;
        private System.Windows.Forms.Label outputPathLabel;
        private System.Windows.Forms.Button deletePointsButton;
    }
}

