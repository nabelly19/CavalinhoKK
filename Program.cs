using System;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.VisualBasic;


ApplicationConfiguration.Initialize();

var pb = new PictureBox { Dock = DockStyle.Fill, };

var timer = new System.Windows.Forms.Timer { Interval = 20, };

Bitmap bmp = null;
Graphics g = null;

var form = new PlayerAnimation()
{
    WindowState = FormWindowState.Maximized,
    FormBorderStyle = FormBorderStyle.None,
    Text = "Sprite-Test",
    Controls = { pb }
};

form.Load += (o, e) =>
{
    bmp = new Bitmap(pb.Width, pb.Height);
    g = Graphics.FromImage(bmp);
    g.Clear(Color.Black);
    pb.Image = bmp;
    form.SetUp();
    timer.Start();
};

timer.Tick += (o, e) =>
{
    g.Clear(Color.Black);
    form.TimerEvent(o, e);
    form.Draw(g);

    pb.Refresh();
};
form.KeyDown += (o, e) =>
{
    switch (e.KeyCode)
    {
        case Keys.W:
            form.goUp = true;
            break;

        case Keys.A:
            form.goLeft = true;

            break;

        case Keys.S:
            form.goDown = true;

            break;

        case Keys.D:
            form.goRight = true;
            break;
    };
};

form.KeyUp += (o, e) =>
{
    switch (e.KeyCode)
    {
        case Keys.W:
            form.goUp = false;
            break;

        case Keys.A:
            form.goLeft = false;

            break;

        case Keys.S:
            form.goDown = false;

            break;

        case Keys.D:
            form.goRight = false;
            break;
    }
};
Application.Run(form);
public class PlayerAnimation : Form
{
    Bitmap bmp = null;
    Graphics g = null;
    Image player;
    List<string> pMove = new List<string>();
    public int steps = 0;
    public int slowFrameRate = 0;
    public bool goLeft, goRight, goUp, goDown;
    public int X;
    public int Y;
    public int height = 50;
    public int width = 50;
    public int speed = 10;


    public PlayerAnimation()
    {

    }

    public void KeyIsDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Left)
            goLeft = true;
        if (e.KeyCode == Keys.Right)
            goRight = true;
        if (e.KeyCode == Keys.Up)
            goUp = true;
        if (e.KeyCode == Keys.Down)
            goDown = true;
    }

    public void KeyIsUp(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Left)
            goLeft = false;
        if (e.KeyCode == Keys.Right)
            goRight = false;
        if (e.KeyCode == Keys.Up)
            goUp = false;
        if (e.KeyCode == Keys.Down)
            goDown = false;
    }

    private void FormEvent(object sender, PaintEventArgs e)
    {
        g = e.Graphics;

        g.DrawImage(player, X, Y, width, height);
    }

    public void TimerEvent(object sender, EventArgs e)
    {
        if (goLeft && X > 0)
        {
            X -= speed;
            AnimatePLayer(5, 8);
        }
        if (goRight && X + width < this.ClientSize.Width)
        {
            X += speed;
            AnimatePLayer(9, 12);
        }
        if (goUp && Y > 0)
        {
            Y -= speed;
            AnimatePLayer(13, 16);
        }
        if (goDown && Y + height < this.ClientSize.Height)
        {
            Y += speed;
            AnimatePLayer(1, 4);
        }

    }

    public void SetUp()
    {
        // this.BackgroundImage = Image.FromFile("../../SPRITE/images.jpg");
        // this.BackgroundImageLayout = ImageLayout.Stretch;
        // this.DoubleBuffered = true;

        pMove = Directory.GetFiles("./SPRITES/SPRITE/", "*.png").ToList();
        player = Image.FromFile(pMove[0]);
        foreach (var item in pMove)
        {
            MessageBox.Show(item.ToString());

        }
    }

    public void AnimatePLayer(int start, int end)
    {
        slowFrameRate += 1;

        if (slowFrameRate == 4)
        {
            steps++;
            slowFrameRate = 0;
        }

        if (steps > end || steps < start)
        {
            steps = start;
        }

        player = Image.FromFile(pMove[steps]);
    }
    public void Draw(Graphics g)
    {
        g.DrawImage(player, X, Y);
    }
}