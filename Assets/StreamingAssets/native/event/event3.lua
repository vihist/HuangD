EVENT_TIANX_YHSX={
	desc = function(self)
		local office = GMData.GetOffice(Selector.ByOffice('JQ1'))
		local person = GMData.GetPerson(Selector.ByOffice('JQ1'))
		return string.format(self.DESC, office.name, person.name)
	end,

	percondition = function(self)
		if(GMData.Flag.Get('TX_YHSX') ~= nil) then
	        return false
	    end
		return false
	end,

	historyrecord = function(self)
		return self.TITLE
	end,

    option1={
		process = function(self)
            GMData.Flag.Set('TX_YHSX', '')
            return 'EVENT_STAB_DEC', 1
        end
    }
}

EVENT_TIANX_YHSX_END={
	percondition = function(self)
	    if(GMData.Flag.Get('TX_YHSX') == nil) then
	        return false
	    end

	    if(Probability.IsProbOccur(0.05)) then
	        return true
	    else
	        return false
	    end
	end,

	historyrecord = function(self)
		return self.TITLE
	end,

    option1={
        process = function(self)
            GMData.Flag.Clear('TX_YHSX')
        end
    }
}

EVENT_STAB_DEC = {

    value=0,

	percondition = function(self)
	    local flagValue = GMData.Flag.Get('TX_YHSX')
	    if(flagValue == nil) then
	        return false
	    end

	    local prob = 0.02
	    if(flagValue == 'Stab') then
	        prob = prob + 0.1
	    end

        if(Probability.IsProbOccur(prob)) then
            return true
	    end

		return false
	end,

	initialize = function(self, param)
	    self.value=param
	end,

    option1={
        process = function(self)
            GMData.Stability.Dec(self.parent.value)
        end
    }
}

EVENT_STAB_INC = {
    value=0,

	percondition = function(self)
		return false
	end,

	initialize = function(self, param)
	    self.value=param
	end,

    option1={
        process = function(self)
            GMData.Stability.Inc(self.parent.value);
        end
    }
}

EVENT_TIANX_YHSX_JQ1={
    suggest='',

	percondition = function(self)
	    local flagValue = GMData.Flag.Get('TX_YHSX')
	    if(flagValue == nil or flagValue ~= '') then
	        return false
	    end

	    local personJQ1 = GMData.GetPerson(Selector.ByOffice('JQ1'))
        if(personJQ1 == nil) then
            return false
        end

        return true
	end,

	initialize = function(self, param)
	    self.suggest = ''
	    self.suggest = self.GetSuggest(self, param)
	end,

	historyrecord = function(self)
		return self.TITLE
	end,

    option1={
        process = function(self)
            GMData.Flag.Set('TX_YHSX', 'Stab')
        end
    },

    option2={
		percondition = function(self)
			if(self.parent.suggest == '') then
				return false
			else
				return true
			end
		end,

        desc = function(self)
            return string.format(self.DESC, self.parent.suggest)
        end,

        process = function(self)
            GMData.Flag.Set('TX_YHSX', self.parent.suggest)
        end
    },

    option3={
        process = function(self)
            GMData.Flag.Set('TX_YHSX', 'Self')
        end
    },

    GetSuggest = function(self, param)
		local FactionJQ = GMData.GetFaction(Selector.ByOffice('JQ1'))
		if(FactionJQ == nil) then
			print('FactionJQ null')
			return ''
		end

		local JQ1faction = FactionJQ.name

		local preferPerson = GMData.GetPerson(Selector.ByOffice('SG1','SG2','SG3').ByFactionNOT(JQ1faction))
		if(preferPerson ~= nil) then
			return preferPerson.name
		end

		local preferPerson = GMData.GetPersonArray(Selector.ByOffice('SG1','SG2','SG3'))
		if(preferPerson[1] ~= nil) then
			return preferPerson[#preferPerson].name
		end

		return ''
	end
}

EVENT_EMP_HEATH_DEC={
	percondition = function(self)
	    local value = 0.001

	    if( GMData.Flag.Get('TX_YHSX') == 'Self') then
	        value = value + 0.1
	    end

        if(Probability.IsProbOccur(value)) then
            return true
        end

		return false
	end,

	initialize = function(self, param)
	end,

	historyrecord = function(self)
		return self.TITLE
	end,

	option1={
		process = function(self, op)
			GMData.Emp.Heath.Dec(1)
		end
	}
}

EVENT_EMP_HEATH_INC={
	percondition = function(self)
	    local value = 0.001

        if(Probability.IsProbOccur(value)) then
            return true
        end

		return false
	end,

	initialize = function(self, param)
	end,

	option1={
		process = function(self, op)
			GMData.Emp.Heath.Inc(1)
		end
	}
}

EVENT_EMP_DIE={
	percondition = function(self)	
		local heath = GMData.Emp.Heath.Value()

		if (heath >= 5) then
			return false;
		end

		if(heath == 0) then
			return true
		end

		local prb = 0.0
		if(heath == 4) then
			prb = 0.001
		elseif(heath == 3) then
			prb = 0.01
		elseif(heath == 2) then
			prb = 0.05
		elseif(heath == 1) then
			prb = 0.1
		end

        if(Probability.IsProbOccur(prb)) then
            return true
        end

		return false
	end,

	historyrecord = function(self)
		return self.TITLE
	end,

	option1={
		process = function(self, op)
			GMData.Emp.Die()
		end
	}
}

EVENT_SG_ILL_RESIGN={
	personname='',

	percondition = function(self)
	    self.personname = ''

        local prob = 0
        local suggest =  GMData.Flag.Get('TX_YHSX')
        if(suggest == nil) then

        else
            local person = GMData.GetPerson(Selector.ByPerson(suggest))
            if( person ~= nil ) then
                self.personname = person.name
                prob = prob + 0.1
            end
        end

        if(Probability.IsProbOccur(prob)) then
            return true
        end

		return false
	end,

	desc = function(self)
		return string.format(self.TITLE, self.personname)
	end,

	initialize = function(self, param)
	end,

	historyrecord = function(self)
		return self.TITLE
	end,

	option1={
		process = function(self, op)
			GMData.Person.Die(self.parent.personname);
			GMData.Flag.Set('TX_YHSX', 'DIE')
		end
	}
}

EVENT_SG_SUICDIE={
	personname='',

	percondition = function(self)
	    personname=''

        local prob = 0
        local suggest =  GMData.Flag.Get('TX_YHSX')
        if(suggest == nil) then

        else
            local person = GMData.GetPerson(Selector.ByPerson(suggest))
            if( person ~= nil ) then
                self.personname = person.name
                prob = prob + 0.1
            end
        end

        if(Probability.IsProbOccur(prob)) then
            return true
        end

		return false
	end,

	historyrecord = function(self)
		return self.TITLE
	end,

	option1={
		process = function(self, op)
			GMData.Person.Die(self.parent.personname);
			GMData.Flag.Set('TX_YHSX', 'DIE')
			return 'EVENT_STAB_DEC', 1
		end
	}
}

EVENT_SG_EMPTY={
	officename='',

    suggest1='',
    suggest2='',
    suggest3='',

	percondition = function(self)
	    self.officename=''
	    self.suggest1=''
        self.suggest2=''
        self.suggest3=''

	    array = {'SG1', 'SG2', 'SG3'}
        for  i=1,#array do
			local person = GMData.GetPerson(Selector.ByOffice(array[i]))
            if( person == nil ) then
                self.officename = array[i]
                return true
            end
		end

		return false
	end,

	historyrecord = function(self)
		return self.TITLE
	end,

	initialize = function(self, param)
		local tableSelect = {}

		local factions = GMData.GetFactionArray()
		for i =1, #factions do
			local persons = GMData.GetPersonArray(Selector.ByOffice('JQX').ByFaction(factions[i].name))
			table.sort(persons, function(a, b)
				return a.score > b.score
			end)
			table.insertRange(tableSelect, persons, 2)
		end

		table.sort(tableSelect, function(a, b)
		    return a.score > b.score
		end)

        self.suggest1 = tableSelect[1].name
        self.suggest2 = tableSelect[2].name
        self.suggest3 = tableSelect[3].name
	end,

	option1={
		desc = function(self)
            return string.format(self.DESC, self.parent.suggest1)
        end,
		process = function(self, op)
		     GMData.Appoint(self.parent.suggest1, self.parent.officename)
		end
	},

	option2={
		desc = function(self)
            return string.format(self.DESC, self.parent.suggest2)
        end,
		process = function(self, op)
		    GMData.Appoint(self.parent.suggest2, self.parent.officename)
		end
	},

	option3={
		desc = function(self)
            return string.format(self.DESC, self.parent.suggest3)
        end,
		process = function(self, op)
		    GMData.Appoint(self.parent.suggest3, self.parent.officename)
		end
	}
}

EVENT_JQ_EMPTY={
	officename='',

    suggest1='',
    suggest2='',
    suggest3='',

	percondition = function(self)
	    self.officename=''
	    self.suggest1=''
        self.suggest2=''
        self.suggest3=''

		local office = GMData.GetOffice(Selector.ByOffice('JQX').ByPerson(''))
		if(office == nil) then			
			return false
		end

		local person = GMData.GetPerson(Selector.ByOffice('SG1'))
		if(person == nil) then			
			return false
		end

		self.officename = office.name
		return true
	end,

	historyrecord = function(self)
		return self.TITLE
	end,

	initialize = function(self, param)
		local tableSelect = {}

		local factionSG1 = GMData.GetFaction(Selector.ByOffice('SG1'))

		local factions = GMData.GetFactionArray()
		for i =1, #factions do
			local persons = GMData.GetPersonArray(Selector.ByOffice('CSX').ByFaction(factions[i].name))
			table.sort(persons, function(a, b)
				return a.score > b.score
			end)
			
			if(factionSG1.name == factions[i].name) then
				table.insertRange(tableSelect, persons, 2)
			else
				table.insertRange(tableSelect, persons, 1)
			end
			
		end

		table.sort(tableSelect, function(a, b)
		    return a.score > b.score
		end)

        self.suggest1 = tableSelect[1].name
        self.suggest2 = tableSelect[2].name
        self.suggest3 = tableSelect[3].name
	end,

	option1={
		desc = function(self)
            return string.format(self.DESC, self.parent.suggest1)
        end,
		process = function(self, op)
		     GMData.Appoint(self.parent.suggest1, self.parent.officename)
		end
	},

	option2={
		desc = function(self)
            return string.format(self.DESC, self.parent.suggest2)
        end,
		process = function(self, op)
		    GMData.Appoint(self.parent.suggest2, self.parent.officename)
		end
	},

	option3={
		desc = function(self)
            return string.format(self.DESC, self.parent.suggest3)
        end,
		process = function(self, op)
		    GMData.Appoint(self.parent.suggest3, self.parent.officename)
		end
	}
}

EVENT_CS_EMPTY={
	officename='',

    suggest1='',
    suggest2='',
    suggest3='',

	percondition = function(self)
	    self.officename=''
	    self.suggest1=''
        self.suggest2=''
        self.suggest3=''

		local office = GMData.GetOffice(Selector.ByOffice('CSX').ByPerson(''))
		if(office == nil) then			
			return false
		end

		local person = GMData.GetPerson(Selector.ByOffice('SG1'))
		if(person == nil) then			
			return false
		end

		self.officename = office.name
		return true
	end,

	historyrecord = function(self)
		return self.TITLE
	end,

	initialize = function(self, param)
		local tableSelect = {}

		local factionSG1 = GMData.GetFaction(Selector.ByOffice('SG1'))

		while(#tableSelect < 3)
		do
			local factionName = ''
			if(Probability.IsProbOccur(0.5)) then
				factionName = factionSG1.name
			else
				factionName = GMData.GetFaction(Selector.ByFactionNOT(factionSG1.name)).name
			end

			local temp = GMData.Person.PreCreate('WAI', 10)
			print(temp)

			table.insert(tableSelect, temp)
		end

        self.suggest1 = tableSelect[1]
        self.suggest2 = tableSelect[2]
        self.suggest3 = tableSelect[3]
	end,

	option1={
		desc = function(self)
            return string.format(self.DESC, self.parent.suggest1.personName)
        end,
		process = function(self, op)
			GMData.Person.Create(self.parent.suggest1, self.parent.officename)
		end
	},

	option2={
		desc = function(self)
            return string.format(self.DESC, self.parent.suggest2.personName)
        end,
		process = function(self, op)
			GMData.Person.Create(self.parent.suggest2, self.parent.officename)
		end
	},

	option3={
		desc = function(self)
            return string.format(self.DESC, self.parent.suggest3.personName)
        end,
		process = function(self, op)
			GMData.Person.Create(self.parent.suggest3, self.parent.officename)
		end
	}
}

EVENT_CS_KAOHE={
	KaoheTable = {},

	percondition = function(self)
		if(GMData.Date.month() == 1 and GMData.Date.day() == 2) then
			return true
		else
			return false
		end
	end,

	historyrecord = function(self)
		return self.TITLE
	end,

	initialize = function(self, param)
		table.remove(self.KaoheTable)
		table.insert(self.KaoheTable, {TABLE_KAOHE_COLUM.OFFICE, TABLE_KAOHE_COLUM.NAME, TABLE_KAOHE_COLUM.FACTION, TABLE_KAOHE_COLUM.VALUE})

		factionSG1 = GMData.GetFaction(Selector.ByOffice('SG1'))

		local offices = GMData.GetOfficeArray(Selector.ByOffice('CSX'))
		for i = 1, #offices do
			local person = GMData.GetPerson(Selector.ByOffice(offices[i].name))
			local faction = GMData.GetFaction(Selector.ByPerson(person.name))

			local value = Probability.GetGaussianRandomNum(0, 5)
			if (faction.name ~= factionSG1.name) then
				value = value - Probability.GetGaussianRandomNum(0, 4)
			end

			table.insert(self.KaoheTable, {offices[i].name, person.name, faction.name, value})
		end
	end,

	option1={
		process = function(self, op)
			local scoreTotal = 0
			for i=2, #self.parent.KaoheTable do
				GMData.Person.ScoreAdd(self.parent.KaoheTable[i][2], self.parent.KaoheTable[i][4])
				scoreTotal = scoreTotal + self.parent.KaoheTable[i][4]
			end
			
			local DebTotal = 0;
			local provArray = GMData.GetProvinceArray()
			for i = 1, #provArray do
				if(GMData.Province.HasDebuff(provArray[i])) then
					DebTotal = DebTotal - 10
				elseif(GMData.Province.HasBuff(provArray[i])) then
					DebTotal = DebTotal + 10
				end
			end

			GMData.Economy.Inc(100 + DebTotal + scoreTotal*10)
			return self.parent.KaoheTable
		end
	},
}

EVENT_PROV_HONG_START={
	prov = nil,

	percondition = function(self)
		local provArray = GMData.GetProvinceArray()
		for i = 1, #provArray do
			if(not GMData.Province.HasDebuff(provArray[i],'HONG')) then
				print(provArray[i].name)
				self.prov = provArray[i]
				return true
			end
		end
		return false
	end,

	historyrecord = function(self)
		return self.TITLE
	end,

	initialize = function(self, param)
		
	end,

	desc = function(self)
		return string.format(self.DESC, self.prov.name)
	end,

	option1={
		process = function(self, op)
			GMData.Province.SetBuff(self.parent.prov, 'HONG');
		end
	}
}

EVENT_PROV_HONG_END={
	prov = nil,

	percondition = function(self)
		local provArray = GMData.GetProvinceArray()
		for i = 1, #provArray do
			if(GMData.Province.HasDebuff(provArray[i],'HONG')) then
				self.prov = provArray[i]
				return true
			end
		end
		return false
	end,

	historyrecord = function(self)
		return self.TITLE
	end,

	initialize = function(self, param)
		
	end,

	desc = function(self)
		return string.format(self.DESC, self.prov.name)
	end,

	option1={
		process = function(self, op)
			GMData.Province.ClearBuff(self.parent.prov, 'HONG');
		end
	}
}