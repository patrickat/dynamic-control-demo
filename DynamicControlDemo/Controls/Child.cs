using System;
using System.Configuration;
using System.Windows.Forms;
using Timer = System.Timers.Timer;

namespace DynamicControlDemo.Controls
{
    public partial class Child : UserControl
    {
        #region Public Properties

        // Getter/setter for the count.
        public int Count
        {
            get
            {
                return _count;
            }
            set
            {
                this._count = value;

                this.lblCount.Invoke(new MethodInvoker(delegate
                {
                    this.lblCount.Text = this._count.ToString();
                }));

                this._mainForm.GetTotal();
            }
        }

        #endregion

        #region Private Fields

        private int _count;
        private Timer _timer;
        private readonly Parent _mainForm;

        private const int StartCount = 0;

        private readonly int _downInterval = Convert.ToInt32(ConfigurationManager.AppSettings["Down Interval"]);
        private readonly int _upInterval = Convert.ToInt32(ConfigurationManager.AppSettings["Up Interval"]);
        private readonly int _subtractValue = Convert.ToInt32(ConfigurationManager.AppSettings["Subtract Value"]);
        private readonly int _addValue = Convert.ToInt32(ConfigurationManager.AppSettings["Add Value"]);

        #endregion

        #region Public Methods

        /// <summary>
        /// Constructor for the child class.
        /// </summary>
        /// <param name="parent">
        /// The parent form.
        /// </param>
        public Child(Parent parent)
        {
            InitializeComponent();

            this._mainForm = parent;
            this._count = StartCount;
            this.lblCount.Text = this._count.ToString();
            this._timer = null;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// When the up or down button is clicked, start the timer.
        /// </summary>
        /// <param name="sender">
        /// The up or down button.
        /// </param>
        /// <param name="e">
        /// The event args.
        /// </param>
        private void btn_Click(object sender, EventArgs e)
        {
            var button = sender as Button;

            if (button != null)
            {
                this.AddOrSubtractButton(button.Equals(this.btnUp));
            }
        }

        /// <summary>
        /// Add n to the count.
        /// </summary>
        /// <param name="n">
        /// n to add to the count.
        /// </param>
        private void AddToCount(int n)
        {
            this.Count += n;
        }

        /// <summary>
        /// Stop the timer.
        /// </summary>
        private void StopTimer()
        {
            // If the timer exists, stop it.
            this._timer?.Stop();
            this._timer = null;
        }

        /// <summary>
        /// Start adding or subtracting based on the which button was clicked.
        /// </summary>
        /// <param name="isUp">
        /// True represents that the up button was clicked.
        /// </param>
        private void AddOrSubtractButton(bool isUp)
        {
            // Get the appropriate values for the button.
            var value = this._addValue;
            var interval = this._upInterval;

            if (!isUp)
            {
                value = -this._subtractValue;
                interval = this._downInterval;
            }

            this.btnUp.Enabled = !isUp;
            this.btnDown.Enabled = isUp;

            // Prepare the timer.
            this.StopTimer();

            this._timer = new Timer
            {
                Enabled = true,
                Interval = interval
            };

            this._timer.Elapsed += delegate { this.AddToCount(value); };

            // Start the timer.
            this._timer.Start();
        }

        #endregion

        
    }
}
