using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QueuwWebAppProject
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["TokenQueue"] == null)
            { 
                Queue<int> queueTokens = new Queue<int>();
                Session["TokenQueue"] = queueTokens;
            }


        }

        protected void btnPrintToken_Click(object sender, EventArgs e)
        {
            Queue<int> tokenQueue = (Queue<int>)Session["TokenQueue"];
            lblStatus.Text = "There are " + tokenQueue.Count.ToString() + " Customers before you in the queue.";
            if (Session["LastTokenNumberIssued"] == null)
            {
                Session["LastTokenNumberIssued"] = 0;
            }
            int nextTokenNumberTobeIssued = (int)Session["LastTokenNumberIssued"] + 1;
            Session["LastTokenNumberIssued"] = nextTokenNumberTobeIssued;
            tokenQueue.Enqueue(nextTokenNumberTobeIssued);
            AddTokentoListBox(tokenQueue);
        }

        private void AddTokentoListBox(Queue<int> tokenQueue)
        {
            ListBox1.Items.Clear();
            foreach (int token in tokenQueue)
            {
                ListBox1.Items.Add(token.ToString());
            }
        }

        private void ServeNextCustomer(TextBox textbox, int counterNumber)
        {
            Queue<int> tokenQueue = (Queue<int>)Session["TokenQueue"];
            if (tokenQueue.Count == 0)
            {
                textbox.Text = "No customers in queue";
            }
            else
            {
                textbox.Text = "No customers in the queue";
                int tokeNumberTobeServer = tokenQueue.Dequeue();
                textbox.Text = tokeNumberTobeServer.ToString();
                TxtDisplay.Text = "Token Number : " + tokeNumberTobeServer.ToString() + " to counter" + counterNumber.ToString();
                AddTokentoListBox(tokenQueue);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            ServeNextCustomer(TxtCounter1, 1);
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            ServeNextCustomer(TxtCounter2, 2);
        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            ServeNextCustomer(TxtCounter3, 3);
        }
    }
}