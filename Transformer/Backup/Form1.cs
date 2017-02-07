using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.XPath;

namespace Transformer
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox txtSource;
		private System.Windows.Forms.TextBox txtXslt;
		private System.Windows.Forms.Label lblSource;
		private System.Windows.Forms.Label lblXslt;
		private System.Windows.Forms.Button btnGenerate;
		private String sourceDoc = "";
		private String xsltDoc = "";
		private String resultDoc = "result.xml";
		private System.Windows.Forms.TextBox txtResult;
		private System.Windows.Forms.Button btnReset;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
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
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.txtSource = new System.Windows.Forms.TextBox();
			this.txtXslt = new System.Windows.Forms.TextBox();
			this.lblSource = new System.Windows.Forms.Label();
			this.lblXslt = new System.Windows.Forms.Label();
			this.btnGenerate = new System.Windows.Forms.Button();
			this.txtResult = new System.Windows.Forms.TextBox();
			this.btnReset = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// txtSource
			// 
			this.txtSource.Location = new System.Drawing.Point(136, 16);
			this.txtSource.Name = "txtSource";
			this.txtSource.Size = new System.Drawing.Size(136, 22);
			this.txtSource.TabIndex = 0;
			this.txtSource.Text = "";
			// 
			// txtXslt
			// 
			this.txtXslt.Location = new System.Drawing.Point(136, 48);
			this.txtXslt.Name = "txtXslt";
			this.txtXslt.Size = new System.Drawing.Size(136, 22);
			this.txtXslt.TabIndex = 1;
			this.txtXslt.Text = "";
			// 
			// lblSource
			// 
			this.lblSource.Location = new System.Drawing.Point(16, 16);
			this.lblSource.Name = "lblSource";
			this.lblSource.TabIndex = 2;
			this.lblSource.Text = "Source XML";
			// 
			// lblXslt
			// 
			this.lblXslt.Location = new System.Drawing.Point(16, 48);
			this.lblXslt.Name = "lblXslt";
			this.lblXslt.TabIndex = 3;
			this.lblXslt.Text = "XSLT";
			// 
			// btnGenerate
			// 
			this.btnGenerate.Location = new System.Drawing.Point(40, 88);
			this.btnGenerate.Name = "btnGenerate";
			this.btnGenerate.TabIndex = 4;
			this.btnGenerate.Text = "Generate";
			this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
			// 
			// txtResult
			// 
			this.txtResult.Location = new System.Drawing.Point(16, 120);
			this.txtResult.Multiline = true;
			this.txtResult.Name = "txtResult";
			this.txtResult.ReadOnly = true;
			this.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtResult.Size = new System.Drawing.Size(264, 152);
			this.txtResult.TabIndex = 5;
			this.txtResult.Text = "";
			// 
			// btnReset
			// 
			this.btnReset.Location = new System.Drawing.Point(160, 88);
			this.btnReset.Name = "btnReset";
			this.btnReset.TabIndex = 6;
			this.btnReset.Text = "Reset";
			this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
			this.ClientSize = new System.Drawing.Size(292, 288);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.btnReset,
																		  this.txtResult,
																		  this.btnGenerate,
																		  this.lblXslt,
																		  this.lblSource,
																		  this.txtXslt,
																		  this.txtSource});
			this.Name = "Form1";
			this.Text = "XML Transformer";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
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
			//if(!(txtSource.Text.EndsWith(".xml")&& (txtXslt.Text.EndsWith(".xsl"))))
			//{
				sourceDoc = txtSource.Text + ".xml";
				xsltDoc = txtXslt.Text + ".xsl";
			//}

			if ((txtSource.Text.Trim() == "") || (txtXslt.Text.Trim() == "")) 
			{
				MessageBox.Show("Enter the filename!", "File Name Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				/*if (MessageBox.Show ("Do you want to exit?", "My Application", 
					MessageBoxButtons.YesNo, MessageBoxIcon.Question)
					== DialogResult.Yes) 
				{
					Application.Exit();
				}*/

			}

			try
			{
				//txtResult.Text = "Generating...";

				XPathDocument myXPathDocument = new XPathDocument (sourceDoc);
				XslTransform myXslTransform = new XslTransform();
        
				XmlTextWriter writer = new XmlTextWriter(resultDoc, null);
				myXslTransform.Load(xsltDoc);

				myXslTransform.Transform(myXPathDocument, null, writer);
				writer.Close();

				StreamReader stream = new StreamReader (resultDoc);
				txtResult.Text = stream.ReadToEnd();
				//Console.Write("**This is result document**\n\n");
				//Console.Write(stream.ReadToEnd());
  
			}

			catch (FileNotFoundException filexc)
			{
				MessageBox.Show("File Not Found!", "File Not Found Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}


			catch (Exception exc)
			{
				Console.WriteLine ("Exception: {0}", exc.ToString());
			}
		
		}

		private void btnReset_Click(object sender, System.EventArgs e)
		{
			txtSource.Text = "";
			txtXslt.Text = "";
			txtResult.Text = "";
		}
	}
}
