# PasswordManager_205

A secure WPF-based desktop password manager built in C# using AES-256 encryption. User credentials are stored locally and encrypted with user-specific keys. Each user has their own encryption key stored in the AppData folder and protected by their master password. if you no longer want these files on your computer: first remove application folder, then navigate to appdata and delete the "passwordmanager" folder.

### Repository
[View on GitHub](https://github.com/SkogsTomte04/PasswordManager_205.git)

---

## Features

- Multi-user support
- AES-256 encrypted password storage
- Per-user encryption keys (stored securely in AppData)
- Password strength analysis
- Credential editing and deletion
- Secure clipboard copy


---

## How to Run

### 1. Launch WpfApp1.exe
### 2. Create a new user
### 3. you might need to resize the window to see all elements
### 4. click the "+" icon next to the search bar to create a new credential
## 5. click the "Import CSV" button to import many passwords from somewhere like google.com
### 6. click the blue "Go to Checkpoint" button to view passwords ratings in strength
### 7. type in the searchbar to find specific credentials based on url, username and email.
