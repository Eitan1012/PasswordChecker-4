namespace PasswordChecker;

public partial class Form1 : Form
{
    private int minimumPasswordLength = 12;
    private bool requireUppercaseSetting = true;
    private bool requireLowercaseSetting = true;
    private bool requireNumberSetting = true;
    private bool requireSpecialSetting = true;
    private bool wasPasswordValid = false;

    public Form1()
    {
        InitializeComponent();
        Text = "Password Checker";
        ClientSize = new Size(440, 760);
        StartPosition = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        TextBox passwordBox = new TextBox();
        passwordBox.Location = new Point(30, 30);
        passwordBox.Width = 300;
        passwordBox.UseSystemPasswordChar = true;
        Controls.Add(passwordBox);
        Button eyeButton = new Button();
        eyeButton.Text = "👁";
        eyeButton.Location = new Point(335, 30);
        eyeButton.Size = new Size(40, 25);
        eyeButton.Click += (sender, e) =>
        {
            passwordBox.UseSystemPasswordChar = !passwordBox.UseSystemPasswordChar;
        };
        Controls.Add(eyeButton);
        Button copyButton = new Button();
        copyButton.Font = new Font("Segoe Fluent Icons", 10);
        copyButton.Text = "\uE8C8"; // Unicode for copy icon
        copyButton.Location = new Point(380, 30);
        copyButton.Size = new Size(40, 25);
        copyButton.Click += (sender, e) =>
        {
            if (!string.IsNullOrEmpty(passwordBox.Text))
            {
                Clipboard.SetText(passwordBox.Text);
            }
        };
        Controls.Add(copyButton);
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
         bool allRequirementsValid = true;
         void AddRequirement(string text, bool isValid = false)
            {
                allRequirementsValid &= isValid;
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
            if (allRequirementsValid && !wasPasswordValid)
            {
                MessageBox.Show("Password is valid!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                wasPasswordValid = true;
            }
            else if (!allRequirementsValid)
            {
                wasPasswordValid = false;
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
            settingsForm.Size = new Size(440, 760);
            settingsForm.StartPosition = FormStartPosition.CenterParent;
            settingsForm.FormBorderStyle = FormBorderStyle.FixedSingle;
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
        Panel welcomePanel = new Panel();
        welcomePanel.Dock = DockStyle.Fill;
        welcomePanel.BackColor = Color.FromArgb(16, 24, 40);
        Controls.Add(welcomePanel);
        welcomePanel.BringToFront();
        Label welcomeTitle = new Label();
        welcomeTitle.Text = "Password Checker";
        welcomeTitle.Font = new Font("Segoe UI", 24, FontStyle.Bold);
        welcomeTitle.ForeColor = Color.White;
        welcomeTitle.AutoSize = true;
        welcomeTitle.Location = new Point(45, 180);
        welcomePanel.Controls.Add(welcomeTitle);
        Label welcomeSlogan = new Label();
        welcomeSlogan.Text = "Your shortcut to safer passwords";
        welcomeSlogan.Font = new Font("Segoe UI", 11);
        welcomeSlogan.ForeColor = Color.LightGray;
        welcomeSlogan.AutoSize = true;
        welcomeSlogan.Location = new Point(45, 235);
        welcomePanel.Controls.Add(welcomeSlogan);
        welcomeSlogan.Left = welcomeTitle.Left + (welcomeTitle.Width - welcomeSlogan.Width) / 2;
        Button getStartedButton = new Button();
        getStartedButton.Text = "Get Started";
        getStartedButton.Font = new Font("Segoe UI", 13, FontStyle.Bold);
        getStartedButton.ForeColor = Color.FromArgb(16, 24, 40);
        getStartedButton.BackColor = Color.FromArgb(65, 211, 170);
        getStartedButton.FlatStyle = FlatStyle.Flat;
        getStartedButton.FlatAppearance.BorderSize = 0;
        getStartedButton.Size = new Size(250, 55);
        getStartedButton.Location = new Point(95, 350);
        getStartedButton.Cursor = Cursors.Hand;
        welcomePanel.Controls.Add(getStartedButton);
                    Panel settingsPanel = new Panel();
            settingsPanel.Dock = DockStyle.Fill;
            settingsPanel.BackColor = Color.FromArgb(16, 24, 40);
            settingsPanel.Visible = false;
            Controls.Add(settingsPanel);
            Label settingsTitle = new Label();
            settingsTitle.Text = "Password Settings";
            settingsTitle.Font = new Font("Segoe UI", 24, FontStyle.Bold);
            settingsTitle.ForeColor = Color.White;
            settingsTitle.AutoSize = true;
            settingsTitle.Location = new Point(40, 85);
            settingsPanel.Controls.Add(settingsTitle);
            Label minLengthSettingsLabel = new Label();
            minLengthSettingsLabel.Text = "Minimum Length:";
            minLengthSettingsLabel.Font = new Font("Segoe UI", 12);
            minLengthSettingsLabel.ForeColor = Color.White;
            minLengthSettingsLabel.AutoSize = true;
            minLengthSettingsLabel.Location = new Point(40, 185);
            settingsPanel.Controls.Add(minLengthSettingsLabel);
            NumericUpDown minLengthSettingsInput = new NumericUpDown();
            minLengthSettingsInput.Minimum = 4;
            minLengthSettingsInput.Maximum = 64;
            minLengthSettingsInput.Value = minimumPasswordLength;
            minLengthSettingsInput.Font = new Font("Segoe UI", 11);
            minLengthSettingsInput.Location = new Point(300, 183);
            minLengthSettingsInput.Size = new Size(85, 30);
            settingsPanel.Controls.Add(minLengthSettingsInput);
            CheckBox requireUppercaseSettings = new CheckBox();
            requireUppercaseSettings.Text = "Require Uppercase Letters (A-Z)";
            requireUppercaseSettings.Font = new Font("Segoe UI", 12);
            requireUppercaseSettings.ForeColor = Color.White;
            requireUppercaseSettings.BackColor = Color.Transparent;
            requireUppercaseSettings.Location = new Point(40, 260);
            requireUppercaseSettings.AutoSize = true;
            requireUppercaseSettings.Checked = requireUppercaseSetting;
            settingsPanel.Controls.Add(requireUppercaseSettings);
            CheckBox requireLowercaseSettings = new CheckBox();
            requireLowercaseSettings.Text = "Require Lowercase Letters (a-z)";
            requireLowercaseSettings.Font = new Font("Segoe UI", 12);
            requireLowercaseSettings.ForeColor = Color.White;
            requireLowercaseSettings.BackColor = Color.Transparent;
            requireLowercaseSettings.Location = new Point(40, 300);
            requireLowercaseSettings.AutoSize = true;
            requireLowercaseSettings.Checked = requireLowercaseSetting;
            settingsPanel.Controls.Add(requireLowercaseSettings);
            CheckBox requireNumberSettings = new CheckBox();
            requireNumberSettings.Text = "Require Numbers (0-9)";
            requireNumberSettings.Font = new Font("Segoe UI", 12);
            requireNumberSettings.ForeColor = Color.White;
            requireNumberSettings.BackColor = Color.Transparent;
            requireNumberSettings.Location = new Point(40, 340);
            requireNumberSettings.AutoSize = true;
            requireNumberSettings.Checked = requireNumberSetting;
            settingsPanel.Controls.Add(requireNumberSettings);
            CheckBox requireSpecialSettings = new CheckBox();
            requireSpecialSettings.Text = "Require Special Characters (!@#$%)";
            requireSpecialSettings.Font = new Font("Segoe UI", 12);
            requireSpecialSettings.ForeColor = Color.White;
            requireSpecialSettings.BackColor = Color.Transparent;
            requireSpecialSettings.Location = new Point(40, 380);
            requireSpecialSettings.AutoSize = true;
            requireSpecialSettings.Checked = requireSpecialSetting;
            settingsPanel.Controls.Add(requireSpecialSettings);
            Button saveContinueButton = new Button();
            saveContinueButton.Text = "Save & Continue";
            saveContinueButton.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            saveContinueButton.ForeColor = Color.FromArgb(16, 24, 40);
            saveContinueButton.BackColor = Color.FromArgb(65, 211, 170);
            saveContinueButton.FlatStyle = FlatStyle.Flat;
            saveContinueButton.FlatAppearance.BorderSize = 0;
            saveContinueButton.Size = new Size(250, 55);
            saveContinueButton.Location = new Point(95, 520);
            saveContinueButton.Cursor = Cursors.Hand;
            settingsPanel.Controls.Add(saveContinueButton);
            saveContinueButton.Click += (sender, e) =>
            {
                minimumPasswordLength = (int)minLengthSettingsInput.Value;
                requireUppercaseSetting = requireUppercaseSettings.Checked;
                requireLowercaseSetting = requireLowercaseSettings.Checked;
                requireNumberSetting = requireNumberSettings.Checked;
                requireSpecialSetting = requireSpecialSettings.Checked;
                RefreshRequirments();
                settingsPanel.Visible = false;
            };


        getStartedButton.Click += (sender, e) =>
        {
            welcomePanel.Visible = false;
            settingsPanel.Visible = true;
            settingsPanel.BringToFront();
        };
    }
}
