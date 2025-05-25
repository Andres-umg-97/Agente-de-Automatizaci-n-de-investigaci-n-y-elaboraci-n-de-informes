namespace Agente_de_Automatización_de_investigación
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTema = new System.Windows.Forms.Label();
            this.txtPrompt = new System.Windows.Forms.TextBox();
            this.txtResultado = new System.Windows.Forms.TextBox();
            this.btBuscar = new System.Windows.Forms.Button();
            this.btLimpiar = new System.Windows.Forms.Button();
            this.btPresentacion = new System.Windows.Forms.Button();
            this.btDocumento = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTema
            // 
            this.lblTema.AutoSize = true;
            this.lblTema.Location = new System.Drawing.Point(88, 164);
            this.lblTema.Name = "lblTema";
            this.lblTema.Size = new System.Drawing.Size(43, 16);
            this.lblTema.TabIndex = 0;
            this.lblTema.Text = "Tema";
            // 
            // txtPrompt
            // 
            this.txtPrompt.Location = new System.Drawing.Point(146, 158);
            this.txtPrompt.Name = "txtPrompt";
            this.txtPrompt.Size = new System.Drawing.Size(581, 22);
            this.txtPrompt.TabIndex = 1;
            // 
            // txtResultado
            // 
            this.txtResultado.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtResultado.Location = new System.Drawing.Point(146, 237);
            this.txtResultado.Multiline = true;
            this.txtResultado.Name = "txtResultado";
            this.txtResultado.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResultado.Size = new System.Drawing.Size(581, 198);
            this.txtResultado.TabIndex = 2;
            // 
            // btBuscar
            // 
            this.btBuscar.Location = new System.Drawing.Point(146, 479);
            this.btBuscar.Name = "btBuscar";
            this.btBuscar.Size = new System.Drawing.Size(96, 44);
            this.btBuscar.TabIndex = 3;
            this.btBuscar.Text = "Buscar";
            this.btBuscar.UseVisualStyleBackColor = true;
            this.btBuscar.Click += new System.EventHandler(this.btBuscar_Click);
            // 
            // btLimpiar
            // 
            this.btLimpiar.Location = new System.Drawing.Point(309, 479);
            this.btLimpiar.Name = "btLimpiar";
            this.btLimpiar.Size = new System.Drawing.Size(96, 44);
            this.btLimpiar.TabIndex = 4;
            this.btLimpiar.Text = "Limpiar";
            this.btLimpiar.UseVisualStyleBackColor = true;
            this.btLimpiar.Click += new System.EventHandler(this.btLimpiar_Click);
            // 
            // btPresentacion
            // 
            this.btPresentacion.Location = new System.Drawing.Point(146, 550);
            this.btPresentacion.Name = "btPresentacion";
            this.btPresentacion.Size = new System.Drawing.Size(96, 44);
            this.btPresentacion.TabIndex = 5;
            this.btPresentacion.Text = "Generar presentacion";
            this.btPresentacion.UseVisualStyleBackColor = true;
            this.btPresentacion.Click += new System.EventHandler(this.btPresentacion_Click);
            // 
            // btDocumento
            // 
            this.btDocumento.Location = new System.Drawing.Point(309, 550);
            this.btDocumento.Name = "btDocumento";
            this.btDocumento.Size = new System.Drawing.Size(96, 44);
            this.btDocumento.TabIndex = 6;
            this.btDocumento.Text = "Generar documento";
            this.btDocumento.UseVisualStyleBackColor = true;
            this.btDocumento.Click += new System.EventHandler(this.btDocumento_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(997, 643);
            this.Controls.Add(this.btDocumento);
            this.Controls.Add(this.btPresentacion);
            this.Controls.Add(this.btLimpiar);
            this.Controls.Add(this.btBuscar);
            this.Controls.Add(this.txtResultado);
            this.Controls.Add(this.txtPrompt);
            this.Controls.Add(this.lblTema);
            this.Name = "Form1";
            this.Text = "Agente de Automatización de investigación y elaboración de informes";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTema;
        private System.Windows.Forms.TextBox txtPrompt;
        private System.Windows.Forms.TextBox txtResultado;
        private System.Windows.Forms.Button btBuscar;
        private System.Windows.Forms.Button btLimpiar;
        private System.Windows.Forms.Button btPresentacion;
        private System.Windows.Forms.Button btDocumento;
    }
}

