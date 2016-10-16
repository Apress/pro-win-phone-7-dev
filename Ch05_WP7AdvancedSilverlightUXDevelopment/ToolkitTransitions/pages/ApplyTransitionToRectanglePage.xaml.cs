using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace ToolkitTransitions
{
    public partial class ApplyTransitionToRectanglePage : PhoneApplicationPage
    {
        public  ApplyTransitionToRectanglePage()
        {
            InitializeComponent();
        }

        private void ApplyTransitionAppBarBtn_Click(object sender, EventArgs e)
        {
          RotateTransition rotateTransition = 
            new RotateTransition { Mode = RotateTransitionMode.In180Clockwise};

          ITransition transition = rotateTransition.GetTransition(targetRectangle);

          transition.Completed += 
            (s, eventarg) => { transition.Stop(); targetRectangle.Opacity = 1; };

          transition.Begin();
        }
    }
}
