using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DynamicControlDemo.Controls;

namespace DynamicControlDemo
{
    public partial class Parent : Form
    {
        #region Private Fields

        private readonly List<Child> _childList;
        private const string Zero = "0";

        #endregion

        #region Public Methods

        /// <summary>
        /// The constructor.
        /// </summary>
        public Parent()
        {
            InitializeComponent();

            this._childList = new List<Child>();
            this.lblTotalCount.Text = Zero;
        }

        /// <summary>
        /// Sum up all the counts from the child classes.
        /// </summary>
        public void GetTotal()
        {
            var sum = this._childList.Sum(child => child.Count);

            if (this.lblTotalCount.InvokeRequired)
            {
                this.lblTotalCount.Invoke(new MethodInvoker(delegate
                {
                    this.lblTotalCount.Text = sum.ToString();
                }));
            }
            else
            {
                this.lblTotalCount.Text = sum.ToString();
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Add a child control.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The event args.
        /// </param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            var child = new Child(this);

            this.flpChildContainer.Controls.Add(child);
            this._childList.Add(child);
        }

        #endregion
    }
}
