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

			GMData.Economy.Inc(100 + DebTotal + scoreTotal)
			return self.parent.KaoheTable
		end
	},
}

EVENT_PROV_DISASTER_START={
	prov = nil,
	debuff = nil,

	percondition = function(self)
		local provArray = GMData.GetProvinceArray()
		for i = 1, #provArray do
			if(not GMData.Province.HasDebuff(provArray[i])) then
				--if(Probability.IsProbOccur(0.001)) then
					print(provArray[i].name)
					self.prov = provArray[i]
					self.debuff = GMData.Province.GetDebuff()
					return true
				--end
			end
		end
		return false
	end,

	historyrecord = function(self)
		return self.TITLE
	end,

	desc = function(self)
		return string.format(self.DESC, self.prov.name, self.debuff.name)
	end,

	option1={
		process = function(self, op)
			GMData.Province.SetBuff(self.parent.prov, self.parent.debuff);
			if(Probability.IsProbOccur(0.8)) then
				return EVENT_STAB_DEC
			end
		end
	},
	option2={
		percondition = function(self)
			if(GMData.Economy.Vaule() < 20) then
				return false
			end
			return true
		end,
		process = function(self, op)
			self.parent.debuff.cover = 0.05
			if(Probability.IsProbOccur(0.3)) then
				self.parent.debuff.corrput = true
				local Person = GMData.GetPerson(Selector.ByOffice(self.parent.prov, 'CS'))
				GMData.Person.SetImp(Person, GMData.Crime(CORRPUT, self.parent.prov))
			end

			GMData.Province.SetBuff(self.parent.prov, self.parent.debuff);
			GMData.Economy = GMData.Economy - 20
		end
	},
	option3={
		percondition = function(self)
			if(GMData.Economy < 50) then
				return false
			end
			return true
		end,
		process = function(self, op)
			self.parent.debuff.cover = 0.09
			if(Probability.IsProbOccur(0.3)) then
				self.parent.debuff.corrput = true
				local Person = GMData.GetPerson(Selector.ByOffice(self.parent.prov, 'CS'))
				GMData.Person.SetImp(Person, GMData.Crime(CORRPUT, self.parent.prov))
			end

			GMData.Province.SetBuff(self.parent.prov, self.parent.debuff);
			GMData.Economy = GMData.Economy - 50
		end
	}

}

EVENT_PROV_DISASTER_END={
	prov = nil,
	debuff = nil,

	percondition = function(self)
		local provArray = GMData.GetProvinceArray()
		for i = 1, #provArray do
			local debuff = GMData.Province.GetDebuff(provArray[i])
			if(debuff ~= nil) then
				local prob = 0.01
				if(debuff.name == 'KOU' and not debuff.corrput) then
					prob = prob + debuff.cover
				end
				if(Probability.IsProbOccur(prob)) then
					print(provArray[i].name)
					self.prov = provArray[i]
					self.debuff = GMData.Province.GetDebuff(self.prov)
					return true
				end
			end
		end
		return false
	end,

	historyrecord = function(self)
		return self.TITLE
	end,

	desc = function(self)
		return string.format(self.DESC, self.prov.name, self.debuff.name)
	end,

	option1={
		process = function(self, op)
			GMData.Province.ClearBuff(self.parent.prov, self.parent.debuff);
		end
	}
}

EVENT_PROV_BUFF_START={
	prov = nil,
	buff = nil,

	percondition = function(self)
		local provArray = GMData.GetProvinceArray()
		for i = 1, #provArray do
			local debuff = GMData.Province.GetDebuff(provArray[i])
			local buff = GMData.Province.GetDebuff(provArray[i])
			if(debuff == nil and buff == nil) then
				local prob = 0.001
				if(Probability.IsProbOccur(prob)) then
					self.prov = provArray[i]
					self.buff = GMData.Province.GetBuff()
					return true
				end
			end
		end
		return false
	end,

	historyrecord = function(self)
		return self.TITLE
	end,

	desc = function(self)
		return string.format(self.DESC, self.prov.name, self.debuff.name)
	end,

	option1={
		process = function(self, op)
			GMData.Province.SetBuff(self.parent.prov, self.parent.buff);
		end
	}
}

EVENT_PROV_KOU_START={
	prov = nil,
	debuff = nil,

	percondition = function(self)
		local provArray = GMData.GetProvinceArray()
		for i = 1, #provArray do
			local debuff = GMData.Province.GetDebuff(provArray[i])
			if(debuff.name ~= 'KOU') then
				local prob = 0.0
				if (debuff.days < 10) then
					prob = 0.01
				elseif(debuff.days < 20) then
					prob = 0.05
				elseif(debuff.days < 30) then
					prob = 0.1
				elseif(debuff.days < 40) then
					prob = 0.3
				else
					prob = 0.5
				end				
				
				if(not debuff.corrput) then
					prob = prob-debuff.cover
					if(prob < 0.0) then
						prob = 0.01
					end
				end

				if(Probability.IsProbOccur(prob)) then
					self.prov = provArray[i]
					return true
				end
			end
		end
		return false
	end,

	historyrecord = function(self)
		return self.TITLE
	end,

	desc = function(self)
		return string.format(self.DESC, self.prov.name, self.debuff.name)
	end,

	option1={
		process = function(self, op)
			GMData.Province.SetBuff(self.parent.prov, self.parent.debuff);
			if(Probability.IsProbOccur(0.8)) then
				return EVENT_STAB_DEC
			end
		end
	},
	option2={
		percondition = function(self)
			if(GMData.Economy < 20) then
				return false
			end
			return true
		end,
		process = function(self, op)
			self.parent.debuff.cover = 0.05
			if(Probability.IsProbOccur(0.3)) then
				self.parent.debuff.corrput = true
				local Person = GMData.GetPerson(Selector.ByOffice(self.parent.prov, 'CS'))
				GMData.Person.SetCrime(Person, GMData.Crime(CORRPUT))
			end

			GMData.Province.SetBuff(self.parent.prov, self.parent.debuff);
			GMData.Economy = GMData.Economy - 20
		end
	},
	option3={
		percondition = function(self)
			if(GMData.Economy < 50) then
				return false
			end
			return true
		end,
		process = function(self, op)
			self.parent.debuff.cover = 0.09
			if(Probability.IsProbOccur(0.3)) then
				self.parent.debuff.corrput = true
				local Person = GMData.GetPerson(Selector.ByOffice(self.parent.prov, 'CS'))
				GMData.Person.SetCrime(Person, GMData.Crime(CORRPUT))
			end

			GMData.Province.SetBuff(self.parent.prov, self.parent.debuff);
			GMData.Economy = GMData.Economy - 50
		end
	}
}

EVENT_YSDF_DISCOVER_CRIME = {
	person = nil,
	crime = nil,

	percondition = function(self)
		self.person = nil
		self.crime = nil

		local crimes = GMData.GetCrimeArray()
		for i=1, #crimes do
			if(not crimes[i].checked) then
				local prob = 0.05
				local curFaction = GMData.GetFaction(Selector.ByPerson(crimes[i].person.name))
				local SG3Faction = GMData.GetFaction(Selector.ByOffice('SG3'))
				if(curFaction.name == SG3Faction.name)then
					prob = prob-0.02
				end
				if(Probability.IsProbOccur(prob)) then
					self.person = crimes[i].person
					self.crime = crimes[i]
					return true
				end
			end
		end

		if(Probability.IsProbOccur(0.001)) then
			local SG3Faction = GMData.GetFaction(Selector.ByOffice('SG3'))
			local person = GMData.GetPerson(Selector.ByOffice('CSX').ByFaction(NOT(SG3Faction.name)))
			self.person = person
			self.crime = GMData.Crime()
			return true
		end

		return false
	end,

	historyrecord = function(self)
		return self.TITLE
	end,

	desc = function(self)
		return string.format(self.DESC, self.person.name, self.crime.name)
	end,

	option1={
		process = function(self, op)
			GMData.Person.Die(self.parent.person)
			return EVENT_COMMON_SCORE_INC {GMData.GetPerson(Selector.ByOffice('SG3')), crimes.score}
		end
	},
	option2={
		process = function(self, op)
			self.crime.checked = true
			return EVENT_COMMON_SCORE_DEC {GMData.GetPerson(Selector.ByOffice('SG3')), crimes.score}
		end
	}
}

EVENT_COMMON_SCORE_INC = {
	person = nil,
	value = 0,

	percondition = function(self)
		return false
	end,

	initialize = function(self, param)
		self.person = param[1]
		self.vaule = param[2]
	end,

	historyrecord = function(self)
		return self.TITLE
	end,

	desc = function(self)
		return string.format(self.DESC, self.person.name, self.vaule)
	end,

	option1={
		process = function(self, op)
			GMData.Person.ScoreAdd(self.parent.KaoheTable[i][2], self.parent.KaoheTable[i][4])
		end
	}
}