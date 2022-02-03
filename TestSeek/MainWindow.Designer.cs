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
            this.tempUnitsLabel = new System.Windows.Forms.Label();
            this.unitsFRadio = new System.Windows.Forms.RadioButton();
            this.histogramPicture = new System.Windows.Forms.PictureBox();
            this.unitsKRadio = new System.Windows.Forms.RadioButton();
            this.unitsCRadio = new System.Windows.Forms.RadioButton();
            this.paletteCombo = new System.Windows.Forms.ComboBox();
            this.paletteLabel = new System.Windows.Forms.Label();
            this.livePicture = new System.Windows.Forms.PictureBox();
            this.gModeLeftLabel = new System.Windows.Forms.Label();
            this.gModeRightLabel = new System.Windows.Forms.Label();
            this.temperatureGaugePicture = new System.Windows.Forms.PictureBox();
            this.firstAfterCalPicture = new System.Windows.Forms.PictureBox();
            this.maxTempRawLabel = new System.Windows.Forms.Label();
            this.maxTempLabel = new System.Windows.Forms.Label();
            this.minTempLabel = new System.Windows.Forms.Label();
            this.mainLayout = new System.Windows.Forms.TableLayoutPanel();
            this.pictureTabs = new System.Windows.Forms.TabControl();
            this.livePage = new System.Windows.Forms.TabPage();
            this.firstAfterCalPage = new System.Windows.Forms.TabPage();
            this.mainSettingsLayout = new System.Windows.Forms.TableLayoutPanel();
            this.unitsLayout = new System.Windows.Forms.TableLayoutPanel();
            this.debugValueLayout = new System.Windows.Forms.TableLayoutPanel();
            this.intCalToolTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.maxTempSlider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minTempSlider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.histogramPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.livePicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.temperatureGaugePicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.firstAfterCalPicture)).BeginInit();
            this.mainLayout.SuspendLayout();
            this.pictureTabs.SuspendLayout();
            this.livePage.SuspendLayout();
            this.firstAfterCalPage.SuspendLayout();
            this.mainSettingsLayout.SuspendLayout();
            this.unitsLayout.SuspendLayout();
            this.debugValueLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // extCalButton
            // 
            this.extCalButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.extCalButton.Location = new System.Drawing.Point(165, 3);
            this.extCalButton.Name = "extCalButton";
            this.extCalButton.Size = new System.Drawing.Size(76, 24);
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
            this.maxTempSlider.Location = new System.Drawing.Point(3, 697);
            this.maxTempSlider.Maximum = 20000;
            this.maxTempSlider.Minimum = 4000;
            this.maxTempSlider.Name = "maxTempSlider";
            this.maxTempSlider.Size = new System.Drawing.Size(932, 19);
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
            this.minTempSlider.Location = new System.Drawing.Point(3, 647);
            this.minTempSlider.Maximum = 20000;
            this.minTempSlider.Minimum = 4000;
            this.minTempSlider.Name = "minTempSlider";
            this.minTempSlider.Size = new System.Drawing.Size(932, 19);
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
            this.mainSettingsLayout.SetColumnSpan(this.manualRangeSwitchButton, 3);
            this.manualRangeSwitchButton.Location = new System.Drawing.Point(3, 253);
            this.manualRangeSwitchButton.Name = "manualRangeSwitchButton";
            this.manualRangeSwitchButton.Size = new System.Drawing.Size(238, 24);
            this.manualRangeSwitchButton.TabIndex = 13;
            this.manualRangeSwitchButton.Text = "Switch to manual range";
            this.manualRangeSwitchButton.UseVisualStyleBackColor = true;
            this.manualRangeSwitchButton.Click += new System.EventHandler(this.manualRangeSwitchButton_Click);
            // 
            // autoSaveCheck
            // 
            this.autoSaveCheck.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.autoSaveCheck.AutoSize = true;
            this.mainSettingsLayout.SetColumnSpan(this.autoSaveCheck, 3);
            this.autoSaveCheck.Location = new System.Drawing.Point(3, 96);
            this.autoSaveCheck.Name = "autoSaveCheck";
            this.autoSaveCheck.Size = new System.Drawing.Size(73, 17);
            this.autoSaveCheck.TabIndex = 14;
            this.autoSaveCheck.Text = "AutoSave";
            this.autoSaveCheck.UseVisualStyleBackColor = true;
            this.autoSaveCheck.CheckedChanged += new System.EventHandler(this.autoSaveCheck_CheckedChanged);
            // 
            // dynSlidersCheck
            // 
            this.dynSlidersCheck.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.dynSlidersCheck.AutoSize = true;
            this.mainSettingsLayout.SetColumnSpan(this.dynSlidersCheck, 3);
            this.dynSlidersCheck.Location = new System.Drawing.Point(3, 286);
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
            this.mainSettingsLayout.SetColumnSpan(this.applySharpenCheck, 3);
            this.applySharpenCheck.Location = new System.Drawing.Point(3, 126);
            this.applySharpenCheck.Name = "applySharpenCheck";
            this.applySharpenCheck.Size = new System.Drawing.Size(117, 17);
            this.applySharpenCheck.TabIndex = 20;
            this.applySharpenCheck.Text = "Apply Sharpen filter";
            this.applySharpenCheck.UseVisualStyleBackColor = true;
            this.applySharpenCheck.CheckedChanged += new System.EventHandler(this.applySharpenCheck_CheckedChanged);
            // 
            // sliderMinLabel
            // 
            this.sliderMinLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sliderMinLabel.Location = new System.Drawing.Point(941, 644);
            this.sliderMinLabel.Name = "sliderMinLabel";
            this.sliderMinLabel.Size = new System.Drawing.Size(54, 25);
            this.sliderMinLabel.TabIndex = 22;
            this.sliderMinLabel.Text = "MIN";
            this.sliderMinLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // sliderMaxLabel
            // 
            this.sliderMaxLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sliderMaxLabel.Location = new System.Drawing.Point(941, 694);
            this.sliderMaxLabel.Name = "sliderMaxLabel";
            this.sliderMaxLabel.Size = new System.Drawing.Size(54, 25);
            this.sliderMaxLabel.TabIndex = 23;
            this.sliderMaxLabel.Text = "MAX";
            this.sliderMaxLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // startStopButton
            // 
            this.startStopButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.startStopButton.Location = new System.Drawing.Point(3, 3);
            this.startStopButton.Name = "startStopButton";
            this.startStopButton.Size = new System.Drawing.Size(75, 24);
            this.startStopButton.TabIndex = 27;
            this.startStopButton.Text = "STOP";
            this.startStopToolTip.SetToolTip(this.startStopButton, "Start/stop streaming");
            this.startStopButton.UseVisualStyleBackColor = true;
            this.startStopButton.Click += new System.EventHandler(this.startStopButton_Click);
            // 
            // intCalButton
            // 
            this.intCalButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.intCalButton.Location = new System.Drawing.Point(84, 3);
            this.intCalButton.Name = "intCalButton";
            this.intCalButton.Size = new System.Drawing.Size(75, 24);
            this.intCalButton.TabIndex = 43;
            this.intCalButton.Text = "INT Cal";
            this.intCalToolTip.SetToolTip(this.intCalButton, "Do internal calibration");
            this.intCalButton.UseVisualStyleBackColor = true;
            this.intCalButton.Click += new System.EventHandler(this.intCalButton_Click);
            // 
            // tempUnitsLabel
            // 
            this.tempUnitsLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tempUnitsLabel.AutoSize = true;
            this.tempUnitsLabel.Location = new System.Drawing.Point(3, 68);
            this.tempUnitsLabel.Name = "tempUnitsLabel";
            this.tempUnitsLabel.Size = new System.Drawing.Size(62, 13);
            this.tempUnitsLabel.TabIndex = 33;
            this.tempUnitsLabel.Text = "Temp units:";
            this.tempUnitsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // unitsFRadio
            // 
            this.unitsFRadio.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.unitsFRadio.AutoSize = true;
            this.unitsFRadio.Location = new System.Drawing.Point(107, 3);
            this.unitsFRadio.Name = "unitsFRadio";
            this.unitsFRadio.Size = new System.Drawing.Size(35, 17);
            this.unitsFRadio.TabIndex = 26;
            this.unitsFRadio.Text = "°F";
            this.unitsFRadio.UseVisualStyleBackColor = true;
            // 
            // histogramPicture
            // 
            this.histogramPicture.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.histogramPicture.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.histogramPicture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mainSettingsLayout.SetColumnSpan(this.histogramPicture, 3);
            this.histogramPicture.Location = new System.Drawing.Point(3, 153);
            this.histogramPicture.Name = "histogramPicture";
            this.histogramPicture.Size = new System.Drawing.Size(238, 94);
            this.histogramPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.histogramPicture.TabIndex = 40;
            this.histogramPicture.TabStop = false;
            // 
            // unitsKRadio
            // 
            this.unitsKRadio.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.unitsKRadio.AutoSize = true;
            this.unitsKRadio.Checked = true;
            this.unitsKRadio.Location = new System.Drawing.Point(3, 3);
            this.unitsKRadio.Name = "unitsKRadio";
            this.unitsKRadio.Size = new System.Drawing.Size(32, 17);
            this.unitsKRadio.TabIndex = 32;
            this.unitsKRadio.TabStop = true;
            this.unitsKRadio.Text = "K";
            this.unitsKRadio.UseVisualStyleBackColor = true;
            // 
            // unitsCRadio
            // 
            this.unitsCRadio.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.unitsCRadio.AutoSize = true;
            this.unitsCRadio.Location = new System.Drawing.Point(55, 3);
            this.unitsCRadio.Name = "unitsCRadio";
            this.unitsCRadio.Size = new System.Drawing.Size(36, 17);
            this.unitsCRadio.TabIndex = 25;
            this.unitsCRadio.Text = "°C";
            this.unitsCRadio.UseVisualStyleBackColor = true;
            // 
            // paletteCombo
            // 
            this.paletteCombo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.mainSettingsLayout.SetColumnSpan(this.paletteCombo, 2);
            this.paletteCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.paletteCombo.FormattingEnabled = true;
            this.paletteCombo.Location = new System.Drawing.Point(84, 34);
            this.paletteCombo.Name = "paletteCombo";
            this.paletteCombo.Size = new System.Drawing.Size(157, 21);
            this.paletteCombo.TabIndex = 31;
            this.paletteCombo.SelectedIndexChanged += new System.EventHandler(this.paletteCombo_SelectedIndexChanged);
            // 
            // paletteLabel
            // 
            this.paletteLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.paletteLabel.AutoSize = true;
            this.paletteLabel.Location = new System.Drawing.Point(3, 38);
            this.paletteLabel.Name = "paletteLabel";
            this.paletteLabel.Size = new System.Drawing.Size(43, 13);
            this.paletteLabel.TabIndex = 30;
            this.paletteLabel.Text = "Palette:";
            this.paletteLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // livePicture
            // 
            this.livePicture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.livePicture.Location = new System.Drawing.Point(3, 3);
            this.livePicture.Name = "livePicture";
            this.livePicture.Size = new System.Drawing.Size(668, 606);
            this.livePicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.livePicture.TabIndex = 34;
            this.livePicture.TabStop = false;
            // 
            // gModeLeftLabel
            // 
            this.gModeLeftLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.gModeLeftLabel.AutoSize = true;
            this.gModeLeftLabel.Location = new System.Drawing.Point(11, 3);
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
            this.gModeRightLabel.Location = new System.Drawing.Point(89, 3);
            this.gModeRightLabel.Name = "gModeRightLabel";
            this.gModeRightLabel.Size = new System.Drawing.Size(65, 13);
            this.gModeRightLabel.TabIndex = 36;
            this.gModeRightLabel.Text = "gModeRight";
            this.gModeRightLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // temperatureGaugePicture
            // 
            this.temperatureGaugePicture.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.temperatureGaugePicture.Location = new System.Drawing.Point(941, 28);
            this.temperatureGaugePicture.Name = "temperatureGaugePicture";
            this.temperatureGaugePicture.Size = new System.Drawing.Size(54, 588);
            this.temperatureGaugePicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.temperatureGaugePicture.TabIndex = 37;
            this.temperatureGaugePicture.TabStop = false;
            // 
            // firstAfterCalPicture
            // 
            this.firstAfterCalPicture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.firstAfterCalPicture.Location = new System.Drawing.Point(3, 3);
            this.firstAfterCalPicture.Name = "firstAfterCalPicture";
            this.firstAfterCalPicture.Size = new System.Drawing.Size(688, 606);
            this.firstAfterCalPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.firstAfterCalPicture.TabIndex = 38;
            this.firstAfterCalPicture.TabStop = false;
            // 
            // maxTempRawLabel
            // 
            this.maxTempRawLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.maxTempRawLabel.AutoSize = true;
            this.maxTempRawLabel.Location = new System.Drawing.Point(165, 3);
            this.maxTempRawLabel.Name = "maxTempRawLabel";
            this.maxTempRawLabel.Size = new System.Drawing.Size(75, 13);
            this.maxTempRawLabel.TabIndex = 39;
            this.maxTempRawLabel.Text = "maxTempRaw";
            this.maxTempRawLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // maxTempLabel
            // 
            this.maxTempLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.maxTempLabel.Location = new System.Drawing.Point(941, 0);
            this.maxTempLabel.Name = "maxTempLabel";
            this.maxTempLabel.Size = new System.Drawing.Size(54, 25);
            this.maxTempLabel.TabIndex = 41;
            this.maxTempLabel.Text = "30.5";
            this.maxTempLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // minTempLabel
            // 
            this.minTempLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.minTempLabel.Location = new System.Drawing.Point(941, 619);
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
            this.mainLayout.Controls.Add(this.minTempSlider, 0, 3);
            this.mainLayout.Controls.Add(this.maxTempSlider, 0, 5);
            this.mainLayout.Controls.Add(this.pictureTabs, 1, 0);
            this.mainLayout.Controls.Add(this.mainSettingsLayout, 0, 0);
            this.mainLayout.Controls.Add(this.temperatureGaugePicture, 2, 1);
            this.mainLayout.Controls.Add(this.minTempLabel, 2, 2);
            this.mainLayout.Controls.Add(this.maxTempLabel, 2, 0);
            this.mainLayout.Controls.Add(this.debugValueLayout, 0, 2);
            this.mainLayout.Controls.Add(this.sliderMaxLabel, 2, 5);
            this.mainLayout.Controls.Add(this.sliderMinLabel, 2, 3);
            this.mainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainLayout.Location = new System.Drawing.Point(5, 5);
            this.mainLayout.Name = "mainLayout";
            this.mainLayout.RowCount = 6;
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.mainLayout.Size = new System.Drawing.Size(998, 719);
            this.mainLayout.TabIndex = 43;
            // 
            // pictureTabs
            // 
            this.pictureTabs.Controls.Add(this.livePage);
            this.pictureTabs.Controls.Add(this.firstAfterCalPage);
            this.pictureTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureTabs.Location = new System.Drawing.Point(253, 3);
            this.pictureTabs.Name = "pictureTabs";
            this.mainLayout.SetRowSpan(this.pictureTabs, 3);
            this.pictureTabs.SelectedIndex = 0;
            this.pictureTabs.Size = new System.Drawing.Size(682, 638);
            this.pictureTabs.TabIndex = 44;
            // 
            // livePage
            // 
            this.livePage.Controls.Add(this.livePicture);
            this.livePage.Location = new System.Drawing.Point(4, 22);
            this.livePage.Name = "livePage";
            this.livePage.Padding = new System.Windows.Forms.Padding(3);
            this.livePage.Size = new System.Drawing.Size(674, 612);
            this.livePage.TabIndex = 0;
            this.livePage.Text = "Live";
            this.livePage.UseVisualStyleBackColor = true;
            // 
            // firstAfterCalPage
            // 
            this.firstAfterCalPage.Controls.Add(this.firstAfterCalPicture);
            this.firstAfterCalPage.Location = new System.Drawing.Point(4, 22);
            this.firstAfterCalPage.Name = "firstAfterCalPage";
            this.firstAfterCalPage.Padding = new System.Windows.Forms.Padding(3);
            this.firstAfterCalPage.Size = new System.Drawing.Size(694, 612);
            this.firstAfterCalPage.TabIndex = 1;
            this.firstAfterCalPage.Text = "First after cal";
            this.firstAfterCalPage.UseVisualStyleBackColor = true;
            // 
            // mainSettingsLayout
            // 
            this.mainSettingsLayout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainSettingsLayout.AutoSize = true;
            this.mainSettingsLayout.ColumnCount = 3;
            this.mainSettingsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.mainSettingsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.mainSettingsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.mainSettingsLayout.Controls.Add(this.startStopButton, 0, 0);
            this.mainSettingsLayout.Controls.Add(this.intCalButton, 1, 0);
            this.mainSettingsLayout.Controls.Add(this.extCalButton, 2, 0);
            this.mainSettingsLayout.Controls.Add(this.paletteLabel, 0, 1);
            this.mainSettingsLayout.Controls.Add(this.paletteCombo, 1, 1);
            this.mainSettingsLayout.Controls.Add(this.tempUnitsLabel, 0, 2);
            this.mainSettingsLayout.Controls.Add(this.unitsLayout, 1, 2);
            this.mainSettingsLayout.Controls.Add(this.autoSaveCheck, 0, 3);
            this.mainSettingsLayout.Controls.Add(this.applySharpenCheck, 0, 4);
            this.mainSettingsLayout.Controls.Add(this.histogramPicture, 0, 5);
            this.mainSettingsLayout.Controls.Add(this.manualRangeSwitchButton, 0, 6);
            this.mainSettingsLayout.Controls.Add(this.dynSlidersCheck, 0, 7);
            this.mainSettingsLayout.Location = new System.Drawing.Point(3, 3);
            this.mainSettingsLayout.Name = "mainSettingsLayout";
            this.mainSettingsLayout.RowCount = 8;
            this.mainLayout.SetRowSpan(this.mainSettingsLayout, 2);
            this.mainSettingsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.mainSettingsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.mainSettingsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.mainSettingsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.mainSettingsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.mainSettingsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.mainSettingsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.mainSettingsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.mainSettingsLayout.Size = new System.Drawing.Size(244, 310);
            this.mainSettingsLayout.TabIndex = 43;
            // 
            // unitsLayout
            // 
            this.unitsLayout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.unitsLayout.ColumnCount = 3;
            this.mainSettingsLayout.SetColumnSpan(this.unitsLayout, 2);
            this.unitsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.unitsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.unitsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.unitsLayout.Controls.Add(this.unitsKRadio, 0, 0);
            this.unitsLayout.Controls.Add(this.unitsCRadio, 1, 0);
            this.unitsLayout.Controls.Add(this.unitsFRadio, 2, 0);
            this.unitsLayout.Location = new System.Drawing.Point(84, 63);
            this.unitsLayout.Name = "unitsLayout";
            this.unitsLayout.RowCount = 1;
            this.unitsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.unitsLayout.Size = new System.Drawing.Size(157, 24);
            this.unitsLayout.TabIndex = 44;
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
            this.debugValueLayout.Location = new System.Drawing.Point(3, 622);
            this.debugValueLayout.Name = "debugValueLayout";
            this.debugValueLayout.RowCount = 1;
            this.debugValueLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.debugValueLayout.Size = new System.Drawing.Size(244, 19);
            this.debugValueLayout.TabIndex = 45;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 729);
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
            ((System.ComponentModel.ISupportInitialize)(this.livePicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.temperatureGaugePicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.firstAfterCalPicture)).EndInit();
            this.mainLayout.ResumeLayout(false);
            this.mainLayout.PerformLayout();
            this.pictureTabs.ResumeLayout(false);
            this.livePage.ResumeLayout(false);
            this.firstAfterCalPage.ResumeLayout(false);
            this.mainSettingsLayout.ResumeLayout(false);
            this.mainSettingsLayout.PerformLayout();
            this.unitsLayout.ResumeLayout(false);
            this.unitsLayout.PerformLayout();
            this.debugValueLayout.ResumeLayout(false);
            this.debugValueLayout.PerformLayout();
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
        private System.Windows.Forms.PictureBox livePicture;
        private System.Windows.Forms.Label gModeLeftLabel;
        private System.Windows.Forms.Label gModeRightLabel;
        private System.Windows.Forms.PictureBox temperatureGaugePicture;
        private System.Windows.Forms.PictureBox firstAfterCalPicture;
        private System.Windows.Forms.Label maxTempRawLabel;
        private System.Windows.Forms.PictureBox histogramPicture;
        private System.Windows.Forms.Label maxTempLabel;
        private System.Windows.Forms.Label minTempLabel;
        private System.Windows.Forms.Label tempUnitsLabel;
        private System.Windows.Forms.RadioButton unitsFRadio;
        private System.Windows.Forms.RadioButton unitsKRadio;
        private System.Windows.Forms.RadioButton unitsCRadio;
        private System.Windows.Forms.Button intCalButton;
        private System.Windows.Forms.TableLayoutPanel mainLayout;
        private System.Windows.Forms.TableLayoutPanel mainSettingsLayout;
        private System.Windows.Forms.TableLayoutPanel unitsLayout;
        private System.Windows.Forms.ToolTip intCalToolTip;
        private System.Windows.Forms.TabControl pictureTabs;
        private System.Windows.Forms.TabPage livePage;
        private System.Windows.Forms.TabPage firstAfterCalPage;
        private System.Windows.Forms.TableLayoutPanel debugValueLayout;
    }
}

