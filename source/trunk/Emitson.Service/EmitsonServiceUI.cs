using Emitson.Service.Configuration;
using Figlut.Server.Toolkit.Utilities;
using Figlut.Server.Toolkit.Utilities.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Emitson.Service
{
    public partial class EmitsonServiceUI : Form
    {
        public EmitsonServiceUI()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            EmitsonServiceSettings settings = GOC.Instance.GetSettings<EmitsonServiceSettings>(true, true);
            EmitsonServiceApplication.Instance.Initialize(settings);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            GOC.Instance.GetByTypeName<ServiceHost>().Close();
            GOC.Instance.Logger.LogMessage(new LogMessage(
                "Emitson Service stopped.",
                LogMessageType.Information,
                LoggingLevel.Minimum));
        }
    }
}
