--Requires
local CLI = require "FredOS.CLI";
local FS = require "FredOS.FileSystem";

--OS
CLI.writeline("FredOS has started");

while true do
	
	local command = CLI.readline();

	if command == "help" then
		CLI.writeline("-----------HELP-------------");	
		CLI.writeline("help - shows this list");	
		CLI.writeline("cat - read the content of a file");	
		CLI.writeline("----------------------------");	
	elseif command == "cat" then
		CLI.writeline(FS.readalltext(CLI.readalltext()));
	end


end