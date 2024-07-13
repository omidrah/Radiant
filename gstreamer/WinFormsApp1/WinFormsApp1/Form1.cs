using Gst;
using System.Diagnostics;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        int scenario = 0;
        private Pipeline pipeline;
        private Element playbin;
        private string mediaUri = string.Empty;
        private string ip=string.Empty;
        private int port = 5000;
        private TraceSource logger;

        public Form1()
        {
            InitializeComponent();
            InitializeLogger();
            InitializeGStreamer();

        }
        private void InitializeLogger()
        {
            logger = new TraceSource("GStreamerMediaPlayerLogger");
            logger.Switch = new SourceSwitch("sourceSwitch", "Verbose");
            logger.Listeners.Add(new ConsoleTraceListener());
            logger.Listeners.Add(new TextWriterTraceListener("GStreamerMediaPlayer.log", "myListener"));
            logger.Listeners["myListener"].TraceOutputOptions = TraceOptions.DateTime;
        }
        private void InitializeGStreamer()
        {
            Gst.Application.Init();
        }
        private void LogMessage(string message)
        {
            logger.TraceEvent(TraceEventType.Information, 0, message);
        }
        private void OnMessage(object o, MessageArgs args)
        {
            var msg = args.Message;

            switch (msg.Type)
            {
                case MessageType.Error:
                    msg.ParseError(out var err, out var debug);
                    MessageBox.Show($"Error received from element {msg.Src.Name}: {err.Message}\nDebugging information: {debug ?? "none"}");
                    break;

                case MessageType.Eos:
                    MessageBox.Show("End-Of-Stream reached.");
                    break;

                default:
                    break;
            }
        }

        private void btn_play_Click(object sender, EventArgs e)
        {
            LogMessage("Play button clicked.");
            var typeStream = "localfile";
            switch (scenario)
            {
                case 1:
                    typeStream = "localfile";
                    if (pipeline == null)
                    {
                        pipeline = new Pipeline("pipeline");
                        playbin = ElementFactory.Make("playbin", "playbin");
                        if (playbin == null)
                        {
                            MessageBox.Show("Playbin could not be created. Exiting.");
                            Environment.Exit(1);
                        }

                        pipeline.Add(playbin);
                        var bus = pipeline.Bus;
                        bus.AddSignalWatch();
                        bus.Message += OnMessage;
                    }
                    if (string.IsNullOrEmpty(mediaUri))
                    {
                        MessageBox.Show("Please upload a media file first.");
                        return;
                    }
                    else
                    {
                        //playbin["uri"] = mediaUri; or below code
                        playbin.SetProperty("uri", new GLib.Value(mediaUri));
                        pipeline.SetState(State.Playing);
                        statusLabel.Text = "Playing";
                    }
                    break;
                case 2:
                    typeStream = "ReadFromIPAndPort";
                    if (pipeline == null)
                    {
                        pipeline = new Pipeline("pipeline");
                        var udpsrc = ElementFactory.Make("udpsrc", "udpsrc");
                        var caps = Caps.FromString("application/x-rtp, media=(string)video, clock-rate=(int)90000, encoding-name=(string)H264");
                        udpsrc.SetProperty("caps", new GLib.Value(caps));
                        //set ip and port
                        udpsrc.SetProperty("port",new GLib.Value(port) );
                        udpsrc.SetProperty("address",new GLib.Value(ip));
                        pipeline.Add(udpsrc);

                        playbin = ElementFactory.Make("playbin", "playbin");
                        pipeline.Add(playbin);
                        // Link udpsrc to playbin for playback
                        var decodebin = ElementFactory.Make("decodebin", "decodebin");
                        pipeline.Add(decodebin);
                        udpsrc.Link(decodebin);
                        decodebin.Link(playbin);
                    }
                    pipeline.SetState(State.Playing);
                    statusLabel.Text = "Playing";
                    break;
                case 3:
                    typeStream = "ReadFromUrlLiveStream";
                    if (pipeline == null)
                    {
                        pipeline = new Pipeline("pipeline");
                        playbin = ElementFactory.Make("playbin", "playbin");
                        if (playbin == null)
                        {
                            MessageBox.Show("Playbin could not be created. Exiting.");
                            Environment.Exit(1);
                        }

                        pipeline.Add(playbin);
                        var bus = pipeline.Bus;
                        bus.AddSignalWatch();
                        bus.Message += OnMessage;
                    }
                    if (string.IsNullOrEmpty(mediaUri))
                    {
                        MessageBox.Show("Please upload a media file first.");
                        return;
                    }
                    else
                    {
                        //playbin["uri"] = mediaUri; or below code
                        playbin.SetProperty("uri", new GLib.Value(mediaUri));
                        pipeline.SetState(State.Playing);
                        statusLabel.Text = "Playing";
                    }
                    break;
            }
        }

        private void btn_pause_Click(object sender, EventArgs e)
        {
            LogMessage("Pause button clicked.");
            pipeline.SetState(State.Paused);
            statusLabel.Text = "Paused";
        }

        private void btn_stop_Click(object sender, EventArgs e)
        {
            LogMessage("Stop button clicked.");
            pipeline.SetState(State.Ready);
            statusLabel.Text = "Stopped";
        }
        /// <summary>
        /// only local video format 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_upload_Click(object sender, EventArgs e)
        {
            scenario = 1;
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Media files|*.mp4;*.mp3;*.avi;*.mkv;*.mov;*.wmv;*.flv|All files|*.*";
                openFileDialog.Title = "Select a media file";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    mediaUri = "file:///" + openFileDialog.FileName.Replace("\\", "/");
                    statusLabel.Text = "File uploaded: " + openFileDialog.FileName;
                    LogMessage("file upload successfully");
                }
            }
        }
        /// <summary>
        /// read streaming ip and port
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNetworkConfigFrm_Click(object sender, EventArgs e)
        {
            scenario = 2;
            using (frmNetwork networkStreamForm = new frmNetwork())
            {
                if (networkStreamForm.ShowDialog() == DialogResult.OK)
                {
                    ip = networkStreamForm.IpAddress;
                    port = networkStreamForm.Port;                   
                    LogMessage("Ip and Port set Successfully.");
                }
            }
        }

        private void btnPlayByUrl_Click(object sender, EventArgs e)
        {
            scenario = 3;
            using (UrlConfigForm urlStreamForm = new UrlConfigForm())
            {
                if (urlStreamForm.ShowDialog() == DialogResult.OK)
                {
                    string url = urlStreamForm.UrlStreaming;
                    mediaUri = $"{url}";
                    LogMessage("Live Url streaming set Succesfully");
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
