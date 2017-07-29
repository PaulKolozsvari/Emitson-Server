namespace Emitson.Service
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.EmitsonServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.EmitsonServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // EmitsonServiceProcessInstaller
            // 
            this.EmitsonServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.EmitsonServiceProcessInstaller.Password = null;
            this.EmitsonServiceProcessInstaller.Username = null;
            // 
            // EmitsonServiceInstaller
            // 
            this.EmitsonServiceInstaller.Description = "Web Service for interacting with music players.";
            this.EmitsonServiceInstaller.DisplayName = "Emitson Service";
            this.EmitsonServiceInstaller.ServiceName = "EmitsonService";
            this.EmitsonServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.EmitsonServiceProcessInstaller,
            this.EmitsonServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller EmitsonServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller EmitsonServiceInstaller;
    }
}