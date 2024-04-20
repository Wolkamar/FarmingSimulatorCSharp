using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;


namespace SimulationSimulator
{
    public partial class Gameplay : Form
    {
        int time = 0;
        Gamemode game = new Gamemode();

        Dictionary<CheckBox, Cell> field = new Dictionary<CheckBox, Cell>();
        public Gameplay()
        {
            int i = 1;
            InitializeComponent();
            panel1.Cursor = new Cursor(GetType(), "Cursor2.cur");
            if (!File.Exists(Gamemode.sav)) game.newgame();
            foreach (CheckBox cb in panel1.Controls)
            {
                field.Add(cb, new Cell());
                if (game.game_state[i] == 1) field[cb].unlocked = true;
                i++;
                UpdateBox(cb);
            }
            if (File.Exists(Gamemode.sav))
            {
                load();
                button7.Enabled = false;
                timer4.Enabled = true;
            }
        }

        private static void AddText(FileStream fs, string value)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(value);
            fs.Write(info, 0, info.Length);
        }


        public void upgrade(int level)
        {
            if (level == 1)
            {
                button1.Enabled = false;
                button2.Enabled = true;
                button1.BackColor = Color.YellowGreen;
                button2.BackColor = Color.Gold;
                field[checkBox17].unlocked = true;
                UpdateBox(checkBox17);
                field[checkBox18].unlocked = true;
                UpdateBox(checkBox18);
                field[checkBox19].unlocked = true;
                UpdateBox(checkBox19);
                field[checkBox20].unlocked = true;
                UpdateBox(checkBox20);
            }
            if (level == 2)
            {
                button2.Enabled = false;
                button3.Enabled = true;
                button2.BackColor = Color.YellowGreen;
                button3.BackColor = Color.Gold;
                field[checkBox21].unlocked = true;
                UpdateBox(checkBox21);
                field[checkBox22].unlocked = true;
                UpdateBox(checkBox22);
                field[checkBox23].unlocked = true;
                UpdateBox(checkBox23);
                field[checkBox24].unlocked = true;
                UpdateBox(checkBox24);
            }
            if (level == 3)
            {
                button3.Enabled = false;
                button4.Enabled = true;
                button3.BackColor = Color.YellowGreen;
                button4.BackColor = Color.Gold;
                field[checkBox25].unlocked = true;
                UpdateBox(checkBox25);
                field[checkBox26].unlocked = true;
                UpdateBox(checkBox26);
                field[checkBox27].unlocked = true;
                UpdateBox(checkBox27);
                field[checkBox28].unlocked = true;
                UpdateBox(checkBox28);
                field[checkBox30].unlocked = true;
                UpdateBox(checkBox30);
                field[checkBox32].unlocked = true;
                UpdateBox(checkBox32);
                game.cost = 7;
            }
            if (level == 4)
            {
                button4.Enabled = false;
                button4.BackColor = Color.YellowGreen;
                field[checkBox33].unlocked = true;
                UpdateBox(checkBox33);
                field[checkBox34].unlocked = true;
                UpdateBox(checkBox34);
                field[checkBox35].unlocked = true;
                UpdateBox(checkBox35);
                field[checkBox36].unlocked = true;
                UpdateBox(checkBox36);
                field[checkBox29].unlocked = true;
                UpdateBox(checkBox29);
                field[checkBox31].unlocked = true;
                UpdateBox(checkBox31);
                game.cost = 10;
            }
        }
        

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if ((game.money > 0) && (game.money < 2000))
            {
                CheckBox cb = (sender as CheckBox);
                if (field[cb].unlocked == true)
                {
                    if (cb.Checked) Plant(cb);
                    else Harvest(cb);
                    UpdateUI();
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            game.days++;
            if (game.days == 501) game.difficulty++;
            if (game.days == 1001) game.difficulty++;
            if (game.days % (25 - game.difficulty*5) == 0) game.money--;
            UpdateUI();
            foreach (CheckBox cb in panel1.Controls)
                NextStep(cb);
        }

        private void Plant(CheckBox cb)
        {
            System.Media.SoundPlayer plant_sound = new System.Media.SoundPlayer("plant_sound.wav");
            game.money -= 2;
            field[cb].Plant();
            UpdateBox(cb);
            if (timer2.Enabled == false) plant_sound.Play();
        }

        private void Harvest(CheckBox cb)
        {
            System.Media.SoundPlayer harvest_sound = new System.Media.SoundPlayer("harvest_sound.wav");
            if (timer2.Enabled == false) harvest_sound.Play();
            switch (field[cb].state)
            {
                case CellState.Planted: game.money--;
                    break;
                case CellState.Green: game.money--;
                    break;
                case CellState.Immature: game.money += game.cost - 2;
                    break;
                case CellState.Mature: game.money += game.cost;
                    break;
                case CellState.Overgrown: game.money--;
                    break;
            }
            field[cb].Harvest();
            UpdateBox(cb);
        }

        public void timercaller()
        {
            timer4.Enabled = true;
        }

        public void NextStep(CheckBox cb)
        {
            field[cb].NextStep();
            UpdateBox(cb);
        }

            public void UpdateUI()
        {
            label2.Text = "Money: " + game.money + "$";
            label1.Text = "Day: " + game.days;
            if (game.money < 1)
            {
                label2.Text = "Gameover";
                timer1.Enabled = false;
            }
            else if (game.money > 1999)
            {
                label2.Text = "You win!";
                timer1.Enabled = false;
                MessageBox.Show("Congratulations!\nYou have wasted: " + time/60 + " minutes of your time..");
            }
        }

        private void UpdateBox(CheckBox cb)
        {
            if (field[cb].unlocked == true) cb.Image = SimulationSimulator.Properties.Resources.Grass;
            if (field[cb].unlocked == false) cb.Image = SimulationSimulator.Properties.Resources.Rock;
            switch (field[cb].state)
            {
                case CellState.Planted:
                    cb.Image = SimulationSimulator.Properties.Resources.Dirt;
                    break;
                case CellState.Green:
                    cb.Image = SimulationSimulator.Properties.Resources.Green;
                    break;
                case CellState.Immature:
                    cb.Image = SimulationSimulator.Properties.Resources.Immature;
                    break;
                case CellState.Mature:
                    cb.Image = SimulationSimulator.Properties.Resources.Mature;
                    break;
                case CellState.Overgrown:
                    cb.Image = SimulationSimulator.Properties.Resources.Overgrown;
                    break;
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (trackBar1.Value == 0) timer1.Enabled = false;
            else
            {
                if ((game.money > 0) && (game.money < 2000)) 
                {
                    timer1.Enabled = true;
                    timer1.Interval = 140 - trackBar1.Value * 10;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer3.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (game.money > 49)
            {
                game.upgrades++;
                upgrade(game.upgrades);
                game.money -= 50;
                UpdateUI();
                game.field_upgrade(game.upgrades);
                System.Media.SoundPlayer money_sound = new System.Media.SoundPlayer("money_sound.wav");
                money_sound.Play();
            }
            else
            {
                timer2.Enabled = false;
                System.Media.SoundPlayer denying = new System.Media.SoundPlayer("denying.wav");
                denying.Play();
                timer2.Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (game.money > 99)
            {
                game.upgrades++;
                upgrade(game.upgrades);
                game.money -= 100;
                UpdateUI();
                game.field_upgrade(game.upgrades);
                System.Media.SoundPlayer money_sound = new System.Media.SoundPlayer("money_sound.wav");
                money_sound.Play();
            }
            else
            {
                timer2.Enabled = false;
                System.Media.SoundPlayer denying = new System.Media.SoundPlayer("denying.wav");
                denying.Play();
                timer2.Enabled = true;
            }
        }

        /*public void Refreshing()
        {
            foreach (CheckBox cb in panel1.Controls)
            {
                int i = 1;
                if (game.game_state[i] == 1) field[cb].unlocked = true;
                else field[cb].unlocked = false;
                i++;
                UpdateBox(cb);
            }
        }*/

        private void button3_Click(object sender, EventArgs e)
        {
            if (game.money > 199)
            {
                game.upgrades++;
                upgrade(game.upgrades);
                game.money -= 200;
                UpdateUI();
                game.cost = 7;
                game.field_upgrade(game.upgrades);
                System.Media.SoundPlayer money_sound = new System.Media.SoundPlayer("money_sound.wav");
                money_sound.Play();
            }
            else
            {
                timer2.Enabled = false;
                System.Media.SoundPlayer denying = new System.Media.SoundPlayer("denying.wav");
                denying.Play();
                timer2.Enabled = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (game.money > 299)
            {
                game.upgrades++;
                upgrade(game.upgrades);
                game.money -= 300;
                UpdateUI();
                game.cost = 10;
                game.field_upgrade(game.upgrades);
                System.Media.SoundPlayer money_sound = new System.Media.SoundPlayer("money_sound.wav");
                money_sound.Play();
            }
            else
            {
                timer2.Enabled = false;
                System.Media.SoundPlayer denying = new System.Media.SoundPlayer("denying.wav");
                denying.Play();
                timer2.Enabled = true;
            }
        }

        private void Gameplay_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            System.Media.SoundPlayer click_sound = new System.Media.SoundPlayer("click_sound.wav");
            click_sound.Play();
            Application.Exit();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            System.Media.SoundPlayer click_sound = new System.Media.SoundPlayer("click_sound.wav");
            click_sound.Play();
            int i = 1;
            time = 0;
            game.newgame();
            foreach (CheckBox cb in panel1.Controls)
            {
                if (game.game_state[i] == 1) field[cb].unlocked = true;
                else field[cb].unlocked = false;
                field[cb].state = CellState.Empty;
                field[cb].progress = 0;
                cb.Checked = false;
                i++;
                UpdateBox(cb);
            }
            UpdateUI();
            game.upgrades = 0;
            button1.BackColor = Color.Gold;
            button1.Enabled = true;
            button2.BackColor = Color.DarkRed;
            button2.Enabled = false;
            button3.BackColor = Color.DarkRed;
            button3.Enabled = false;
            button4.BackColor = Color.DarkRed;
            button4.Enabled = false;
            trackBar1.Value = 0;
            timer1.Enabled = false;
            game.cost = 5;
        }

        public void savegame()
        {
            if (File.Exists(Gamemode.sav))
            {
                File.Create(Gamemode.sav).Close();
            }
            FileStream fs = File.Create(Gamemode.sav);
            int j = 1;
            game.field_upgrade(game.upgrades);
            game.game_state[0] = game.days;
            AddText(fs, game.game_state[0].ToString());
            AddText(fs, " ");
            game.game_state[Gamemode.mem - 1] = game.money;
            foreach (CheckBox cb in panel1.Controls)
            {
                if ((field[cb].state != CellState.Empty) && (game.game_state[j] == 1))
                {
                    game.game_state[j] = field[cb].progress + 2;
                }
                AddText(fs, game.game_state[j].ToString());
                AddText(fs, " ");
                j++;
            }
            AddText(fs, game.game_state[Gamemode.mem - 1].ToString());
            AddText(fs, " ");
            AddText(fs, time.ToString());
        }

        public void load()
        {
            int j = 1;
            string str;
            StreamReader sr = new StreamReader(Gamemode.sav);
            str = sr.ReadToEnd();
            sr.Close();
            string[] arr = str.Split(' ');
            for (int i = 0; i < arr.GetUpperBound(0) + 1; i++) game.game_state[i] = int.Parse(arr[i]);
            game.days = game.game_state[0];
            game.money = game.game_state[Gamemode.mem - 1];
            UpdateUI();
            int counter = 0;
            if (game.days > 500) game.difficulty = 2;
            if (game.days > 1000) game.difficulty = 3;
            foreach (CheckBox cb in panel1.Controls)
            {
                if (game.game_state[j] == 0)
                {
                    field[cb].unlocked = false;
                    cb.Checked = false;
                    field[cb].progress = 0;
                    UpdateBox(cb);
                    counter++;
                }
                else if (game.game_state[j] == 1)
                {
                    field[cb].unlocked = true;
                    cb.Checked = false;
                    field[cb].progress = 0;
                    field[cb].state = CellState.Empty;
                    UpdateBox(cb);
                }


                else if (game.game_state[j] - 2 < 20)
                {
                    field[cb].unlocked = true;
                    cb.Checked = true;
                    field[cb].progress = game.game_state[j] - 2;
                    field[cb].state = CellState.Planted;
                    UpdateBox(cb);
                }
                else if (game.game_state[j] - 2 < 100)
                {
                    field[cb].unlocked = true;
                    cb.Checked = true;
                    field[cb].progress = game.game_state[j] - 2;
                    field[cb].state = CellState.Green;
                    UpdateBox(cb);
                }
                else if (game.game_state[j] - 2 < 120)
                {
                    field[cb].unlocked = true;
                    cb.Checked = true;
                    field[cb].progress = game.game_state[j] - 2;
                    field[cb].state = CellState.Immature;
                    UpdateBox(cb);
                }
                else if (game.game_state[j] - 2 < 140)
                {
                    field[cb].unlocked = true;
                    cb.Checked = true;
                    field[cb].progress = game.game_state[j] - 2;
                    field[cb].state = CellState.Mature;
                    UpdateBox(cb);
                }
                else if (game.game_state[j] - 2 > 139)
                {
                    field[cb].unlocked = true;
                    cb.Checked = true;
                    field[cb].progress = game.game_state[j] - 2;
                    field[cb].state = CellState.Overgrown;
                    UpdateBox(cb);
                }
                j++;
            }
            if (counter < 5)
            {
                game.upgrades = 4;
                upgrade(1);
                upgrade(2);
                upgrade(3);
                upgrade(4);
            }
            else if (counter < 11)
            {
                game.upgrades = 3;
                upgrade(1);
                upgrade(2);
                upgrade(3);
            }
            else if (counter < 15)
            {
                game.upgrades = 2;
                upgrade(1);
                upgrade(2);
            }
            else if (counter < 19)
            {
                game.upgrades = 1;
                upgrade(1);
            }
            time = game.game_state[Gamemode.mem];
        }


        private void button5_MouseEnter(object sender, EventArgs e)
        {
            button5.Image = (SimulationSimulator.Properties.Resources.ex_click);
        }

        private void button5_MouseLeave(object sender, EventArgs e)
        {
            button5.Image = (SimulationSimulator.Properties.Resources.ex);
        }

        private void button6_MouseEnter(object sender, EventArgs e)
        {
            button6.Image = (SimulationSimulator.Properties.Resources.reset_hov);
        }

        private void button6_MouseLeave(object sender, EventArgs e)
        {
            button6.Image = (SimulationSimulator.Properties.Resources.reset);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            savegame();
            System.Media.SoundPlayer click_sound = new System.Media.SoundPlayer("click_sound.wav");
            if (timer2.Enabled == false) click_sound.Play();
            button7.Enabled = false;
            timer4.Enabled = true;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Enabled = false;
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            time++;
        }

        private void button7_MouseEnter(object sender, EventArgs e)
        {
            button7.Image = Properties.Resources.sav_hov;
        }

        private void button7_MouseLeave(object sender, EventArgs e)
        {
            button7.Image = Properties.Resources.sav;
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            button7.Enabled = true;
            timer4.Enabled = false;
        }
    }

    enum CellState
    {
        Empty,
        Planted,
        Green,
        Immature,
        Mature,
        Overgrown
    }

    class Gamemode
    {
        public int cost = 5;
        public int[] game_state;
        public int money;
        public int days;
        public int difficulty = 1;
        public const int mem = 38;
        public const string sav = "save.txt";
        public int upgrades = 0;
        public int k = 0;
        public Gamemode()
        {
            game_state = new int[mem + 1];
        }



        public void newgame()
        {
            difficulty = 1;
            upgrades = 0;
            money = 100;
            days = 1;
            field_upgrade(0);
        }

        public void field_upgrade(int upgrades)
        {
            this.upgrades = upgrades;
            switch (upgrades)
            {
                case 0: k = 21;
                    break;
                case 1: k = 17;
                    break;
                case 2: k = 13;
                    break;
                case 3: k = 7;
                    break;
                case 4: k = 1;
                    break;
            }
            for (int i = 1; i < k; i++) game_state[i] = 0;
            for (int i = k; i < mem - 1; i++) game_state[i] = 1;
        }
    }

    class Cell
    {
        public CellState state = CellState.Empty;
        public bool unlocked = false;
        public int progress = 0;

        private const int prPlanted = 20;
        private const int prGreen = 100;
        private const int prImmature = 120;
        private const int prMature = 140;

        public void Plant()
        {
            state = CellState.Planted;
            progress = 1;
        }

        public void Harvest()
        {
            state = CellState.Empty;
            progress = 0;
        }


        public void NextStep()
        {
            if ((state != CellState.Empty) && (state != CellState.Overgrown))
            {
                progress++;
                if (progress < prPlanted) state = CellState.Planted;
                else if (progress < prGreen) state = CellState.Green;
                else if (progress < prImmature) state = CellState.Immature;
                else if (progress < prMature) state = CellState.Mature;
                else
                {
                    state = CellState.Overgrown;
                    System.Media.SoundPlayer doh = new System.Media.SoundPlayer("doh.wav");
                    doh.Play();
                }

            }
        }
    }

}
