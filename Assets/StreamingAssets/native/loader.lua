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
	require(array[i])
end