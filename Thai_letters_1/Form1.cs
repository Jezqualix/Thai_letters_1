using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Media;

namespace Thai_letters_1
{
    public partial class Form1 : Form
    {
        int randomRijNr, correctButton, score = 0, tries = 0;
        string[,] thaiTable;
        SoundPlayer splayerCorrect = new SoundPlayer(Properties.Resources.correct);
        SoundPlayer splayerWrong = new SoundPlayer(Properties.Resources.wrong);
        //SoundPlayer spWrong = new SoundPlayer(@"c:\temp\wrong.wav");
        public Form1()
        {
            FillArrays();
            FillArrayWithXML();
            InitializeComponent();
            Randomizer();
        }

        private void FillArrayWithXML() //not finished yet
        {
            //var path = @"C:\Users\dabi\Dropbox\C-sharp\Projects\Thai_letters_1\Thai_letters_1\thaiTable.xml";
            //var thaiTableXML=XDocument.Load(path).Root.Elements().Select(y => y.Elements().ToDictionary(x => x.Name, x => x.Value)).ToArray();
            var thaiTableXML = XDocument.Load(Path.GetFullPath("thaiTable.xml")).Root.Elements().Select(y => y.Elements().ToDictionary(x => x.Name, x => x.Value)).ToArray();
        }

        private void Randomizer()
        {
            Random rnd = new Random();
            int previousRandomRijNr = randomRijNr;
            while (previousRandomRijNr == randomRijNr)
            {
                randomRijNr = rnd.Next(0, thaiTable.GetLength(0)); //Randomizes row number, rerandomize when number is the same, (1) for column number
            }
            lblThaiSymbol.Text = thaiTable[randomRijNr, 0];

            string correctSound = thaiTable[randomRijNr, 1], wrongSound1 = "", wrongSound2 = "", sType = thaiTable[randomRijNr, 4];
            while (correctSound == wrongSound1 || correctSound == wrongSound2 || wrongSound1 == wrongSound2) //making sure not same 'sounds' are getting used and for now only Consonants will be used
            {
                wrongSound1 = thaiTable[rnd.Next(0, thaiTable.GetLength(0)), 1];
                wrongSound2 = thaiTable[rnd.Next(0, thaiTable.GetLength(0)), 1];
            }

            correctButton = rnd.Next(3); //vul de buttons in
            switch (correctButton)
            {
                case 0:
                    button1.Text = correctSound;
                    button2.Text = wrongSound1;
                    button3.Text = wrongSound2;
                    break;
                case 1:
                    button1.Text = wrongSound1;
                    button2.Text = correctSound;
                    button3.Text = wrongSound2;
                    break;
                case 2:
                    button1.Text = wrongSound2;
                    button2.Text = wrongSound1;
                    button3.Text = correctSound;
                    break;
                default:
                    debugText.Text = "Error in correctButton";
                    throw new Exception();
            }
        }


        private async void Scores(bool correct)
        {
            if (correct)
            {
                debugText.BackColor = Color.Green;
                debugText.Text = "Correct!";
                score++;
                tries++;
                scoreBoard.Text = string.Format("{0}/{1}", score, tries);
                lblThaiSymbol.Focus(); //drops focus from buttons
                splayerCorrect.Play();
                await Task.Delay(1000);
            }
            else
            {
                debugText.BackColor = Color.Red;
                debugText.Text = "Incorrect!";
                tries++;
                scoreBoard.Text = string.Format("{0}/{1}", score, tries);
                lblThaiSymbol.Focus(); //drops focus from buttons
                splayerWrong.Play();
                switch (correctButton)
                {
                    case 0:
                        button1.BackColor = Color.Green;
                        await Task.Delay(1000);
                        button1.BackColor = default(Color);
                        break;
                    case 1:
                        button2.BackColor = Color.Green;
                        await Task.Delay(1000);
                        button2.BackColor = default(Color);
                        break;
                    case 2:
                        button3.BackColor = Color.Green;
                        await Task.Delay(1000);
                        button3.BackColor = default(Color);
                        break;
                    default:
                        debugText.Text = "Error in buttonscorecolors";
                        throw new Exception();
                }

            }
            debugText.BackColor = default(Color);
            debugText.Text = "";
            Randomizer();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (correctButton == 0)
            {
                Scores(true);
            }
            else
            {
                Scores(false);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (correctButton == 1)
            {
                Scores(true);
            }
            else
            {
                Scores(false);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (correctButton == 2)
            {
                Scores(true);
            }
            else
            {
                Scores(false);
            }
        }

        private void FillArrays()
        {
            thaiTable = new string[71, 5] //0=Thai symbol, 1=English Sound, 2= English sound end of syllable, 3= class (low, middle,high), 4=type (vowel, consonant, special)
            {
                { "ก", "k", "k", "m", "c" },
                { "ข", "kh", "k", "h", "c" },
                { "ค", "kh", "k", "l", "c" },
                { "ฃ", "kh", "k", "l", "c" },
                { "ง", "ng", "ng", "l", "c" },
                { "จ", "c", "t", "m", "c" },
                { "ฉ", "ch", "t", "h", "c" },
                { "ช", "ch", "t", "l", "c" },
                { "ซ", "s", "t", "l", "c" },
                { "ฌ", "ch", "t", "l", "c" },
                { "ญ", "y", "n", "l", "c" },
                { "ฎ", "d", "t", "m", "c" },
                { "ฏ", "t", "t", "m", "c" },
                { "ฐ", "th", "t", "h", "c" },
                { "ฑ", "th", "t", "l", "c" },
                { "ฒ", "th", "t", "l", "c" },
                { "ณ", "n", "n", "l", "c" },
                { "ด", "d", "t", "m", "c" },
                { "ต", "t", "t", "m", "c" },
                { "ถ", "th", "t", "h", "c" },
                { "ท", "th", "t", "l", "c" },
                { "ธ", "th", "t", "l", "c" },
                { "น", "n", "n", "l", "c" },
                { "บ", "b", "p", "m", "c" },
                { "ป", "p", "p", "m", "c" },
                { "ผ", "ph", "p", "h", "c" },
                { "ฝ", "f", "p", "h", "c" },
                { "พ", "ph", "p", "l", "c" },
                { "ฟ", "f", "p", "l", "c" },
                { "ภ", "ph", "p", "l", "c" },
                { "ม", "m", "m", "l", "c" },
                { "ย", "y", "y", "l", "c" },
                { "ร", "r", "r", "l", "c" },
                { "ล", "l", "n", "l", "c" },
                { "ว", "w", "w", "l", "c" },
                { "ศ", "s", "t", "h", "c" },
                { "ษ", "s", "t", "h", "c" },
                { "ส", "s", "t", "h", "c" },
                { "ห", "h", "-", "h", "c" },
                { "อ", "zero", "-", "m", "s" },
                { "ออ", "oo", "", "", "v" },
                { "อะ", "a", "","","v" },
                { "อั", "a", "","","v" },
                { "อัว", "au", "","","v" },
                { "อา", "aa", "","","v" },
                { "อำ", "am", "","","v" },
                { "อิ", "i", "","","v" },
                { "อี", "ii", "","","v" },
                { "อึ", "u", "","","v" },
                { "อื", "uu", "","","v" },
                { "อุ", "u", "","","v" },
                { "อู", "uu", "","","v" },
                { "เอ", "ee", "","","v" },
                { "เอ็", "e", "","","v" },
                { "เอย", "eey", "","","v" },
                { "เออ", "ee", "","","v" },
                { "เออะ", "e", "","","v" },
                { "เอะ", "e", "","","v" },
                { "เอา", "aw", "","","v" },
                { "เอาะ", "c", "","","v" },
                { "เอิอ", "ee", "","","v" },
                { "เอีย", "ia", "","","v" },
                { "เอียะ", "ia", "","","v" },
                { "เอีอ", "ua", "","","v" },
                { "แอ", "ee", "","","v" },
                { "แอ็อ", "e", "","","v" },
                { "แอะ", "e", "","","v" },
                { "โอ", "oo", "","","v" },
                { "โอะ", "o", "","","v" },
                { "ใอ", "ay", "","","v" },
                { "ไอ", "ay", "","","v" }

            };
        }

    }
}
