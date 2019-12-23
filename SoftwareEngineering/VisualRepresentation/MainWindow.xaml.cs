using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Forms;
using VisualRepresentation.ViewModels;
using TextBox = System.Windows.Controls.TextBox;
using Microsoft.Msagl.GraphViewerGdi;
using Microsoft.Msagl.Drawing;
using VisualRepresentation.Models;

namespace VisualRepresentation
{
    public partial class MainWindow : Window
    {
        public MainViewModel mainWindowViewModel;
        //create a viewer object 
        public GViewer viewer { get; set; }
        //changed to property, initialized only on construction and 'stays alive' througout timespan of application.
        //GViewer viewer = new GViewer();

        public MainWindow()
        {
            InitializeComponent();
            viewer = new GViewer();
            mainWindowViewModel = this.DataContext as MainViewModel;

        }
       
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void btnZoomIn_Click(object sender, RoutedEventArgs e)
        {
            viewer.ZoomInPressed();
        }
        private void btnZoomOut_Click(object sender, RoutedEventArgs e)
        {
            viewer.ZoomOutPressed();
        }
        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            viewer.BackwardButtonPressed();
        }    
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            viewer.SaveButtonPressed();
        }
        void ChooseFolderToGraphClick(object sender, RoutedEventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                if (dialog.ShowDialog(this.GetIWin32Window()) == System.Windows.Forms.DialogResult.OK)
                {
                    mainWindowViewModel.PathToFolder = dialog.SelectedPath;
                }
            }
            FilesPathsModels filesPathModel = new FilesPathsModels(mainWindowViewModel.PathToFolder);
            mainWindowViewModel.FoundCsFiles = filesPathModel.GetFiles();
        }

        private void GenerateGraph(object sender, RoutedEventArgs e)
        {
            //create a graph object 
            var graph = new Graph("graph");
            
            //create the graph content 
            graph.AddEdge("A", "B");
            graph.AddEdge("B", "C");
            graph.AddEdge("A", "C").Attr.Color = Microsoft.Msagl.Drawing.Color.Green;
            graph.FindNode("A").Attr.FillColor = Microsoft.Msagl.Drawing.Color.Magenta;
            graph.FindNode("B").Attr.FillColor = Microsoft.Msagl.Drawing.Color.MistyRose;
            Microsoft.Msagl.Drawing.Node c = graph.FindNode("C");
            c.Attr.FillColor = Microsoft.Msagl.Drawing.Color.PaleGreen;
            c.Attr.Shape = Microsoft.Msagl.Drawing.Shape.Diamond;
            graph.AddNode("X");
            graph.AddNode("X");
            var nonNode = graph.FindNode("Dupeczka");
            //bind the graph to the viewer 
            viewer.Graph = graph;
            
            
            //viewer.Dock = System.Windows.Forms.DockStyle.Fill;

            // Assign the control as the host control's child.
            graphCanvas.Child = viewer;

            //Hide toolbar
            //viewer.ToolBarIsVisible = false;

            //Make pan button on by default which enables to moving around the graph and zooming it by scroll button
            //viewer.PanButtonPressed = true;            
        }

        #region Select all text after clicking textbox
        private void SelectAddress(object sender, RoutedEventArgs e)
        {
            TextBox tb = (sender as TextBox);
            if (tb != null)
            {
                tb.SelectAll();
            }
        }
        private void SelectivelyIgnoreMouseButton(object sender, MouseButtonEventArgs e)
        {
            TextBox tb = (sender as TextBox);
            if (tb != null)
            {
                if (!tb.IsKeyboardFocusWithin)
                {
                    e.Handled = true;
                    tb.Focus();
                }
            }
        }
        #endregion

        private void GraphCanvas_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }

        private void btnGenerateGraph_Click(object sender, MouseButtonEventArgs e)
        {
            //GenerateGraph(sender, e);
            var graphModels = new GraphModels();
            var graph = graphModels.GenerateGraph(true, true, true, mainWindowViewModel.FoundCsFiles);
            viewer.Graph = graph;
            graphCanvas.Child = viewer;
            //no action if FoundFIles is empty or invalid
            //var graph = GenerateGraph(bool story1, bool story2, bool story3, VMFoundFiles)

            //viewer.Graph = graph;
            //graphCanvas.Child = viewer;
        }
    }
    public static class WinFormsCompatibility
    {
        public static IWin32Window GetIWin32Window(this Window window)
        {
            return new Win32Window(new System.Windows.Interop.WindowInteropHelper(window).Handle);
        }
    
        class Win32Window : IWin32Window
        { 
            public Win32Window(IntPtr handle)
            {
                Handle = handle;
            }
            public IntPtr Handle { get; }
        }
    }
}
