namespace TechStore.WinForms
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            buttonRefresh = new Button();
            buttonInStock = new Button();
            buttonGroup = new Button();
            buttonAddProduct = new Button();
            buttonDelete = new Button();
            statusStrip1 = new StatusStrip();
            statusLabelInfo = new ToolStripStatusLabel();
            buttonEdit = new Button();
            dataGridViewProducts = new DataGridView();
            statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewProducts).BeginInit();
            SuspendLayout();
            // 
            // buttonRefresh
            // 
            buttonRefresh.Location = new Point(689, 53);
            buttonRefresh.Name = "buttonRefresh";
            buttonRefresh.Size = new Size(99, 52);
            buttonRefresh.TabIndex = 1;
            buttonRefresh.Text = "Обновить список";
            buttonRefresh.UseVisualStyleBackColor = true;
            buttonRefresh.Click += buttonRefresh_Click;
            // 
            // buttonInStock
            // 
            buttonInStock.Location = new Point(689, 111);
            buttonInStock.Name = "buttonInStock";
            buttonInStock.Size = new Size(99, 54);
            buttonInStock.TabIndex = 2;
            buttonInStock.Text = "Товары в наличии";
            buttonInStock.UseVisualStyleBackColor = true;
            buttonInStock.Click += buttonInStock_Click;
            // 
            // buttonGroup
            // 
            buttonGroup.Location = new Point(689, 171);
            buttonGroup.Name = "buttonGroup";
            buttonGroup.Size = new Size(99, 49);
            buttonGroup.TabIndex = 3;
            buttonGroup.Text = "Сгруппировать";
            buttonGroup.UseVisualStyleBackColor = true;
            buttonGroup.Click += buttonGroup_Click;
            // 
            // buttonAddProduct
            // 
            buttonAddProduct.Location = new Point(689, 226);
            buttonAddProduct.Name = "buttonAddProduct";
            buttonAddProduct.Size = new Size(99, 51);
            buttonAddProduct.TabIndex = 4;
            buttonAddProduct.Text = "Добавить товар";
            buttonAddProduct.UseVisualStyleBackColor = true;
            buttonAddProduct.Click += buttonAddProduct_Click;
            // 
            // buttonDelete
            // 
            buttonDelete.Location = new Point(689, 333);
            buttonDelete.Name = "buttonDelete";
            buttonDelete.Size = new Size(99, 23);
            buttonDelete.TabIndex = 5;
            buttonDelete.Text = "Удалить";
            buttonDelete.UseVisualStyleBackColor = true;
            buttonDelete.Click += buttonDelete_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { statusLabelInfo });
            statusStrip1.Location = new Point(0, 428);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(800, 22);
            statusStrip1.TabIndex = 6;
            statusStrip1.Text = "statusStrip1";
            // 
            // statusLabelInfo
            // 
            statusLabelInfo.Name = "statusLabelInfo";
            statusLabelInfo.Size = new Size(118, 17);
            statusLabelInfo.Text = "toolStripStatusLabel1";
            // 
            // buttonEdit
            // 
            buttonEdit.Location = new Point(689, 283);
            buttonEdit.Name = "buttonEdit";
            buttonEdit.Size = new Size(99, 44);
            buttonEdit.TabIndex = 8;
            buttonEdit.Text = "Изменить товар";
            buttonEdit.UseVisualStyleBackColor = true;
            buttonEdit.Click += buttonEdit_Click;
            // 
            // dataGridViewProducts
            // 
            dataGridViewProducts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewProducts.Location = new Point(31, 53);
            dataGridViewProducts.Name = "dataGridViewProducts";
            dataGridViewProducts.Size = new Size(640, 304);
            dataGridViewProducts.TabIndex = 9;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dataGridViewProducts);
            Controls.Add(buttonEdit);
            Controls.Add(statusStrip1);
            Controls.Add(buttonDelete);
            Controls.Add(buttonAddProduct);
            Controls.Add(buttonGroup);
            Controls.Add(buttonInStock);
            Controls.Add(buttonRefresh);
            Name = "Form1";
            Text = "Form1";
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewProducts).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button buttonRefresh;
        private Button buttonInStock;
        private Button buttonGroup;
        private Button buttonAddProduct;
        private Button buttonDelete;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel statusLabelInfo;
        private Button buttonEdit;
        private DataGridView dataGridViewProducts;
    }
}