EVENT_TIANX_YHSX={
	title='title_YHSX',
	desc='desc_YHSX',
	suggest='',

	percondition = function(self)
		return true
	end,

	Initlize = function(self, param)
	    local personJQ1 = GMData.GetPerson(Selector.ByOffice('JQ1'))
	    if(next(personJQ1) == nil) then
	    	print('FactionJQ null')
        	return
	    end

	    self.suggest = self.GetSuggest(self, param)
	    if(self.suggest == '') then
	        print('Suggest null')
	    end

	end,

    option1={
        desc = function()
           return 'op1_test'
        end,
        process = function(self)
           print(' do '..self.desc)
        end
    },

    option2={
        desc = function(self)
            return string.format('op2_test%s', EVENT_TIANX_YHSX.suggest)
        end,
        process = function(self)
            --if(Tools.Probability.IsProbOccur(0.5)) then
			    return 'EVENT_SG_SUICDIE', EVENT_TIANX_YHSX.suggest
			--else
				--return 'EVENT_SG_ILL_RESIGN', EVENT_TIANX_YHSX.suggest
			--end
        end
    },
    option3={
        desc = function()
            return 'op3_test'
        end,
        process = function(self)
            if(Tools.Probability.IsProbOccur(0.5)) then
            	return 'EVENT_EMP_HEATH_DEC'
            end
        end
    },

    GetSuggest = function(self, param)
		local FactionJQ = GMData.GetFaction(Selector.ByOffice('JQ1'))
		if(next(FactionJQ) == nil) then
			print('FactionJQ null')
			return
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

EVENT_SG_SUICDIE={
	title='EVENT_SG_SUICDIE',
	desc='EVENT_SG_SUICDIE%s__',
	personname='',
	percondition = function(self)
		return false
	end,

	Initlize = function(self, param)
	    print('param'..param)
		self.personname = param
		self.desc=string.format(self.desc, param)
		print(self.desc)
	end,

	option1={
		desc = function()
		    return 'op1_test'
		end,
		process = function(self, op)
			local person = GMData.GetPerson(Selector.ByPerson(EVENT_SG_SUICDIE.personname))
			person[1]:Die()
		end
	}
}

