using Jint;
using Newtonsoft.Json;
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

namespace JavascriptInterpreterApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            textBox1.Text = "a + b - 1";
            textBox.Text =
@"{
    ""a"": 5,
    ""b"": 8,
    ""c"": ""Hello "",
    ""d"": {
        ""e"": ""WORLD""
    },
    ""f"": [ 1, 2, 3 ],
}";
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            var javascriptEngine = new Engine();
            var tokens = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(textBox.Text);
            List<int> ints = new List<int> { 1, 2, 3 };
            errorTextBlock.Text = "";
            textBlock.Text = "";

            foreach (var key in tokens.Keys)
            {
                var value = tokens[key];
                javascriptEngine.SetValue(key, value);
            }

            javascriptEngine.SetValue("arrayTest", ints);

            try
            {
                var result = javascriptEngine.Execute(textBox1.Text)
                    .GetCompletionValue();
                textBlock.Text = result?.ToObject()?.ToString() ?? null;
            }
            catch (Exception exception)
            {
                errorTextBlock.Text = exception.Message;
            }
        }
    }
}
