namespace TestGraph
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnPanel = new System.Windows.Forms.Panel();
            this.btnAddMoyenne = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.fondGraph = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.valueMoyenneMobile = new System.Windows.Forms.TextBox();
            this.btnPanel.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnPanel
            // 
            this.btnPanel.Controls.Add(this.panel2);
            this.btnPanel.Controls.Add(this.btnAddMoyenne);
            this.btnPanel.Controls.Add(this.panel1);
            resources.ApplyResources(this.btnPanel, "btnPanel");
            this.btnPanel.Name = "btnPanel";
            // 
            // btnAddMoyenne
            // 
            resources.ApplyResources(this.btnAddMoyenne, "btnAddMoyenne");
            this.btnAddMoyenne.Name = "btnAddMoyenne";
            this.btnAddMoyenne.UseVisualStyleBackColor = true;
            this.btnAddMoyenne.Click += new System.EventHandler(this.btnAddMoyenne_Click);
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // fondGraph
            // 
            resources.ApplyResources(this.fondGraph, "fondGraph");
            this.fondGraph.Name = "fondGraph";
            this.fondGraph.Paint += new System.Windows.Forms.PaintEventHandler(this.fondGraph_Paint);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.valueMoyenneMobile);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // valueMoyenneMobile
            // 
            resources.ApplyResources(this.valueMoyenneMobile, "valueMoyenneMobile");
            this.valueMoyenneMobile.Name = "valueMoyenneMobile";
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.fondGraph);
            this.Controls.Add(this.btnPanel);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.btnPanel.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel btnPanel;
        private System.Windows.Forms.Panel fondGraph;
        private System.Windows.Forms.Button btnAddMoyenne;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox valueMoyenneMobile;
    }
}

