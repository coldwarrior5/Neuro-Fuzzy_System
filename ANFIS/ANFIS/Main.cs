using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using ANFIS.ANN;
using ANFIS.Handlers.Error;
using ANFIS.Handlers.GUI;
using ANFIS.Handlers.IO;
using ANFIS.Handlers.Mathematics;
using ANFIS.Structures;

namespace ANFIS
{
	public partial class Main : Form
	{
		private Mover _screenMover;
		private Parser _parser;
		private Instance _instance;
		private List<Panel> _panels;
		
		private NeuralNetwork _ann;

		public Main()
		{
			InitializeComponent();
			_parser = new Parser();
			_screenMover = new Mover();
			_panels = new List<Panel> { panelTrainSet, panelTrain, panelResult };
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			Location = Owner.Location;
			Size = Owner.Size;
		}

		private void Main_Load(object sender, EventArgs e)
		{
		}

		private void SetNeuralNetwork()
		{
			if(_ann is null)
				_ann = new NeuralNetwork(_instance);
			else if(!_ann.Consistent())
				_ann = new NeuralNetwork(_instance);
		}

		private void SetTexts()
		{
			for (int i = 0; i < layoutArchitexture.Controls.Count; i++)
			{
				if (layoutArchitexture.Controls[i] is TextBox)
				{
					layoutArchitexture.Controls[i].KeyDown += LayerNumber_Changed;
					layoutArchitexture.Controls[i].Leave += LayerNumber_Left;
				}
					
			}
		}
		// ______________________________________________________________

		//
		// Exit button
		//
		private void ButtonExit_Click(object sender, EventArgs e)
		{
			ErrorHandler.TerminateExecution(ErrorCode.UserTermination);
		}

		private void ButtonExit_MouseEnter(object sender, EventArgs e)
		{
			if (sender is Button btn) btn.BackgroundImage = Properties.Resources.ExitHighlighted;
		}

		private void ButtonExit_MouseLeave(object sender, EventArgs e)
		{
			if (sender is Button btn) btn.BackgroundImage = Properties.Resources.Exit;
		}
		// ______________________________________________________________

		//
		// Titlebar
		//
		private void Titlebar_MouseDown(object sender, MouseEventArgs e)
		{
			_screenMover.MouseDown(e.Location);
		}

		private void Titlebar_MouseMove(object sender, MouseEventArgs e)
		{
			var moved = _screenMover.MouseMove(e.Location, Location, out Point newLocation);
			if (moved)
				Location = newLocation;
			Update();
		}

		private void Titlebar_MouseUp(object sender, MouseEventArgs e)
		{
			_screenMover.MouseUp();
		}
		// ______________________________________________________________

		//
		// Train set panel
		//
		private void ParamsPanel_Visible(object sender, EventArgs e)
		{
			if (!(sender is Panel panel)) return;
			if (!panel.Visible) return;
			InputParams.FillTrainSetChoices(loadTrainSet, comboBoxFunction);
			buttonLoadTrainSet.Enabled = loadTrainSet.SelectedItem != null;
		}
		
		private void CreateTrainSet_Click(object sender, EventArgs e)
		{
			panelFunction.Visible = true;
		}

		private void ButtonFunction_Click(object sender, EventArgs e)
		{
			Sampler.HardcodedSampling(Functions.GetFunction(comboBoxFunction.SelectedItem.ToString()), out _instance, _parser);
			InputParams.FillTrainSetChoices(loadTrainSet, comboBoxFunction);
			buttonLoadTrainSet.Enabled = loadTrainSet.SelectedItem != null;
			panelFunction.Visible = false;
		}

		private void ButtonLoadTrainSet_Click(object sender, EventArgs e)
		{
			_instance = _parser.ParseData(loadTrainSet.Text);
			UiHandler.SetSlider(panelSlider, buttonTrain.Top, buttonTrain.Height);
			buttonTrain.Enabled = true;
			UiHandler.PanelVisible(panelTrain, _panels);
		}

		private void ButtonTrainSet_Click(object sender, EventArgs e)
		{
			UiHandler.SetSlider(panelSlider, buttonTrainSet.Top, buttonTrainSet.Height);
			InputParams.FillTrainSetChoices(loadTrainSet, comboBoxFunction);
			buttonTrain.Enabled = false;
			buttonResult.Enabled = false;
			UiHandler.PanelVisible(panelTrainSet, _panels);
		}
		// ______________________________________________________________


		//
		// Train panel
		//
		private void TrainPanel_Visible(object sender, EventArgs e)
		{
			if (!(sender is Panel panel)) return;
			if (!panel.Visible) return;
			SetNeuralNetwork();
			NeuralNetwork.FillTrainChoices(errorChart, labelTotalError, layoutArchitexture, comboBoxType, textBoxEta, textBoxDesiredError, _ann);
			SetTexts();
		}

		private void LayerNumber_Changed(object sender, KeyEventArgs e)
		{
			if (!(sender is TextBox t)) return;

			switch (e.KeyCode)
			{
				case Keys.Enter:
					var splits = t.Name.Split('_');
					int.TryParse(splits[1], out int whichLayer);
					int.TryParse(t.Text, out int howMany);
					howMany = MathHandler.Clamp(howMany, NeuralNetwork.NeuronMin, NeuralNetwork.NeuronMax);
					t.Text = howMany.ToString();
					_ann.UpdateLayer(whichLayer, howMany);
					break;
				case Keys.Escape:
					splits = t.Name.Split('_');
					int.TryParse(splits[1], out whichLayer);
					t.Text = _ann.GetArchitecture()[whichLayer].ToString();
					break;
			}
			
		}

		private void LayerNumber_Left(object sender, EventArgs e)
		{
			if (!(sender is TextBox t)) return;
			
			var splits = t.Name.Split('_');
			int.TryParse(splits[1], out int whichLayer);
			int.TryParse(t.Text, out int howMany);
			howMany = MathHandler.Clamp(howMany, NeuralNetwork.NeuronMin, NeuralNetwork.NeuronMax);
			t.Text = howMany.ToString();
			_ann.UpdateLayer(whichLayer, howMany);
		}

		private void ButtonRemoveLayer_Click(object sender, EventArgs e)
		{
			buttonAddLayer.Enabled = true;
			_ann.RemoveLayer();
			NeuralNetwork.FillPanel(layoutArchitexture, _ann);
			SetTexts();
			if (_ann.GetArchitecture().Count == NeuralNetwork.LayersMin)
				buttonRemoveLayer.Enabled = false;
		}

		private void ButtonAddLayer_Click(object sender, EventArgs e)
		{
			buttonRemoveLayer.Enabled = true;
			_ann.AddLayer();
			NeuralNetwork.FillPanel(layoutArchitexture, _ann);
			SetTexts();
			if (_ann.GetArchitecture().Count == NeuralNetwork.LayersMax)
				buttonAddLayer.Enabled = false;
		}

		private void OnValueChanged_Type(object sender, EventArgs e)
		{
			_ann?.OnValueChanged_Type(sender, e);
		}

		private void Eta_Changed(object sender, KeyEventArgs e)
		{
			if (!(sender is TextBox t)) return;

			switch (e.KeyCode)
			{
				case Keys.Enter:
					double.TryParse(t.Text, out double eta);
					eta = MathHandler.Clamp(eta, NeuralNetwork.EtaMin, NeuralNetwork.EtaMax);
					t.Text = eta.ToString(CultureInfo.InvariantCulture);
					_ann.ChangeEta(eta);
					break;
				case Keys.Escape:
					t.Text = _ann.GetEta().ToString(CultureInfo.InvariantCulture);
					break;
			}
		}

		private void Eta_Left(object sender, EventArgs e)
		{
			if (!(sender is TextBox t)) return;
			
			double.TryParse(t.Text, out double eta);
			eta = MathHandler.Clamp(eta, NeuralNetwork.EtaMin, NeuralNetwork.EtaMax);
			t.Text = eta.ToString(CultureInfo.InvariantCulture);
			_ann.ChangeEta(eta);
		}

		private void DesiredError_Changed(object sender, KeyEventArgs e)
		{
			if (!(sender is TextBox t)) return;

			switch(e.KeyCode)
			{
				case Keys.Enter:
					double.TryParse(t.Text, out double desiredError);
					desiredError = MathHandler.Clamp(desiredError, NeuralNetwork.DesiredErrorMin, NeuralNetwork.DesiredErrorMax);
					t.Text = desiredError.ToString(CultureInfo.InvariantCulture);
					_ann.ChangeDesiredError(desiredError);
					break;
				case Keys.Escape:
					t.Text = _ann.GetDesiredError().ToString(CultureInfo.InvariantCulture);
					break;
			}
		}

		private void DesiredError_Left(object sender, EventArgs e)
		{
			if (!(sender is TextBox t)) return;

			double.TryParse(t.Text, out double desiredError);
			desiredError = MathHandler.Clamp(desiredError, NeuralNetwork.DesiredErrorMin, NeuralNetwork.DesiredErrorMax);
			t.Text = desiredError.ToString(CultureInfo.InvariantCulture);
			_ann.ChangeDesiredError(desiredError);
		}

		private void Train_Click(object sender, EventArgs e)
		{
			Train.Visible = false;
			GoToTest.Visible = true;
			_ann.Train(errorChart, labelTotalError, GoToTest);
			NeuralNetwork.FillChart(errorChart, labelTotalError, _ann);
		}

		private void GoToTest_Click(object sender, EventArgs e)
		{
			Train.Visible = true;
			GoToTest.Visible = false;
			GoToTest.Enabled = false;
			_ann.FixArchitecture();
			UiHandler.PanelVisible(panelResult, _panels);
			buttonResult.Enabled = true;
			UiHandler.SetSlider(panelSlider, buttonResult.Top, buttonResult.Height);
		}

		private void ButtonTrain_Click(object sender, EventArgs e)
		{
			_ann?.ResetNetwork();
			_ann?.ResetTraining();
			Train.Visible = true;
			GoToTest.Visible = false;
			UiHandler.SetSlider(panelSlider, buttonTrain.Top, buttonTrain.Height);
			buttonResult.Enabled = false;
			UiHandler.PanelVisible(panelTrain, _panels);
		}
		// ______________________________________________________________

		//
		// Result panel
		//
		private void TestPanel_Visible(object sender, EventArgs e)
		{
			if (!(sender is Panel panel)) return;
			if (!panel.Visible)
			{
				return;
			}
			labelClass.Text = "";
		}

		private void Test_Click(object sender, EventArgs e)
		{
			
		}

		private void ButtonTest_Click(object sender, EventArgs e)
		{
			Test.Enabled = false;
			UiHandler.SetSlider(panelSlider, buttonResult.Top, buttonResult.Height);
			UiHandler.PanelVisible(panelResult, _panels);
		}
		
		// ______________________________________________________________
	}
}
