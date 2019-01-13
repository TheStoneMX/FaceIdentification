
namespace BioTechSys.Face.Enroll.Forms
{
    partial class dudeHuellasFound
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Infragistics.Win.UltraWinDock.DockAreaPane dockAreaPane1 = new Infragistics.Win.UltraWinDock.DockAreaPane(Infragistics.Win.UltraWinDock.DockedLocation.DockedRight, new System.Guid("9135902e-19f4-4fd0-9656-6b919ed5ba6d"));
            Infragistics.Win.UltraWinDock.DockableControlPane dockableControlPane1 = new Infragistics.Win.UltraWinDock.DockableControlPane(new System.Guid("3a86d238-c322-4f2f-ac9c-d7bd9e36f72a"), new System.Guid("00000000-0000-0000-0000-000000000000"), -1, new System.Guid("9135902e-19f4-4fd0-9656-6b919ed5ba6d"), -1);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dudeHuellasFound));
            this.lbxFoundDudes = new BioTechSys.Controls.ListBoxImage();
            this._DateRegistered = new Infragistics.Win.UltraWinMaskedEdit.UltraMaskedEdit();
            this.ultraLabel8 = new Infragistics.Win.Misc.UltraLabel();
            this.ultraLabel7 = new Infragistics.Win.Misc.UltraLabel();
            this._txtNCP = new Infragistics.Win.UltraWinMaskedEdit.UltraMaskedEdit();
            this._txtSecondLastName = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this._txtFirstLastName = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this._txtNames = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this._fechaNacimiento = new Infragistics.Win.UltraWinMaskedEdit.UltraMaskedEdit();
            this.ultraLabel5 = new Infragistics.Win.Misc.UltraLabel();
            this.ultraLabel4 = new Infragistics.Win.Misc.UltraLabel();
            this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
            this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
            this._btnClose = new Infragistics.Win.Misc.UltraButton();
            this.DockManager = new Infragistics.Win.UltraWinDock.UltraDockManager(this.components);
            this._dudeHuellasFoundUnpinnedTabAreaLeft = new Infragistics.Win.UltraWinDock.UnpinnedTabArea();
            this._dudeHuellasFoundUnpinnedTabAreaRight = new Infragistics.Win.UltraWinDock.UnpinnedTabArea();
            this._dudeHuellasFoundUnpinnedTabAreaTop = new Infragistics.Win.UltraWinDock.UnpinnedTabArea();
            this._dudeHuellasFoundUnpinnedTabAreaBottom = new Infragistics.Win.UltraWinDock.UnpinnedTabArea();
            this._dudeHuellasFoundAutoHideControl = new Infragistics.Win.UltraWinDock.AutoHideControl();
            this.windowDockingArea1 = new Infragistics.Win.UltraWinDock.WindowDockingArea();
            this.dockableWindow1 = new Infragistics.Win.UltraWinDock.DockableWindow();
            ((System.ComponentModel.ISupportInitialize)(this._txtSecondLastName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._txtFirstLastName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._txtNames)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DockManager)).BeginInit();
            this.windowDockingArea1.SuspendLayout();
            this.dockableWindow1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbxFoundDudes
            // 
            this.lbxFoundDudes.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.lbxFoundDudes.ColumnWidth = 150;
            this.lbxFoundDudes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbxFoundDudes.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbxFoundDudes.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.lbxFoundDudes.FormattingEnabled = true;
            this.lbxFoundDudes.Images = null;
            this.lbxFoundDudes.ItemHeight = 100;
            this.lbxFoundDudes.Location = new System.Drawing.Point(0, 26);
            this.lbxFoundDudes.Name = "lbxFoundDudes";
            this.lbxFoundDudes.ScrollAlwaysVisible = true;
            this.lbxFoundDudes.Size = new System.Drawing.Size(279, 501);
            this.lbxFoundDudes.TabIndex = 78;
            this.lbxFoundDudes.UseTabStops = false;
            this.lbxFoundDudes.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lbxFoundDudes_MouseDoubleClick);
            // 
            // _DateRegistered
            // 
            this._DateRegistered.EditAs = Infragistics.Win.UltraWinMaskedEdit.EditAsType.Date;
            this._DateRegistered.InputMask = "{LOC}mm/dd/yyyy";
            this._DateRegistered.Location = new System.Drawing.Point(172, 95);
            this._DateRegistered.Name = "_DateRegistered";
            this._DateRegistered.ReadOnly = true;
            this._DateRegistered.Size = new System.Drawing.Size(214, 20);
            this._DateRegistered.TabIndex = 72;
            this._DateRegistered.Text = "//";
            // 
            // ultraLabel8
            // 
            this.ultraLabel8.AutoSize = true;
            this.ultraLabel8.Location = new System.Drawing.Point(28, 95);
            this.ultraLabel8.Name = "ultraLabel8";
            this.ultraLabel8.Size = new System.Drawing.Size(103, 14);
            this.ultraLabel8.TabIndex = 71;
            this.ultraLabel8.Text = "Fecha de Registro :";
            // 
            // ultraLabel7
            // 
            this.ultraLabel7.AutoSize = true;
            this.ultraLabel7.Location = new System.Drawing.Point(28, 130);
            this.ultraLabel7.Name = "ultraLabel7";
            this.ultraLabel7.Size = new System.Drawing.Size(34, 14);
            this.ultraLabel7.TabIndex = 70;
            this.ultraLabel7.Text = "NCP :";
            // 
            // _txtNCP
            // 
            this._txtNCP.EditAs = Infragistics.Win.UltraWinMaskedEdit.EditAsType.UseSpecifiedMask;
            this._txtNCP.InputMask = ">A<AAAAAAAAAAAA";
            this._txtNCP.Location = new System.Drawing.Point(172, 127);
            this._txtNCP.Name = "_txtNCP";
            this._txtNCP.PromptChar = '-';
            this._txtNCP.ReadOnly = true;
            this._txtNCP.Size = new System.Drawing.Size(214, 20);
            this._txtNCP.TabIndex = 56;
            this._txtNCP.TextRenderingMode = Infragistics.Win.TextRenderingMode.GDIPlus;
            // 
            // _txtSecondLastName
            // 
            this._txtSecondLastName.DisplayStyle = Infragistics.Win.EmbeddableElementDisplayStyle.Office2007;
            this._txtSecondLastName.Location = new System.Drawing.Point(172, 219);
            this._txtSecondLastName.Name = "_txtSecondLastName";
            this._txtSecondLastName.ReadOnly = true;
            this._txtSecondLastName.Size = new System.Drawing.Size(214, 21);
            this._txtSecondLastName.TabIndex = 60;
            // 
            // _txtFirstLastName
            // 
            this._txtFirstLastName.DisplayStyle = Infragistics.Win.EmbeddableElementDisplayStyle.Office2007;
            this._txtFirstLastName.Location = new System.Drawing.Point(172, 185);
            this._txtFirstLastName.Name = "_txtFirstLastName";
            this._txtFirstLastName.ReadOnly = true;
            this._txtFirstLastName.Size = new System.Drawing.Size(214, 21);
            this._txtFirstLastName.TabIndex = 59;
            // 
            // _txtNames
            // 
            this._txtNames.DisplayStyle = Infragistics.Win.EmbeddableElementDisplayStyle.Office2007;
            this._txtNames.Location = new System.Drawing.Point(172, 157);
            this._txtNames.Name = "_txtNames";
            this._txtNames.ReadOnly = true;
            this._txtNames.Size = new System.Drawing.Size(214, 21);
            this._txtNames.TabIndex = 57;
            // 
            // _fechaNacimiento
            // 
            this._fechaNacimiento.EditAs = Infragistics.Win.UltraWinMaskedEdit.EditAsType.Date;
            this._fechaNacimiento.InputMask = "{LOC}mm/dd/yyyy";
            this._fechaNacimiento.Location = new System.Drawing.Point(172, 246);
            this._fechaNacimiento.Name = "_fechaNacimiento";
            this._fechaNacimiento.ReadOnly = true;
            this._fechaNacimiento.Size = new System.Drawing.Size(214, 20);
            this._fechaNacimiento.TabIndex = 61;
            this._fechaNacimiento.Text = "//";
            // 
            // ultraLabel5
            // 
            this.ultraLabel5.AutoSize = true;
            this.ultraLabel5.Location = new System.Drawing.Point(28, 246);
            this.ultraLabel5.Name = "ultraLabel5";
            this.ultraLabel5.Size = new System.Drawing.Size(117, 14);
            this.ultraLabel5.TabIndex = 68;
            this.ultraLabel5.Text = "Fecha de Nacimiento :";
            // 
            // ultraLabel4
            // 
            this.ultraLabel4.AutoSize = true;
            this.ultraLabel4.Location = new System.Drawing.Point(28, 219);
            this.ultraLabel4.Name = "ultraLabel4";
            this.ultraLabel4.Size = new System.Drawing.Size(92, 14);
            this.ultraLabel4.TabIndex = 67;
            this.ultraLabel4.Text = "Apellido Materno:";
            // 
            // ultraLabel3
            // 
            this.ultraLabel3.AutoSize = true;
            this.ultraLabel3.Location = new System.Drawing.Point(28, 192);
            this.ultraLabel3.Name = "ultraLabel3";
            this.ultraLabel3.Size = new System.Drawing.Size(94, 14);
            this.ultraLabel3.TabIndex = 66;
            this.ultraLabel3.Text = "Apellido Paterno :";
            // 
            // ultraLabel1
            // 
            this.ultraLabel1.AutoSize = true;
            this.ultraLabel1.Location = new System.Drawing.Point(28, 164);
            this.ultraLabel1.Name = "ultraLabel1";
            this.ultraLabel1.Size = new System.Drawing.Size(56, 14);
            this.ultraLabel1.TabIndex = 64;
            this.ultraLabel1.Text = "Nombres :";
            // 
            // _btnClose
            // 
            this._btnClose.AutoSize = true;
            this._btnClose.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Office2007RibbonButton;
            this._btnClose.Location = new System.Drawing.Point(295, 288);
            this._btnClose.Name = "_btnClose";
            this._btnClose.Size = new System.Drawing.Size(91, 24);
            this._btnClose.TabIndex = 77;
            this._btnClose.Text = "Cerrar Ventana";
            this._btnClose.UseOsThemes = Infragistics.Win.DefaultableBoolean.True;
            this._btnClose.Click += new System.EventHandler(this._btnClose_Click);
            // 
            // DockManager
            // 
            this.DockManager.AnimationSpeed = Infragistics.Win.UltraWinDock.AnimationSpeed.StandardSpeedPlus4;
            this.DockManager.AutoHideDelay = 300;
            dockableControlPane1.Control = this.lbxFoundDudes;
            dockableControlPane1.OriginalControlBounds = new System.Drawing.Rectangle(872, 0, 323, 527);
            dockableControlPane1.Size = new System.Drawing.Size(100, 100);
            dockableControlPane1.Text = "Huellas de Candidatos";
            dockAreaPane1.Panes.AddRange(new Infragistics.Win.UltraWinDock.DockablePaneBase[] {
            dockableControlPane1});
            dockAreaPane1.Size = new System.Drawing.Size(279, 527);
            this.DockManager.DockAreas.AddRange(new Infragistics.Win.UltraWinDock.DockAreaPane[] {
            dockAreaPane1});
            this.DockManager.HostControl = this;
            this.DockManager.ShowCloseButton = false;
            this.DockManager.WindowStyle = Infragistics.Win.UltraWinDock.WindowStyle.Office2003;
            // 
            // _dudeHuellasFoundUnpinnedTabAreaLeft
            // 
            this._dudeHuellasFoundUnpinnedTabAreaLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this._dudeHuellasFoundUnpinnedTabAreaLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._dudeHuellasFoundUnpinnedTabAreaLeft.Location = new System.Drawing.Point(0, 0);
            this._dudeHuellasFoundUnpinnedTabAreaLeft.Name = "_dudeHuellasFoundUnpinnedTabAreaLeft";
            this._dudeHuellasFoundUnpinnedTabAreaLeft.Owner = this.DockManager;
            this._dudeHuellasFoundUnpinnedTabAreaLeft.Size = new System.Drawing.Size(0, 527);
            this._dudeHuellasFoundUnpinnedTabAreaLeft.TabIndex = 79;
            // 
            // _dudeHuellasFoundUnpinnedTabAreaRight
            // 
            this._dudeHuellasFoundUnpinnedTabAreaRight.Dock = System.Windows.Forms.DockStyle.Right;
            this._dudeHuellasFoundUnpinnedTabAreaRight.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._dudeHuellasFoundUnpinnedTabAreaRight.Location = new System.Drawing.Point(700, 0);
            this._dudeHuellasFoundUnpinnedTabAreaRight.Name = "_dudeHuellasFoundUnpinnedTabAreaRight";
            this._dudeHuellasFoundUnpinnedTabAreaRight.Owner = this.DockManager;
            this._dudeHuellasFoundUnpinnedTabAreaRight.Size = new System.Drawing.Size(0, 527);
            this._dudeHuellasFoundUnpinnedTabAreaRight.TabIndex = 80;
            // 
            // _dudeHuellasFoundUnpinnedTabAreaTop
            // 
            this._dudeHuellasFoundUnpinnedTabAreaTop.Dock = System.Windows.Forms.DockStyle.Top;
            this._dudeHuellasFoundUnpinnedTabAreaTop.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._dudeHuellasFoundUnpinnedTabAreaTop.Location = new System.Drawing.Point(0, 0);
            this._dudeHuellasFoundUnpinnedTabAreaTop.Name = "_dudeHuellasFoundUnpinnedTabAreaTop";
            this._dudeHuellasFoundUnpinnedTabAreaTop.Owner = this.DockManager;
            this._dudeHuellasFoundUnpinnedTabAreaTop.Size = new System.Drawing.Size(700, 0);
            this._dudeHuellasFoundUnpinnedTabAreaTop.TabIndex = 81;
            // 
            // _dudeHuellasFoundUnpinnedTabAreaBottom
            // 
            this._dudeHuellasFoundUnpinnedTabAreaBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._dudeHuellasFoundUnpinnedTabAreaBottom.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._dudeHuellasFoundUnpinnedTabAreaBottom.Location = new System.Drawing.Point(0, 527);
            this._dudeHuellasFoundUnpinnedTabAreaBottom.Name = "_dudeHuellasFoundUnpinnedTabAreaBottom";
            this._dudeHuellasFoundUnpinnedTabAreaBottom.Owner = this.DockManager;
            this._dudeHuellasFoundUnpinnedTabAreaBottom.Size = new System.Drawing.Size(700, 0);
            this._dudeHuellasFoundUnpinnedTabAreaBottom.TabIndex = 82;
            // 
            // _dudeHuellasFoundAutoHideControl
            // 
            this._dudeHuellasFoundAutoHideControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._dudeHuellasFoundAutoHideControl.Location = new System.Drawing.Point(0, 0);
            this._dudeHuellasFoundAutoHideControl.Name = "_dudeHuellasFoundAutoHideControl";
            this._dudeHuellasFoundAutoHideControl.Owner = this.DockManager;
            this._dudeHuellasFoundAutoHideControl.Size = new System.Drawing.Size(0, 0);
            this._dudeHuellasFoundAutoHideControl.TabIndex = 83;
            // 
            // windowDockingArea1
            // 
            this.windowDockingArea1.Controls.Add(this.dockableWindow1);
            this.windowDockingArea1.Dock = System.Windows.Forms.DockStyle.Right;
            this.windowDockingArea1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.windowDockingArea1.Location = new System.Drawing.Point(416, 0);
            this.windowDockingArea1.Name = "windowDockingArea1";
            this.windowDockingArea1.Owner = this.DockManager;
            this.windowDockingArea1.Size = new System.Drawing.Size(284, 527);
            this.windowDockingArea1.TabIndex = 84;
            // 
            // dockableWindow1
            // 
            this.dockableWindow1.Controls.Add(this.lbxFoundDudes);
            this.dockableWindow1.Location = new System.Drawing.Point(5, 0);
            this.dockableWindow1.Name = "dockableWindow1";
            this.dockableWindow1.Owner = this.DockManager;
            this.dockableWindow1.Size = new System.Drawing.Size(279, 527);
            this.dockableWindow1.TabIndex = 85;
            // 
            // dudeHuellasFound
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(700, 527);
            this.Controls.Add(this._dudeHuellasFoundAutoHideControl);
            this.Controls.Add(this._btnClose);
            this.Controls.Add(this._DateRegistered);
            this.Controls.Add(this.ultraLabel8);
            this.Controls.Add(this.ultraLabel7);
            this.Controls.Add(this._txtNCP);
            this.Controls.Add(this._txtSecondLastName);
            this.Controls.Add(this._txtFirstLastName);
            this.Controls.Add(this._txtNames);
            this.Controls.Add(this._fechaNacimiento);
            this.Controls.Add(this.ultraLabel5);
            this.Controls.Add(this.ultraLabel4);
            this.Controls.Add(this.ultraLabel3);
            this.Controls.Add(this.ultraLabel1);
            this.Controls.Add(this.windowDockingArea1);
            this.Controls.Add(this._dudeHuellasFoundUnpinnedTabAreaTop);
            this.Controls.Add(this._dudeHuellasFoundUnpinnedTabAreaBottom);
            this.Controls.Add(this._dudeHuellasFoundUnpinnedTabAreaLeft);
            this.Controls.Add(this._dudeHuellasFoundUnpinnedTabAreaRight);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "dudeHuellasFound";
            this.Load += new System.EventHandler(this.dude_Load);
            ((System.ComponentModel.ISupportInitialize)(this._txtSecondLastName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._txtFirstLastName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._txtNames)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DockManager)).EndInit();
            this.windowDockingArea1.ResumeLayout(false);
            this.dockableWindow1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Infragistics.Win.UltraWinMaskedEdit.UltraMaskedEdit _DateRegistered;
        private Infragistics.Win.Misc.UltraLabel ultraLabel8;
        private Infragistics.Win.Misc.UltraLabel ultraLabel7;
        private Infragistics.Win.UltraWinMaskedEdit.UltraMaskedEdit _txtNCP;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor _txtSecondLastName;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor _txtFirstLastName;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor _txtNames;
        private Infragistics.Win.UltraWinMaskedEdit.UltraMaskedEdit _fechaNacimiento;
        private Infragistics.Win.Misc.UltraLabel ultraLabel5;
        private Infragistics.Win.Misc.UltraLabel ultraLabel4;
        private Infragistics.Win.Misc.UltraLabel ultraLabel3;
        private Infragistics.Win.Misc.UltraLabel ultraLabel1;
        private Infragistics.Win.Misc.UltraButton _btnClose;
        private global::BioTechSys.Controls.ListBoxImage lbxFoundDudes;
        private Infragistics.Win.UltraWinDock.UltraDockManager DockManager;
        private Infragistics.Win.UltraWinDock.AutoHideControl _dudeHuellasFoundAutoHideControl;
        private Infragistics.Win.UltraWinDock.WindowDockingArea windowDockingArea1;
        private Infragistics.Win.UltraWinDock.DockableWindow dockableWindow1;
        private Infragistics.Win.UltraWinDock.UnpinnedTabArea _dudeHuellasFoundUnpinnedTabAreaTop;
        private Infragistics.Win.UltraWinDock.UnpinnedTabArea _dudeHuellasFoundUnpinnedTabAreaBottom;
        private Infragistics.Win.UltraWinDock.UnpinnedTabArea _dudeHuellasFoundUnpinnedTabAreaLeft;
        private Infragistics.Win.UltraWinDock.UnpinnedTabArea _dudeHuellasFoundUnpinnedTabAreaRight;
    }
}