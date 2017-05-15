using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Windows.Forms;

namespace LogOnLoader
{
    class Program3
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Program3 p = new Program3();
            p.ExitRequested += p_ExitRequested;

            Task programStart = p.StartAsync();

            Application.Run();
        }

        static void p_ExitRequested(object sender, EventArgs e)
        {
            Application.ExitThread();
        }

        private readonly fMain m_mainForm;
        private Program3()
        {
            m_mainForm = new fMain();
            m_mainForm.FormClosed += m_mainForm_FormClosed;
        }


        public async Task StartAsync()
        {
            m_mainForm.Show();
            await m_mainForm.AsyncProcedures();
        }

        //public void Start()
        //{
        //    m_mainForm.Initialize();
        //    m_mainForm.Show();
        //}

        public event EventHandler<EventArgs> ExitRequested;
        void m_mainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            OnExitRequested(EventArgs.Empty);
        }

        protected virtual void OnExitRequested(EventArgs e)
        {
            if (ExitRequested != null)
                ExitRequested(this, e);
        }
    }
}
