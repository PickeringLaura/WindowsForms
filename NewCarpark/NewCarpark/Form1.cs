using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewCarpark
{
    public partial class grid_background : Form
    {
        TextBox[] txtBlcks;
        private int CurrentlySelectedBay;

        public grid_background()
        {
            InitializeComponent();
            LocalInterface.LoadCarpark("Wheatfield");

            txtBlcks = new TextBox[] { txt_space1, txt_space2, txt_space3, txt_space4, txt_space5 };
            UpdateSpaces();
        }

        #region spaces
        private void AllocateNewSpace()
        {
            Space space = CarparkManager.Instance.GetCarpark(3).nextAvailableCarParkingSpace();
            space.SetAllocated(true);
            txtBlcks[space.GetId()].Text = "Allocated";
        }

        private void DeallocateSpace()
        {
            int id = CarparkManager.Instance.GetCarpark(3).getAllocatedSpaces();
            if (id != null)
            {
                Space space = CarparkManager.Instance.GetCarpark(3).GetSpace(id);

                if (space != null)
                {
                    space.SetAllocated(false);
                    txtBlcks[space.GetId()].Text = "Free";
                }
            }
        }

        private void UpdateSpaces()
        {
            txt_spacesAvailable1.Text = LocalInterface.Instance.GetSpaces() + " spaces available.";
        }
        #endregion

        #region enter and exit
        private void button1_Click(object sender, EventArgs e)
        {
            txt_spacesAvailable1.Text = CarparkManager.Instance.GetCarpark(3).GetEmptySpaces().ToString() + " spaces are available";
            txt_displayBarriers.Text = "Carpark Barrier Lifted";
            AllocateNewSpace();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (IsCoinCollected())
            {
                DeallocateSpace();
                txt_spacesAvailable1.Text = CarparkManager.Instance.GetCarpark(3).GetEmptySpaces().ToString();
                txt_displayBarriers.Text = "Carpark Barrier Lifted";
            }
        }

        #endregion

        #region Forgotten pass and coin
        private void button19_Click(object sender, EventArgs e)
        {
            TextBox[] passes = new TextBox[] { pass_bay1, pass_bay2, pass_bay3, pass_bay4, pass_bay5 };

            for (int i = 0; i < passes.Length; i++)
            {
                if (passes[i].Text == pass_coin.Text)
                {
                    DeterminePayment(i);
                    txt_paymentDone.Text = "Continue to pay";
                    return;
                }
            }

            txt_paymentDone.Text = "Password for forgotten coin does not match";
        }

        private void button18_Click(object sender, EventArgs e)
        {
            txt_paymentDone.Text = "Validation sucessful. Continue to payment.";
        }
        #endregion

        #region bay unlock buttons
        private void button17_Click(object sender, EventArgs e)
        {
            txt_space1.Text = "Pay!";
            txt_AmountDue1.Text = "£6.00";
            CurrentlySelectedBay = 0;

            txt_displayBarriers.Text = "Barrier on bay 1 will be raised after payment";
            txt_paymentDone.Text = "Please pay at the machine located at Space 1";
        }

        private void button16_Click(object sender, EventArgs e)
        {
            txt_space2.Text = "Pay!";
            txt_AmountDue1.Text = "£10.00";
            CurrentlySelectedBay = 1;

            txt_displayBarriers.Text = "Barrier on bay 2 will be raised after payment";
            txt_paymentDone.Text = "Please pay at the machine located at Space 2";
        }

        private void button15_Click(object sender, EventArgs e)
        {
            txt_space3.Text = "Pay!";
            txt_AmountDue1.Text = "£2.00";
            CurrentlySelectedBay = 2;

            txt_displayBarriers.Text = "Barrier on bay 3 will be raised after payment";
            txt_paymentDone.Text = "Please pay at the machine located at Space 3";
        }

        private void button14_Click(object sender, EventArgs e)
        {
            txt_space4.Text = "Pay!";
            txt_AmountDue1.Text = "£15.00";
            CurrentlySelectedBay = 3;

            txt_displayBarriers.Text = "Barrier on bay 4 will be raised after payment";
            txt_paymentDone.Text = "Please pay at the machine located at Space 4";
        }

        private void button13_Click(object sender, EventArgs e)
        {
            txt_space5.Text = "Pay!";
            txt_AmountDue1.Text = "£6.00";
            CurrentlySelectedBay = 4;

            txt_displayBarriers.Text = "Barrier on bay 5 will be raised after payment";
            txt_paymentDone.Text = "Please pay at the machine located at Space 5";
        }
        #endregion

        #region payment
        private void button23_Click(object sender, EventArgs e)
        {
            if (CarparkManager.Instance.ValidateDiscountCode(pass_DiscountCode.Text))
            {
                if (txt_AmountDue1.Text.Length > 0)
                {
                    double price = double.Parse(txt_AmountDue1.Text.TrimStart('£'));
                    price *= 0.8;
                    txt_AmountDue1.Text = "£" + Math.Round(price, 2).ToString();
                }
                else
                {
                    txt_AmountDue1.Text = "Please unlock a pay before payment";
                }
            }
        }


        private void DeterminePayment(int i)
        {
            switch (i)
            {
                case 0:
                    button17_Click(this, null);
                    break;
                case 1:
                    button16_Click(this, null);
                    break;
                case 2:
                    button15_Click(this, null);
                    break;
                case 3:
                    button14_Click(this, null);
                    break;
                case 4:
                    button13_Click(this, null);
                    break;
                default:
                    txt_AmountDue1.Text = "Failed to determine bay";
                    break;
            }
        }




        #endregion

        #region bay submit buttons
        private void button12_Click(object sender, EventArgs e)
        {
            if (CheckReg(pass_reg1) && CheckPassword(pass_bay1))
            {
                CheckIfSpace1IsAllocated();
                if (txt_space1.Text == "Space 1")
                {
                    txt_displayBarriers.Text = "Space 1 Has Not Yet Been Allocated.  Cannot Lock";
                }
                else
                {
                    txt_displayBarriers.Text = "Barrier on Bay 1 Lowered";
                }

                /*if (!CarparkManager.Instance.GetCarpark(3).GetSpace(0).IsAllocated())
                {
                    CarparkManager.Instance.GetCarpark(3).GetSpace(0).SetAllocated(true);
                    txt_space1.Text = "Locked";
                }*/
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            CheckIfSpace2IsAllocated();
            if (CheckReg(pass_reg2) && CheckPassword(pass_bay2))
            {
                if (txt_space2.Text == "Space 2")
                {
                    txt_displayBarriers.Text = "Space 2 Has Not Yet Been Allocated.  Cannot Lock";
                }
                else
                {
                    txt_displayBarriers.Text = "Barrier on Bay 2 Lowered";
                }
                /*if (!CarparkManager.Instance.GetCarpark(3).GetSpace(1).IsAllocated())
                {
                    CarparkManager.Instance.GetCarpark(3).GetSpace(1).SetAllocated(true);
                    txt_space2.Text = "Locked";
                }*/
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            CheckIfSpace3IsAllocated();
            if (CheckReg(pass_reg3) && CheckPassword(pass_bay3))
            {
                if (txt_space3.Text == "Space 3")
                {
                    txt_displayBarriers.Text = "Space 3 Has Not Yet Been Allocated.  Cannot Lock";
                }
                else
                {
                    txt_displayBarriers.Text = "Barrier on Bay 3 Lowered";
                }
                /*if (!CarparkManager.Instance.GetCarpark(3).GetSpace(2).IsAllocated())
                {
                    CarparkManager.Instance.GetCarpark(3).GetSpace(2).SetAllocated(true);
                    txt_space3.Text = "Locked";
                }*/
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            CheckIfSpace4IsAllocated();
            if (CheckReg(pass_reg4) && CheckPassword(pass_bay4))
            {
                if (txt_space4.Text == "Space 4")
                {
                    txt_displayBarriers.Text = "Space 4 Has Not Yet Been Allocated.  Cannot Lock";
                }
                else
                {
                    txt_displayBarriers.Text = "Barrier on Bay 4 Lowered";
                }
                /*if (!CarparkManager.Instance.GetCarpark(3).GetSpace(3).IsAllocated())
                {
                    CarparkManager.Instance.GetCarpark(3).GetSpace(3).SetAllocated(true);
                    txt_space4.Text = "Locked";
                }*/
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            CheckIfSpace3IsAllocated();
            if (CheckReg(pass_reg5) && CheckPassword(pass_bay5))
            {
                if (txt_space5.Text == "Space 5")
                {
                    txt_displayBarriers.Text = "Space 5 Has Not Yet Been Allocated.  Cannot Lock";
                }
                else
                {
                    txt_displayBarriers.Text = "Barrier on Bay 5 Lowered";
                }
                /*if (!CarparkManager.Instance.GetCarpark(3).GetSpace(4).IsAllocated())
                {
                        CarparkManager.Instance.GetCarpark(3).GetSpace(4).SetAllocated(true);
                        txt_space5.Text = "Locked";
                }*/
            }
        }

        #endregion

        #region Check If Spaces Are Allocated
        private bool CheckIfSpace5IsAllocated()
        {
            if (txt_space5.Text == "Allocated")
            {
                txt_space5.Text = "Locked";
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CheckIfSpace4IsAllocated()
        {
            if (txt_space4.Text == "Allocated")
            {
                txt_space4.Text = "Locked";
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CheckIfSpace3IsAllocated()
        {
            if (txt_space3.Text == "Allocated")
            {
                txt_space3.Text = "Locked";
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CheckIfSpace2IsAllocated()
        {
            if (txt_space2.Text == "Allocated")
            {
                txt_space2.Text = "Locked";
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CheckIfSpace1IsAllocated()
        {
            if (txt_space1.Text == "Allocated")
            {
                txt_space1.Text = "Locked";
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region CheckReg and CheckPassword
        private bool CheckReg(TextBox pass)
        {
            if (pass.Text.Length == 0)
            {
                txt_paymentDone.Text = "The registration field cannot be left empty";
                return false;
            }
            else if (pass.Text.Length <= 7)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CheckPassword(TextBox pass)
        {
            if (pass.Text == "Password")
            {
                txt_paymentDone.Text = " 'Password' is not allowed to be set as a password";
                return false;
            }
            else if (pass.Text.Length == 0)
            {
                txt_paymentDone.Text = "The password field cannot be left empty";
                return false;
            }
            else if (pass.Text.Length <= 16)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region Coin Collected Buttons
        private void txt_space1_TextChanged(object sender, EventArgs e)
        {
            txt_coinDispensed1.Text = "Coin";
            txt_displayBarriers.Text = "Barrier On Bay 1 Raised";
        }

        private void txt_space2_TextChanged(object sender, EventArgs e)
        {
            txt_coinDispensed2.Text = "Coin";
            txt_displayBarriers.Text = "Barrier On Bay 2 Raised";
        }

        private void txt_space4_TextChanged(object sender, EventArgs e)
        {
            txt_coinDispensed3.Text = "Coin";
            txt_displayBarriers.Text = "Barrier On Bay 3 Raised";
        }

        private void txt_space3_TextChanged(object sender, EventArgs e)
        {
            txt_coinDispensed4.Text = "Coin";
            txt_displayBarriers.Text = "Barrier On Bay 4 Raised";
        }

        private void txt_space5_TextChanged(object sender, EventArgs e)
        {
            txt_coinDispensed5.Text = "Coin";
            txt_displayBarriers.Text = "Barrier On Bay 5 Raised";
        }

        private bool IsCoinCollected()
        {
            if (txt_space1.Text == "Pay!")
            {
                txt_coinDispensed1.Text = "";
                return true;
            }
            else if (txt_space2.Text == "Pay!")
            {
                txt_coinDispensed2.Text = "";
                return true;
            }
            else if (txt_space3.Text == "Pay!")
            {
                txt_coinDispensed3.Text = "";
                return true;
            }
            else if (txt_space4.Text == "Pay!")
            {
                txt_coinDispensed4.Text = "";
                return true;
            }
            else if (txt_space5.Text == "Pay!")
            {
                txt_coinDispensed5.Text = "";
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region not needed methods
        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txt_EmergencyVehicles_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region you have paid
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (txt_AmountDue1.Text.Contains("£"))
            {
                txt_paymentDone.Text = "You Have Paid. Exit Through Ground FLoor - Exit 1";
                txt_displayBarriers.Text = "Barrier Raised, You Are Free To Exit.";
                txt_Payment1.Text = "";
                ClearFields(CurrentlySelectedBay);
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            if (txt_AmountDue1.Text.Contains("£"))
            {
                txt_paymentDone.Text = "You Have Paid. Exit Through Ground FLoor - Exit 2";
                txt_displayBarriers.Text = "Barrier Raised, You Are Free To Exit.";
                txt_Payment1.Text = "";
                ClearFields(CurrentlySelectedBay);
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (txt_AmountDue1.Text.Contains("£"))
            {
                txt_paymentDone.Text = "You Have Paid. Exit Through First FLoor - Exit 1";
                txt_displayBarriers.Text = "Barrier Raised, You Are Free To Exit.";
                txt_Payment1.Text = "";
                ClearFields(CurrentlySelectedBay);
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (txt_AmountDue1.Text.Contains("£"))
            {
                txt_paymentDone.Text = "You Have Paid. Exit Through First FLoor - Exit 1";
                txt_displayBarriers.Text = "Barrier Raised, You Are Free To Exit.";
                txt_Payment1.Text = "";
                ClearFields(CurrentlySelectedBay);
            }
        }

        private void button24_Click(object sender, EventArgs e)
        {
            if (pass_PrepayCode.Text == "prepay1")
            {
                txt_AmountDue1.Text = "";
            }
            else if (pass_PrepayCode.Text == "prepay2")
            {
                txt_AmountDue1.Text = "";
            }
            else if (pass_PrepayCode.Text == "prepay3")
            {
                txt_AmountDue1.Text = "";
            }
            else
            {
                txt_AmountDue1.Text = "This is not a valid pre-pay code.";
            }
            txt_paymentDone.Text = "You Have Paid. Exit Through First FLoor - Exit 2";
            txt_displayBarriers.Text = "Barrier Raised, You Are Free To Exit.";
            ClearFields(CurrentlySelectedBay);
        }

       
        #endregion

        #region clear fields
        private void ClearFields(int currentlySelectedBay)
        {
            if (currentlySelectedBay == -1)
            {
                return;
            }

            txt_AmountDue1.Text = "";
            pass_DiscountCode.Text = "";
            pass_coin.Text = "";

            switch (currentlySelectedBay)
            {
                case 0:
                    pass_bay1.Text = "";
                    pass_reg1.Text = "";
                    break;
                case 1:
                    pass_bay2.Text = "";
                    pass_reg2.Text = "";
                    break;
                case 2:
                    pass_bay3.Text = "";
                    pass_reg3.Text = "";
                    break;
                case 3:
                    pass_bay4.Text = "";
                    pass_reg4.Text = "";
                    break;
                case 4:
                    pass_bay5.Text = "";
                    pass_reg5.Text = "";
                    break;
                default:
                    txt_AmountDue1.Text = "Failed to determine bay";
                    break;
            }

            currentlySelectedBay = -1;
        }
        #endregion

        #region emergency buttons
        private void button22_Click(object sender, EventArgs e)
        {
            Color.FromArgb(255, 255, 60, 60);
            txt_space1.Text = "Unlocked!";
            txt_space2.Text = "Unlocked!";
            txt_space3.Text = "Unlocked!";
            txt_space4.Text = "Unlocked!";
            txt_space5.Text = "Unlocked!";
            txt_paymentDone.Text = "Emergency Vehicles are on the way.  Please exit through exit 2 and 3 only.";
            txt_EmergencyVehicles.Text = "Emergency Vehicles enter through Entry 1 and use Exit 1. Please keep these entry and exit points clear";
            txt_displayBarriers.Text = "Carpark Barrier Lifted, Fire In Progress";
        }

        private void button21_Click(object sender, EventArgs e)
        {
            Color.FromArgb(255, 60, 60, 255);
            txt_space1.Text = "Locked!";
            txt_space2.Text = "Locked!";
            txt_space3.Text = "Locked!";
            txt_space4.Text = "Locked!";
            txt_space5.Text = "Locked!";

            txt_paymentDone.Text = "Police are on their way.  All cars locked in until further notice.";
            txt_EmergencyVehicles.Text = "Police will enter through Entry 1 and use Exit 1.";
            txt_displayBarriers.Text = "Carpark Barrier Lowered. Will Only Raise For Police Vehicles.";
        }

        private void button20_Click(object sender, EventArgs e)
        {
            Color.FromArgb(255, 255, 255, 255);

            txt_space1.Text = "Space 1";
            txt_space2.Text = "Space 2";
            txt_space3.Text = "Space 3";
            txt_space4.Text = "Space 4";
            txt_space5.Text = "Space 5";

            txt_paymentDone.Text = "";
            txt_EmergencyVehicles.Text = "";
            txt_displayBarriers.Text = "";

            txt_paymentDone.Text = "";
            txt_coinDispensed1.Text = "";
            txt_coinDispensed2.Text = "";
            txt_coinDispensed3.Text = "";
            txt_coinDispensed4.Text = "";
            txt_coinDispensed5.Text = "";

            ResetAllocatedSpaces();
        }

        private bool ResetAllocatedSpaces()
        {
            CarparkManager.Instance.GetCarpark(3).ResetSpaces();

            UpdateSpaces();
            return true;
        }


        #endregion

        
    }
}
