using mixer_control_globalver.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mixer_control_globalver.View.CustomComponent
{
    public partial class AppIntro : Form
    {
        public static MainWindow main = new MainWindow();
        static Image[] images;
        int frameCount = 0;
        Timer t1 = new Timer();
        Timer t2 = new Timer();
        System.Timers.Timer timer = new System.Timers.Timer();
        public AppIntro()
        {
            InitializeComponent();
            Opacity = 0;      //first the opacity is 0
            t1.Interval = 20;  //we'll increase the opacity every 10ms
            t1.Tick += new EventHandler(fadeIn);  //this calls the function that changes opacity 
            t1.Start();
        }
        void fadeIn(object sender, EventArgs e)
        {
            if (Opacity >= 1)
                t1.Stop();   //this stops the timer if the form is completely displayed
            else
                Opacity += 0.05;
        }
        #region LoadIntro
        Image[] getFrames(Image originalImg)
        {
            int numberOfFrames = originalImg.GetFrameCount(FrameDimension.Time);
            Image[] frames = new Image[numberOfFrames];

            for (int i = 0; i < numberOfFrames; i++)
            {
                originalImg.SelectActiveFrame(FrameDimension.Time, i);
                frames[i] = ((Image)originalImg.Clone());
            }
            return frames;
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            pbxLogoAnimate.Image = images[frameCount];
            frameCount++;
            if (frameCount > images.Length - 1)
            {
                frameCount = 0;
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            t2.Stop();
            this.Hide();
            
            main.ShowDialog();
            this.Close();
            this.Dispose();
        }
       
        private void AppIntro_Load(object sender, EventArgs e)
        {
            string introName = "techlinkGifIntro";
            object techlinkIntro = Resources.ResourceManager.GetObject(introName); // Doi intro bang ten trong resource
            images = getFrames((Image)techlinkIntro);

            timer.Interval = 60;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
            t2.Interval = images.Length * 60;
            t2.Tick += Timer_Tick;
            t2.Start();
        }

        #endregion
    }
}
