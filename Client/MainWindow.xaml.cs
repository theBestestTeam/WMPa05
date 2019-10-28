using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Sockets;
using Server.Domain;
using System.ServiceModel;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            ConnectClient("127.0.0.1", "Hello, SET World!");
        }

        private MainWindow _window;
        public MainWindow MainView { get; set; }
        private readonly SolidColorBrush[] userBackground = new SolidColorBrush[4];
        private User _user;

        public MainWindow(MainWindow window, User user)
        {
            InitializeComponent();
            this._window = window;
            _user = user;
            _window.Width = 540;
            _window.Height = 400;

            _window.Background = new SolidColorBrush();

            //Opening 4 new window instances? Need to make it random if the user wants to open up new chat boxes
            userBackground[0] = new SolidColorBrush(Color.FromArgb(223, 108, 41, 239));
            userBackground[1] = new SolidColorBrush(Color.FromArgb(223, 239, 41, 210));
            userBackground[2] = new SolidColorBrush(Color.FromArgb(223, 73, 44, 130));
            userBackground[3] = new SolidColorBrush(Color.FromArgb(223, 115, 36, 103));

        }

        static void ConnectClient(String server, String message)
        {
            try
            {
                // Create a TcpClient.
                // Note, for this client to work you need to have a TcpServer 
                // connected to the same address as specified by the server, port
                // combination.
                Int32 port = 13000;
                TcpClient client = new TcpClient(server, port);

                // Translate the passed message into ASCII and store it as a Byte array.
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

                // Get a client stream for reading and writing.
                //  Stream stream = client.GetStream();

                NetworkStream stream = client.GetStream();

                // Send the message to the connected TcpServer. 
                stream.Write(data, 0, data.Length);

                Console.WriteLine("Sent: {0}", message);

                // Receive the TcpServer.response.

                // Buffer to store the response bytes.
                data = new Byte[256];

                // String to store the response ASCII representation.
                String responseData = String.Empty;

                // Read the first batch of the TcpServer response bytes.
                Int32 bytes = stream.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                Console.WriteLine("Received: {0}", responseData);

                // Close everything.
                stream.Close();
                client.Close();
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }

            Console.WriteLine("\n Press Enter to continue...");
            Console.Read();
        }

        private void connectButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(Username.Text))
            {
                User user = new User
                {
                    TimeCreated = DateTime.UtcNow,
                    Name = Username.Text
                };

                _window.MainView = new MainWindow(_window, user);
                var uri = "net.tcp://localhost:6565/MessageService";
                //var callBack = new InstanceContext(new MessageServiceCallBack());
                var binding = new NetTcpBinding(SecurityMode.None);
                //var channel = new DuplexChannelFactory<IMessageService>(callBack, binding);
                var endPoint = new EndpointAddress(uri);
                //var proxy = channel.CreateChannel(endPoint);
                //proxy?.Connect(user);
                
                _window.Main.Children.Clear();
                _window.Main.Children.Add(_window.MainView);

            }
        }

        //public void DisplayMessage(Server.CompositeType composite)
        //{

        //}

        //private void textBoxEntryField_KeyDown(object sender, KeyEventArgs e)
        //{

        //}
    }
}