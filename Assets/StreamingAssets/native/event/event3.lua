EVENT_003={
	title='title_test3',
	desc='desc_test3',
	
	percondition = function(self)
		return false
	end,
	
	Initlize = function(self, param)
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

local Inst = CS.MyGame.Inst
local Tools = CS.Tools

EVENT_TX_yinghuoshouxin={
	title='yinghuoshouxin',
	desc='yinghuoshouxin',
	suggest='',
	
	percondition = function(self)
		return true
	end,
	
	Initlize = function(self, param)
			local office = Inst.officeManager:GetByName('JQ1')
			local person = Inst.relOffice2Person:GetPerson(office)
			local faction = Inst.relFaction2Person:GetFaction(person)
			
			array = {'SG1', 'SG2', 'SG3'}
			for i= 1, #array do
				local office1 = Inst.officeManager:GetByName(array[i])
				local person1 = Inst.relOffice2Person:GetPerson(office1)
				local faction1 = Inst.relFaction2Person:GetFaction(person1)
				if(faction1.name ~= faction.name) then
					self.suggest=office1.name
					break
				end
			end
			
			if(self.suggest=='') then
				self.suggest = 'SG3'
			end

			self.option.op2 = string.format(self.option.op2, self.suggest)
			print(self.option.op2)
	end,
	
	option={
		op1='op1_test',
		op2='op2_test%s',
		op3='op3_test',
		
		process = function(self, op)
			if (op=='op1') then
				print('op1')
			elseif(op=='op2') then
					if(Tools.Probability.IsProbOccur(0.5)) then
						return 'EVENT_SG_SUICDIE', EVENT_TX_yinghuoshouxin.suggest
					else
						return 'EVENT_SG_ILL_RESIGN', EVENT_TX_yinghuoshouxin.suggest
					end
			elseif(op=='op3') then
				if(Tools.Probability.IsProbOccur(0.5)) then
					return 'EVENT_EMP_HEATH_DEC'
				end
			end
		end
		
	}
}

EVENT_SG_SUICDIE={
	title='EVENT_SG_SUICDIE',
	desc='EVENT_SG_SUICDIE%s__',
	officename='',
	percondition = function(self)
		return false
	end,
	
	Initlize = function(self, param)
		self.officename = param
		self.desc=string.format(self.desc, param)
	end,
	
	option={
		op1='op1_test',
		process = function(self, op)
			local office = Inst.officeManager:GetByName(EVENT_SG_SUICDIE.officename)
			local person = Inst.relOffice2Person:GetPerson(office)
			person:Die()
		end
	}
}

EVENT_SG_ILL_RESIGN={
	title='EVENT_SG_ILL_RESIGN',
	desc='EVENT_SG_ILL_RESIGN%s__',
	officename='',
	percondition = function(self)
		return false
	end,

	Initlize = function(self, param)
		self.officename=param
		self.desc=string.format(self.desc, param)
	end,
	
	option={
		op1='op1_test',
		process = function(self, op)
			local office = Inst.officeManager:GetByName(EVENT_SG_ILL_RESIGN.officename)
			local person = Inst.relOffice2Person:GetPerson(office)
			person:Die()
		end
	}
}

EVENT_EMP_HEATH_DEC={
	title='EMP_HEATH_DEC',
	desc='EMP_HEATH_DEC',
	
	percondition = function(self)
		return false
	end,
	
	Initlize = function(self, param)
		desc=string.format(desc, param)
	end,
	
	option={
		op1='op1_test',
		process = function(self, op)
			Inst.empHeath=Inst.empHeath-1
		end
	}
}
	
	EVENT_EMP_HEATH_INC={
	title='EMP_HEATH_INC',
	desc='EMP_HEATH_INC',
	
	percondition = function(self)
		return false
	end,

	Initlize = function(self, param)
	end,
	
	option={
		op1='op1_test',
		process = function(self, op)
			Inst.empHeath=Inst.empHeath+1
		end
	}
}
	
	EVENT_EMP_DIEATH={
	title='EVENT_EMP_DIEATH',
	desc='EVENT_EMP_DIEATH',
	
	percondition = function(self)
		if(Inst.empHeath == 0) then
			return ture
		elseif(Inst.empHeath == 1 and Tools.Probability.IsProbOccur(0.5)) then
			return ture
		elseif(Inst.empHeath == 2 and Tools.Probability.IsProbOccur(0.3)) then
			return ture
		elseif(Inst.empHeath == 3 and Tools.Probability.IsProbOccur(0.1)) then
			return ture
		else
			return false
		end
	end,

	option={
		op1='op1_test',
		process = function(self, op)
			Inst.empDie();
		end
	}
}