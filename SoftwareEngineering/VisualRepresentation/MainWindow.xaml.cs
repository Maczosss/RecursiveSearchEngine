using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using VisualRepresentation.ViewModels;

namespace VisualRepresentation
{
    public partial class MainWindow : Window
    {
        public MainViewModel mainWindowViewModel;
        public MainWindow()
        {
            InitializeComponent();
            mainWindowViewModel = this.DataContext as MainViewModel;
        }
        void ButtonClick(object sender, RoutedEventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                if (dialog.ShowDialog(this.GetIWin32Window()) == System.Windows.Forms.DialogResult.OK)
                {
                    Console.WriteLine(dialog.SelectedPath);
                }
            }
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
