using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSR_System
{
    class DataContainerDelivery
    {
        public event EventHandler AcceptedChanges;
        protected virtual void OnAcceptedChnages()
        {
            if ((this.AcceptedChanges != null))
            {
                this.AcceptedChanges(this, EventArgs.Empty);
            }
        }

        public void AcceptChanges()
        {
            this.OnAcceptedChnages();
        }

        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }
}
