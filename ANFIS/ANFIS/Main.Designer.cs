using System.Windows.Forms;

namespace ANFIS
{
	partial class Main
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
			System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
			System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
			System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
			this.progressionPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.panelIcon = new System.Windows.Forms.Panel();
			this.buttonTrainSet = new System.Windows.Forms.Button();
			this.buttonTrain = new System.Windows.Forms.Button();
			this.buttonResult = new System.Windows.Forms.Button();
			this.titleBar = new System.Windows.Forms.Panel();
			this.buttonExit = new System.Windows.Forms.Button();
			this.panelSlider = new System.Windows.Forms.Panel();
			this.panelTrainSet = new System.Windows.Forms.Panel();
			this.panelFunction = new System.Windows.Forms.Panel();
			this.labelFunction = new System.Windows.Forms.Label();
			this.buttonFunction = new System.Windows.Forms.Button();
			this.comboBoxFunction = new System.Windows.Forms.ComboBox();
			this.loadTrainSet = new System.Windows.Forms.ComboBox();
			this.labelLoadTrainSet = new System.Windows.Forms.Label();
			this.buttonLoadTrainSet = new System.Windows.Forms.Button();
			this.labelOr = new System.Windows.Forms.Label();
			this.separator = new System.Windows.Forms.Label();
			this.buttonCreateTrainSet = new System.Windows.Forms.Button();
			this.panelTrain = new System.Windows.Forms.Panel();
			this.labelTotalError = new System.Windows.Forms.Label();
			this.labelTotalErrorStatic = new System.Windows.Forms.Label();
			this.buttonRemoveLayer = new System.Windows.Forms.Button();
			this.layoutArchitexture = new System.Windows.Forms.TableLayoutPanel();
			this.textBoxDesiredError = new System.Windows.Forms.TextBox();
			this.textBoxEta = new System.Windows.Forms.TextBox();
			this.comboBoxType = new System.Windows.Forms.ComboBox();
			this.labelDesiredError = new System.Windows.Forms.Label();
			this.labelEta = new System.Windows.Forms.Label();
			this.labelType = new System.Windows.Forms.Label();
			this.buttonAddLayer = new System.Windows.Forms.Button();
			this.labelArchitecture = new System.Windows.Forms.Label();
			this.errorChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
			this.Train = new System.Windows.Forms.Button();
			this.GoToTest = new System.Windows.Forms.Button();
			this.panelResult = new System.Windows.Forms.Panel();
			this.resultClasses = new System.Windows.Forms.TableLayoutPanel();
			this.labelClass = new System.Windows.Forms.Label();
			this.labelClassStatic = new System.Windows.Forms.Label();
			this.Test = new System.Windows.Forms.Button();
			this.progressionPanel.SuspendLayout();
			this.titleBar.SuspendLayout();
			this.panelTrainSet.SuspendLayout();
			this.panelFunction.SuspendLayout();
			this.panelTrain.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.errorChart)).BeginInit();
			this.panelResult.SuspendLayout();
			this.SuspendLayout();
			// 
			// progressionPanel
			// 
			this.progressionPanel.Controls.Add(this.panelIcon);
			this.progressionPanel.Controls.Add(this.buttonTrainSet);
			this.progressionPanel.Controls.Add(this.buttonTrain);
			this.progressionPanel.Controls.Add(this.buttonResult);
			this.progressionPanel.Dock = System.Windows.Forms.DockStyle.Left;
			this.progressionPanel.Location = new System.Drawing.Point(0, 0);
			this.progressionPanel.Name = "progressionPanel";
			this.progressionPanel.Size = new System.Drawing.Size(156, 600);
			this.progressionPanel.TabIndex = 2;
			// 
			// panelIcon
			// 
			this.panelIcon.BackgroundImage = global::ANFIS.Properties.Resources.NN;
			this.panelIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.panelIcon.Location = new System.Drawing.Point(0, 0);
			this.panelIcon.Margin = new System.Windows.Forms.Padding(0);
			this.panelIcon.Name = "panelIcon";
			this.panelIcon.Size = new System.Drawing.Size(159, 92);
			this.panelIcon.TabIndex = 0;
			// 
			// buttonTrainSet
			// 
			this.buttonTrainSet.Dock = System.Windows.Forms.DockStyle.Top;
			this.buttonTrainSet.FlatAppearance.BorderSize = 0;
			this.buttonTrainSet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonTrainSet.Location = new System.Drawing.Point(0, 92);
			this.buttonTrainSet.Margin = new System.Windows.Forms.Padding(0);
			this.buttonTrainSet.Name = "buttonTrainSet";
			this.buttonTrainSet.Size = new System.Drawing.Size(156, 93);
			this.buttonTrainSet.TabIndex = 1;
			this.buttonTrainSet.Text = "Define train set";
			this.buttonTrainSet.UseVisualStyleBackColor = true;
			this.buttonTrainSet.Click += new System.EventHandler(this.ButtonTrainSet_Click);
			// 
			// buttonTrain
			// 
			this.buttonTrain.Enabled = false;
			this.buttonTrain.FlatAppearance.BorderSize = 0;
			this.buttonTrain.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonTrain.Location = new System.Drawing.Point(0, 185);
			this.buttonTrain.Margin = new System.Windows.Forms.Padding(0);
			this.buttonTrain.Name = "buttonTrain";
			this.buttonTrain.Size = new System.Drawing.Size(156, 93);
			this.buttonTrain.TabIndex = 4;
			this.buttonTrain.Text = "Train neural network";
			this.buttonTrain.UseVisualStyleBackColor = true;
			this.buttonTrain.Click += new System.EventHandler(this.ButtonTrain_Click);
			// 
			// buttonResult
			// 
			this.buttonResult.Enabled = false;
			this.buttonResult.FlatAppearance.BorderSize = 0;
			this.buttonResult.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonResult.Location = new System.Drawing.Point(0, 278);
			this.buttonResult.Margin = new System.Windows.Forms.Padding(0);
			this.buttonResult.Name = "buttonResult";
			this.buttonResult.Size = new System.Drawing.Size(156, 93);
			this.buttonResult.TabIndex = 4;
			this.buttonResult.Text = "Result";
			this.buttonResult.UseVisualStyleBackColor = true;
			this.buttonResult.Click += new System.EventHandler(this.ButtonTest_Click);
			// 
			// titleBar
			// 
			this.titleBar.BackColor = System.Drawing.Color.Transparent;
			this.titleBar.Controls.Add(this.buttonExit);
			this.titleBar.Cursor = System.Windows.Forms.Cursors.SizeAll;
			this.titleBar.Dock = System.Windows.Forms.DockStyle.Top;
			this.titleBar.Location = new System.Drawing.Point(156, 0);
			this.titleBar.Name = "titleBar";
			this.titleBar.Size = new System.Drawing.Size(644, 32);
			this.titleBar.TabIndex = 3;
			this.titleBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Titlebar_MouseDown);
			this.titleBar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Titlebar_MouseMove);
			this.titleBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Titlebar_MouseUp);
			// 
			// buttonExit
			// 
			this.buttonExit.BackColor = System.Drawing.Color.Transparent;
			this.buttonExit.BackgroundImage = global::ANFIS.Properties.Resources.Exit;
			this.buttonExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.buttonExit.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.buttonExit.Dock = System.Windows.Forms.DockStyle.Right;
			this.buttonExit.FlatAppearance.BorderSize = 0;
			this.buttonExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
			this.buttonExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonExit.Location = new System.Drawing.Point(612, 0);
			this.buttonExit.Name = "buttonExit";
			this.buttonExit.Size = new System.Drawing.Size(32, 32);
			this.buttonExit.TabIndex = 0;
			this.buttonExit.UseVisualStyleBackColor = false;
			this.buttonExit.Click += new System.EventHandler(this.ButtonExit_Click);
			this.buttonExit.MouseEnter += new System.EventHandler(this.ButtonExit_MouseEnter);
			this.buttonExit.MouseLeave += new System.EventHandler(this.ButtonExit_MouseLeave);
			// 
			// panelSlider
			// 
			this.panelSlider.BackColor = System.Drawing.Color.LightSkyBlue;
			this.panelSlider.Location = new System.Drawing.Point(159, 92);
			this.panelSlider.Name = "panelSlider";
			this.panelSlider.Size = new System.Drawing.Size(10, 93);
			this.panelSlider.TabIndex = 4;
			// 
			// panelTrainSet
			// 
			this.panelTrainSet.Controls.Add(this.panelFunction);
			this.panelTrainSet.Controls.Add(this.loadTrainSet);
			this.panelTrainSet.Controls.Add(this.labelLoadTrainSet);
			this.panelTrainSet.Controls.Add(this.buttonLoadTrainSet);
			this.panelTrainSet.Controls.Add(this.labelOr);
			this.panelTrainSet.Controls.Add(this.separator);
			this.panelTrainSet.Controls.Add(this.buttonCreateTrainSet);
			this.panelTrainSet.Location = new System.Drawing.Point(175, 38);
			this.panelTrainSet.Name = "panelTrainSet";
			this.panelTrainSet.Size = new System.Drawing.Size(625, 562);
			this.panelTrainSet.TabIndex = 6;
			this.panelTrainSet.VisibleChanged += new System.EventHandler(this.ParamsPanel_Visible);
			// 
			// panelFunction
			// 
			this.panelFunction.Controls.Add(this.labelFunction);
			this.panelFunction.Controls.Add(this.buttonFunction);
			this.panelFunction.Controls.Add(this.comboBoxFunction);
			this.panelFunction.Location = new System.Drawing.Point(157, 74);
			this.panelFunction.Name = "panelFunction";
			this.panelFunction.Size = new System.Drawing.Size(300, 283);
			this.panelFunction.TabIndex = 12;
			this.panelFunction.Visible = false;
			// 
			// labelFunction
			// 
			this.labelFunction.AutoSize = true;
			this.labelFunction.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.labelFunction.Location = new System.Drawing.Point(88, 56);
			this.labelFunction.Name = "labelFunction";
			this.labelFunction.Size = new System.Drawing.Size(138, 21);
			this.labelFunction.TabIndex = 2;
			this.labelFunction.Text = "Choose function";
			// 
			// buttonFunction
			// 
			this.buttonFunction.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonFunction.Location = new System.Drawing.Point(52, 170);
			this.buttonFunction.Name = "buttonFunction";
			this.buttonFunction.Size = new System.Drawing.Size(207, 48);
			this.buttonFunction.TabIndex = 1;
			this.buttonFunction.Text = "Create train set";
			this.buttonFunction.UseVisualStyleBackColor = true;
			this.buttonFunction.Click += new System.EventHandler(this.ButtonFunction_Click);
			// 
			// comboBoxFunction
			// 
			this.comboBoxFunction.DropDownWidth = 350;
			this.comboBoxFunction.FormattingEnabled = true;
			this.comboBoxFunction.Location = new System.Drawing.Point(27, 106);
			this.comboBoxFunction.Name = "comboBoxFunction";
			this.comboBoxFunction.Size = new System.Drawing.Size(250, 29);
			this.comboBoxFunction.TabIndex = 0;
			// 
			// loadTrainSet
			// 
			this.loadTrainSet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.loadTrainSet.FormattingEnabled = true;
			this.loadTrainSet.Location = new System.Drawing.Point(237, 127);
			this.loadTrainSet.Name = "loadTrainSet";
			this.loadTrainSet.Size = new System.Drawing.Size(150, 29);
			this.loadTrainSet.TabIndex = 11;
			// 
			// labelLoadTrainSet
			// 
			this.labelLoadTrainSet.AutoSize = true;
			this.labelLoadTrainSet.Location = new System.Drawing.Point(247, 90);
			this.labelLoadTrainSet.Name = "labelLoadTrainSet";
			this.labelLoadTrainSet.Size = new System.Drawing.Size(136, 21);
			this.labelLoadTrainSet.TabIndex = 10;
			this.labelLoadTrainSet.Text = "Choose train set";
			// 
			// buttonLoadTrainSet
			// 
			this.buttonLoadTrainSet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonLoadTrainSet.Location = new System.Drawing.Point(209, 189);
			this.buttonLoadTrainSet.Name = "buttonLoadTrainSet";
			this.buttonLoadTrainSet.Size = new System.Drawing.Size(207, 48);
			this.buttonLoadTrainSet.TabIndex = 3;
			this.buttonLoadTrainSet.Text = "Load train set";
			this.buttonLoadTrainSet.UseVisualStyleBackColor = true;
			this.buttonLoadTrainSet.Click += new System.EventHandler(this.ButtonLoadTrainSet_Click);
			// 
			// labelOr
			// 
			this.labelOr.AutoSize = true;
			this.labelOr.BackColor = System.Drawing.Color.Transparent;
			this.labelOr.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.labelOr.Location = new System.Drawing.Point(299, 254);
			this.labelOr.Name = "labelOr";
			this.labelOr.Size = new System.Drawing.Size(34, 21);
			this.labelOr.TabIndex = 2;
			this.labelOr.Text = "OR";
			// 
			// separator
			// 
			this.separator.BackColor = System.Drawing.Color.LightSkyBlue;
			this.separator.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.separator.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.separator.ForeColor = System.Drawing.Color.LightSkyBlue;
			this.separator.Location = new System.Drawing.Point(5, 264);
			this.separator.Name = "separator";
			this.separator.Size = new System.Drawing.Size(616, 3);
			this.separator.TabIndex = 1;
			// 
			// buttonCreateTrainSet
			// 
			this.buttonCreateTrainSet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonCreateTrainSet.Location = new System.Drawing.Point(209, 303);
			this.buttonCreateTrainSet.Name = "buttonCreateTrainSet";
			this.buttonCreateTrainSet.Size = new System.Drawing.Size(207, 48);
			this.buttonCreateTrainSet.TabIndex = 0;
			this.buttonCreateTrainSet.Text = "Create train set";
			this.buttonCreateTrainSet.UseVisualStyleBackColor = true;
			this.buttonCreateTrainSet.Click += new System.EventHandler(this.CreateTrainSet_Click);
			// 
			// panelTrain
			// 
			this.panelTrain.Controls.Add(this.labelTotalError);
			this.panelTrain.Controls.Add(this.labelTotalErrorStatic);
			this.panelTrain.Controls.Add(this.buttonRemoveLayer);
			this.panelTrain.Controls.Add(this.layoutArchitexture);
			this.panelTrain.Controls.Add(this.textBoxDesiredError);
			this.panelTrain.Controls.Add(this.textBoxEta);
			this.panelTrain.Controls.Add(this.comboBoxType);
			this.panelTrain.Controls.Add(this.labelDesiredError);
			this.panelTrain.Controls.Add(this.labelEta);
			this.panelTrain.Controls.Add(this.labelType);
			this.panelTrain.Controls.Add(this.buttonAddLayer);
			this.panelTrain.Controls.Add(this.labelArchitecture);
			this.panelTrain.Controls.Add(this.errorChart);
			this.panelTrain.Controls.Add(this.Train);
			this.panelTrain.Controls.Add(this.GoToTest);
			this.panelTrain.Location = new System.Drawing.Point(175, 38);
			this.panelTrain.Name = "panelTrain";
			this.panelTrain.Size = new System.Drawing.Size(625, 562);
			this.panelTrain.TabIndex = 0;
			this.panelTrain.Visible = false;
			this.panelTrain.VisibleChanged += new System.EventHandler(this.TrainPanel_Visible);
			// 
			// labelTotalError
			// 
			this.labelTotalError.AutoSize = true;
			this.labelTotalError.Location = new System.Drawing.Point(319, 200);
			this.labelTotalError.Name = "labelTotalError";
			this.labelTotalError.Size = new System.Drawing.Size(0, 21);
			this.labelTotalError.TabIndex = 16;
			// 
			// labelTotalErrorStatic
			// 
			this.labelTotalErrorStatic.AutoSize = true;
			this.labelTotalErrorStatic.Location = new System.Drawing.Point(220, 200);
			this.labelTotalErrorStatic.Name = "labelTotalErrorStatic";
			this.labelTotalErrorStatic.Size = new System.Drawing.Size(92, 21);
			this.labelTotalErrorStatic.TabIndex = 15;
			this.labelTotalErrorStatic.Text = "Total error:";
			// 
			// buttonRemoveLayer
			// 
			this.buttonRemoveLayer.Enabled = false;
			this.buttonRemoveLayer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonRemoveLayer.Location = new System.Drawing.Point(157, 276);
			this.buttonRemoveLayer.Name = "buttonRemoveLayer";
			this.buttonRemoveLayer.Size = new System.Drawing.Size(155, 29);
			this.buttonRemoveLayer.TabIndex = 14;
			this.buttonRemoveLayer.Text = "Remove layer";
			this.buttonRemoveLayer.UseVisualStyleBackColor = true;
			this.buttonRemoveLayer.Click += new System.EventHandler(this.ButtonRemoveLayer_Click);
			// 
			// layoutArchitexture
			// 
			this.layoutArchitexture.ColumnCount = 2;
			this.layoutArchitexture.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.layoutArchitexture.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.layoutArchitexture.Location = new System.Drawing.Point(157, 236);
			this.layoutArchitexture.Name = "layoutArchitexture";
			this.layoutArchitexture.RowCount = 2;
			this.layoutArchitexture.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.layoutArchitexture.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.layoutArchitexture.Size = new System.Drawing.Size(456, 29);
			this.layoutArchitexture.TabIndex = 13;
			// 
			// textBoxDesiredError
			// 
			this.textBoxDesiredError.Location = new System.Drawing.Point(215, 399);
			this.textBoxDesiredError.Name = "textBoxDesiredError";
			this.textBoxDesiredError.Size = new System.Drawing.Size(208, 27);
			this.textBoxDesiredError.TabIndex = 12;
			this.textBoxDesiredError.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DesiredError_Changed);
			this.textBoxDesiredError.Leave += new System.EventHandler(this.DesiredError_Left);
			// 
			// textBoxEta
			// 
			this.textBoxEta.Location = new System.Drawing.Point(215, 363);
			this.textBoxEta.Name = "textBoxEta";
			this.textBoxEta.Size = new System.Drawing.Size(208, 27);
			this.textBoxEta.TabIndex = 11;
			this.textBoxEta.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Eta_Changed);
			this.textBoxEta.Leave += new System.EventHandler(this.Eta_Left);
			// 
			// comboBoxType
			// 
			this.comboBoxType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxType.FormattingEnabled = true;
			this.comboBoxType.Location = new System.Drawing.Point(215, 325);
			this.comboBoxType.Name = "comboBoxType";
			this.comboBoxType.Size = new System.Drawing.Size(208, 29);
			this.comboBoxType.TabIndex = 10;
			this.comboBoxType.SelectedValueChanged += new System.EventHandler(this.OnValueChanged_Type);
			// 
			// labelDesiredError
			// 
			this.labelDesiredError.AutoSize = true;
			this.labelDesiredError.Location = new System.Drawing.Point(10, 405);
			this.labelDesiredError.Name = "labelDesiredError";
			this.labelDesiredError.Size = new System.Drawing.Size(110, 21);
			this.labelDesiredError.TabIndex = 9;
			this.labelDesiredError.Text = "Desired error:";
			// 
			// labelEta
			// 
			this.labelEta.AutoSize = true;
			this.labelEta.Location = new System.Drawing.Point(10, 369);
			this.labelEta.Name = "labelEta";
			this.labelEta.Size = new System.Drawing.Size(118, 21);
			this.labelEta.TabIndex = 8;
			this.labelEta.Text = "Learning rate:";
			// 
			// labelType
			// 
			this.labelType.AutoSize = true;
			this.labelType.Location = new System.Drawing.Point(10, 333);
			this.labelType.Name = "labelType";
			this.labelType.Size = new System.Drawing.Size(192, 21);
			this.labelType.TabIndex = 7;
			this.labelType.Text = "Backpropagation type:";
			// 
			// buttonAddLayer
			// 
			this.buttonAddLayer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonAddLayer.Location = new System.Drawing.Point(328, 276);
			this.buttonAddLayer.Name = "buttonAddLayer";
			this.buttonAddLayer.Size = new System.Drawing.Size(155, 29);
			this.buttonAddLayer.TabIndex = 6;
			this.buttonAddLayer.Text = "Add layer";
			this.buttonAddLayer.UseVisualStyleBackColor = true;
			this.buttonAddLayer.Click += new System.EventHandler(this.ButtonAddLayer_Click);
			// 
			// labelArchitecture
			// 
			this.labelArchitecture.AutoSize = true;
			this.labelArchitecture.Location = new System.Drawing.Point(10, 244);
			this.labelArchitecture.Name = "labelArchitecture";
			this.labelArchitecture.Size = new System.Drawing.Size(151, 21);
			this.labelArchitecture.TabIndex = 4;
			this.labelArchitecture.Text = "ANN architexture:";
			// 
			// errorChart
			// 
			chartArea2.Name = "ChartArea1";
			this.errorChart.ChartAreas.Add(chartArea2);
			this.errorChart.Location = new System.Drawing.Point(114, 0);
			this.errorChart.Name = "errorChart";
			this.errorChart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Pastel;
			series2.ChartArea = "ChartArea1";
			series2.Name = "SymbolError";
			this.errorChart.Series.Add(series2);
			this.errorChart.Size = new System.Drawing.Size(411, 194);
			this.errorChart.TabIndex = 3;
			this.errorChart.Text = "Per character error";
			title2.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
			title2.Name = "Title1";
			title2.Text = "Per character error";
			this.errorChart.Titles.Add(title2);
			// 
			// Train
			// 
			this.Train.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Train.Location = new System.Drawing.Point(214, 460);
			this.Train.Name = "Train";
			this.Train.Size = new System.Drawing.Size(207, 48);
			this.Train.TabIndex = 1;
			this.Train.Text = "Train";
			this.Train.UseVisualStyleBackColor = true;
			this.Train.Click += new System.EventHandler(this.Train_Click);
			// 
			// GoToTest
			// 
			this.GoToTest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.GoToTest.Location = new System.Drawing.Point(214, 460);
			this.GoToTest.Name = "GoToTest";
			this.GoToTest.Size = new System.Drawing.Size(207, 48);
			this.GoToTest.TabIndex = 2;
			this.GoToTest.Text = "Test neural network";
			this.GoToTest.UseVisualStyleBackColor = true;
			this.GoToTest.Visible = false;
			this.GoToTest.Click += new System.EventHandler(this.GoToTest_Click);
			// 
			// panelResult
			// 
			this.panelResult.Controls.Add(this.resultClasses);
			this.panelResult.Controls.Add(this.labelClass);
			this.panelResult.Controls.Add(this.labelClassStatic);
			this.panelResult.Controls.Add(this.Test);
			this.panelResult.Location = new System.Drawing.Point(175, 38);
			this.panelResult.Name = "panelResult";
			this.panelResult.Size = new System.Drawing.Size(625, 562);
			this.panelResult.TabIndex = 0;
			this.panelResult.Visible = false;
			this.panelResult.VisibleChanged += new System.EventHandler(this.TestPanel_Visible);
			// 
			// resultClasses
			// 
			this.resultClasses.ColumnCount = 2;
			this.resultClasses.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.resultClasses.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.resultClasses.Location = new System.Drawing.Point(125, 266);
			this.resultClasses.Name = "resultClasses";
			this.resultClasses.RowCount = 2;
			this.resultClasses.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.resultClasses.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.resultClasses.Size = new System.Drawing.Size(400, 67);
			this.resultClasses.TabIndex = 6;
			// 
			// labelClass
			// 
			this.labelClass.AutoSize = true;
			this.labelClass.Location = new System.Drawing.Point(270, 377);
			this.labelClass.Name = "labelClass";
			this.labelClass.Size = new System.Drawing.Size(0, 21);
			this.labelClass.TabIndex = 5;
			// 
			// labelClassStatic
			// 
			this.labelClassStatic.AutoSize = true;
			this.labelClassStatic.Location = new System.Drawing.Point(211, 377);
			this.labelClassStatic.Name = "labelClassStatic";
			this.labelClassStatic.Size = new System.Drawing.Size(53, 21);
			this.labelClassStatic.TabIndex = 4;
			this.labelClassStatic.Text = "Class:";
			// 
			// Test
			// 
			this.Test.Enabled = false;
			this.Test.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Test.Location = new System.Drawing.Point(214, 460);
			this.Test.Name = "Test";
			this.Test.Size = new System.Drawing.Size(207, 48);
			this.Test.TabIndex = 0;
			this.Test.Text = "Test character";
			this.Test.UseVisualStyleBackColor = true;
			this.Test.Click += new System.EventHandler(this.Test_Click);
			// 
			// Main
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
			this.ClientSize = new System.Drawing.Size(800, 600);
			this.Controls.Add(this.panelTrainSet);
			this.Controls.Add(this.panelTrain);
			this.Controls.Add(this.panelResult);
			this.Controls.Add(this.panelSlider);
			this.Controls.Add(this.titleBar);
			this.Controls.Add(this.progressionPanel);
			this.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Margin = new System.Windows.Forms.Padding(5);
			this.Name = "Main";
			this.Text = "Main";
			this.Load += new System.EventHandler(this.Main_Load);
			this.progressionPanel.ResumeLayout(false);
			this.titleBar.ResumeLayout(false);
			this.panelTrainSet.ResumeLayout(false);
			this.panelTrainSet.PerformLayout();
			this.panelFunction.ResumeLayout(false);
			this.panelFunction.PerformLayout();
			this.panelTrain.ResumeLayout(false);
			this.panelTrain.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.errorChart)).EndInit();
			this.panelResult.ResumeLayout(false);
			this.panelResult.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private FlowLayoutPanel progressionPanel;
		private Button buttonExit;
		private Panel titleBar;
		private Panel panelIcon;
		private Button buttonTrainSet;
		private Button buttonTrain;
		private Button buttonResult;
		private Panel panelSlider;
		private Panel panelTrainSet;
		private Panel panelTrain;
		private Button Train;
		private Panel panelResult;
		private Button buttonCreateTrainSet;
		private Button GoToTest;
		private Button Test;
		private Label labelOr;
		private Label separator;
		private Button buttonLoadTrainSet;
		private ComboBox loadTrainSet;
		private Label labelLoadTrainSet;
		private Label labelClass;
		private Label labelClassStatic;
		private System.Windows.Forms.DataVisualization.Charting.Chart errorChart;
		private TextBox textBoxDesiredError;
		private TextBox textBoxEta;
		private ComboBox comboBoxType;
		private Label labelDesiredError;
		private Label labelEta;
		private Label labelType;
		private Button buttonAddLayer;
		private Label labelArchitecture;
		private TableLayoutPanel layoutArchitexture;
		private Button buttonRemoveLayer;
		private Label labelTotalError;
		private Label labelTotalErrorStatic;
		private TableLayoutPanel resultClasses;
		private Panel panelFunction;
		private Label labelFunction;
		private Button buttonFunction;
		private ComboBox comboBoxFunction;
	}
}

