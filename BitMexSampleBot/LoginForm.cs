using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BitMexSampleBot
{
    public partial class LoginForm : Form
    {
        private const String LOGIN_URI = "https://www.trades-king.com/api/auth/generate_auth_cookie/?username=${username}&password=${password}";

        public LoginForm()
        {
            InitializeComponent();
            ActiveControl = tbUsername;

        }

        public String Username
        {
            get
            {
                return tbUsername.Text.Trim();
            }
            set
            {
                tbUsername.Text = value.Trim();
            }
        }

        public String Password
        {
            get
            {
                return tbPassword.Text;
            }
            set
            {
                tbPassword.Text = value;
            }
        }

        private async void btnOK_Click(object sender, EventArgs e)
        {
            lblMessage.Visible = false;
            lblMessage.ForeColor = Color.Black;

            try
            {
                if (!await Login())
                    throw new Exception("Login failed.");

                DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                lblMessage.Visible = true;
                lblMessage.Text = "Login failed!";
                lblMessage.ForeColor = Color.Red;
            }
        }

        private bool VerifyInputs()
        {
            return Username.Length > 0 && Password.Length > 0;
        }

        private void VerifyInputs(object sender, EventArgs e)
        {
            btnOK.Enabled = VerifyInputs();
        }

        internal async Task<bool> Login()
        {
            if (!VerifyInputs())
            {
                throw new Exception("Error: No Login-data provided!");
            }

            string loginUri = LOGIN_URI
                                .Replace("${username}", Username)
                                .Replace("${password}", Password);

            try
            {
                pgbStatus.Style = ProgressBarStyle.Marquee;

                var checkAuthCookie = new Task<bool>(() => {

                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(loginUri);
                    request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                    Regex re = new Regex(".+\\\"status\\\"\\s?:\\s?\\\"ok\\\".+", RegexOptions.IgnoreCase);

                    try
                    {
                        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                        using (Stream stream = response.GetResponseStream())
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            Debug.WriteLine("HTTP-StatusCode: {0}", response.StatusCode);

                            string content = reader.ReadToEnd();

                            Debug.WriteLine("JSON: " + content);

                            // TODO: better parse JSON data
                            if(re.IsMatch(content))
                            {
                                return true;
                            }

                            return false;
                        }
                    }
                    catch (WebException e)
                    {
                        Debug.WriteLine("HTTP-Request failed with: {0}", ((HttpWebResponse)e.Response).StatusCode);
                    }
                    Application.Exit();
                    return false;
                });

                checkAuthCookie.Start();

                await checkAuthCookie;

                return checkAuthCookie.Result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);

                throw;
            }
            finally
            {
                pgbStatus.Style = ProgressBarStyle.Continuous;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
            this.Close();
        }
    }
}
