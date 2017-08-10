using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Lor_o_Tune
{
    class groupBoxArray : System.Collections.CollectionBase
    {

        //private readonly System.Windows.Forms.Form HostForm;
        
        /*
        public groupBoxArray(System.Windows.Forms.Form host)
        {
           HostForm = host;
           this.AddNewGroupBox();
        }
        */

        public System.Windows.Forms.ComboBox AddNewGroupBox();
        {
            // Create a new instance of the GroupBox class.
            System.Windows.Forms.Button gBox = new System.Windows.Forms.GroupBox();
            // Add the button to the collection's internal list.
            this.List.Add(gBox);
            // Add the button to the controls collection of the form 
            // referenced by the HostForm field.
            hostForm.Controls.Add(gBox);
            // Set intial properties for the button object.
            gBox.Tag = this.Count;
        }
    }
}
