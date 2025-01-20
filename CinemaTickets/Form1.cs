using System.Windows.Forms;

namespace VSP_178knr_MyProject
{
    public partial class Form1 : Form
    {
        Decimal regularPrice = 15.00M;
        Decimal childPrice = 13.00M;
        Decimal studentPrice = 12.50M;
        Decimal pensionerPrice = 11.00M;

        int regularTickets = 0;
        int childTickets = 0;
        int studentTickets = 0;
        int pensionerTickets = 0;
        int totalTickets = 0;

        Decimal totalPrice;
        bool proceed1 = false;
        bool proceed2 = false;
        bool proceed3 = false;
        bool comboBoxReset = false;

        public class Seat
        {
            public int Row { get; set; }
            public int SeatNumber { get; set; }

            public Seat(int row, int seatNumber)
            {
                Row = row;
                SeatNumber = seatNumber;
            }
        }

        List<Seat> chosenSeatsList = new List<Seat>();
        public Form1()
        {
            InitializeComponent();
            this.Size = new Size(442, 278);
            this.MaximumSize = new System.Drawing.Size(442, 278);
            this.MinimumSize = new System.Drawing.Size(442, 278);
            labelNoTicketsChosen.Text = "";
            labelSeatsPicked.Text = "";
            comboBoxQuantityRegular.SelectedIndex = 0;
            comboBoxQuantityChild.SelectedIndex = 0;
            comboBoxQuantityStudent.SelectedIndex = 0;
            comboBoxQuantityPensioner.SelectedIndex = 0;
            comboBoxReset = true;




        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            e.Cancel = (e.TabPageIndex == 1 && !proceed1) || (e.TabPageIndex == 2 && !proceed2);
            if (!e.Cancel)
            {
                switch (e.TabPageIndex)
                {
                    case 1:
                        this.Size = new Size(922, 609);
                        this.MaximumSize = new System.Drawing.Size(679, 401);
                        this.MinimumSize = new System.Drawing.Size(679, 401);
                        break;
                    case 2:
                        this.Size = new Size(1007, 503);
                        this.MaximumSize = new System.Drawing.Size(684, 324);
                        this.MinimumSize = new System.Drawing.Size(684, 324);
                        break;
                    default:
                        this.Size = new Size(442, 278);
                        this.MaximumSize = new System.Drawing.Size(442, 278);
                        this.MinimumSize = new System.Drawing.Size(442, 278);
                        break;
                }
            }
            
        }

        private void buttonForward1_Click(object sender, EventArgs e)
        {
            CheckTickets();
            tabControl1.SelectTab(1);
        }

        private void SeatChosenHandler(object sender, EventArgs e)
        {
         
            Button seatButton = sender as Button;
            string seat = seatButton.Name;
            string[] seatLocation = seat.Split(new[] { "Row", "Seat" }, StringSplitOptions
                .RemoveEmptyEntries);
            int row = int.Parse(seatLocation[1]);
            int number = int.Parse(seatLocation[2]);
            Seat seat1 = new Seat(row, number);

            if (seatButton.BackColor == Color.FromArgb(255, 128, 0))
            {
                foreach (Seat s in chosenSeatsList)
                {
                    if (s.Row == row && s.SeatNumber == number)
                    {
                        chosenSeatsList.Remove(s);
                        break;
                    }
                }
                seatButton.BackColor = Color.LimeGreen;
            }
            else if (chosenSeatsList.Count != totalTickets)
            {
                chosenSeatsList.Add(seat1);
                seatButton.BackColor = Color.FromArgb(255, 128, 0);
            }

            CheckSeatsLeft();
            
        }
        

        private void ResetChosenSeats(object sender, EventArgs e)
        {
            if (comboBoxReset)
            {
                List<Button> buttonsToResetList = new List<Button>();
                foreach (Control control in tabPageSeats.Controls)
                {
                    if (control is Button btn && btn.BackColor == Color.FromArgb(255, 128, 0))
                    {
                        buttonsToResetList.Add(btn);
                    }
                }
                foreach (Button btn in buttonsToResetList)
                {
                    btn.BackColor = Color.LimeGreen;
                }
                chosenSeatsList.Clear();
                CheckSeatsLeft();
                CheckTickets();
                
            }

        }
            
        private void CheckSeatsLeft()
        {
            int seatsLeft = totalTickets - chosenSeatsList.Count;

            switch (seatsLeft)
            {
                case 0:
                    labelSeatsPicked.Text = "Всички места са избрани!";
                    buttonForward2.Enabled = true;
                    break;
                case 1:
                    labelSeatsPicked.Text = "Моля, изберете още " + seatsLeft + " място.";
                    buttonForward2.Enabled = false;
                    proceed2 = false;
                    break;
                default:
                    labelSeatsPicked.Text = "Моля, изберете още " + seatsLeft + " места.";
                    buttonForward2.Enabled = false;
                    proceed2 = false;
                    break;
            }

            if (chosenSeatsList.Count == 0)
            {
                
                if (totalTickets != 1)
                {
                    labelSeatsPicked.Text = "Моля, изберете " + totalTickets + " места.";
                }
                else
                {
                    labelSeatsPicked.Text = "Моля, изберете " + totalTickets + " място.";
                }
                buttonForward2.Enabled = false;
                proceed2 = false;
            }
        }

        private void buttonForward2_Click(object sender, EventArgs e)
        {
            int seatsLeft = totalTickets - chosenSeatsList.Count;
            if (seatsLeft == 0)
            {
                labelRegularTicketCount.Text = regularTickets.ToString();
                labelChildTicketCount.Text = childTickets.ToString();
                labelStudentTicketCount.Text = studentTickets.ToString();
                labelPensionerTicketCount.Text = pensionerTickets.ToString();
                labelTotal.Text = totalPrice + "лв.";
                string seats = "";
                var groupedSeats = chosenSeatsList
                .OrderBy(seat => seat.Row)
                .GroupBy(seat => seat.Row);

                foreach (var group in groupedSeats)
                {
                    seats += $"Ред: {group.Key}, ";

                    var seatNumbers = group
                        .OrderBy(seat => seat.SeatNumber)
                        .Select(seat => seat.SeatNumber);

                    seats += $"Място: {string.Join(", ", seatNumbers)}\n";
                }
                labelSeats.Text = seats;
                proceed2 = true;
                tabControl1.SelectTab(2);
            }
        }

        private void buttonBack1_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(0);
        }

        private void CheckTickets()
        {
            if (comboBoxQuantityRegular.SelectedIndex < 1 && comboBoxQuantityChild.SelectedIndex < 1
                && comboBoxQuantityStudent.SelectedIndex < 1 && comboBoxQuantityPensioner.SelectedIndex < 1)
            {
                labelNoTicketsChosen.Text = "Не сте избрали билети!";
                buttonForward1.Enabled = false;
                proceed1 = false;
                proceed2 = false;
            }
            else
            {
                labelNoTicketsChosen.Text = "";

                regularTickets = Convert.ToInt32(comboBoxQuantityRegular.SelectedItem);
                childTickets = Convert.ToInt32(comboBoxQuantityChild.SelectedItem);
                studentTickets = Convert.ToInt32(comboBoxQuantityStudent.SelectedItem);
                pensionerTickets = Convert.ToInt32(comboBoxQuantityPensioner.SelectedItem);
                totalTickets = regularTickets + childTickets + studentTickets + pensionerTickets;

                totalPrice = (Decimal)((regularTickets * regularPrice) +
                    (childTickets * childPrice) +
                    (studentTickets * studentPrice) +
                    (pensionerTickets * pensionerPrice));
                buttonForward1.Enabled = true;
                proceed1 = true;
                CheckSeatsLeft();
                
            }
        }

        private void buttonBack2_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(1);
        }

        private void buttonFinish_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Успешно завършване на поръчката!");
            this.Close();
        }
    }
}