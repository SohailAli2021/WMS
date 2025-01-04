using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace WMS
{
    public partial class MDI : Form
    {
        private BindingSource bindingSource;
        private DataTable barcodeTable;

        public MDI()
        {
            InitializeComponent();

            // Initialize the DataTable to store barcode data
            barcodeTable = new DataTable();
            barcodeTable.Columns.Add("Scanned Barcode");
            barcodeTable.Columns.Add("Live Weight");

            // Initialize the BindingSource with the DataTable
            bindingSource = new BindingSource();
            bindingSource.DataSource = barcodeTable;

            // Bind the DataGridView to the BindingSource
            dataGridView1.DataSource = bindingSource;

            // Ensure KeyPress event is connected
            txtjoborderbarcode.KeyPress += txtBarcode_KeyPress;
            txtweight.KeyPress += txtweight_KeyPress;
            // Optionally, set the column width to make sure it's visible
            dataGridView1.Columns[0].Width = 100;
            dataGridView1.Columns[1].Width = 100;

            // Adjust as necessary
        }  // To check barcode input

        private void txtBarcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            // If the Enter key is pressed, process the scanned barcode
            if (e.KeyChar == (char)Keys.Enter)
            {
                string scannedBarcode = txtjoborderbarcode.Text.Trim();

                // Only add the barcode if it's not empty
                if (!string.IsNullOrEmpty(scannedBarcode))
                {
                    // Add the barcode to the DataTable (which will update the BindingSource)
                    barcodeTable.Rows.Add(scannedBarcode);

                    // Clear the TextBox for the next scan
                    txtjoborderbarcode.Clear();

                    // Optionally, refresh the DataGridView
                    dataGridView1.Refresh();
                }




            }
        }

        private void txtweight_KeyPress(object sender, KeyPressEventArgs e)
        {
            // If the Enter key is pressed, process the weight input
            if (e.KeyChar == (char)Keys.Enter)
            {
                string weight = txtweight.Text.Trim();

                // Only add the weight if it's not empty
                if (!string.IsNullOrEmpty(weight))
                {
                    // Get the last row added to ensure we associate the weight with the correct barcode
                    if (barcodeTable.Rows.Count > 0)
                    {
                        // Update the "Live Weight" column for the last added row
                        barcodeTable.Rows[barcodeTable.Rows.Count - 1]["Live Weight"] = weight;

                        // Clear the TextBox for the next weight input
                        txtweight.Clear();

                        // Optionally, refresh the DataGridView
                        dataGridView1.Refresh();
                    }
                    else
                    {
                        MessageBox.Show("Please scan a barcode before entering the weight.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

                Console.WriteLine("Weight: " + weight);
            }
        }

    }
}