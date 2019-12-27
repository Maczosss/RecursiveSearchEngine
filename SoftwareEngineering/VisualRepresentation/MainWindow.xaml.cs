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
        
        public GViewer Viewer { get; set; }
        //changed to property, initialized only on construction and 'stays alive' througout timespan of application.
        //GViewer viewer = new GViewer();
        public FilesPathsModels PathModel { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Viewer = new GViewer();
            mainWindowViewModel = this.DataContext as MainViewModel;

        }
       
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnZoomIn_Click(object sender, RoutedEventArgs e)
        {
            Viewer.ZoomInPressed();
        }

        private void btnZoomOut_Click(object sender, RoutedEventArgs e)
        {
            Viewer.ZoomOutPressed();
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            Viewer.BackwardButtonPressed();
        }    

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Viewer.SaveButtonPressed();
        }

        private void ChooseFolderToGraphClick(object sender, RoutedEventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                if (dialog.ShowDialog(this.GetIWin32Window()) == System.Windows.Forms.DialogResult.OK)
                {
                    mainWindowViewModel.PathToFolder = dialog.SelectedPath;
                }
            }
            
            this.PathModel = new FilesPathsModels(mainWindowViewModel.PathToFolder);
            mainWindowViewModel.FoundCsFiles = PathModel.GetFiles();
            
        }

        private void btnGenerateGraph_Click(object sender, MouseButtonEventArgs e)
        {
            if (this.PathModel.HasFolderAnyCsFiles())
            {
                var graphModels = new GraphModels();
                
                var graph = graphModels.GenerateGraph(true, true, true, mainWindowViewModel.FoundCsFiles);
                Viewer.CurrentLayoutMethod = LayoutMethod.MDS;
                Viewer.Graph = graph;
                //TODO: GenerateGRaph() returns viewer and replaces prop Viewer. Move "CurrentLaoytMethod" to bour buttons or even better: dropdawn list. 
                Viewer.CurrentLayoutMethod = LayoutMethod.MDS;
                graphCanvas.Child = Viewer;
                
                //Hide toolbar
                //viewer.ToolBarIsVisible = false;

                //Make pan button on by default which enables to moving around the graph and zooming it by scroll button
                //viewer.PanButtonPressed = true; 
            }
            else
            {
                MessageBoxResult result = System.Windows.MessageBox.Show("Selected folder contains no *.cs files.\n Do You want to choose different folder?",
                                          "Invalid Folder",
                                          MessageBoxButton.OKCancel,
                                          MessageBoxImage.Warning);
                if (result == MessageBoxResult.OK)
                {
                    this.ChooseFolderToGraphClick(sender, e);
                }
            }       
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
