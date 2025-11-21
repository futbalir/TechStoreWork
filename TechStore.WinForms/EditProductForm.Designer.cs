namespace TechStore.WinForms
{
    partial class EditProductForm
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
            labelName = new Label();
            labelPrice = new Label();
            labelCategory = new Label();
            labelStock = new Label();
            labelManufacturer = new Label();
            textBoxName = new TextBox();
            textBoxPrice = new TextBox();
            textBoxCategory = new TextBox();
            textBoxStock = new TextBox();
            textBoxManufacturer = new TextBox();
            buttonSave = new Button();
            buttonCancel = new Button();
            SuspendLayout();
            // 
            // labelName
            // 
            labelName.AutoSize = true;
            labelName.Location = new Point(53, 107);
            labelName.Name = "labelName";
            labelName.Size = new Size(102, 15);
            labelName.TabIndex = 0;
            labelName.Text = "Название товара:";
            // 
            // labelPrice
            // 
            labelPrice.AutoSize = true;
            labelPrice.Location = new Point(190, 107);
            labelPrice.Name = "labelPrice";
            labelPrice.Size = new Size(38, 15);
            labelPrice.TabIndex = 1;
            labelPrice.Text = "Цена:";
            // 
            // labelCategory
            // 
            labelCategory.AutoSize = true;
            labelCategory.Location = new Point(286, 107);
            labelCategory.Name = "labelCategory";
            labelCategory.Size = new Size(63, 15);
            labelCategory.TabIndex = 2;
            labelCategory.Text = "Категория";
            // 
            // labelStock
            // 
            labelStock.AutoSize = true;
            labelStock.Location = new Point(375, 107);
            labelStock.Name = "labelStock";
            labelStock.Size = new Size(131, 15);
            labelStock.TabIndex = 3;
            labelStock.Text = "Количество на складе:";
            // 
            // labelManufacturer
            // 
            labelManufacturer.AutoSize = true;
            labelManufacturer.Location = new Point(526, 107);
            labelManufacturer.Name = "labelManufacturer";
            labelManufacturer.Size = new Size(95, 15);
            labelManufacturer.TabIndex = 4;
            labelManufacturer.Text = "Производитель:";
            // 
            // textBoxName
            // 
            textBoxName.Location = new Point(53, 125);
            textBoxName.Name = "textBoxName";
            textBoxName.Size = new Size(102, 23);
            textBoxName.TabIndex = 5;
            // 
            // textBoxPrice
            // 
            textBoxPrice.Location = new Point(190, 125);
            textBoxPrice.Name = "textBoxPrice";
            textBoxPrice.Size = new Size(48, 23);
            textBoxPrice.TabIndex = 6;
            // 
            // textBoxCategory
            // 
            textBoxCategory.Location = new Point(286, 125);
            textBoxCategory.Name = "textBoxCategory";
            textBoxCategory.Size = new Size(63, 23);
            textBoxCategory.TabIndex = 7;
            // 
            // textBoxStock
            // 
            textBoxStock.Location = new Point(375, 125);
            textBoxStock.Name = "textBoxStock";
            textBoxStock.Size = new Size(131, 23);
            textBoxStock.TabIndex = 8;
            // 
            // textBoxManufacturer
            // 
            textBoxManufacturer.Location = new Point(526, 125);
            textBoxManufacturer.Name = "textBoxManufacturer";
            textBoxManufacturer.Size = new Size(95, 23);
            textBoxManufacturer.TabIndex = 9;
            // 
            // buttonSave
            // 
            buttonSave.Location = new Point(153, 307);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(75, 23);
            buttonSave.TabIndex = 10;
            buttonSave.Text = "Сохранить";
            buttonSave.UseVisualStyleBackColor = true;
            buttonSave.Click += this.buttonSave_Click;
            // 
            // buttonCancel
            // 
            buttonCancel.Location = new Point(234, 307);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(75, 23);
            buttonCancel.TabIndex = 11;
            buttonCancel.Text = "Отмена";
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += this.buttonCancel_Click;
            // 
            // EditProductForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(buttonCancel);
            Controls.Add(buttonSave);
            Controls.Add(textBoxManufacturer);
            Controls.Add(textBoxStock);
            Controls.Add(textBoxCategory);
            Controls.Add(textBoxPrice);
            Controls.Add(textBoxName);
            Controls.Add(labelManufacturer);
            Controls.Add(labelStock);
            Controls.Add(labelCategory);
            Controls.Add(labelPrice);
            Controls.Add(labelName);
            Name = "EditProductForm";
            Text = "Form2";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelName;
        private Label labelPrice;
        private Label labelCategory;
        private Label labelStock;
        private Label labelManufacturer;
        private TextBox textBoxName;
        private TextBox textBoxPrice;
        private TextBox textBoxCategory;
        private TextBox textBoxStock;
        private TextBox textBoxManufacturer;
        private Button buttonSave;
        private Button buttonCancel;
    }
}