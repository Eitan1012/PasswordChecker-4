namespace PasswordChecker;

public partial class Form1 : Form
{
    private int minimumPasswordLength = 12;
    private bool requireUppercaseSetting = true;
    private bool requireLowercaseSetting = true;
    private bool requireNumberSetting = true;
    private bool requireSpecialSetting = true;

    public Form1()
    {
        InitializeComponent();
        Text = "Password Checker";
        TextBox passwordBox = new TextBox();
        passwordBox.Location = new Point(30, 30);
        passwordBox.Width = 300;
        passwordBox.UseSystemPasswordChar = true;
        Controls.Add(passwordBox);
        FlowLayoutPanel requirementsPanel = new FlowLayoutPanel();
        requirementsPanel.Location = new Point(30, 130);
        requirementsPanel.Size = new Size(320, 180);
        requirementsPanel.FlowDirection = FlowDirection.TopDown;
        requirementsPanel.WrapContents = false;
        requirementsPanel.AutoScroll = true;
        Controls.Add(requirementsPanel);
        void RefreshRequirments()
        {
         requirementsPanel.Controls.Clear();
         void AddRequirement(string text, bool isValid = false)
            {
                
                Label label = new Label();
                label.Text = (isValid ? "✓ " : "✗ ") + text;
                label.ForeColor = isValid ? Color.Green : Color.Red;
                label.AutoSize = true;
                label.Margin = new Padding(3, 5, 3, 5);
                requirementsPanel.Controls.Add(label);
            }   
            bool isLengthValid = passwordBox.Text.Length >= minimumPasswordLength;
            AddRequirement("At least " + minimumPasswordLength + " characters", isLengthValid);

            if (requireUppercaseSetting)
            {
                bool hasUppercase = passwordBox.Text.Any(c => c >= 'A' && c <= 'Z');
                AddRequirement("At least one uppercase letter (A-Z)", hasUppercase);
            }
            if (requireLowercaseSetting)
            {
                bool hasLowercase = passwordBox.Text.Any(c => c >= 'a' && c <= 'z');
                AddRequirement("At least one lowercase letter (a-z)", hasLowercase);
            }
            if (requireNumberSetting)
            {
                bool hasNumber = passwordBox.Text.Any(c => c >= '0' && c <= '9');
                AddRequirement("At least one number (0-9)", hasNumber);
            }
            if (requireSpecialSetting)
            {
                bool hasSpecial = passwordBox.Text.Any(c => "!@#$%".Contains(c));
                AddRequirement("At least one special character (!@#$%)", hasSpecial);
            }
        }
        RefreshRequirments();
        passwordBox.TextChanged += (sender, e) =>
        {
            RefreshRequirments();
        };
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
            NumericUpDown minLengthInput = new NumericUpDown();
            minLengthInput.Minimum = 4;
            minLengthInput.Maximum = 64;
            minLengthInput.Value = minimumPasswordLength;
            minLengthInput.Location = new Point(200, 30);
            minLengthInput.Width = 80;
            settingsForm.Controls.Add(minLengthInput);
            Label minLengthLabel = new Label();
            minLengthLabel.Text = "Minimum Password Length:";
            minLengthLabel.Location = new Point(20, 33);
            minLengthLabel.AutoSize = true;
            settingsForm.Controls.Add(minLengthLabel);
            CheckBox requireUppercase = new CheckBox();
            requireUppercase.Text = "Require Uppercase Letters (A-Z)";
            requireUppercase.Location = new Point(20, 80);
            requireUppercase.AutoSize = true;
            requireUppercase.Checked = requireUppercaseSetting;
            settingsForm.Controls.Add(requireUppercase);
            CheckBox requireLowercase = new CheckBox();
            requireLowercase.Text = "Require Lowercase Letters (a-z)";
            requireLowercase.Location = new Point(20, 110);
            requireLowercase.AutoSize = true;
            requireLowercase.Checked = requireLowercaseSetting;
            settingsForm.Controls.Add(requireLowercase);
            CheckBox requireNumber = new CheckBox();
            requireNumber.Text = "Require Numbers (0-9)";
            requireNumber.Location = new Point(20, 140);
            requireNumber.AutoSize = true;
            requireNumber.Checked = requireNumberSetting;
            settingsForm.Controls.Add(requireNumber);
            CheckBox requireSpecial = new CheckBox();
            requireSpecial.Text = "Require Special Characters (!@#$%)";
            requireSpecial.Location = new Point(20, 170);
            requireSpecial.AutoSize = true;
            requireSpecial.Checked = requireSpecialSetting;
            settingsForm.Controls.Add(requireSpecial);
            Button saveButton = new Button();
            saveButton.Text = "Save Settings";
            saveButton.Location = new Point(120, 230);
            saveButton.Size = new Size(130, 40);
            settingsForm.Controls.Add(saveButton);
            saveButton.Click += (sender, e) =>
            {
                minimumPasswordLength = (int)minLengthInput.Value;
                requireUppercaseSetting = requireUppercase.Checked;
                requireLowercaseSetting = requireLowercase.Checked;
                requireNumberSetting = requireNumber.Checked;
                requireSpecialSetting = requireSpecial.Checked;
                RefreshRequirments();
                settingsForm.Close();
            };  
            
            settingsForm.ShowDialog();

        };  
    }
}
