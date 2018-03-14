function listToTable(clrlist)
    local t = {}
    local it = clrlist:GetEnumerator()
    while it:MoveNext() do
      t[#t+1] = it.Current
    end
    return t
end

function requirefile(name)
		print("require "..name)
		require(name)
end

function requiredir(name)
	array = listToTable(CS.Tools.StreamDir.GetLuaFileName(name))
	for  i=1,#array do
		requirefile(name..array[i])
	end
end

function loadmod(name)
	print("********** LOAD START "..name)
	requirefile(name.."/info")
	if(on) then
		print(name.." is on")
	else
		print(name.." is not on")
	end
	requiredir(name.."/static")
	requiredir(name.."/event")
	print("********** LOAD END "..name)
end

loadmod("native")

local array = listToTable(CS.Tools.StreamDir.GetSubDirName("mod"))
for  i=1,#array do
	loadmod("mod"..array[i])
end



