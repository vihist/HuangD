EVENT_003={
	title='title_test3',
	desc='desc_test3',
	
	percondition = function(self)
		return true
	end,
	
	Initlize = function(self, param)
		local FactionJQ = listToTable(Inst:GetFaction(Selector.ByOffice('JQ1')))
		if(next(FactionJQ) == nil) then
			print('FactionJQ null')
			return
		end
		
		local JQ1faction = FactionJQ[1].name
		
		local preferPerson = listToTable(Inst:GetPerson(Selector.ByOffice('SG1','SG2','SG3'):ByFactionNOT(JQ1faction)))
		if(next(preferPerson) ~= nil) then
			print('not same faction'..preferPerson[1].name)
			return
		end
		
		local preferPerson = listToTable(Inst:GetPerson(Selector.ByOffice('SG1','SG2','SG3')))
		if(next(preferPerson) ~= nil) then
			print('same faction'..preferPerson[#preferPerson].name)
			return
		end
	end,
	
	option={
		op1='op1_test',
		op2='op2_test',
		op3='op3_test',
		
		process = function(self, op)
			print(' do '..op)
		end
		
	}
}