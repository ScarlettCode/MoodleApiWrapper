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
using RestSharp;
using RestSharp.Authenticators;
using MoodleApiWrapper;

namespace CastingWithCSharp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

             //ApiWrapper.ApiToken = "dabe95abf7e2bdf5a9633a2de16b4ac2";
            //ApiWrapper.ApiToken = "dabe95abf7e2bdf5a9633aac2";
            ApiWrapper.Host = new Uri("http://detussenschool.nl/elo/");
            


            dostuff();
        }

        public async void dostuff()
        {
            var token = await ApiWrapper.GetApiToken("tm", "MK3$tpthggguhdwu", "services");
           
            var stuffisdone = "";
        }
   
        

    }


}
