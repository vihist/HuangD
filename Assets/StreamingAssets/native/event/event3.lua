EVENT_TIANX_YHSX={
	title='EVENT_TIANX_YHSX_title',
	desc='EVENT_TIANX_YHSX_desc',
	select_desc = '',

	percondition = function(self)
		if(GMData.Flag.Get('TX_YHSX') ~= nil) then
	        return false
	    end
		return true
	end,

	historyrecord = function(self)
		return self.title..self.select_desc
	end,

    option1={
        desc = function()
            return 'op1_test'
		end,
		
		process = function(self)
			EVENT_TIANX_YHSX.select_desc = 'op1_test'
            GMData.Flag.Set('TX_YHSX', '')
            return 'EVENT_STAB_DEC', 1
        end
    }
}

EVENT_TIANX_YHSX_END={
	title='EVENT_TIANX_YHSX_END_title',
	desc='EVENT_TIANX_YHSX_END_desc',

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
		return self.title
	end,

    option1={
        desc = function()
            return 'op1_test'
        end,
        process = function(self)
            GMData.Flag.Clear('TX_YHSX')
        end
    }
}

EVENT_STAB_DEC = {
	title='EVENT_STAB_DEC_title',
	desc='EVENT_STAB_DEC_desc',

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
        desc = function()
            return 'op1_test'
        end,
        process = function(self)
            GMData.Stability.Dec(EVENT_STAB_DEC.value)
        end
    }
}

EVENT_STAB_INC = {
	title='EVENT_STAB_INC_title',
	desc='EVENT_STAB_INC_desc',

    value=0,

	percondition = function(self)
		return false
	end,

	initialize = function(self, param)
	    self.value=param
	end,

    option1={
        desc = function()
            return 'op1_test'
        end,
        process = function(self)
            GMData.Stability.Inc(EVENT_STAB_DEC.value);
        end
    }
}

EVENT_TIANX_YHSX_JQ1={
	title='EVENT_TIANX_YHSX_JQ1_title',
	desc='EVENT_TIANX_YHSX_JQ1_desc',

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
		return self.title
	end,

    option1={
        desc = function()
           return 'op1_test'
        end,
        process = function(self)
            GMData.Flag.Set('TX_YHSX', 'Stab')
        end
    },

    option2={
        desc = function(self)
            if(EVENT_TIANX_YHSX_JQ1.suggest ~= '') then
                return {string.format('op2_test%s', EVENT_TIANX_YHSX_JQ1.suggest), true}
            else
                return {'suggest is null', false}
            end
        end,

        process = function(self)
            GMData.Flag.Set('TX_YHSX', EVENT_TIANX_YHSX_JQ1.suggest)
        end
    },

    option3={
        desc = function()
            return 'op3_test'
        end,
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
	title='EMP_HEATH_DEC',
	desc='EMP_HEATH_DEC',

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
		desc=string.format(self.desc, param)
	end,

	historyrecord = function(self)
		return self.title
	end,

	option1={
		desc = function()
            return 'op1_test'
        end,

		process = function(self, op)
			GMData.Emp.Heath.Dec(1)
		end
	}
}

EVENT_EMP_HEATH_INC={
	title='EMP_HEATH_INC',
	desc='EMP_HEATH_INC',

	percondition = function(self)
	    local value = 0.001

        if(Probability.IsProbOccur(value)) then
            return true
        end

		return false
	end,

	initialize = function(self, param)
		desc=string.format(self.desc, param)
	end,

	option1={
		desc = function()
            return 'op1_test'
        end,

		process = function(self, op)
			GMData.Emp.Heath.Inc(1)
		end
	}
}

EVENT_EMP_DIE={
	title='EVENT_EMP_DIE',
	desc='EVENT_EMP_DIE',

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
		return self.title
	end,

	option1={
		desc = function()
            return 'op1_test'
        end,

		process = function(self, op)
			GMData.Emp.Die()
		end
	}
}

EVENT_SG_ILL_RESIGN={
	title='EVENT_SG_ILL_RESIGN',
	desc='EVENT_SG_ILL_RESIGN%s__',
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

	initialize = function(self, param)
		self.desc=string.format(self.desc, param)
	end,

	historyrecord = function(self)
		return self.title
	end,

	option1={
		desc = function()
            return 'op1_test'
        end,
		process = function(self, op)
		    print(EVENT_SG_ILL_RESIGN.personname)
			local person = GMData.GetPerson(Selector.ByPerson(EVENT_SG_ILL_RESIGN.personname ))
			person:Die()
			GMData.Flag.Set('TX_YHSX', 'DIE')
		end
	}
}

EVENT_SG_SUICDIE={
	title='EVENT_SG_SUICDIE',
	desc='EVENT_SG_SUICDIE%s__',
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
		return self.title
	end,

	option1={
		desc = function()
            return 'op1_test'
        end,
		process = function(self, op)
		    print(EVENT_SG_SUICDIE.personname)
			local person = GMData.GetPerson(Selector.ByPerson(EVENT_SG_SUICDIE.personname ))
			person:Die()
			GMData.Flag.Set('TX_YHSX', 'DIE')
			return 'EVENT_STAB_DEC', 1
		end
	}
}

EVENT_SG_EMPTY={
	title='EVENT_SG_EMPTY',
	desc='EVENT_SG_EMPTY%s__',

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
		return self.title
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
		desc = function()
            return 'op1_test'..EVENT_SG_EMPTY.suggest1
        end,
		process = function(self, op)
		     GMData.Appoint(EVENT_SG_EMPTY.suggest1, EVENT_SG_EMPTY.officename)
		end
	},

	option2={
		desc = function()
            return 'op2_test'..EVENT_SG_EMPTY.suggest2
        end,
		process = function(self, op)
		    GMData.Appoint(EVENT_SG_EMPTY.suggest2, EVENT_SG_EMPTY.officename)
		end
	},

	option3={
		desc = function()
            return 'op3_test'..EVENT_SG_EMPTY.suggest3
        end,
		process = function(self, op)
		    GMData.Appoint(EVENT_SG_EMPTY.suggest3, EVENT_SG_EMPTY.officename)
		end
	}
}