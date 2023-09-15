using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mixer_control_globalver.Controller
{
    public class CountDownTimer : IDisposable
    {
        public Stopwatch _stpWatch = new Stopwatch();

        public Action TimeChanged;
        public Action CountDownFinished;

        public bool IsRunning => timer.Enabled;

        public int StepMs
        {
            get => timer.Interval;
            set => timer.Interval = value;
        }

        private Timer timer = new Timer();

        private TimeSpan _max = TimeSpan.FromSeconds(30);

        public TimeSpan TimeLeft => (_max.TotalSeconds - Math.Round(Convert.ToDouble(_stpWatch.ElapsedMilliseconds / 1000))) > 0 ? TimeSpan.FromSeconds(_max.TotalSeconds - Math.Round(Convert.ToDouble(_stpWatch.ElapsedMilliseconds / 1000))) : TimeSpan.FromSeconds(0);

        private bool _mustStop => (_max.TotalSeconds - Math.Round(Convert.ToDouble(_stpWatch.ElapsedMilliseconds / 1000))) < 0;

        public string TimeLeftStr => TimeLeft.ToString(@"hh\:mm\:ss");

        private void TimerTick(object sender, EventArgs e)
        {
            TimeChanged?.Invoke();

            if (_mustStop)
            {
                CountDownFinished?.Invoke();
                _stpWatch.Stop();
                timer.Enabled = false;
            }
        }

        public CountDownTimer(int min, int sec)
        {
            SetTime(min, sec);
            Init();
        }

        public CountDownTimer(TimeSpan ts)
        {
            SetTime(ts);
            Init();
        }

        public CountDownTimer()
        {
            Init();
        }

        private void Init()
        {
            StepMs = 1000;
            timer.Tick += new EventHandler(TimerTick);
        }

        public void SetTime(TimeSpan ts)
        {
            _max = ts;
            TimeChanged?.Invoke();
        }

        public void SetTime(int min, int sec = 0) => SetTime(TimeSpan.FromSeconds(min * 60 + sec));

        public void Start()
        {
            timer.Start();
            _stpWatch.Start();
        }

        public void Pause()
        {
            timer.Stop();
            _stpWatch.Stop();
        }
        public void Continue()
        {
            timer.Start();
            _stpWatch.Start();
        }

        public void Delete()
        {
            _stpWatch.Stop();
            _stpWatch.Reset();
            timer.Enabled = false;
        }

        public void Dispose() => timer.Dispose();
    }
}
