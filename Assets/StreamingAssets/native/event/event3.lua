EVENT_TIANX_YHSX={
	title='EVENT_TIANX_YHSX_title',
	desc='EVENT_TIANX_YHSX_desc',

	percondition = function(self)
		return true
	end,

    option1={
        desc = function()
            return 'op1_test'
        end,
        process = function(self)
            GM.SetFlag('TX_YHSX', '')
            return 'EVENT_STAB_DEC', 1
        end
    }
}

EVENT_TIANX_YHSX_END={
	title='EVENT_TIANX_YHSX_title',
	desc='EVENT_TIANX_YHSX_desc',

	percondition = function(self)
	    if(not(GM.IsFlagExit('TX_YHSX'))) then
	        return false
	    end

	    if(Probability.IsProbOccur(0.05)) then
	        return true
	    esle
	        return false
	    end
	end,

    option1={
        desc = function()
            return 'op1_test'
        end,
        process = function(self)
            GM.ClearFlag('TX_YHSX')
        end
    }
}

EVENT_STAB_DEC = {
	title='EVENT_TIANX_YHSX_title',
	desc='EVENT_TIANX_YHSX_desc',

    value=0,

	percondition = function(self)
	    local flagValue = GM.GetFlag('TX_YHSX')
	    if(flagValue == nil) then
	        return false
	    end

	    local value = 0.02
	    if(flagValue == 'Stab')
	        value = value + 0.1
	    end

        if(Probability.IsProbOccur(value)) then
            return true
	    end

		return false
	end,

	Initlize = function(self, param)
	    value=param
	end,

    option1={
        desc = function()
            return 'op1_test'
        end,
        process = function(self)
            GM.Stability.Dec(value);
        end
    }
}

EVENT_STAB_INC = {
	title='EVENT_TIANX_YHSX_title',
	desc='EVENT_TIANX_YHSX_desc',

    value=0,

	percondition = function(self)
		return false
	end,

	Initlize = function(self, param)
	    value=param
	end,

    option1={
        desc = function()
            return 'op1_test'
        end,
        process = function(self)
            GM.Stability.Inc(value);
        end
    }
}

EVENT_TIANX_YHSX_JQ1={
	title='EVENT_TIANX_YHSX_JQ1_title',
	desc='EVENT_TIANX_YHSX_JQ1_desc',

    suggest='',

	percondition = function(self)
	    local flagValue = GM.GetFlag('TX_YHSX')
	    if(flagValue == nil or flagValue ~= '') then
	        return false
	    end

	    local personJQ1 = GMData.GetPerson(Selector.ByOffice('JQ1'))
        if(next(personJQ1) ~= nil) then
            return false
        end

        return true
	end,

	Initlize = function(self, param)
	    self.suggest = ''
	    self.suggest = self.GetSuggest(self, param)
	end,

    option1={
        desc = function()
           return 'op1_test'
        end,
        process = function(self)
            GM.SetFlag('TX_YHSX', 'Stab')
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
            GM.SetFlag('TX_YHSX', suggest)
        end
    },

    option3={
        desc = function()
            return 'op3_test'
        end,
        process = function(self)
            GM.SetFlag('TX_YHSX', 'Self')
        end
    },

    GetSuggest = function(self, param)
		local FactionJQ = GMData.GetFaction(Selector.ByOffice('JQ1'))
		if(next(FactionJQ) == nil) then
			print('FactionJQ null')
			return ''
		end

		local JQ1faction = FactionJQ[1].name

		local preferPerson = GMData.GetPerson(Selector.ByOffice('SG1','SG2','SG3').ByFactionNOT(JQ1faction))
		if(next(preferPerson) ~= nil) then
			return preferPerson[1].name
		end

		local preferPerson = GMData.GetPerson(Selector.ByOffice('SG1','SG2','SG3'))
		if(next(preferPerson) ~= nil) then
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

	    if( GM.GetFlag('TX_YHSX') == 'Self')
	        value = value + 0.1
	    end

        if(Probability.IsProbOccur(value)) then
            return true
        end

		return false
	end,

	Initlize = function(self, param)
		desc=string.format(self.desc, param)
	end,

	option1={
		desc = function()
            return 'op1_test'
        end,

		process = function(self, op)
			GM.empHeath.Dec(1)
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

	Initlize = function(self, param)
		desc=string.format(self.desc, param)
	end,

	option1={
		desc = function()
            return 'op1_test'
        end,

		process = function(self, op)
			GM.empHeath.Inc(1)
		end
	}
}