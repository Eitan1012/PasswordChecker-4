namespace PasswordChecker;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
        Text = "Password Checker";
        TextBox passwordBox = new TextBox();
        passwordBox.Location = new Point(30, 30);
        passwordBox.Width = 300;
        passwordBox.UseSystemPasswordChar = true;
        Controls.Add(passwordBox);
        Button settingsButton = new Button();
        settingsButton.Text = "";
        settingsButton.BackgroundImage = Image.FromFile(Path.Combine(Application.StartupPath, "Images", "settings_gear.png"));
        settingsButton.BackgroundImageLayout = ImageLayout.Zoom;
        settingsButton.Location = new Point(30, 70);
        settingsButton.Size = new Size(40, 40);
        Controls.Add(settingsButton);
        settingsButton.Click += (sender, e) =>
        {
            Form settingsForm = new Form();
            settingsForm.Text = " Password Settings";
            settingsForm.Size = new Size(400, 350);
            settingsForm.ShowDialog();

        };  
    }
}
