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

        Panel checkerPanel = new Panel();
        checkerPanel.Dock = DockStyle.Fill;
        checkerPanel.BackColor = Color.FromArgb(16, 24, 40);
        Controls.Add(checkerPanel);
        checkerPanel.Controls.Add(passwordBox);
        checkerPanel.Controls.Add(eyeButton);
        checkerPanel.Controls.Add(copyButton);
        checkerPanel.Controls.Add(requirementsPanel);
        checkerPanel.Controls.Add(settingsButton);
        settingsButton.Location = new Point(370, 35);
        passwordBox.Location = new Point(40, 200);
        passwordBox.Width = 280;
        eyeButton.Location = new Point(325, 200);
        copyButton.Location = new Point(370, 200);
        eyeButton.Size = new Size(40, 40);
        eyeButton.BackColor = Color.FromArgb(65, 211, 170);
        eyeButton.ForeColor = Color.FromArgb(16, 24, 40);
        eyeButton.Font = new Font("Segoe UI Emoji", 15);
        eyeButton.FlatStyle = FlatStyle.Flat;
        eyeButton.FlatAppearance.BorderSize = 0;
        copyButton.Size = new Size(40, 40);
        copyButton.BackColor = Color.FromArgb(65, 211, 170);
        copyButton.ForeColor = Color.FromArgb(16, 24, 40);
        copyButton.Font = new Font("Segoe Fluent Icons", 16);
        copyButton.FlatStyle = FlatStyle.Flat;
        copyButton.FlatAppearance.BorderSize = 0;
        requirementsPanel.Location = new Point(40, 280);
        Label checkerTitle = new Label();
        checkerTitle.Text = "Check Your Password";
        checkerTitle.Font = new Font("Segoe UI", 22, FontStyle.Bold);
        checkerTitle.ForeColor = Color.White;
        checkerTitle.AutoSize = true;
        checkerTitle.Location = new Point(40, 115);
        checkerPanel.Controls.Add(checkerTitle);

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
            Button backButton = new Button();
            backButton.Text = "→";
            backButton.Font = new Font("Segoe UI", 18);
            backButton.ForeColor = Color.LightGray;
            backButton.BackColor = Color.FromArgb(16, 24, 40);
            backButton.FlatStyle = FlatStyle.Flat;
            backButton.FlatAppearance.BorderSize = 0;
            backButton.Size = new Size(40, 40);
            backButton.Location = new Point(375, 25);
            backButton.Cursor = Cursors.Hand;
            settingsPanel.Controls.Add(backButton);
            backButton.Click += (sender, e) =>
            {
                settingsPanel.Visible = false;
                welcomePanel.Visible = true;
                welcomePanel.BringToFront();
            };
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
            Panel choicePanel = new Panel();
            choicePanel.Dock = DockStyle.Fill;
            choicePanel.BackColor = Color.FromArgb(16, 24, 40);
            Controls.Add(choicePanel);
            Label choiceTitle = new Label();
            choiceTitle.Text = "Choose Your Path";
            choiceTitle.Font = new Font("Segoe UI", 24, FontStyle.Bold);
            choiceTitle.ForeColor = Color.White;
            choiceTitle.AutoSize = true;
            choiceTitle.Location = new Point(65, 170);
            choicePanel.Controls.Add(choiceTitle);
            Button checkPasswordButton = new Button();
            checkPasswordButton.Text = "Check Password";
            checkPasswordButton.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            checkPasswordButton.BackColor = Color.FromArgb(65, 211, 170);
            checkPasswordButton.ForeColor = Color.FromArgb(16, 24, 40);
            checkPasswordButton.FlatStyle = FlatStyle.Flat;
            checkPasswordButton.FlatAppearance.BorderSize = 0;
            checkPasswordButton.Size = new Size(350, 90);
            checkPasswordButton.Location = new Point(45, 300);
            checkPasswordButton.Cursor = Cursors.Hand;
            choicePanel.Controls.Add(checkPasswordButton);
            Button generatePasswordButton = new Button();
            generatePasswordButton.Text = "Generate Password";
            generatePasswordButton.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            generatePasswordButton.BackColor = Color.FromArgb(65, 211, 170);
            generatePasswordButton.ForeColor = Color.FromArgb(16, 24, 40);
            generatePasswordButton.FlatStyle = FlatStyle.Flat;
            generatePasswordButton.FlatAppearance.BorderSize = 0;  
            generatePasswordButton.Size = new Size(350, 90);
            generatePasswordButton.Location = new Point(45, 420);
            generatePasswordButton.Cursor = Cursors.Hand;
            choicePanel.Controls.Add(generatePasswordButton);
            Panel generatedPasswordPanel = new Panel();
            generatedPasswordPanel.Dock = DockStyle.Fill;
            generatedPasswordPanel.BackColor = Color.FromArgb(16, 24, 40);
            Controls.Add(generatedPasswordPanel);
            Label generatedPasswordTitle = new Label();
            generatedPasswordTitle.Text = "Generate Password";
            generatedPasswordTitle.Font = new Font("Segoe UI", 24, FontStyle.Bold);
            generatedPasswordTitle.ForeColor = Color.White;
            generatedPasswordTitle.AutoSize = true;
            generatedPasswordTitle.Location = new Point(40, 150);
            generatedPasswordPanel.Controls.Add(generatedPasswordTitle);
            Label generatedPasswordLabel = new Label();
            generatedPasswordLabel.Text = "Your Generated Password:";
            generatedPasswordLabel.Font = new Font("Segoe UI", 12);
            generatedPasswordLabel.ForeColor = Color.White;
            generatedPasswordLabel.AutoSize = true;
            generatedPasswordLabel.Location = new Point(40, 250);
            generatedPasswordPanel.Controls.Add(generatedPasswordLabel);
            TextBox generatedPasswordBox = new TextBox();
            generatedPasswordBox.Location = new Point(40, 290);
            generatedPasswordBox.Size = new Size(360, 35);
            generatedPasswordBox.Font = new Font("Segoe UI", 13);
            generatedPasswordBox.ReadOnly = true;
            generatedPasswordBox.BackColor = Color.FromArgb(30, 40, 58);
            generatedPasswordBox.ForeColor = Color.White;
            generatedPasswordPanel.Controls.Add(generatedPasswordBox);
            Button generateNewButton = new Button();
            generateNewButton.Text = "Generate";
            generateNewButton.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            generateNewButton.BackColor = Color.FromArgb(65, 211, 170);
            generateNewButton.ForeColor = Color.FromArgb(16, 24, 40);
            generateNewButton.FlatStyle = FlatStyle.Flat;
            generateNewButton.FlatAppearance.BorderSize = 0;
            generateNewButton.Size = new Size(170, 55);
            generateNewButton.Location = new Point(40, 360);
            generateNewButton.Cursor = Cursors.Hand;
            generatedPasswordPanel.Controls.Add(generateNewButton);
            Button copyGeneratedButton = new Button();
            copyGeneratedButton.Text = "Copy";
            copyGeneratedButton.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            copyGeneratedButton.BackColor = Color.FromArgb(65, 211, 170);
            copyGeneratedButton.ForeColor = Color.FromArgb(16, 24, 40);
            copyGeneratedButton.FlatStyle = FlatStyle.Flat;
            copyGeneratedButton.FlatAppearance.BorderSize = 0;
            copyGeneratedButton.Size = new Size(170, 55);
            copyGeneratedButton.Location = new Point(230, 360);
            copyGeneratedButton.Cursor = Cursors.Hand;
            generatedPasswordPanel.Controls.Add(copyGeneratedButton);
            void GenerateNewPassword()
        {
            string uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string lowercase = "abcdefghijklmnopqrstuvwxyz";
            string numbers = "0123456789";
            string special = "!@#$%";
            string allowedCharacters = "";
            List<char> passwordCharacters = new List<char>();
            char GenerateRandomCharacter(string characters)
            {
                int index = System.Security.Cryptography.RandomNumberGenerator.GetInt32(characters.Length);
                return characters[index];

            }
            if (requireUppercaseSetting)
            {
                allowedCharacters += uppercase;
                passwordCharacters.Add(GenerateRandomCharacter(uppercase));
            }
            if (requireLowercaseSetting)
            {
                allowedCharacters += lowercase;
                passwordCharacters.Add(GenerateRandomCharacter(lowercase));
            }
            if (requireNumberSetting)
            {
                allowedCharacters += numbers;
                passwordCharacters.Add(GenerateRandomCharacter(numbers));
            }
            if (requireSpecialSetting)
            {
                allowedCharacters += special;
                passwordCharacters.Add(GenerateRandomCharacter(special));
            }
            if (allowedCharacters.Length == 0)
            {
                allowedCharacters = uppercase + lowercase + numbers + special;
            }
            while (passwordCharacters.Count < minimumPasswordLength)
            {
                passwordCharacters.Add(GenerateRandomCharacter(allowedCharacters));
            }
            for (int i = passwordCharacters.Count - 1; i > 0; i--)
            {
                int j = System.Security.Cryptography.RandomNumberGenerator.GetInt32(i + 1);
                char temp = passwordCharacters[i];
                passwordCharacters[i] = passwordCharacters[j];
                passwordCharacters[j] = temp;
            }
            generatedPasswordBox.Text = new string(passwordCharacters.ToArray());
        }
        generateNewButton.Click += (sender, e) =>
        {
            GenerateNewPassword();
        };
        copyGeneratedButton.Click += (sender, e) =>
        {
            if (!string.IsNullOrEmpty(generatedPasswordBox.Text))
            {
                Clipboard.SetText(generatedPasswordBox.Text);
            }
        };
            generatePasswordButton.Click += (sender, e) =>
            {
                choicePanel.Visible = false;
                generatedPasswordPanel.Visible = true;
                generatedPasswordPanel.BringToFront();
            };
            checkPasswordButton.Click += (sender, e) =>
            {
                choicePanel.Visible = false;
                checkerPanel.BringToFront();
            };
            saveContinueButton.Click += (sender, e) =>
            {
                minimumPasswordLength = (int)minLengthSettingsInput.Value;
                requireUppercaseSetting = requireUppercaseSettings.Checked;
                requireLowercaseSetting = requireLowercaseSettings.Checked;
                requireNumberSetting = requireNumberSettings.Checked;
                requireSpecialSetting = requireSpecialSettings.Checked;
                RefreshRequirments();
                settingsPanel.Visible = false;
                choicePanel.Visible = true;
                choicePanel.BringToFront();

                
        
            };


        getStartedButton.Click += (sender, e) =>
        {
            welcomePanel.Visible = false;
            settingsPanel.Visible = true;
            settingsPanel.BringToFront();
        };
        settingsButton.Click += (sender, e) =>
        {
            minLengthSettingsInput.Value = minimumPasswordLength;
            requireUppercaseSettings.Checked = requireUppercaseSetting;
            requireLowercaseSettings.Checked = requireLowercaseSetting;
            requireNumberSettings.Checked = requireNumberSetting;
            requireSpecialSettings.Checked = requireSpecialSetting;
            welcomePanel.Visible = false;   
            settingsPanel.Visible = true;
            settingsPanel.BringToFront();
        };
    }

}
