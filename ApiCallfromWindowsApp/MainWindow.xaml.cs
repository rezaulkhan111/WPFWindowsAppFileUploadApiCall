using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ApiCallfromWindowsApp
{
    public partial class MainWindow : Window
    {
        string filename;
        TestModelClass obj;
        Microsoft.Win32.OpenFileDialog dlg;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Btn_select_file_Click(object sender, RoutedEventArgs e)
        {
            dlg = new Microsoft.Win32.OpenFileDialog
            {
                Multiselect = true
            };
            bool? result = dlg.ShowDialog();
            if (result != true)
            {
                filename = dlg.FileName;
            }
        }

        private async void Btn_post_data_Click(object sender, RoutedEventArgs e)
        {
            //HttpClient client = new HttpClient();

            //client.BaseAddress = new Uri("http://localhost:12789/");



            //client.DefaultRequestHeaders.Accept.Add(

            //   new MediaTypeWithQualityHeaderValue("application/json"));

            //obj = new TestModelClass();
            //obj.Id = int.Parse(tb_id.Text);
            //obj.Name = tb_name.Text;
            //if (dlg != null)
            //{
            //    //obj.formFile = dlg.FileName;
            //}
            ////employee.Name = txtName.Text;

            ////employee.Address = txtAddress.Text;

            ////employee.Designation = txtDesignation.Text;



            //var response = client.PostAsJsonAsync("api/employee", employee).Result;

            obj = new TestModelClass();

            if (dlg != null)
            {
                using (var httpClient = new HttpClient())
                {
                    string fileName = dlg.SafeFileName;
                    using (var content = new MultipartFormDataContent())
                    {
                        obj.Id = int.Parse(tb_id.Text);
                        obj.Name = tb_name.Text;
                        FileStream fileStream = File.OpenRead(dlg.FileName);
                        HttpContent fileStreamContent = new StreamContent(fileStream);

                        content.Add(new StringContent(Convert.ToString(obj.Id)), "Id");
                        content.Add(new StringContent(Convert.ToString(obj.Name)), "Name");
                        fileStreamContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data") { Name = "formFiles", FileName = fileName };
                        fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                        content.Add(fileStreamContent);

                        using (var response = await httpClient.PostAsync("https://localhost:44359/weatherforecast/UploadModelFiles", content))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                        }
                    }
                }
            }

            //using (var httpClient = new HttpClient())
            //{

            //    using (var content = new MultipartFormDataContent())
            //    {

            //        byte[] Bytes = new byte[dlg.FileName.Length + 1];
            //        var fileContent = new ByteArrayContent(Bytes);

            //        //content.Headers.ContentType = new MediaTypeHeaderValue("multipart/form-data");

            //        content.Add(new StringContent(Convert.ToString(tb_id.Text)), "Id");
            //        content.Add(new StringContent(Convert.ToString(tb_name.Text)), "Name");

            //        fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = obj.formFile.FileName };

            //        content.Add(fileContent);

            //        using (var response = await httpClient.PostAsync("https://localhost:44366/weatherforecast/UploadModelFiles", content))
            //        {
            //            string apiResponse = await response.Content.ReadAsStringAsync();
            //            //receivedReservation = JsonConvert.DeserializeObject<TestModelClass>(apiResponse);
            //        }
            //    }
            //}


        }
    }
}
