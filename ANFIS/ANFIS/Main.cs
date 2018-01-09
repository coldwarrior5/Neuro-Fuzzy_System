using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using ANFIS.ANN;
using ANFIS.Handlers.Error;
using ANFIS.Handlers.GUI;
using ANFIS.Handlers.IO;
using ANFIS.Handlers.Mathematics;
using ANFIS.Structures;
using Point = System.Drawing.Point;

namespace ANFIS
{
	public partial class Main : Form
	{
		public BackgroundWorker Worker;
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
			if(_ann is null || !_ann.Instance.Equals(_instance))
				_ann = new NeuralNetwork(_instance, ilPanelFunctions, labelTotalError);
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
			if (!(_ann is null))
				_ann.ForcedStop = true;
		}
		// ______________________________________________________________


		//
		// Train panel
		//
		private void TrainPanel_Visible(object sender, EventArgs e)
		{
			if (!(sender is Panel panel)) return;
			if (!panel.Visible) return;

			Train.Visible = true;
			GoToResults.Visible = false;
			SetNeuralNetwork();
			NeuralNetwork.FillTrainChoices(ilPanelFunctions, labelTotalError, textBoxRules, comboBoxType, textBoxEta, textBoxDesiredError, _ann);
		}

		private void Rules_Changed(object sender, KeyEventArgs e)
		{
			_ann.NumberOfRules = UiHandler.TextBoxChanged(sender, e, _ann.NumberOfRules, NeuralNetwork.RulesMin, NeuralNetwork.RulesMax);
		}

		private void Rules_Left(object sender, EventArgs e)
		{
			_ann.NumberOfRules = UiHandler.TextBoxLeft(sender, e, _ann.NumberOfRules, NeuralNetwork.RulesMin, NeuralNetwork.RulesMax);
		}

		private void OnValueChanged_Type(object sender, EventArgs e)
		{
			_ann?.OnValueChanged_Type(sender, e);
		}

		private void Eta_Changed(object sender, KeyEventArgs e)
		{
			_ann.Eta = UiHandler.TextBoxChanged(sender, e, _ann.Eta, NeuralNetwork.EtaMin, NeuralNetwork.EtaMax);
		}

		private void Eta_Left(object sender, EventArgs e)
		{
			_ann.Eta = UiHandler.TextBoxLeft(sender, e, _ann.Eta, NeuralNetwork.EtaMin, NeuralNetwork.EtaMax);
		}

		private void DesiredError_Changed(object sender, KeyEventArgs e)
		{
			_ann.DesiredError = UiHandler.TextBoxChanged(sender, e, _ann.DesiredError, NeuralNetwork.DesiredErrorMin, NeuralNetwork.DesiredErrorMax);
		}

		private void DesiredError_Left(object sender, EventArgs e)
		{
			_ann.DesiredError = UiHandler.TextBoxLeft(sender, e, _ann.DesiredError, NeuralNetwork.DesiredErrorMin, NeuralNetwork.DesiredErrorMax);
		}

		private void Train_Click(object sender, EventArgs e)
		{
			Train.Visible = false;
			buttonStop.Visible = true;
			Worker worker = new Worker(_ann, Train, buttonStop, GoToResults);
			worker.Start();
		}

		private void buttonStop_Click(object sender, EventArgs e)
		{
			_ann.Stop = true;
		}

		private void GoToResult_Click(object sender, EventArgs e)
		{
			Train.Visible = true;
			GoToResults.Visible = false;
			UiHandler.PanelVisible(panelResult, _panels);
			buttonResult.Enabled = true;
			UiHandler.SetSlider(panelSlider, buttonResult.Top, buttonResult.Height);
		}

		private void ButtonTrain_Click(object sender, EventArgs e)
		{
			if (!(_ann is null))
				_ann.ForcedStop = true;
			Train.Visible = true;
			buttonStop.Visible = false;
			GoToResults.Visible = false;
			_ann?.ResetTraining();
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
			if (!panel.Visible) return;
			textBoxSaveName.Text = @"Default";
			NeuralNetwork.FillResults(ilPanelResults, panelFuzzySets, _ann);
		}

		private void Save_Click(object sender, EventArgs e)
		{
			panelSave.Visible = true;
		}
		
		private void buttonCancel_Click(object sender, EventArgs e)
		{
			panelSave.Visible = false;
		}

		private void buttonSaveResults_Click(object sender, EventArgs e)
		{
			panelSave.Visible = false;
			_parser.FormatAndSaveResult(textBoxSaveName.Text, _ann.ErrorTimeline, _instance);
		}

		private void ButtonTest_Click(object sender, EventArgs e)
		{
			UiHandler.SetSlider(panelSlider, buttonResult.Top, buttonResult.Height);
			UiHandler.PanelVisible(panelResult, _panels);
		}
		// ______________________________________________________________
	}
}
