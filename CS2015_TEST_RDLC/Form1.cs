using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CS2015_TEST_RDLC
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<Student> list = new List<Student>();
            list.Clear();
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                list.Add(new Student { Name = dataGridView1.Rows[i].Cells[0].Value.ToString(), Grade = dataGridView1.Rows[i].Cells[1].Value.ToString() });
            }
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dt = converter.ToDataTable(list);

            Form2 Form2Buf = new Form2();
            Form2Buf.reportViewer1.LocalReport.DataSources.Clear();
            Form2Buf.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));
            Form2Buf.reportViewer1.LocalReport.ReportEmbeddedResource = "CS2015_TEST_RDLC.Report1.rdlc";
            Form2Buf.reportViewer1.RefreshReport();
            Form2Buf.ShowDialog();
        }
    }

    public class Student
    {
        public string Name { get; set; }
        public string Grade { get; set; }
    }
    public class ListtoDataTableConverter
    {
        //https://www.c-sharpcorner.com/UploadFile/1a81c5/list-to-datatable-converter-using-C-Sharp/
        public DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }
    }
}
