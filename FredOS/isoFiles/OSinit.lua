-- Requires
local CLI = require "FredOS.CLI";
local FS = require "FredOS.FileSystem";
local os = require "os";

-- Needs time and date api and fix for filesystem commands!

-- OS
CLI.writeline("FredOS has started");

while true do
    local command = CLI.readline();

    if command == "help" then
        CLI.writeline("-----------HELP-------------");
        CLI.writeline("help - shows this list");
        CLI.writeline("cat <filename> - read the content of a file");
        CLI.writeline("add <filename> <content> - add content to a file");
        CLI.writeline("ls - list files in current directory");
        CLI.writeline("echo <message> - prints the message");
        CLI.writeline("time - displays current time");
        CLI.writeline("date - displays current date");
        CLI.writeline("exit - exit the shell");
        CLI.writeline("----------------------------");
    elseif command:sub(1, 3) == "cat" then
        local filename = command:sub(5) -- Extract filename from command
        local content = FS.readalltext(filename)
        if content then
            CLI.writeline(content)
            CLI.writeline("Press ESC to leave cat")
            while true do
                local key = CLI.readkey()
                if key == CLI.Keys.Escape then
                    break
                end
            end
        else
            CLI.writeline("File not found or unable to read: " .. filename)
        end
    elseif command:sub(1, 3) == "add" then
        local filename, content = command:match("^add%s+(%S+)%s+(.+)$")
        if filename and content then
            local success, error_message = FS.writealltext(filename, content)
            if success then
                CLI.writeline("Content added to file: " .. filename)
            else
                CLI.writeline("Error adding content to file: " .. error_message)
            end
        else
            CLI.writeline("Invalid command. Usage: add <filename> <content>")
        end
    elseif command == "ls" then
        local files = FS.listfiles()
        CLI.writeline("Files in current directory:")
        for _, file in ipairs(files) do
            CLI.writeline(file)
        end
    elseif command:sub(1, 4) == "echo" then
        local message = command:sub(6)
        CLI.writeline(message)
    elseif command == "time" then
        local current_time = os.date("%H:%M:%S")
        CLI.writeline("Current time is: " .. current_time)
    elseif command == "date" then
        local current_date = os.date("%Y-%m-%d")
        CLI.writeline("Current date is: " .. current_date)
    elseif command == "exit" then
        CLI.writeline("Exiting FredOS shell...")
        break
    else
        CLI.writeline("Command not found: " .. command)
    end
    collectgarbage()
end
