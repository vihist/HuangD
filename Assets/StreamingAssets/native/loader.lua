function listToTable(clrlist)
    local t = {}
    local it = clrlist:GetEnumerator()
    while it:MoveNext() do
      t[#t+1] = it.Current
    end
    return t
end

local array = listToTable(CS.Tools.StreamDir.GetLuaFileName("/native/static/"))
for  i=1,#array do
	print("static."..array[i])
	require("static."..array[i])
end

array = listToTable(CS.Tools.StreamDir.GetLuaFileName("/native/event/"))
for  i=1,#array do
	print("event."..array[i])
	require("event."..array[i])
end