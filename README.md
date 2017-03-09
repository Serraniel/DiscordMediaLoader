# Discord Media Loader
*Discord Media Loader* is a small tool for downloading all attachments of *Discord* servers. 

**[Download](https://github.com/Serraniel/DiscordMediaLoader/releases)**


## License
Apache License 2.0


## Functionality
* Log into your discord account and gathering information about servers (guilds) and their channels
* Downloading attachments
  * You can optionally specify a date. Only newer messages will be scanned
  * You can specify a destination path
  * You can specify amount of parallel workers
  * You can skip existing for speeding up repeated scans (messages still must be scanned)
  
  
## Requirements
 * [.Net Framework 4.6](https://www.microsoft.com/en-us/download/details.aspx?id=48137) by Microsoft

## How to use
First things first: **Do not use if you have MFA enabled** as long as login is only supported via username and password. That might get you lost your account. A switch to a token based login will come soon™!

Otherwise you may just do the following steps:
 1. Login
 2. Select a guild
 3. Select a channel
 4. Select a directory to save the files
 5. Do other settings if wished
 6. Press the magic button to download stuff
