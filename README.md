# ob-remote
Command line remote control for Owlbear Rodeo via [Magic Circle](https://github.com/snailington/magic-circle).  Largely a proof of concept but has full access to the Magic Circle RPC interface, such as item visibility, position, rotation, etc.  It also contains a Dungeons and Dragons Beyond campaign log proxy, that a DM can run to collect rolls for all players in a campaign and display them in Owlbear with no effort required from the players.

## Installation
Download the latest release or pull this repo, and either run ```ob-remote.exe``` or ```dotnet run```. Its been tested primarily on Windows, but it should be fine on Linux and OSX as well. You will likely need to install the [.NET 7.0 runtime](https://dotnet.microsoft.com/en-us/download/dotnet/7.0) if you don't have it already.

## DDB command
### Step by step setup
 1. First of all, ensure all the characters you're interested in are in the same campaign in D&D Beyond.
 2. Then, you'll need to collect some information from the D&D Beyond website for ob-remote to login:
     1. Go to https://www.dndbeyond.com/ and open your web browser's inspector (Ctrl+Shift+I)
     2. Type ```User.Cobalt.ID``` into the console to find your user id
     3. Find your cookies
         - On Chrome this is under Application > Storage > Cookies > dndbeyond.com
         - On Firefox its under Storage > Cookies > dndbeyond.com
     4. Locate your CobaltSession cookie and copy its value. Do not share your session token with anyone.
     5. Go to your campaign's page and copy the game ID number at the end of the URL
 3. Edit the included ddb-example.json and replace the marked areas with your user ID, cobalt token, and game ID

### Notes and observations
- After setup, run ```ob-remote ddb``` and make sure its added to Magic Circle as a source.
- Currently, you may need to refresh Owlbear depending on the order in which you launched things, this will be made a bit smoother in the future. 
- The DDB tunnel should stay up for hours but may occasionally stop working for whatever reason, that may require restarting ```ob-remote ddb```, refreshing Owlbear, or occasionally restarting your browser due to bugs on D&D Beyond's end.
- Your session cookie will eventually expire and ob-remote will no longer be able to log in. If you start getting auth errors, redo the setup to get the new CobaltSession

## Other commands
- Most of the commands in the ob-remote suite require you to run ```ob-remote server``` first, to prepare the source server for Magic Circle to connect to, and proxy RPCs.
- Mostly they take the format ```ob-remote <subcmd> <item id> <subcmd args>```, to find an item's ID, select the item and click this button in Magic Circle:

![2023-05-18_23-13-17](https://github.com/snailington/ob-remote/assets/17015327/634b7df4-df73-4ce2-8a60-fc9e516de4df)
