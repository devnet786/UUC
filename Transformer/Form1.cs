using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using iTextSharp;
using System.Xml.XPath;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using Transformer.Module;
using Transformer.Common;

namespace Transformer
{
	
	public class Form1 : System.Windows.Forms.Form

	{
        string filenamepath;
        private System.Windows.Forms.TextBox txtSource;
        private System.Windows.Forms.Label lblSource;
		private System.Windows.Forms.Button btnGenerate;
		private String sourceDoc = "";
		private String xsltDoc = "";
		private String resultDoc = "result.xml";
        private System.Windows.Forms.TextBox txtResult;
        private Button button1;
		
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			
			InitializeComponent();			
		}	
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
	
		private void InitializeComponent()
		{
            this.txtSource = new System.Windows.Forms.TextBox();
            this.lblSource = new System.Windows.Forms.Label();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtSource
            // 
            this.txtSource.Location = new System.Drawing.Point(113, 14);
            this.txtSource.Name = "txtSource";
            this.txtSource.Size = new System.Drawing.Size(300, 20);
            this.txtSource.TabIndex = 0;
            // 
            // lblSource
            // 
            this.lblSource.Location = new System.Drawing.Point(13, 14);
            this.lblSource.Name = "lblSource";
            this.lblSource.Size = new System.Drawing.Size(84, 20);
            this.lblSource.TabIndex = 2;
            this.lblSource.Text = "XML File";
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(33, 76);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(63, 20);
            this.btnGenerate.TabIndex = 4;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(13, 104);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.ReadOnly = true;
            this.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtResult.Size = new System.Drawing.Size(1052, 337);
            this.txtResult.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(468, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(111, 20);
            this.button1.TabIndex = 7;
            this.button1.Text = "Select XML File";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(1077, 453);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.lblSource);
            this.Controls.Add(this.txtSource);
            this.Name = "Form1";
            this.Text = "XML Transformer";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{
		
		}

		private void btnGenerate_Click(object sender, System.EventArgs e)
		{
            Parameter objParameter = new Parameter();
            objParameter.SourceXML = txtSource.Text;
            ConvertUtility objconvert = new ConvertUtility();
            var result = objconvert.utility(txtSource.Text);
            txtResult.Text = result;		
		}	

        private void button1_Click(object sender, EventArgs e)
        {
            string folderpath = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            OpenFileDialog openfiledialog = new OpenFileDialog();
            DialogResult result = openfiledialog.ShowDialog();           

            if (result == DialogResult.OK) 
            {
                string filename = System.IO.Path.GetFileName(openfiledialog.FileName);
                string splitstring = "\\";
                string filepath = folderpath + splitstring + filename;
                filenamepath = folderpath + splitstring;

                //Check file Already Exist or not

                if (System.IO.File.Exists(filepath))
                {
                    System.IO.File.Delete(filepath);
                }
                var onlyFileName = System.IO.Path.GetFileNameWithoutExtension(openfiledialog.FileName);
                txtSource.Text = onlyFileName;
            }
            int count = 0;
            string[] FilenameName;
            foreach (string item in openfiledialog.FileNames)
            {
                FilenameName = item.Split('\\');
                File.Copy(item, filenamepath + FilenameName[FilenameName.Length - 1]);
                count++;
            }
        }
	}
}
